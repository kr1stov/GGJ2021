using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;
    public GameSettings gameSettings;
    public MenuController menuController;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += KillAllUnnecessaryCams;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= KillAllUnnecessaryCams;
    }
    
    
    private void KillAllUnnecessaryCams(Scene scene, LoadSceneMode mode)
    {
        Camera[] cams = FindObjectsOfType<Camera>();
        Camera mainCam = Camera.main;
        
        foreach (var cam in cams)
        {
            if(mainCam != cam)
                Destroy(cam);
        }
    }
}