using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [SerializeField] private GridSpace[] gridSpaceTypes;
    private int[,] gridValues; // -1 None, 0 Blue, 1 Green, 2 Yellow
    private List<GridSpace> grid;
    
    public void GenerateMap(int gridSizeX, int gridSizeZ){  
        grid = new List<GridSpace>(); 
        CreateGrid(gridSizeX, gridSizeZ);
        Spawn(gridSizeX, gridSizeZ);
    }

    //Creates an empty grid of a defined size and perminates it with a default value
    private void CreateGrid(int x, int y){
        gridValues = new int[x,y];
        for (int i = 0; i < x; i ++){
            for (int j = 0; j < y; j++){
                gridValues[i,j] = -1;
            }
        }
    }

    //Instantiates a grid by placeing objects on the grid of varying sizes, and then fills the rest of the space with 1x1 tiles
    private void Spawn(int gridSizeX, int gridSizeZ) {
        int x = gridSizeX;
        int z = gridSizeZ;

        int starts = Random.Range(1, 5);
        //For loop for placing objects
        for (int s = 0; s < starts; s++){
            int length = Random.Range(0, 3);
            int size = gridSpaceTypes[length].objectSize;
            int fails = 0;
            bool success = false;
            //5 attempts to succeed, otherwise moves on to the next object
            while (fails < 5 && !success){
                int startX = Random.Range(0, x);
                int startZ = Random.Range(0, z);
                bool fail = false;
                //Checks if object falls outside grid, or on an occupied space
                for (int i = startX; i < startX+size; i++){
                    for (int j = startZ; j < startZ+size; j++){
                        if (i >= x || j >= z){
                            fail = true;
                        }
                        else if (gridValues[i, j] != -1){
                            fail = true;
                        }
                    }
                }
                if (fail) {fails++;}
                //If successful, defines all spaces of object as the object's enumeration
                else {
                    success = true;
                    for (int i = startX; i < startX+size; i++){
                        for (int j = startZ; j < startZ+size; j++){
                            gridValues[i,j] = size-1;
                        }
                    }
                }
            }
        }
        //After placing all objects, fill remaining values with a 1x1 and create all objects in game space
        for (int i = 0; i < x; i++){
            for (int j = 0; j < z; j++){
                if (gridValues[i,j] == -1) {gridValues[i,j] = 0;}
                grid.Add(Instantiate(gridSpaceTypes[gridValues[i,j]], new Vector3(i, 0, j), Quaternion.identity));                
            }
        }
    }

    //Returns the list of grid spaces
    public List<GridSpace> GetGrid(){
        return grid;
    }

}
