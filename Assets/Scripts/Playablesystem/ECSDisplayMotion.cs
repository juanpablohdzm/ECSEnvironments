using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ECSDisplayMotion : MonoBehaviour
{
    [Header("Sphere variables")]
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float animSpeed = 1.0f;
    [SerializeField] private float animAmplitud = 1.0f;
    [Header("Object display variables")]
    [SerializeField, Required] private GameObject displayObject;
    [SerializeField] private float rotSpeed = 20.0f;

    private float startHeight;
    // Start is called before the first frame update
    void Start()
    {
        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newHeight = startHeight + animationCurve.Evaluate(Time.time * animSpeed)*animAmplitud;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);

        displayObject.transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime,Space.Self);
    }
}
