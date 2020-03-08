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

    public Transform SpawnPointOne;
    public Transform SpawnPointTwo;
    public Transform SpawnPointThree;
    public Transform SpawnPointFour;
    public Transform SpawnPointFive;

    public int ranNum; 

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
        ranNum = Random.Range(0, 4);

        for(int BasicIndex = 0; BasicIndex < BasicDroid; BasicIndex++)
        {
            if (ranNum == 0)
            {
                Instantiate(BasicDroidObject, SpawnPointOne.position, SpawnPointOne.rotation);
            }
            if (ranNum == 1)
            {
                Instantiate(BasicDroidObject, SpawnPointTwo.position, SpawnPointTwo.rotation);
            }
            if (ranNum == 2)
            {
                Instantiate(BasicDroidObject, SpawnPointThree.position, SpawnPointThree.rotation);
            }
            if (ranNum == 3)
            {
                Instantiate(BasicDroidObject, SpawnPointFour.position, SpawnPointFour.rotation);
            }
            if (ranNum == 4)
            {
                Instantiate(BasicDroidObject, SpawnPointFive.position, SpawnPointFive.rotation);
            }
        }

        for (int SecondIndex = 0; SecondIndex < SecondDroid; SecondIndex++)
        {
            if (ranNum == 0)
            {
                Instantiate(SecondDroidObject, SpawnPointOne.position, SpawnPointOne.rotation);
            }
            else if (ranNum == 1)
            {
                Instantiate(SecondDroidObject, SpawnPointTwo.position, SpawnPointTwo.rotation);
            }
            else if (ranNum == 2)
            {
                Instantiate(SecondDroidObject, SpawnPointThree.position, SpawnPointThree.rotation);
            }
            else if (ranNum == 3)
            {
                Instantiate(SecondDroidObject, SpawnPointFour.position, SpawnPointFour.rotation);
            }
            else if (ranNum == 4)
            {
                Instantiate(SecondDroidObject, SpawnPointFive.position, SpawnPointFive.rotation);
            }
        }

        for (int ThirdIndex = 0; ThirdIndex < ThirdDroid; ThirdIndex++)
        {
            if (ranNum == 0)
            {
                Instantiate(ThirdDroidObject, SpawnPointOne.position, SpawnPointOne.rotation);
            }
            else if (ranNum == 1)
            {
                Instantiate(ThirdDroidObject, SpawnPointTwo.position, SpawnPointTwo.rotation);
            }
            else if (ranNum == 2)
            {
                Instantiate(ThirdDroidObject, SpawnPointThree.position, SpawnPointThree.rotation);
            }
            else if (ranNum == 3)
            {
                Instantiate(ThirdDroidObject, SpawnPointFour.position, SpawnPointFour.rotation);
            }
            else if (ranNum == 4)
            {
                Instantiate(ThirdDroidObject, SpawnPointFive.position, SpawnPointFive.rotation);
            }
        }

        for (int FourthIndex = 0; FourthIndex < FourthDroid; FourthIndex++)
        {
            if (ranNum == 0)
            {
                Instantiate(FourthDroidObject, SpawnPointOne.position, SpawnPointOne.rotation);
            }
            else if (ranNum == 1)
            {
                Instantiate(FourthDroidObject, SpawnPointTwo.position, SpawnPointTwo.rotation);
            }
            else if (ranNum == 2)
            {
                Instantiate(FourthDroidObject, SpawnPointThree.position, SpawnPointThree.rotation);
            }
            else if (ranNum == 3)
            {
                Instantiate(FourthDroidObject, SpawnPointFour.position, SpawnPointFour.rotation);
            }
            else if (ranNum == 4)
            {
                Instantiate(FourthDroidObject, SpawnPointFive.position, SpawnPointFive.rotation);
            }
        }


    }


   
}
