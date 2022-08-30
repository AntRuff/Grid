using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private PlayerManager player;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeZ;

    private Ray ray; 
    private RaycastHit hit;
    private GridSpace lastHit;
    private PlayerManager lastPlayer;
    private bool playerSelected = false;


    //Initialize the grid and place player at the first space
    private void Start() {
        lastHit = null;
        lastPlayer = null;
        grid.GenerateMap(gridSizeX, gridSizeZ);
        grid.GetGrid()[0].SetPlayer(player);
    }

    //Every frame, cast ray from mouse and highlight the space it hits
    private void Update() {
        if (playerSelected) {CheckGrid();}
        else {CheckPlayer();}

        if (Input.GetMouseButtonDown(0)){
            if (!playerSelected){
                if (lastPlayer){
                    playerSelected = true;
                }
            } else {
                if (lastHit){
                    lastPlayer.SetCurrentSpace(lastHit);
                    lastHit.GlowOff();
                    lastHit = null;
                    playerSelected = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(1)){
            if (playerSelected){
                playerSelected = false;
            }
            if (lastHit){
                lastHit.GlowOff();
                lastHit = null;
            }
        }
    }

    private void CheckPlayer(){
        ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100)){
            if (hit.collider.gameObject.tag == "Player"){
                if (hit.collider.gameObject.GetComponentInParent<PlayerManager>() != lastPlayer){
                    if (lastPlayer){
                        lastPlayer.DisableHalo();
                    }
                    lastPlayer = hit.collider.GetComponentInParent<PlayerManager>();
                    lastPlayer.EnableHalo();
                }
            }
            else {
                if (lastPlayer) {
                    lastPlayer.DisableHalo();
                    lastPlayer = null;
                }
            }
        }
        else {
            if (lastPlayer) {
                lastPlayer.DisableHalo();
                lastPlayer = null;
            }
        }
    }

    private void CheckGrid(){
        ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100)){
            if (hit.collider.gameObject.tag == "GridSpace"){
                if (hit.collider.gameObject.GetComponent<GridSpace>() != lastHit){
                    if (lastHit){
                        lastHit.GlowOff();
                    }
                    lastHit = hit.collider.gameObject.GetComponent<GridSpace>();
                    lastHit.GlowOn();
                }
            }
            else {
                if (lastHit){
                    lastHit.GlowOff();
                    lastHit = null;
                }
            }
        }
        else {
            if (lastHit){
                lastHit.GlowOff();
                lastHit = null;
            }
        }
    }
}



/*else if (hit.collider.gameObject.tag == "Player"){
    hit.collider.gameObject.GetComponentInParent<PlayerManager>().EnableHalo();
}*/
