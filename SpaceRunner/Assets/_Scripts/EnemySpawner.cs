using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] Enemies;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        i = Random.Range(0, Enemies.Length);
        Instantiate(Enemies[i], transform.position, transform.rotation);
    }
}
