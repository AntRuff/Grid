using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    [SerializeField] private Transform playerPoint;
    public int objectSize;
    //private PlayerManager player;
    [SerializeField]private GameObject glow;
    private int xGridPos;
    private int zGridPos;

    private bool hasPlayer = false;


    void Start(){
        //player = null;
        xGridPos = 0;
        zGridPos = 0;
    }

    //Sets the player to this space
    public void GivePlayer(){
        hasPlayer = true;
    }

    public Transform GetPlayerPosition() {
        return playerPoint;
    }
    
    //Removes player from this space
    public void RemovePlayer(){
        hasPlayer = false;
    }

    public bool HasPlayer(){
        return hasPlayer;
    }

    //Turn the highlight on
    public void GlowOn(){
        glow.SetActive(true);
    }

    //Turn the highlight off
    public void GlowOff(){
        glow.SetActive(false);
    }

    public void SetPos(int x, int z){
        xGridPos = x;
        zGridPos = z;
    }

    public int GetPosX(){
        return xGridPos;
    }

    public int GetPosZ(){
        return zGridPos;
    }
}