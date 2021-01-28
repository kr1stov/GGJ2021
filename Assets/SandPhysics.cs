using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SandPhysics : MonoBehaviour
{
    public Tilemap SandTilemap;
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
        SandTilemap.SetTile(position, null);

    }
}
