using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Umbrella_LR") != 0)
        {
            transform.position = Vector3.right;
            Debug.Log("test");
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }
}
