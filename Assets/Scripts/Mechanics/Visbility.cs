using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class Visbility : MonoBehaviour
{
    public Tile[] tiles;
    public int vWidth;
    public int vHeight;
    
    private PlayerController _player;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        BoundsInt sandBounds = GameManager.Instance.sandTilemap.cellBounds;
        
    }

    private void Update()
    {
        Reveal(_player.transform.position, vWidth, vHeight);
    }
    
    private void OnEnable()
    {
        Torch.torchActivated += RevealFixed;
    }

    private void OnDisable()
    {
        Torch.torchActivated -= RevealFixed;
    }

    private void Reveal(Vector3 playerWorldPosition, int width, int height)
    {
        Vector3Int tilePos = Vector3Int.zero;
        tilePos.x = (int) playerWorldPosition.x;
        tilePos.y = (int) playerWorldPosition.y-1;
        tilePos.z = 0;
        
        var posX = tilePos.x;
        var posY = tilePos.y;

        for (posX = tilePos.x-width/2; posX <= tilePos.x+width/2;posX++)
        {
            for (posY = tilePos.y-height/2; posY <= tilePos.y+height/2;posY++)
            {
                var t = Vector3.Magnitude(new Vector3Int(posX, posY, 0) - tilePos);
                t = t % 1 != 0 ? (int) t + 1 : t;
                t = Mathf.Clamp(t, 0, tiles.Length - 1);
                
                // DebugUtil.Log($"t: {t}");
                RevealTile(new Vector3Int(posX, posY, 0), tiles[(int)t]);
            }
        }
    }
    
    private void RevealFixed(object sender, Vector3Int playerWorldPosition)
    {
        int width = 7;
        int height = 7;
        
        Vector3Int tilePos = Vector3Int.zero;
        tilePos.x = (int) playerWorldPosition.x;
        tilePos.y = (int) playerWorldPosition.y-1;
        tilePos.z = 0;
        
        var posX = tilePos.x;
        var posY = tilePos.y;

        for (posX = tilePos.x-width/2; posX <= tilePos.x+width/2;posX++)
        {
            for (posY = tilePos.y-height/2; posY <= tilePos.y+height/2;posY++)
            {
                var t = Vector3.Magnitude(new Vector3Int(posX, posY, 0) - tilePos);
                t = t % 1 != 0 ? (int) t + 1 : t;
                t = Mathf.Clamp(t, 0, tiles.Length - 1);
                
                DebugUtil.Log($"t: {t}");
                RevealTile(new Vector3Int(posX, posY, 0), tiles[0]);
            }
        }
    }

    private void RevealTile(Vector3Int position, Tile newTile)
    {
        Tile currentTile = (Tile)GameManager.Instance.visibilityTilemap.GetTile(position);

        if (currentTile == null)
            return;
        
        if (newTile == null)
        {
            GameManager.Instance.visibilityTilemap.SetTile(position, null);
            return;
        }
        
        if (currentTile.color.a > newTile.color.a)
            GameManager.Instance.visibilityTilemap.SetTile(position, newTile);
    }
}
