using System;
using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

[RequireComponent (typeof(SpriteRenderer))]
public class TeleportTile : Tile
{
    [SerializeField] private TeleportTile destinationTeleportalTile;

    public override void OnStep()
    {
        StartCoroutine(WaitOneFrameCoroutine());
        Debug.Log("here");
    }

    IEnumerator WaitOneFrameCoroutine()
    {
        Debug.Log("Current Frame: " + Time.frameCount);

        yield return null;
        FindAnyObjectByType<Board>().Teleport(this, destinationTeleportalTile);
    }
}


