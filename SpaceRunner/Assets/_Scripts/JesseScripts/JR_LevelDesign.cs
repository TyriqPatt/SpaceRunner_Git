using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JR_LevelDesign : MonoBehaviour
{
    EnemySpawner m_enemySpawner;
    MeteorSpawner m_metorSpawner;
    public int LevelOnePhases;
    public GameObject[] enemiesInLevel;
    public bool inCombat = true;

    // Start is called before the first frame update
    void Start()
    {
        m_enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        m_metorSpawner = GameObject.FindObjectOfType<MeteorSpawner>();
        LevelOnePhases = 1;
        LevelOneSystems();

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
        }
    }

    void LevelOneSystems()
    {
        switch (LevelOnePhases)
        {
            case 1:
                m_enemySpawner.SpawnEnemies(1, 0, 0, 0);
                LevelOnePhases = 2;
                print("Spawn One Enemy");
                break;
            case 2:
                m_enemySpawner.SpawnEnemies(2, 0, 0, 0);
                LevelOnePhases = 3;
                print("Spawn Two Enemies");
                break;
            case 3:
                m_enemySpawner.SpawnEnemies(3, 0, 0, 0);
                LevelOnePhases = 4;
                print("Spawn Three Enemies");
                break;
            case 4:
                StartCoroutine(m_metorSpawner.Spawn(m_metorSpawner.FirstSpawnTime));
                print("Astroids");
                break;
            case 5:
                print("First half of 20 phase");
                break;
            case 6:
                print("Astroids");
                break;
            case 7:
                print("Second half of 20 phase");
                break;
            default:
                print("No LevelOnePhase Int");
                break;
        }
    }


    void CheckifAllEneiesDead()
    {




    }

}
