using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public bool isSingleUse;
    
    public Receiver[] receivers;
    
    protected bool _isOn;

    public bool IsOn
    {
        get => _isOn;
        set => _isOn = value;
    }

    private void OnDrawGizmos()
    {
        foreach (var receiver in receivers)
        {
            Debug.DrawLine(transform.position, receiver.transform.position, Color.green);
        }
    }
}
