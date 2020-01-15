using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public GameObject MoveText, ShootText, AbilityText;
    public GameObject Tutorial_Asteroids, DmgBots;
    public GameObject[] LastAsteriods;
    public Transform T_Spawner;
    public enum State {StartState, TeachMove, Spawn_Asteriods, WaitForAsteroids, TeachShoot, TeachAbility, SpawnBots, WaitForBots}
    public State TutorialState;
    float input;
    public Canons C;
    float left = 0;
    float right = 0;

    // Start is called before the first frame update
    void Start()
    {
        //C.enabled = false;
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        T_States();
    }

    void T_States()
    {
        switch (TutorialState)
        {
            case State.StartState:
                
                break;
            case State.TeachMove:
                MoveText.SetActive(true);
                //Wait for player to hold both the A and D buttons for 2-3 seconds
                HoldA_D();
                break;
            case State.Spawn_Asteriods:
                MoveText.SetActive(false);
                Instantiate(Tutorial_Asteroids, DmgBots.transform.position = new Vector3(T_Spawner.position.x, T_Spawner.position.y, T_Spawner.position.z + 400), T_Spawner.transform.rotation);
                TutorialState = State.WaitForAsteroids;
                break;
            case State.WaitForAsteroids:
                LastAsteriods = GameObject.FindGameObjectsWithTag("Asteroid");
                if(LastAsteriods == null)
                {
                    TutorialState = State.TeachShoot;
                }
                break;
            case State.TeachShoot:
                ShootText.SetActive(true);
                C.enabled = true;
                //Wait for player to destroy all Ateroids
                break;
            case State.TeachAbility:
                ShootText.SetActive(false);
                AbilityText.SetActive(true);
                //Wait for player to activate ability
                break;
            case State.SpawnBots:
                Instantiate(DmgBots, DmgBots.transform.position = new Vector3(T_Spawner.position.x, T_Spawner.position.y + 7, T_Spawner.position.z), T_Spawner.transform.rotation);
                
                break;
            case State.WaitForBots:
                
                break;
        }
    }

    void HoldA_D()
    {
        input = Input.GetAxis("Horizontal");
        if(input >= .5f)
        {
            right += Time.deltaTime;
        }
        else if (input <= -.5f)
        {
            left += Time.deltaTime;
        }
        if(left >= .5f && right >= .5f)
        {
            TutorialState = State.Spawn_Asteriods;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        TutorialState = State.TeachMove;
    }
}
