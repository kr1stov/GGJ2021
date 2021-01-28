using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utils;

public class CaptureUtil : MonoBehaviour
{
    public static CaptureUtil Instance;

    public InputActionAsset inputActionAsset;
    private InputAction _captureScreenshot;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        var utilsActionMap = inputActionAsset.FindActionMap("Utils");
        _captureScreenshot = utilsActionMap.FindAction("CaptureScreenshot");
        _captureScreenshot.performed += OnCapture;
        _captureScreenshot.Enable();
    }

    public void OnCapture(InputAction.CallbackContext context)
    {
        DebugUtil.Log("OnCapture");
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                CaptureScreenshot();
                break;
            default:
                break;
        }
    }

    private void CaptureScreenshot()
    {
        DateTime now = DateTime.Now;
        string fileName = $"Media/amaze_{SceneManager.GetActiveScene().name}_{now.Year}.{now.Month+1}.{now.Day+1}_{now.Hour}-{now.Minute}-{now.Second}_{Time.frameCount}.png";
        ScreenCapture.CaptureScreenshot(fileName);
        DebugUtil.Log("Screenshot captured");
    }
}
