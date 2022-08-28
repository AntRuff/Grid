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


    private void Start() {
        lastHit = null;
        grid.GenerateMap(gridSizeX, gridSizeZ);
        grid.GetGrid()[0].SetPlayer(player);
    }

    private void Update() {
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
