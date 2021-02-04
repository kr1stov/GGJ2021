using System;
using UnityEngine;

public class DevConsole : MonoBehaviour
{
    public static DevConsole instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        DevMenu.devCommandEntered += DoSeCommand;
    }

    private void DoSeCommand(object sender, string e)
    {
        throw new NotImplementedException();
    }

    // private void OnDisable()
    // {
    //     throw new NotImplementedException();
    // }
}
