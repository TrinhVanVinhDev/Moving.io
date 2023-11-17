using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T ins;
    public static T Ins
    {
        get
        {
            if(ins == null)
            {
                ins = FindObjectOfType<T>();
                if(ins == null)
                {
                    ins = new GameObject(nameof(T)).AddComponent<T>();
                }
            }

            return ins;
        }
    }

    private void Awake()
    {
        if (ins == null)
        {
            ins = this as T;
        }
        else
        {
            if (ins != this)
            {
                Destroy(this);
            }
        }
    }
}
