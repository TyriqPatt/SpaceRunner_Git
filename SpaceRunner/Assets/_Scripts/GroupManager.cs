using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{

    public GameObject[] Children;
    public bool Tutorial;
    public float EnemyCount;
    float f;
    public enum State { SpawnOne, SpawnThree, SpawnFive }

    public State G_ManagerState;

    // Start is called before the first frame update
    void Start()
    {
        if (Tutorial)
        {
            foreach (GameObject enemy in Children)
            {
                enemy.GetComponentInChildren<EnemyHealthBar>().isTutorial = Tutorial;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        f = Children.Length;
        if(f == 0)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnEnemies(State CurState)
    {
        G_ManagerState = CurState;

        if (G_ManagerState == State.SpawnOne)
        {
            Children[0].SetActive(true);
        }
        else if (G_ManagerState == State.SpawnThree)
        {
            Children[0].SetActive(true);
            Children[1].SetActive(true);
            Children[2].SetActive(true);
        }
        else if (G_ManagerState == State.SpawnFive)
        {
            Children[0].SetActive(true);
            Children[1].SetActive(true);
            Children[2].SetActive(true);
            Children[3].SetActive(true);
            Children[4].SetActive(true);
        }
    }
}
