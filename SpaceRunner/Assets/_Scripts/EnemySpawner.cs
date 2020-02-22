using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] Enemies;
    int i;

    public int amount_of_BasicDroids;
    public int amount_of_SecondDroids;
    public int amount_of_ThirdDroids;
    public int amount_of_FourthDroids;


    public GameObject BasicDroidObject;
    public GameObject SecondDroidObject;
    public GameObject ThirdDroidObject;
    public GameObject FourthDroidObject;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            //Spawn();
            SpawnEnemies(amount_of_BasicDroids, amount_of_SecondDroids, amount_of_ThirdDroids, amount_of_FourthDroids);  
        }*/ 
    }

    void Spawn()
    {
        i = Random.Range(0, Enemies.Length);
        Instantiate(Enemies[i], transform.position, transform.rotation);
    }

    public void SpawnEnemies(int BasicDroid, int SecondDroid, int ThirdDroid, int FourthDroid)
    {
        for(int BasicIndex = 0; BasicIndex < BasicDroid; BasicIndex++)
        {
            Instantiate(BasicDroidObject, transform.position, transform.rotation);
        }

        for (int SecondIndex = 0; SecondIndex < SecondDroid; SecondIndex++)
        {
            Instantiate(SecondDroidObject, transform.position, transform.rotation);
        }

        for (int ThirdIndex = 0; ThirdIndex < ThirdDroid; ThirdIndex++)
        {
            Instantiate(ThirdDroidObject, transform.position, transform.rotation);
        }

        for (int FourthIndex = 0; FourthIndex < FourthDroid; FourthIndex++)
        {
            Instantiate(FourthDroidObject, transform.position, transform.rotation);
        }


    }
}
