using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("MoveRight", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveRight()
    {
        transform.position = new Vector3(0.5f, 0, 0);
        Invoke("MoveLeft", 1);
    }

    void MoveLeft()
    {
        transform.position = new Vector3(0, 0, 0);
        Invoke("MoveRight", 1);
    }
}
