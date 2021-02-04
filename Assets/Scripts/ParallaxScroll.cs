using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxScroll : MonoBehaviour
{
    public Transform playerLeft;
    public Transform playerRight;
    
    [FormerlySerializedAs("start")] public Transform left;
    [FormerlySerializedAs("end")] public Transform right;
    public Transform sprite;

    [SerializeField] private float distStartEnd;
    [SerializeField] private float distStartPlayer;
    [SerializeField] private float t;
    
    private PlayerController _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        distStartEnd = Vector3.Distance(playerLeft.position, playerRight.position);
        distStartPlayer = Vector3.Distance(playerLeft.position, _player.transform.position);
        t = distStartPlayer / distStartEnd;

        sprite.position = Vector3.Lerp(left.position, right.position, t);
    }
}
