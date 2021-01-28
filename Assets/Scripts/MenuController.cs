using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utils;

[CreateAssetMenu(fileName = "New MenuController", menuName = "CG TOOLS/Menu Controller", order = 0)]
public class MenuController : ScriptableObject
{
    public InputActionAsset inputActionAsset;
    private InputAction _pause;

    public bool gameIsPaused;

    public Stack<string> loadedScenes;
    
    private void OnEnable()
    {
        var utilsActionMap = inputActionAsset.FindActionMap("UI");
        _pause = utilsActionMap.FindAction("Pause");
        _pause.performed += OnPause;
        _pause.Enable();

        loadedScenes = new Stack<string>();
        gameIsPaused = false;
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        DebugUtil.Log("OnPause");
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                if(!gameIsPaused)
                    PauseGame();
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        LoadScene("Game");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;

        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (var canvas in canvases)
        {
            canvas.enabled = false;
        }
            
        
        LoadScene("IngameMenu", LoadSceneMode.Additive);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (var canvas in canvases)
        {
            canvas.enabled = true;
        }
        GoBack();
    }

    public void ShowOptions()
    {
       LoadScene("Options", LoadSceneMode.Additive);
    }

    public void ShowCredits()
    {
        LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
    }

    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        loadedScenes.Push(sceneName);
        string scene = loadedScenes.Peek();
        DebugUtil.Log("scene on top: " + scene);
        SceneManager.LoadSceneAsync(sceneName, mode);
    }

    public void GoBack()
    {
        if (loadedScenes == null || loadedScenes.Count == 0)
        {
            SceneManager.LoadSceneAsync(0);
            return;
        }
        
        string scene = loadedScenes.Pop();
        DebugUtil.Log("scene popped: " + scene);
        SceneManager.UnloadSceneAsync(scene);
    }
    

}
