using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class Visbility : MonoBehaviour
{
    public Tilemap sandTilemap;
    public Tilemap visibilityTilemap;
    public Tile[] tiles;
    public int vWidth;
    public int vHeight;
    
    private PlayerController _player;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        BoundsInt sandBounds = sandTilemap.cellBounds;
        
    }

    private void Update()
    {
        Reveal(_player.transform.position, vWidth, vHeight);
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
                
                DebugUtil.Log($"t: {t}");
                RevealTile(new Vector3Int(posX, posY, 0), tiles[(int)t]);
            }
        }
    }

    private void RevealTile(Vector3Int position, Tile newTile)
    {
        Tile currentTile = (Tile)visibilityTilemap.GetTile(position);

        if (currentTile == null)
            return;
        
        if (newTile == null)
        {
            visibilityTilemap.SetTile(position, null);
            return;
        }
        
        // DebugUtil.Log($"currentTile.color.a:{currentTile.color.a} | newTile.color.a: { newTile.color.a}");
        
        if (currentTile.color.a > newTile.color.a)
            visibilityTilemap.SetTile(position, newTile);
    }
}
