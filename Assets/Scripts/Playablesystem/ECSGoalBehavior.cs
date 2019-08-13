using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ECSGoalBehavior : MonoBehaviour
{
    [SerializeField] private float distanceMax = 5.0f;
    [SerializeField] private float heightMax = 5.0f;


    private Slider distance;
    private Slider height;
    private Slider rotation;

    // Start is called before the first frame update
    void Start()
    {
        distance = GameObject.Find("DistanceSlider").GetComponent<Slider>();
        distance.onValueChanged.AddListener(delegate { ChangeDistance(); });
        height = GameObject.Find("HeightSlider").GetComponent<Slider>();
        height.onValueChanged.AddListener(delegate { ChangeHeight(); });
        rotation = GameObject.Find("RotationSlider").GetComponent<Slider>();
        rotation.onValueChanged.AddListener(delegate { ChangeRotation(); });
    }

    private void ChangeDistance()
    {

        Vector3 forward = transform.forward * distance.value * distanceMax;
        forward.y = transform.localPosition.y;
        transform.localPosition = forward;

    }

    private void ChangeHeight()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0 + height.value * heightMax, transform.localPosition.z);
    }

   

    private void ChangeRotation()
    {
        Vector3 posProjected = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        Vector3 forward = Quaternion.AngleAxis(rotation.value, Vector3.up)* new Vector3(0,0,1);

        forward = forward * posProjected.magnitude;
        forward.y = transform.position.y;

        transform.position = forward;
        transform.rotation = Quaternion.AngleAxis(rotation.value, Vector3.up);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotation.value += 5.0f;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            height.value += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            distance.value += 0.1f;
        }

#endif
    }
}
