using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private PlayerManager playerPrefab;
    [SerializeField] private Pointer point;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeZ;

    private Ray ray; 
    private RaycastHit hit;
    private GridSpace lastHit;
    private PlayerManager lastPlayer;
    private bool playerSelected = false;

    private List<GridSpace> validMoves;

    public int startingPlayers = 4;

    //Initialize the grid and place player at the first space
    private void Start() {
        lastHit = null;
        lastPlayer = null;
        validMoves = new List<GridSpace>();
        grid.GenerateMap(gridSizeX, gridSizeZ);
        for (int i = 0; i < startingPlayers; i++){
            PlayerManager newPlayer = Instantiate(playerPrefab);
            int fail = 0;
            bool success = false;
            while (fail < 100 && !success) {
                int x = Random.Range(0, gridSizeX) * gridSizeX;
                int z = Random.Range(0, gridSizeZ);
                if (grid.GetGrid()[x+z].HasPlayer()) {fail++;}
                else {success = true; newPlayer.SetCurrentSpace(grid.GetGrid()[x+z]);}
            }
        }


    }

    //Every frame, cast ray from mouse and highlight the space it hits
    private void Update() {
        if (playerSelected) {CheckGrid();}
        else {CheckPlayer();}

        if (Input.GetMouseButtonDown(0)){
            if (!playerSelected){
                if (lastPlayer){
                    playerSelected = true;
                    DisplayRange(lastPlayer);
                }
            } else {
                if (lastHit){
                    if (!lastHit.HasPlayer()) {
                        lastHit.GivePlayer();
                        lastPlayer.SetCurrentSpace(lastHit);
                        //lastHit.GlowOff();
                        point.ResetPosition();
                        lastHit = null;
                        playerSelected = false;
                        HideRange();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1)){
            if (playerSelected){
                playerSelected = false;
                HideRange();
            }
            if (lastHit){
                //lastHit.GlowOff();
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
                        //lastHit.GlowOff();
                    }
                    if (validMoves.Contains(hit.collider.gameObject.GetComponent<GridSpace>())){
                        lastHit = hit.collider.gameObject.GetComponent<GridSpace>();
                        point.UpdatePosition(lastHit.GetPlayerPosition());
                    }
                    //lastHit.GlowOn();
                }
            }
            else {
                if (lastHit){
                    //lastHit.GlowOff();
                    lastHit = null;
                    point.ResetPosition();
                }
            }
        }
        else {
            if (lastHit){
                //lastHit.GlowOff();
                lastHit = null;
                point.ResetPosition();
            }
        }
    }

    private void DisplayRange(PlayerManager player){
        GridSpace startingPos = player.GetCurrentSpace();
        int startX = startingPos.GetPosX();
        int startZ = startingPos.GetPosZ();

        validMoves.AddRange(grid.ShowRange(player.movementRange, startX-1, startZ));
        validMoves.AddRange(grid.ShowRange(player.movementRange, startX, startZ+1));
        validMoves.AddRange(grid.ShowRange(player.movementRange, startX+1, startZ));
        validMoves.AddRange(grid.ShowRange(player.movementRange, startX, startZ-1));

        foreach (GridSpace space in validMoves){space.GlowOn();}
    }

    private void HideRange(){
        foreach (GridSpace space in validMoves){space.GlowOff();}

        validMoves = new List<GridSpace>();
    }
}