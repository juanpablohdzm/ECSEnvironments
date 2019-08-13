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
        public static ECSSceneLoader Instance { get { return instance; } }
        private static ECSSceneLoader instance;

        [SerializeField] private ECSEnvironmentInfoHolderSO infoHolder;

        [SerializeField] private ECSGameEvent OnSceneLoaded;
        [SerializeField] private ECSGameEvent OnSceneUnloaded;

        public ECSEnvironmentInfoSO currentInfo { get; private set; }

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
            currentInfo = infoHolder.GetInfo(name);
            StartCoroutine(LoadSceneAsync(currentInfo.environmentScene));                                             
        }

        public void UnloadScene()
        {
            StartCoroutine("UnloadSceneAsync");
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

        private IEnumerator UnloadSceneAsync()
        {
            AsyncOperation async = SceneManager.UnloadSceneAsync(currentInfo.environmentScene);
            while (async.progress < .9f)
            {
                yield return null;
            }

            OnSceneUnloaded.Raise();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
    }
}
