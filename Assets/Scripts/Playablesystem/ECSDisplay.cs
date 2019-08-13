using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSEnvironments.Interfaces;
using ECSEnvironments.ScriptableObjects;
using TMPro;
using ECSEnvironments.Managers;

namespace ECSEnvironments.PlayableSystems
{
    public class ECSDisplay : MonoBehaviour, ECSIRayInteractable
    {
        [SerializeField] private float speed = 2.0f;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private string displayName;
        private Material material;
        private bool bWantsToGlow = false;

        public void OnRayEnter()
        {
            bWantsToGlow = true;
            textMeshPro.text = displayName;
            Debug.LogError("On Ray Enter");
        }

        public void OnRayExit()
        {
            bWantsToGlow = false;
            textMeshPro.text = "Select Environment";
            Debug.LogError("On Ray Exit");
        }

        public void OnRayTrigger()
        {
            textMeshPro.text = "Select Environment";
            material.SetFloat("_EmissiveIntensity", 1.0f);
            Debug.LogError("On Ray Trigger");
            ECSSceneLoader.Instance.LoadScene(displayName.ToLower());
        }

        // Start is called before the first frame update
        void Start()
        {
            material = GetComponent<MeshRenderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            if (bWantsToGlow)
                material.SetFloat("_EmissiveIntensity", Mathf.Lerp(material.GetFloat("_EmissiveIntensity"), 10.0f, Time.deltaTime * speed));
            else
                material.SetFloat("_EmissiveIntensity", Mathf.Lerp(material.GetFloat("_EmissiveIntensity"), 1.0f, Time.deltaTime * speed));

        }
    }
}
