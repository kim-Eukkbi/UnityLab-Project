using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingleton : MonoBehaviour
{
    
}

public abstract class GenericSingleton<T> : GenericSingleton where T : GenericSingleton<T>
{
    private static T Instance;
    public static T instance
    {
        get
        {
            return Instance;
        }
    }
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = (T)this;
        }
    }
}