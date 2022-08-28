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
        //glow.SetActive(false);
        player = null;
    }

    public void SetPlayer(PlayerManager p){
        player = p;
        player.transform.position = playerPoint.position;
        glow.SetActive(true);
    }
    
    public void RemovePlayer(){
        glow.SetActive(false);
        player = null;
    }

    public PlayerManager GetPlayer(){
        return player;
    }
}
