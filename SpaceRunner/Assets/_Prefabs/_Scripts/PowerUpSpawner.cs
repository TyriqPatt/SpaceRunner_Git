using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject[] Drop;
    public Transform[] spawnpos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PowerUp()
    {
        int ran = Random.Range(0, spawnpos.Length);
        int ranDrop = Random.Range(0, Drop.Length);
        yield return new WaitForSeconds(3);
        GameObject temp;
        temp = Instantiate(Drop[ranDrop], spawnpos[ran].position, spawnpos[ran].rotation);
        temp.transform.SetParent(transform.parent);
    }
}
