using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ECSEnvironments.Managers;

namespace ECSEnvironments.UI
{
    public class ECSBackUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        }        

        private void OnClick()
        {
            ECSSceneLoader.Instance.UnloadScene();
        }
    }
}
