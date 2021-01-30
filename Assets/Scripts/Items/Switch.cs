using UnityEngine;

public class Switch : InteractableItem
{
    public Sprite offState;
    public Sprite onState;
    
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

}
