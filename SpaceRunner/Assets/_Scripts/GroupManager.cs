using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{

    public GameObject[] Children;
    public bool Tutorial;
    float f;

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
}
