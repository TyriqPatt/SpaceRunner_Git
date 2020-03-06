using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarFunctionality : MonoBehaviour
{

    public GameObject[] Sections;
    public GameObject[] HealthBars;
    Vector3 smoothpos;
    float Dir = 5;
    public float speed;
    public float ExpandSpeed;
    public float TilNextExpand;
    float delay;
    public enum State {Positioning, SetAllActive, Expandp1, Expandp2, Connect1, Connect2, TurnOnLasers, MoveRight, MoveLeft, ChooseDir }

    public State PillarState;

    // Start is called before the first frame update
    void Start()
    {
        PillarState = State.SetAllActive;
        smoothpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PillarController();
        transform.position = smoothpos;
    }

    void PillarController()
    {
        switch (PillarState)
        {
            case State.Positioning:
                //Move from Droid boss to V3(0,10,50)
                break;

            case State.SetAllActive:
                Sections[1].SetActive(true);
                Sections[2].SetActive(true);
                Sections[3].SetActive(true);
                Sections[4].SetActive(true);
                PillarState = State.Expandp1;
                break;
            case State.Expandp1:
                if (Sections[1].transform.localPosition.y >= -2 && Sections[2].transform.localPosition.y >= -2)
                {
                    Sections[1].transform.localPosition -= transform.right * ExpandSpeed;
                    Sections[2].transform.localPosition -= transform.right * ExpandSpeed;
                }
                if (Sections[3].transform.localPosition.y <= 2 && Sections[4].transform.localPosition.y <= 2)
                {
                    Sections[3].transform.localPosition += transform.right * ExpandSpeed;
                    Sections[4].transform.localPosition += transform.right * ExpandSpeed;
                }
                else if(Sections[1].transform.localPosition.y <= -2 && Sections[2].transform.localPosition.y <= -2 && Sections[3].transform.localPosition.y >= 2 && Sections[4].transform.localPosition.y >= 2)
                {
                    PillarState = State.Connect1;
                }
                break;
            case State.Connect1:
                Sections[1].transform.localPosition = new Vector3(0, -2, 0);
                Sections[2].transform.localPosition = new Vector3(0, -2, 0);
                Sections[3].transform.localPosition = new Vector3(0, 2, 0);
                Sections[4].transform.localPosition = new Vector3(0, 2, 0);
                
                if(delay < TilNextExpand)
                {
                    delay += Time.deltaTime;
                }
                if(delay >= TilNextExpand)
                {
                    PillarState = State.Expandp2;
                    delay = 0;
                }
                break;
            case State.Expandp2:
                //rotate 1 & 3 up
                Sections[1].transform.Rotate(0, -2.14285f, 0);
                Sections[3].transform.Rotate(0, -2.14285f, 0);
                if (Sections[2].transform.localPosition.y >= -4)
                {
                    Sections[2].transform.localPosition -= transform.right * ExpandSpeed;
                }
                if (Sections[4].transform.localPosition.y <= 4)
                {
                    Sections[4].transform.localPosition += transform.right * ExpandSpeed;
                }
                else if (Sections[2].transform.localPosition.y <= -4 && Sections[4].transform.localPosition.y >= 4)
                {
                    PillarState = State.Connect2;
                }
                break;
            case State.Connect2:
                Sections[2].transform.localPosition = new Vector3(0, -4, 0);
                Sections[4].transform.localPosition = new Vector3(0, 4, 0);
                if (delay < TilNextExpand)
                {
                    delay += Time.deltaTime;
                }
                if (delay >= TilNextExpand)
                {
                    EnableHealth();
                    StartSpin();
                    EnableLaser();
                    PillarState = State.ChooseDir;
                    delay = 0;
                }
                break;
            case State.ChooseDir:
                float Randnum;
                Randnum = Random.Range(0, 2);
                if (Randnum == 0)
                {
                    PillarState = State.MoveRight;
                }
                else if (Randnum == 1)
                {
                    PillarState = State.MoveLeft;
                }
                break;
            case State.MoveRight:
                DisableHealthBars();
                if (transform.position.x >= 50)
                {
                    PillarState = State.MoveLeft;
                }
                else
                {
                    smoothpos = Vector3.Lerp(transform.position,
                        transform.position = new Vector3(transform.position.x + Dir,
                        transform.position.y, transform.position.z), speed * Time.deltaTime);
                }
                break;
            case State.MoveLeft:
                DisableHealthBars();
                if (transform.position.x <= -50)
                {
                    PillarState = State.MoveRight;
                }
                else
                {
                    smoothpos = Vector3.Lerp(transform.position,
                        transform.position = new Vector3(transform.position.x - Dir,
                        transform.position.y, transform.position.z), speed * Time.deltaTime);
                }
                break;

        }
    }

    void EnableHealth()
    {
        HealthBars[0].transform.localPosition = new Vector3(Sections[0].transform.localPosition.x + 1.5f, Sections[0].transform.localPosition.y, Sections[0].transform.localPosition.z);
        HealthBars[1].transform.localPosition = new Vector3(Sections[1].transform.localPosition.x + 1.5f, Sections[1].transform.localPosition.y, Sections[1].transform.localPosition.z);
        HealthBars[2].transform.localPosition = new Vector3(Sections[2].transform.localPosition.x + 1.5f, Sections[2].transform.localPosition.y, Sections[2].transform.localPosition.z);
        HealthBars[3].transform.localPosition = new Vector3(Sections[3].transform.localPosition.x + 1.5f, Sections[3].transform.localPosition.y, Sections[3].transform.localPosition.z);
        HealthBars[4].transform.localPosition = new Vector3(Sections[4].transform.localPosition.x + 1.5f, Sections[4].transform.localPosition.y, Sections[4].transform.localPosition.z);

        HealthBars[0].SetActive(true);
        HealthBars[1].SetActive(true);
        HealthBars[2].SetActive(true);
        HealthBars[3].SetActive(true);
        HealthBars[4].SetActive(true);
    }

    void StartSpin()
    {
        Sections[0].GetComponent<Rotator>().enabled = true;
        Sections[1].GetComponent<Rotator>().enabled = true;
        Sections[2].GetComponent<Rotator>().enabled = true;
        Sections[3].GetComponent<Rotator>().enabled = true;
        Sections[4].GetComponent<Rotator>().enabled = true;
    }

    void EnableLaser()
    {
        Sections[0].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Sections[0].transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        Sections[1].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Sections[1].transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        Sections[2].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Sections[2].transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        Sections[3].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Sections[3].transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        Sections[4].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Sections[4].transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        
    }

    void DisableHealthBars()
    {
        if(Sections[0] == null)
        {
            HealthBars[0].SetActive(false);
        }

        if (Sections[1] == null)
        {
            HealthBars[1].SetActive(false);
        }

        if (Sections[2] == null)
        {
            HealthBars[2].SetActive(false);
        }

        if (Sections[3] == null)
        {
            HealthBars[3].SetActive(false);
        }

        if (Sections[4] == null)
        {
            HealthBars[4].SetActive(false);
        }

        if(Sections[0] == null && Sections[1] == null && Sections[2] == null && Sections[3] == null && Sections[4] == null)
        {
            Destroy(gameObject, 2); 
        }
    }
}
