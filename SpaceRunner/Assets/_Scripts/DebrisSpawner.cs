using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    public Transform Spawner;
    public GameObject[] Debris;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(15, 40));
        //Transform spnr2 = Spawners[Random.Range(0, Spawners.Length)];
        Instantiate(Debris[Random.Range(0, Debris.Length)], Spawner.transform.position, Spawner.rotation);
        //Instantiate(Meteors[Random.Range(0, Meteors.Length)], spnr2.transform.position, spnr.rotation);
        StartCoroutine(Spawn());
    }
}
