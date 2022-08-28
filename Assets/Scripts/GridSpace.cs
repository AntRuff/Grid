using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    [SerializeField] private Transform playerPoint;
    public int objectSize;
    private PlayerManager player;
    [SerializeField]private GameObject glow;

    void Start(){
        player = null;
    }

    //Sets the player to this space
    public void SetPlayer(PlayerManager p){
        player = p;
        player.transform.position = playerPoint.position;
    }
    
    //Removes player from this space
    public void RemovePlayer(){
        player = null;
    }

    //Returns the player at space
    public PlayerManager GetPlayer(){
        return player;
    }

    //Turn the highlight on
    public void GlowOn(){
        glow.SetActive(true);
    }

    //Turn the highlight off
    public void GlowOff(){
        glow.SetActive(false);
    }
}
