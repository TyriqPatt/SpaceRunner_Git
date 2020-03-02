using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canons : MonoBehaviour
{

    public GameObject CurLaser, laser, HeavyLaser;
    public Transform rightCanon, LeftCanon;
    public ParticleSystem Leftps, Rightps;
    public float Firerate = 15f;
    public float nextTimeToFire = 0f;
    public float heavyCanonDur;
    Flight_Controller FC;
    public bool HeavyCanons;


    // Start is called before the first frame update
    void Start()
    {
        FC = GetComponent<Flight_Controller>();
        CurLaser = laser;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time >= nextTimeToFire & !FC.isDisrupted)
            {
                nextTimeToFire = Time.time + 1f / Firerate;
                shoot();
            }
        }
        if (HeavyCanons)
        {
            heavyCanonDur += Time.deltaTime;
            if(heavyCanonDur >= 5)
            {
                HeavyCanons = false;
            }
            CurLaser = HeavyLaser;
            Firerate = 2.5f;
            PlayerHealth.DmgMultiplier = 2;
        }
        else
        {
            CurLaser = laser;
            PlayerHealth.DmgMultiplier = 1;
            Firerate = 4f;
            heavyCanonDur = 0;
        }
    }

    void shoot()
    {
        Instantiate(CurLaser, rightCanon.position, rightCanon.rotation);
        Instantiate(CurLaser, LeftCanon.position, LeftCanon.rotation);
        Rightps.Play();
        Leftps.Play();
    }
}
