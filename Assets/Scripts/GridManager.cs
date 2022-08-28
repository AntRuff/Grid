using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [SerializeField] private GameObject gridSpace;
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }


    //Instantiates a the grid based on 2 random values for the size, and uses the default space
    private void Spawn() {
        int x = Random.Range(1, 11);
        int z = Random.Range(1, 11);

        for (int i = 0; i < z; i++){
            for (int j = 0; j < x; j++){
                Instantiate(gridSpace, new Vector3(j, 0, i), Quaternion.identity);
            }
        }
    }

}
