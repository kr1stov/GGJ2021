using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SandPhysics : MonoBehaviour
{
    public Tilemap SandTilemap;
    public Tilemap ArchitectureTilemap;

    public float sandDelay;
    private void OnEnable()
    {
        PlayerController.sandTileDugAway += DeleteSandTile;
    }

    private void OnDisable()
    {
        PlayerController.sandTileDugAway -= DeleteSandTile;
    }

    private void DeleteSandTile(object sender, Vector3Int position)
    {
        StartCoroutine(DeleteTile(sandDelay, position));

    }

    private IEnumerator DeleteTile(float delay, Vector3Int position)
    {
        SandTilemap.SetTile(position, null);
        yield return new WaitForSeconds(delay);
        FlowToSides(delay, position);
        FlowDown(delay, position);
    }
    
    private void FlowDown(float delay, Vector3Int position)
    {
        Tile up = (Tile)SandTilemap.GetTile(position+Vector3Int.up);
        Tile upArch = (Tile)ArchitectureTilemap.GetTile(position+Vector3Int.up);

        if (up && !upArch)
        {
            SandTilemap.SetTile(position, up);
            StartCoroutine(DeleteTile(delay, position + Vector3Int.up));
        }
    }
    
    private void FlowToSides(float delay, Vector3Int position)
    {
        Tile left = (Tile)SandTilemap.GetTile(position+Vector3Int.left);
        Tile upperleft = (Tile)SandTilemap.GetTile(position+(Vector3Int.up+Vector3Int.left));
        
        Tile leftArch = (Tile)ArchitectureTilemap.GetTile(position+Vector3Int.left);
        Tile upperleftArch = (Tile)ArchitectureTilemap.GetTile(position+(Vector3Int.up+Vector3Int.left));

        if (left && upperleft && !leftArch && !upperleftArch)
        {
            SandTilemap.SetTile(position, left);
            StartCoroutine(DeleteTile(delay, position + Vector3Int.left));
        }
        
        Tile right = (Tile)SandTilemap.GetTile(position+Vector3Int.right);
        Tile upperright = (Tile)SandTilemap.GetTile(position+(Vector3Int.up+Vector3Int.right));
        
        Tile rightArch = (Tile)ArchitectureTilemap.GetTile(position+Vector3Int.right);
        Tile upperrightArch = (Tile)ArchitectureTilemap.GetTile(position+(Vector3Int.up+Vector3Int.right));

        if (right && upperright && !rightArch && !upperrightArch)
        {
            SandTilemap.SetTile(position, right);
            StartCoroutine(DeleteTile(delay, position + Vector3Int.left));
        }
    }
}
