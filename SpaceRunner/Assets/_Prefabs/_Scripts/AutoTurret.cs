using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public float Firerate = 7f;
    public float nextTimeToFire = 0f;
    public Transform ShootPoint;
    public GameObject TurretLaser;
    public bool start;
    public Transform target;
    public float smoothspeed = .125f;
    public Vector3 offset;
    public GameObject[] Asteroids;
    public List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(startShooting());
    }

    // Update is called once per frame
    void Update()
    {
        if (start && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / Firerate;
            shoot();
        }
        if (start)
        {
            
        }
    }


    void shoot()
    {
        Instantiate(TurretLaser, ShootPoint.position, ShootPoint.rotation);
        
        //Leftps.Play();
    }

    IEnumerator startShooting()
    {
        yield return new WaitForSeconds(2f);
        start = true;
    }

    private void OnDisable()
    {
        start = false;
    }
}
