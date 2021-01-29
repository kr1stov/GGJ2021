using UnityEngine;

public class Door : Receiver
{
    public Sprite offState;
    public Sprite onState;
    
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = false;
    }
    
    public override void DoSomething()
    {
        Open();
    }
    
    void Open()
    {
        _renderer.sprite = (_isOn = !_isOn) ? onState : offState;
        _collider.isTrigger = true;
    }
}
