using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public float FirstSpawnTime;
    public Transform[] Spawners;
    public GameObject[] Meteors;
    public GameObject WarningSign;
    public bool ForBossFight;
    public float Waves;
    public DroidBoss DB;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (ForBossFight)
        {
            StartCoroutine(Spawn(FirstSpawnTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawn(float _NextSpawn)
    {
        if (!ForBossFight)
        {
            WarningSign.SetActive(true);
            
        }
        else
        {
            Waves += 1;
        }
        int astr = Random.Range(0, Meteors.Length);
        yield return new WaitForSeconds(_NextSpawn);
        Transform spnr = Spawners[Random.Range(0, Spawners.Length)];
        Transform spnr2 = Spawners[Random.Range(0, Spawners.Length)];
        Instantiate(Meteors[Random.Range(0, Meteors.Length)], Spawners[0].transform.position, Spawners[0].rotation);
        if (ForBossFight)
        {
            StartCoroutine(Spawn(6));
            
            if(Waves == 5)
            {
                DB.BackToLevelPos();
                Waves = 0;
            }
        }
        else
        {
            WarningSign.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (!ForBossFight)
        {
            StartCoroutine(Spawn(2.5f));
        }
    }
}
