using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldSpawner : MonoBehaviour
{

    public GameObject[] Af;
    public Transform Atrs1,Atrs2;
    public bool CanSpawn1, CanSpawn2;
    public float NextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        int ran = Random.Range(0, Af.Length);
        Atrs1 = Instantiate(Af[ran], transform.position, Quaternion.identity).transform;
        Atrs1.GetComponent<Asteroids>().AFS = this.GetComponent<AsteroidFieldSpawner>();
        Atrs1.GetComponent<Asteroids>().AFNum = 0;
        StartCoroutine(DelaySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(Atrs1 == null && !CanSpawn1)
        {
            //Spawn();
            CanSpawn1 = true;
        }
        if(Atrs2 == null && !CanSpawn2)
        {
            //Spawn();
            CanSpawn2 = true;
        }
    }

    void Spawn()
    {
        int ran = Random.Range(0,Af.Length);
        if (Atrs1 == null)
        {
            Atrs1 = Instantiate(Af[ran], transform.position, Quaternion.identity).transform;
            Atrs1.GetComponent<Asteroids>().AFS = this.GetComponent<AsteroidFieldSpawner>();
            Atrs1.GetComponent<Asteroids>().AFNum = 0;
            CanSpawn1 = false;
        }
        else if (Atrs2 == null)
        {
            Atrs2 = Instantiate(Af[ran], transform.position, Quaternion.identity).transform;
            Atrs2.GetComponent<Asteroids>().AFS = this.GetComponent<AsteroidFieldSpawner>();
            Atrs2.GetComponent<Asteroids>().AFNum = 1;
            CanSpawn2 = false;
        }
        
    }

    IEnumerator DelaySpawn()
    {
        int ran = Random.Range(0, Af.Length);
        yield return new WaitForSeconds(NextSpawnTime);
        Atrs2 = Instantiate(Af[ran], transform.position, Quaternion.identity).transform;
        Atrs2.GetComponent<Asteroids>().AFS = this.GetComponent<AsteroidFieldSpawner>();
        Atrs2.GetComponent<Asteroids>().AFNum = 1;
        StartCoroutine(DelaySpawn());
    }
}
