using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [SerializeField] private GridSpace[] gridSpaces;
    private int[,] gridValues; // 0 None, 1 Blue, 2 Green, 3 Yellow
    public int gridSizeX = 10;
    public int gridSizeZ = 10;
    
    // Start is called before the first frame update
    void Start()
    {   
        CreateGrid(gridSizeX, gridSizeZ);
        Spawn();
    }


    //Creates an empty grid of a defined size and perminates it with a default value
    private void CreateGrid(int x, int y){
        gridValues = new int[x,y];
        for (int i = 0; i < x; i ++){
            for (int j = 0; j < y; j++){
                gridValues[i,j] = 0;
            }
        }
    }

    //Instantiates a grid by placeing objects on the grid of varying sizes, and then fills the rest of the space with 1x1 tiles
    private void Spawn() {
        int x = gridSizeX;
        int z = gridSizeZ;

        int starts = Random.Range(1, 5);
        //For loop for placing objects
        for (int s = 0; s < starts; s++){
            int length = Random.Range(0, 3);
            int size = gridSpaces[length].objectSize;
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
                        else if (gridValues[i, j] != 0){
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
                            gridValues[i,j] = size;
                        }
                    }
                }
            }
        }
        //After placing all objects, fill remaining values with a 1x1 and create all objects in game space
        for (int i = 0; i < x; i++){
            for (int j = 0; j < z; j++){
                if (gridValues[i,j] == 0) {gridValues[i,j] = 1;}
                Instantiate(gridSpaces[gridValues[i,j]-1], new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }

}
