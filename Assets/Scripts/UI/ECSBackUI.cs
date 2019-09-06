using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ECSEnvironments.Managers;

namespace ECSEnvironments.UI
{
    public class ECSBackUI : MonoBehaviour
    {
        [SerializeField] private Slider[] sliders;
        // Start is called before the first frame update
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        }        

        private void OnClick()
        {
            foreach (Slider slider in sliders)
            {
                slider.value = slider.minValue;
            }
            ECSLevelManager.DestroyAllEntities();
            ECSSceneLoader.Instance.UnloadScene();
        }
    }
}
