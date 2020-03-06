using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameObject meteor;
    public GameObject meteorspawn;
    public GameObject bullet;
    public GameObject[] bulletSpawn;
    float ShootDelay;
    public float StartDelay = 5;
    public float MeteorDelay = 5;
    public float MaxAmmo = 3;
    float Ammo;
    Vector3 movepos;

    float Dir = 5;


    // Start is called before the first frame update
    void Start()
    {
        ShootDelay = StartDelay;
        Ammo = MaxAmmo;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (ShootDelay > 0)
        {
            ShootDelay -= Time.deltaTime;
        }
        if (ShootDelay <= 0)
        {
            StartCoroutine(shoot());
            ShootDelay = StartDelay;
            Ammo = MaxAmmo;
        }

    }

    IEnumerator shoot()
    {
        GameObject bulletprefab;
        yield return new WaitForSeconds(.2f);
        bulletprefab = Instantiate(bullet, bulletSpawn[0].transform.position, bulletSpawn[0].transform.rotation);
        bulletprefab = Instantiate(bullet, bulletSpawn[1].transform.position, bulletSpawn[1].transform.rotation);
        bulletprefab = Instantiate(bullet, bulletSpawn[2].transform.position, bulletSpawn[2].transform.rotation);
        bulletprefab = Instantiate(bullet, bulletSpawn[3].transform.position, bulletSpawn[3].transform.rotation);
        bulletprefab = Instantiate(bullet, bulletSpawn[4].transform.position, bulletSpawn[4].transform.rotation);
        //bulletprefab.transform.parent = transform.parent;
        StartCoroutine(shootM());
        if (Ammo > 0)
        {
            if (Ammo > 1)
            {
                StartCoroutine(shoot());
                
            }
            Ammo -= 1;
        }
    }

    IEnumerator shootM()
    {
        GameObject bulletprefab;
        yield return new WaitForSeconds(MeteorDelay);
        bulletprefab = Instantiate(meteor, meteorspawn.transform.position, meteor.transform.rotation);

    }
}
