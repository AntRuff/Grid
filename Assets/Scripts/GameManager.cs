using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private PlayerManager player;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeZ;

    private void Start() {
        grid.GenerateMap(gridSizeX, gridSizeZ);
        grid.GetGrid()[0].SetPlayer(player);
    }
}
