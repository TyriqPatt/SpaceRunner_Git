using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldSpawner : MonoBehaviour
{

    public GameObject[] Af;
    public Transform Atrs1,Atrs2;
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
