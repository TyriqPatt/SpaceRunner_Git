using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripletSpawn : MonoBehaviour
{

    public GameObject[] Enemies;
    public Transform spawn, spawn2, spawn3;
    int i;
    int i2;
    int i3;

    // Start is called before the first frame update
    void OnEnable()
    {
        i = Random.Range(0, Enemies.Length);
        i2 = Random.Range(0, Enemies.Length - 1);
        i3 = Random.Range(0, Enemies.Length - 1);
        Instantiate(Enemies[i], spawn.position, spawn.transform.rotation);
        Instantiate(Enemies[i2], spawn2.position, spawn2.transform.rotation);
        Instantiate(Enemies[i3], spawn3.position, spawn3.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
