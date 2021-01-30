using System;
using UnityEngine;

public class Torch : Receiver
{
    public static EventHandler<Vector3Int> torchActivated;
    
    public Color offState;
    public Color onState;
    
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public override void DoSomething()
    {
        Toggle();
    }
    
    void Toggle()
    {
        _renderer.color = (_isOn = !_isOn) ? onState : offState;
        torchActivated?.Invoke(this, new Vector3Int((int)transform.position.x, (int)transform.position.y, 0));
    }
}
