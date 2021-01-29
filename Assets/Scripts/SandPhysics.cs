using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SandPhysics : MonoBehaviour
{
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
        GameManager.Instance.sandTilemap.SetTile(position, null);
        yield return new WaitForSeconds(delay);
        FlowToSides(delay, position);
        FlowDown(delay, position);
    }
    
    private void FlowDown(float delay, Vector3Int position)
    {
        Tile up = (Tile)GameManager.Instance.sandTilemap.GetTile(position+Vector3Int.up);
        Tile upArch = (Tile)GameManager.Instance.architectureTilemap.GetTile(position+Vector3Int.up);

        if (up && !upArch)
        {
            GameManager.Instance.sandTilemap.SetTile(position, up);
            StartCoroutine(DeleteTile(delay, position + Vector3Int.up));
        }
    }
    
    private void FlowToSides(float delay, Vector3Int position)
    {
        Tile left = (Tile)GameManager.Instance.sandTilemap.GetTile(position+Vector3Int.left);
        Tile upperleft = (Tile)GameManager.Instance.sandTilemap.GetTile(position+(Vector3Int.up+Vector3Int.left));
        
        Tile leftArch = (Tile)GameManager.Instance.architectureTilemap.GetTile(position+Vector3Int.left);
        Tile upperleftArch = (Tile)GameManager.Instance.architectureTilemap.GetTile(position+(Vector3Int.up+Vector3Int.left));

        if (left && upperleft && !leftArch && !upperleftArch)
        {
            GameManager.Instance.sandTilemap.SetTile(position, left);
            StartCoroutine(DeleteTile(delay, position + Vector3Int.left));
        }
        
        Tile right = (Tile)GameManager.Instance.sandTilemap.GetTile(position+Vector3Int.right);
        Tile upperright = (Tile)GameManager.Instance.sandTilemap.GetTile(position+(Vector3Int.up+Vector3Int.right));
        
        Tile rightArch = (Tile)GameManager.Instance.architectureTilemap.GetTile(position+Vector3Int.right);
        Tile upperrightArch = (Tile)GameManager.Instance.architectureTilemap.GetTile(position+(Vector3Int.up+Vector3Int.right));

        if (right && upperright && !rightArch && !upperrightArch)
        {
            GameManager.Instance.sandTilemap.SetTile(position, right);
            StartCoroutine(DeleteTile(delay, position + Vector3Int.left));
        }
    }
}
