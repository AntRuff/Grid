using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private Behaviour glow;
    private GridSpace curSpace;

    private void Start() {
        DisableHalo();
    }

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
        curSpace = newGridSpace;
        transform.position = curSpace.GetPlayerPosition().position;
    }
}