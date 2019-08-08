using System.Collections;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using ECSEnvironments.ScriptableObjects;

namespace ECSEnvironments.Managers
{
    public class ECSSceneLoader : MonoBehaviour
    {
        private static ECSSceneLoader instance;
        [SerializeField] private ECSEnvironmentInfoHolderSO infoHolder;
        public ECSSceneLoader Instance { get { return instance; } }
        [SerializeField] private ECSGameEvent OnSceneLoaded;

        private void Start()
        {
            if (instance == null)
                instance = this;
            else
                if (instance != this)
                Destroy(gameObject);
        }

        [Button]
        public void LoadScene(string name = "underwater")
        {
            StartCoroutine(LoadSceneAsync(infoHolder.GetInfo(name).environmentScene));
        }

        private IEnumerator LoadSceneAsync(SceneField scene)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            while (async.progress < .9f)
            {
                yield return null;
            }

            async.allowSceneActivation = true;
            OnSceneLoaded.Raise();
        }
    }
}
