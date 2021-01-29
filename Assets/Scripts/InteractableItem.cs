using System;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public bool isSingleUse;
    public Sprite offState;
    public Sprite onState;

    
    public Receiver[] receivers;
    
    private bool _isOn;

    public bool IsOn
    {
        get => _isOn;
        set => _isOn = value;
    }

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnEnable()
    {
        PlayerController.interactedWithItem += Toggle;
    }

    private void OnDisable()
    {
        PlayerController.interactedWithItem -= Toggle;
    }

    void Toggle(object sender, int id)
    {
        if (id != gameObject.GetInstanceID())
            return;
        
        _isOn = !_isOn;
        _renderer.sprite = _isOn ? onState : offState;

        foreach (var receiver in receivers)
        {
            receiver.DoSomething();
        }
        
        if(isSingleUse)
            PlayerController.interactedWithItem -= Toggle;
    }

    private void OnDrawGizmos()
    {
        foreach (var receiver in receivers)
        {
            Debug.DrawLine(transform.position, receiver.transform.position, Color.green);
        }
    }
}
