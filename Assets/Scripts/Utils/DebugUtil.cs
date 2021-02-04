using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class DebugUtil : MonoBehaviour
    {
        public static DebugUtil Instance;
        
        public InputActionAsset inputActionAsset;
        private InputAction _reloadLevel;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if(Instance != this)
                Destroy(gameObject);
        
            DontDestroyOnLoad(gameObject);

            var utilsActionMap = inputActionAsset.FindActionMap("Utils");
            _reloadLevel = utilsActionMap.FindAction("ReloadLevel");
            _reloadLevel.performed += OnReload;
            _reloadLevel.Enable();
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            Debug.Log("OnReload");
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    ReloadLevel();
                    break;
                default:
                    break;
            }
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            Debug.Log("Level reloaded");
        }

        public static void Log(object o)
        {
// #if UNITY_EDITOR
            if(Application.isEditor || Debug.isDebugBuild)
                Debug.Log(o.ToString());
// #endif
        }
        
        public static void LogWarning(object o)
        {
#if UNITY_EDITOR
            Debug.LogWarning(o.ToString());
#endif
        }
        
        public static void LogError(object o)
        {
#if UNITY_EDITOR
            Debug.LogError(o.ToString());
#endif
        }
    }
}