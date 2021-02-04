using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public Tilemap architectureTilemap;
    public Tilemap sandTilemap;
    public Tilemap sandBackgroundTilemap;
    public Tilemap visibilityTilemap;
    // public Tilemap BackgroundTilemap;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy((gameObject));
    }

    public void Start()
    {
        sandTilemap.gameObject.SetActive(true);
        visibilityTilemap.gameObject.SetActive(true);

    }
}
