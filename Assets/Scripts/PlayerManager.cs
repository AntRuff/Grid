using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private Behaviour glow;
    private GridSpace curSpace;

    public void EnableHalo() {
        glow.enabled = true;
    }

    public void DisableHalo() {
        glow.enabled = false;
    }
    
    public GridSpace GetCurrentSpace() {
        return curSpace;
    }

    public void SetCurrentSpace(GridSpace newGridSpace) {
        Debug.Log("Remove Before");
        if (curSpace){curSpace.RemovePlayer();}
        Debug.Log("Remove After");
        curSpace = newGridSpace;
        Debug.Log("Set");
        transform.position = curSpace.GetPlayerPosition().position;
        curSpace.GivePlayer();
    }
}