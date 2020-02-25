using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JR_DontDestroyOnLoad : MonoBehaviour
{
    private static JR_DontDestroyOnLoad instance;



    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
