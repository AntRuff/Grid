using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private Vector3 startingPos;

    private void Start() {
        startingPos = transform.position;
    }

    public void UpdatePosition(Transform newPos){
        transform.position = newPos.position;
    }

    public void ResetPosition(){
        transform.position = startingPos;
    }
}
