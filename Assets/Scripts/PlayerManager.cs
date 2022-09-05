using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private Behaviour glow;
    private GridSpace curSpace;
    public int movementRange = 3;

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
        if (curSpace){curSpace.RemovePlayer();}
        curSpace = newGridSpace;
        transform.position = curSpace.GetPlayerPosition().position;
        curSpace.GivePlayer();
    }
}