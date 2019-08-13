using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECSDrawLocalVectos : MonoBehaviour
{
    [SerializeField] private float lineLength = 2.0f;
    private void OnDrawGizmos()
    {
        //Forward vector
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * lineLength);

        //Right vector 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * lineLength);

        //Up vector
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * lineLength);

    }
}
