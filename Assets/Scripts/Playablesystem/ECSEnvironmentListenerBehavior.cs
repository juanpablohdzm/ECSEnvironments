using ECSEnvironments.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSEnvironments.PlayableSystems
{
    public class ECSEnvironmentListenerBehavior : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras;
        // Start is called before the first frame update
        void Awake()
        {
            SetCamerasColor();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCamerasColor()
        {
            foreach (Camera camera in cameras)
            {
                camera.backgroundColor = ECSSceneLoader.Instance.currentInfo.cameraBackgroundColor;
            }
        }
    }
}
