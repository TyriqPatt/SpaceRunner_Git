using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flight_Controller : MonoBehaviour
{

  
    public float clampAngle = .5f;
    public float clamppos = 50f;
    public float rotspeed;
    public float _speed;
    public float rotResetSpeed;
    public float input;
    public float DisruptLength = 1.3f;
    public GameObject Child;
    public GameObject Thruster;
    public GameObject Electricity;
    Quaternion rotValues;
    bool L_roll;
    bool R_roll;
    [HideInInspector] public bool isDisrupted;
    float Dashspeed = 10.0F;
    float Rollspeed = 11.0F;
    [HideInInspector] public int States;
    public Image RollBG;
    public Image AllyBG;
    public Image BarrageBG;
    public GameObject Turret;
    public GameObject Missile;
    public Transform MissileSpawn;
    int A_States;
    public PlayerHealth PH;
    public enum State { Roll, Turret, Missile }

    public State AbilityState;
    public bool isTutorial;

    // Start is called before the first frame update
    void Start()
    {
        PH = transform.GetChild(1).GetComponent<PlayerHealth>();
        Turret.SetActive(false);
        RollBG.enabled = true;
        AllyBG.enabled = false;
        BarrageBG.enabled = false;
        AbilityState = State.Roll;
    }

    // Update is called once per frame
    void Update()
    {
        switch (States)
        {
            case 4:
                //Disrupt left
                Child.transform.Rotate(0.0f, 0.0f, -1 * rotspeed);
                transform.Translate(1 * _speed, 0.0f, 0.0f);
                if (Child.transform.rotation.z <= -clampAngle)
                {
                    Child.transform.rotation = new Quaternion(0, 0, -clampAngle, Child.transform.rotation.w);
                }
                break;

            case 3:
                //Disrupt right
                Child.transform.Rotate(0.0f, 0.0f, 1 * rotspeed);
                transform.Translate(-1 * _speed, 0.0f, 0.0f);
               
                if (Child.transform.rotation.z >= clampAngle)
                {
                    Child.transform.rotation = new Quaternion(0, 0, clampAngle, Child.transform.rotation.w);
                }
                break;
                
            case 2:
                //right roll
                Vector3 Rmove = Vector3.Lerp(transform.position, transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), Dashspeed * Time.deltaTime);
                transform.position = Rmove;
                Child.transform.Rotate(0.0f, 0.0f, -Rollspeed);
                break;

            case 1:
                //left roll
                Vector3 Lmove = Vector3.Lerp(transform.position, transform.position = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), Dashspeed * Time.deltaTime);
                transform.position = Lmove;
                Child.transform.Rotate(0.0f, 0.0f, Rollspeed);
                break;

            default:
                //Movement inputs
                transform.Translate(Input.GetAxis("Horizontal") * _speed, 0.0f, 0.0f);
                Child.transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * rotspeed);
                input = Input.GetAxis("Horizontal");

                if(input != 0)
                {
                    //Restricts z rot to max left
                    if (Child.transform.rotation.z >= clampAngle)
                    {
                        Child.transform.rotation = new Quaternion(0, 0, clampAngle, Child.transform.rotation.w);
                    }

                    //Restricts z rot to max right
                    if (Child.transform.rotation.z <= -clampAngle)
                    {
                        Child.transform.rotation = new Quaternion(0, 0, -clampAngle, Child.transform.rotation.w);
                    }
                }
                else
                {
                    //Resets rot back to normal after turning left
                    if (Child.transform.rotation.z > 0)
                    {
                        Child.transform.Rotate(0.0f, 0.0f, -rotResetSpeed);
                        if (Child.transform.rotation.z < .05 && Child.transform.rotation.z > 0)
                        {
                            Child.transform.rotation = Quaternion.identity;
                        }
                    }
                    //Resets rot back to normal after turning right
                    if (Child.transform.rotation.z < 0)
                    {

                        Child.transform.Rotate(0.0f, 0.0f, rotResetSpeed);
                        if (Child.transform.rotation.z > -0.05f && Child.transform.rotation.z < 0)
                        {
                            Child.transform.rotation = Quaternion.identity;
                        }
                    }
                }
                break;
        }

        if(!isDisrupted)
        {
            switch (AbilityState)
            {
                case State.Roll:
                    //left roll
                    if (Input.GetKeyDown(KeyCode.LeftShift) && input < 0 && PH.CurRollCdwn == PH.MaxRollCdwn)
                    {
                        States = 1;
                        StartCoroutine(Barrel_Roll());
                        PH.CurRollCdwn -= PH.CurRollCdwn;
                        PH.RollSlider.value = PH.RollCooldown();
                    }
                    //right roll
                    if (Input.GetKeyDown(KeyCode.LeftShift) && input > 0 && PH.CurRollCdwn == PH.MaxRollCdwn)
                    {
                        States = 2;
                        StartCoroutine(Barrel_Roll());
                        PH.CurRollCdwn -= PH.CurRollCdwn;
                        PH.RollSlider.value = PH.RollCooldown();
                    }
                    break;
                case State.Turret:
                    if (Input.GetKeyDown(KeyCode.LeftShift) && PH.CurTurretCdwn == PH.MaxTurretCdwn)
                    {
                        Turret.SetActive(false);
                        StartCoroutine(ActivateTurret());
                    }
                    break;
                case State.Missile:
                    if (Input.GetKeyDown(KeyCode.LeftShift) && PH.CurMissileCdwn == PH.MaxMissileCdwn)
                    {
                        StartCoroutine(Missiles());
                        PH.CurMissileCdwn -= PH.MaxMissileCdwn;
                        PH.MissileSlider.value = PH.MissileCooldown();
                    }
                    break;
            }

            switch (A_States)
            {
                case 1:
                    //Ally Selected
                    AbilityState = State.Turret;
                    RollBG.enabled = false;
                    AllyBG.enabled = true;
                    BarrageBG.enabled = false;
                    break;
                case 2:
                    //Barrage Selected
                    AbilityState = State.Missile;
                    RollBG.enabled = false;
                    AllyBG.enabled = false;
                    BarrageBG.enabled = true;
                    break;

                default:
                    //Roll Selected
                    AbilityState = State.Roll;
                    RollBG.enabled = true;
                    AllyBG.enabled = false;
                    BarrageBG.enabled = false;
                    break;
            }
            if (!isTutorial)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    A_States += 1;
                    if (A_States > 2)
                    {
                        A_States = 0;
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    A_States -= 1;
                    if (A_States < 0)
                    {
                        A_States = 2;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    A_States = 0;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    A_States = 1;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    A_States = 2;
                }
            }
        }

        //Boundaries
        if (transform.position.x >= clamppos)
        {
            transform.position = new Vector3(clamppos, 5, 0);
        }

        if (transform.position.x <= -clamppos)
        {
            transform.position = new Vector3(-clamppos, 5, 0);
        }
    }

    IEnumerator Missiles()
    {
        Instantiate(Missile, MissileSpawn.transform.position, MissileSpawn.transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(Missile, MissileSpawn.transform.position, MissileSpawn.transform.rotation);
    }

    IEnumerator Barrel_Roll()
    {
        yield return new WaitForSeconds(.5f);
        States = 0;
        Child.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    IEnumerator ActivateTurret()
    {
        PH.CurTurretCdwn -= PH.MaxTurretCdwn;
        PH.TurretSlider.value = PH.TurretCooldown();
        PH.TurretFill.SetActive(false);
        Turret.SetActive(true);
        yield return new WaitForSeconds(10f);
        Turret.SetActive(false);
        PH.TurretFill.SetActive(true);
        PH.StartCdwn = true;
        
    }

    //Disrupt enumerator, seeds players into disrupt state
    public IEnumerator Disrupted()
    {
        float Randnum;
        Randnum = Random.Range(0, 2);
        if (Randnum == 0)
        {
            States = 3;
        }
        else if (Randnum == 1)
        {
            States = 4;
        }
        isDisrupted = true;
        Thruster.SetActive(false);
        Electricity.SetActive(true);
        yield return new WaitForSeconds(DisruptLength);
        States = 0;
        isDisrupted = false;
        Thruster.SetActive(true);
        Electricity.SetActive(false);
    }
}
