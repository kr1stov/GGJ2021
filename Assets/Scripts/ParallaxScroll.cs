using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float maxOffset;

    private PlayerController _player;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 currentPos;
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        startPos = start.position;
        endPos = end.position;
        center = Vector3.Lerp(startPos, endPos, .5f);
        
        currentPos = _player.transform.position;
        currentPos.y = transform.position.y;

        transform.position = currentPos;
    }

    // Update is called once per frame
    void Update()
    {
        var playerDistToCenter = _player.transform.position - center;
        var maxDistToCenter = Vector3.Distance(startPos, endPos);
        var tPlayer = playerDistToCenter / maxDistToCenter;

        var finalPos = _player.transform.position + -tPlayer * maxOffset;
        finalPos.y = .5f;
        
        transform.position = finalPos;
    }
}
