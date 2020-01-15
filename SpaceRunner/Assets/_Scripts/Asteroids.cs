using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float speed;
    public AsteroidFieldSpawner AFS;
    public float AFNum;

    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "KillBox")
        {
            
            if (AFNum == 0)
            {
                AFS.Atrs1 = null;
                AFS.CanSpawn1 = false;
            }
            else if(AFNum == 1)
            {
                AFS.Atrs2 = null;
                AFS.CanSpawn2 = false;
            }
            Destroy(gameObject);
        }
    }
}
