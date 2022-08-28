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

    public void SetPlayer(PlayerManager p){
        player = p;
        player.transform.position = playerPoint.position;
    }
    
    public void RemovePlayer(){
        player = null;
    }

    public PlayerManager GetPlayer(){
        return player;
    }

    public void GlowOn(){
        glow.SetActive(true);
    }

    public void GlowOff(){
        glow.SetActive(false);
    }
}
