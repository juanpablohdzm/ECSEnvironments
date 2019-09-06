using ECSEnvironments.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSEnvironments.PlayableSystems
{
    public class ECSEnvironmentListenerBehavior : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras;
        [SerializeField] private AudioSource audioSource;
        // Start is called before the first frame update
        void Awake()
        {
            SetCamerasColor();
        }

        // Update is called once per frame
        void Update()
        {
            if(!audioSource.isPlaying)
            {
                int audioIndex = Random.Range(0, ECSSceneLoader.Instance.currentInfo.environmentSound.Length);
                audioSource.clip = ECSSceneLoader.Instance.currentInfo.environmentSound[audioIndex];
                audioSource.Play();
            }
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
