using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public Transform[] Spawners;
    public GameObject[] Meteors;

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
        yield return new WaitForSeconds(5);
        Transform spnr = Spawners[Random.Range(0, Spawners.Length)];
        Transform spnr2 = Spawners[Random.Range(0, Spawners.Length)];
        int astr = Random.Range(0, Meteors.Length);
        Instantiate(Meteors[Random.Range(0, Meteors.Length)], spnr.transform.position, spnr.rotation);
        Instantiate(Meteors[Random.Range(0, Meteors.Length)], spnr2.transform.position, spnr.rotation);
        if (astr == 3 || astr == 4)
        {
            Instantiate(Meteors[Random.Range(0, Meteors.Length)], Spawners[0].transform.position, Spawners[0].rotation);
        }
        StartCoroutine(Spawn());
    }
}
