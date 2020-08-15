using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounudMusic : MonoBehaviour
{
    static BackGrounudMusic instance;


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance.gameObject);
    }

    
}
