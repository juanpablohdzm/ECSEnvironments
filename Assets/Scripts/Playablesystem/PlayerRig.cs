using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRig : MonoBehaviour,IAnchorDependent
{
    [SerializeField] private GameObject controller; 

    public void SetAnchor(GameObject HandAnchor)
    {
        Instantiate(controller, HandAnchor.transform);
    }
}
