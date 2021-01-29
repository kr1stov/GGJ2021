public class Key : InteractableItem
{
    private void OnEnable()
    {
        PlayerController.interactedWithItem += Collect;
    }

    private void OnDisable()
    {
        PlayerController.interactedWithItem -= Collect;
    }
    
    void Collect(object sender, int id)
    {
        if (id != gameObject.GetInstanceID())
            return;
        
        _isOn = !_isOn;

        foreach (var receiver in receivers)
        {
            receiver.DoSomething();
        }

        if (isSingleUse)
        {
            PlayerController.interactedWithItem -= Collect;
            Destroy(gameObject);
        }
        
    }
}
