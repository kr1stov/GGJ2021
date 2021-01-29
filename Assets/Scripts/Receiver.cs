using UnityEngine;

public abstract class Receiver : MonoBehaviour
{
    protected bool _isOn = false;

    public abstract void DoSomething();
}
