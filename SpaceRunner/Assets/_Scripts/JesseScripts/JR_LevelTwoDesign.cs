﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JR_LevelTwoDesign : MonoBehaviour
{
    EnemySpawner m_enemySpawner;
    MeteorSpawner m_metorSpawner;
    public int LevelOnePhases;
    public GameObject[] enemiesInLevel;
    private bool inCombat = true;
    private float SpawnDelay;
    private int WavePhase = 0;
    private int HowManyDroidsAdded = 0;
    private bool inStartCases = true;
    public GameObject LastBoss;
    private Transform SpawnerLocation;

    public GameObject WarningBoss;
    public GameObject WarningEnemies;
    // Start is called before the first frame update
    void Start()
    {
        m_enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        m_metorSpawner = GameObject.FindObjectOfType<MeteorSpawner>();
        LevelOnePhases = 1;
        LevelOneSystems();
        SpawnerLocation = GameObject.FindObjectOfType<EnemySpawner>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy");

        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelOneSystems();
        }

        if (enemiesInLevel.Length <= 0)
        {
            inCombat = false;
            if (inStartCases)
            {
                LevelOneSystems();

            }
        }
        if (WavePhase == 1)
        {
            WavePhaseFunction();
        }
    }

    void LevelOneSystems()
    {
        switch (LevelOnePhases)
        {
            case 1:
                m_enemySpawner.SpawnEnemies(0, 1, 0, 0);
                LevelOnePhases = 2;
                print("Spawn One Enemy");
                break;
            case 2:
                m_enemySpawner.SpawnEnemies(2, 1, 0, 0);
                LevelOnePhases = 3;
                print("Spawn Two Enemies");
                break;
            case 3:
                m_enemySpawner.SpawnEnemies(1, 2, 0, 0);
                LevelOnePhases = 4;
                print("Spawn Three Enemies");
                break;
            case 4:
                inStartCases = false;
                StartCoroutine(m_metorSpawner.Spawn(m_metorSpawner.FirstSpawnTime));
                LevelOnePhases = 5;
                StartCoroutine(WaitForAstroids());
                print("Astroids");
                break;
            case 5:
                m_enemySpawner.SpawnEnemies(2, 1, 0, 0);
                WavePhase = 1;
                LevelOnePhases = 6;
                print("First half of 20 phase");
                break;
            case 6:
                WavePhase = 0;
                HowManyDroidsAdded = 0;
                StartCoroutine(m_metorSpawner.Spawn(m_metorSpawner.FirstSpawnTime));
                LevelOnePhases = 7;
                StartCoroutine(WaitForAstroids());
                print("Astroids");
                break;
            case 7:
                m_enemySpawner.SpawnEnemies(2, 1, 0, 0);
                WavePhase = 1;
                LevelOnePhases = 8;
                print("Second half of 20 phase");
                break;
            case 8:
                WavePhase = 0;
                HowManyDroidsAdded = 0;
                StartCoroutine(WaitForBoss());
                LevelOnePhases = 9;
                print("Warning Boss");
                break;
            case 9:

                Instantiate(LastBoss, SpawnerLocation.position, SpawnerLocation.rotation);
                print("NewEnemy");
                break;
            default:
                print("No LevelOnePhase Int");
                break;
        }
    }


    void WavePhaseFunction()
    {
        int Ran;

        if (HowManyDroidsAdded < 15)
        {
            if (enemiesInLevel.Length <= 2)
            {
                SpawnDelay += 1 * Time.deltaTime;
                if (SpawnDelay >= 3)
                {
                    Ran = Random.RandomRange(0, 5);
                    HowManyDroidsAdded += 1;
                    SpawnDelay = 0;

                    if(Ran == 0)
                    {
                        m_enemySpawner.SpawnEnemies(0, 1, 0, 0);

                    }
                    else if(Ran > 0)
                    {
                        m_enemySpawner.SpawnEnemies(1, 0, 0, 0);
                    }
                }

            }
        }
        else
        {
            LevelOneSystems();

        }

    }

    IEnumerator WaitForAstroids()
    {

        yield return new WaitForSeconds(10);
        WarningEnemies.SetActive(true);
        yield return new WaitForSeconds(5);
        WarningEnemies.SetActive(false);
        LevelOneSystems();

    }

    IEnumerator WaitForBoss()
    {
        WarningBoss.SetActive(true);

        yield return new WaitForSeconds(7);
        WarningBoss.SetActive(false);

        LevelOneSystems();


    }

}
