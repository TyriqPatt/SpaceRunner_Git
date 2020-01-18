using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public GameObject MoveText, ShootText, AbilityText, BotText;
    public GameObject Tutorial_Asteroids, DmgBots;
    public GameObject[] LastAsteriods;
    public GameObject[] Enemies;
    public Transform T_Spawner;
    public enum State {StartState, TeachMove, Spawn_Asteriods, TeachShoot, WaitForAsteroids, TeachAbility, SpawnBots, WaitForBots}
    public State TutorialState;
    float input;
    public Canons C;
    float left = 0;
    float right = 0;
    Asteroids A;
    public PlayerHealth PH;

    // Start is called before the first frame update
    void Start()
    {
        //C.enabled = false;
        StartCoroutine(Wait());
        PH.ToggleAbility_Turret();
        PH.ToggleAbility_Missile();
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
                GameObject Temp;
                Temp = Instantiate(Tutorial_Asteroids, DmgBots.transform.position = new Vector3(T_Spawner.position.x, T_Spawner.position.y, T_Spawner.position.z + 400), T_Spawner.transform.rotation);
                A = Temp.GetComponent<Asteroids>();
                Destroy(Temp, 19);
                TutorialState = State.TeachShoot;
                break;
           case State.TeachShoot:
                ShootText.SetActive(true);
                C.enabled = true;
                TutorialState = State.WaitForAsteroids;
                break;
            case State.WaitForAsteroids:
                LastAsteriods = GameObject.FindGameObjectsWithTag("Asteroid");
                float f = LastAsteriods.Length;
                if (f == 0)
                {
                    TutorialState = State.TeachAbility;
                }
                break;
            case State.TeachAbility:
                ShootText.SetActive(false);
                AbilityText.SetActive(true);
                WaitForRoll();
                //Wait for player to activate ability
                break;
            case State.SpawnBots:
                AbilityText.SetActive(false);
                GameObject TempBots;
                TempBots = Instantiate(DmgBots, DmgBots.transform.position = new Vector3(T_Spawner.position.x, T_Spawner.position.y + 5, T_Spawner.position.z), T_Spawner.transform.rotation);
                TempBots.GetComponent<GroupManager>().Tutorial = true;
                BotText.SetActive(true);
                TutorialState = State.WaitForBots;
                break;
            case State.WaitForBots:
                Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                float fe = Enemies.Length;
                if (fe == 0)
                {
                    BotText.SetActive(false);
                    //Complete Tutorial, Send to next level
                }
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

    void WaitForRoll()
    {
        input = Input.GetAxis("Horizontal");
        //left roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && input < 0 && PH.CurRollCdwn == PH.MaxRollCdwn)
        {
            TutorialState = State.SpawnBots;
        }
        //right roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && input > 0 && PH.CurRollCdwn == PH.MaxRollCdwn)
        {
            TutorialState = State.SpawnBots;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        TutorialState = State.TeachMove;
    }
}
