using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public float FirstSpawnTime;
    public Transform[] Spawners;
    public GameObject[] Meteors;

    // Start is called before the first frame update
    void OnEnable()
    {
       // StartCoroutine(Spawn(FirstSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawn(float _NextSpawn)
    {
        int astr = Random.Range(0, Meteors.Length);
        yield return new WaitForSeconds(_NextSpawn);
        Transform spnr = Spawners[Random.Range(0, Spawners.Length)];
        Transform spnr2 = Spawners[Random.Range(0, Spawners.Length)];

        //Instantiate(Meteors[Random.Range(0, Meteors.Length)], spnr.transform.position, spnr.rotation);
        //Instantiate(Meteors[Random.Range(0, Meteors.Length)], spnr2.transform.position, spnr.rotation);
        Instantiate(Meteors[Random.Range(0, Meteors.Length)], Spawners[0].transform.position, Spawners[0].rotation);


        //StartCoroutine(Spawn(6));
    }

    private void OnDisable()
    {
        StopCoroutine(Spawn(3));
    }
}
