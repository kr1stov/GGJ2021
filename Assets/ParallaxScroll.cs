using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Transform start;
    public Transform end;
    
    private PlayerController _player;
    public Transform _playerStart;
    public Transform _playerEnd;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //_player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Scroll()
    {
        //var t = _player.transform.positon - playerStart;
        
    }
}
