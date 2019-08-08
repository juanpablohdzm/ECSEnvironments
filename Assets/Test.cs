using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 forward = transform.forward;
        transform.Rotate(transform.right, 45);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
