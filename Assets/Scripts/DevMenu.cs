using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DevMenu : MonoBehaviour
{
    public static EventHandler<string> devCommandEntered;
    
    public static DevMenu instance;
    public Canvas menuCanvas;
    public Canvas consoleCanvas;
    
    private bool _isConsoleOpen;
    private TMP_InputField _consoleInputField;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _isConsoleOpen = false;
        consoleCanvas.enabled = _isConsoleOpen;
        _consoleInputField = consoleCanvas.GetComponentInChildren<TMP_InputField>();
        
        if (!Debug.isDebugBuild)
        {
            menuCanvas.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    public void OnToggleConsole(InputAction.CallbackContext context)
    {
        _isConsoleOpen = !_isConsoleOpen;
        consoleCanvas.enabled = _isConsoleOpen;
        
        if(_isConsoleOpen)
            _consoleInputField.Select();
    }

    public void ProcessInput()
    {
        string devCmd = _consoleInputField.text;
        Debug.Log(devCmd);
        _consoleInputField.text = string.Empty;
        devCommandEntered?.Invoke(this, devCmd);
    }
}
