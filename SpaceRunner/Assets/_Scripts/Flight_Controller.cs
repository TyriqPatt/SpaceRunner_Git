using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Flight_Controller : MonoBehaviour
{
    public float clampAngle = .5f;
    public float clamppos = 50f;
    public float rotspeed;
    public float _speed;
    public float _RollSpeed;
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
    public int States;
    public Image RollBG;
    public Image AllyBG;
    public Image BarrageBG;
    public GameObject Turret;
    public GameObject Missile;
    public Transform MissileSpawn;
    public PlayerHealth PH;
    public Canons C;
    public AudioSource rollAudio;

    public bool isTutorial;

    // Start is called before the first frame update
    void Start()
    {
        PH = transform.GetChild(1).GetComponent<PlayerHealth>();
        Turret.SetActive(false);
        C = GetComponent<Canons>();
        C.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (States)
        {
            case 5:
                //Dead
                C.enabled = false;
                isDisrupted = true;
                Child.transform.Rotate(0.0f, 1f, -1 * 2);
                transform.Translate(1 * .1f, -.2f, 0.0f);
                if (Child.transform.rotation.z >= clampAngle)
                {
                    Child.transform.rotation = new Quaternion(0, 0, clampAngle, Child.transform.rotation.w);
                }
                break;

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
                Vector3 Rmove = Vector3.Lerp(transform.position, transform.position = new Vector3(transform.position.x + _RollSpeed, transform.position.y, transform.position.z), Dashspeed * Time.deltaTime);
                transform.position = Rmove;
                Child.transform.Rotate(0.0f, 0.0f, -Rollspeed);
                break;

            case 1:
                //left roll
                Vector3 Lmove = Vector3.Lerp(transform.position, transform.position = new Vector3(transform.position.x - _RollSpeed, transform.position.y, transform.position.z), Dashspeed * Time.deltaTime);
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


        Abilities();
        //Boundaries
        if (transform.position.x >= clamppos && States != 5)
        {
            transform.position = new Vector3(clamppos, 5, 0);
        }
        if (transform.position.x <= -clamppos && States != 5)
        {
            transform.position = new Vector3(-clamppos, 5, 0);
        }
    }

    void Abilities()
    {
        if(Time.timeScale != 0)
        {
            if (!isDisrupted)
            {

                //left roll
                if (Input.GetKeyDown(KeyCode.LeftShift) && input < 0 && PH.CurRollCdwn == PH.MaxRollCdwn)
                {
                    rollAudio.Play(); 
                    StartCoroutine(RollTime());
                    States = 1;
                    StartCoroutine(Barrel_Roll());
                    PH.CurRollCdwn -= PH.CurRollCdwn;
                    PH.RollSlider.value = PH.RollCooldown();
                }
                //right roll
                if (Input.GetKeyDown(KeyCode.LeftShift) && input > 0 && PH.CurRollCdwn == PH.MaxRollCdwn)
                {
                    rollAudio.Play();
                    StartCoroutine(RollTime());
                    States = 2;
                    StartCoroutine(Barrel_Roll());
                    PH.CurRollCdwn -= PH.CurRollCdwn;
                    PH.RollSlider.value = PH.RollCooldown();
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && PH.CurTurretCdwn == PH.MaxTurretCdwn || Input.GetKeyDown(KeyCode.W) && PH.CurTurretCdwn == PH.MaxTurretCdwn)
                {
                    StartCoroutine(turretTime());
                    Turret.SetActive(false);
                    StartCoroutine(ActivateTurret());
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && PH.CurMissileCdwn == PH.MaxMissileCdwn || Input.GetKeyDown(KeyCode.S) && PH.CurMissileCdwn == PH.MaxMissileCdwn)
                {
                    StartCoroutine(MissileTime());
                    StartCoroutine(Missiles());
                    PH.CurMissileCdwn -= PH.MaxMissileCdwn;
                    PH.MissileSlider.value = PH.MissileCooldown();
                }

            }
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
        if (!isDisrupted)
        {
            States = 0;
            Child.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
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
        if(States != 5)
        {
            States = 0;
            isDisrupted = false;
            Thruster.SetActive(true);
            Electricity.SetActive(false);
        }
    }

    IEnumerator RollTime()
    {
        RollBG.enabled = true;
        yield return new WaitForSeconds(.6f);
        RollBG.enabled = false;
    }

    IEnumerator turretTime()
    {
        AllyBG.enabled = true;
        yield return new WaitForSeconds(10);
        AllyBG.enabled = false;
    }

    IEnumerator MissileTime()
    {
        BarrageBG.enabled = true;
        yield return new WaitForSeconds(1);
        BarrageBG.enabled = false;
    }
}
