using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ECSEnvironmentsUI : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 20.0f;
    [SerializeField] private float degreeToAdd = 180.0f;

    private bool shouldRotate = false;
    private bool forwardDirection = true;

    private Quaternion finalQuat;
    
    [Button]
    public void Rotate(bool forward = true)
    {
        if (!shouldRotate)
        {
            float degree = forward ? degreeToAdd : degreeToAdd * -1.0f;
            finalQuat = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + degree);
            shouldRotate = true;
        }
    }

    private void Update()
    {
        if (shouldRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, finalQuat, Time.deltaTime * rotSpeed);
            if ((transform.rotation.eulerAngles - finalQuat.eulerAngles).magnitude < 10.0f)
                shouldRotate = false;
        }
    }
}
