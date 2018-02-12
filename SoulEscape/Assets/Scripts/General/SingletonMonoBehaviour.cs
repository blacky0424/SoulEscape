using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T: SingletonMonoBehaviour<T> {

    protected static T instance;

    public static T Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            instance = (T)FindObjectOfType(typeof(T));

            if(instance == null)
            {

            }

            return instance;
        }
    }

}
