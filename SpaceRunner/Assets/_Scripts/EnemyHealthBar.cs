﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    //Current health value
    public float CurrentHealth;
    //Max health value can be set in inspector or from another script
    public float MaxHealth;
    //Slider is set in start method
    public Slider HealthSlider;
    public GameObject hitDet;
    public bool Boss;
    public bool Pillar;
    public bool invulnerable;
    public float Laser_DMG;
    public float Turret_DMG;
    public float Missile_DMG;
    public GameObject Explode;
    public GameObject[] PickUps;
    public bool isTutorial;
    public bool hit;
    public float hitTime = 2;
    public bool shielded;

    // Use this for initialization
    void Start () {

        //Sets current health to max health
        CurrentHealth = MaxHealth;
        //Sets slider in child object to healthslider
        if (!Boss)
        {
            HealthSlider = transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        }
        //Updates health bar to max health
        HealthSlider.value = CalculatedHealth();
        
	}
	
	// Update is called once per frame
	void Update () {

        //For testing
        if (hit)
        {
            hitTime -= Time.deltaTime;
            if (hitTime <= 0)
            {
                hit = false;
            }
        }
	}

    //Call this method when enemy takes damage
    public void DealDamage(float DamageValue)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth -= DamageValue;
            HealthSlider.value = CalculatedHealth();
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        
    }

    //This method is used to update the health bar
    float CalculatedHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    //A death method to call death animation, particles, or destroy
    void Die()
    {
        CurrentHealth = 0;
        if (!Pillar)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
        GameObject tempObj;
        tempObj = Instantiate(Explode, transform.position, transform.rotation);
        if (!isTutorial)
        {
            int randSpawn = Random.Range(0, 5);
            int randPu = Random.Range(0, PickUps.Length);
            if (randSpawn <= 5)
            {
                Instantiate(PickUps[randPu], transform.position, transform.parent.rotation);
            }
        }
        Destroy(tempObj, 3);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "KillBox")
        {
            Destroy(gameObject.transform.parent);
        }
        if (other.gameObject.tag == "Shootable" && !invulnerable)
        {
            DealDamage(Laser_DMG * PlayerHealth.DmgMultiplier);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "TurretShot" && !invulnerable)
        {
            DealDamage(Turret_DMG * PlayerHealth.DmgMultiplier);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "Missile" && !invulnerable)
        {
            
            DealDamage(Missile_DMG);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "SupportBox")
        {
            //shielded = true;
            Laser_DMG = Laser_DMG / 2;
            Turret_DMG = Turret_DMG / 2;
            Missile_DMG = Missile_DMG / 2;
            if (!Boss)
            {
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SupportBox")
        {
            //shielded = false;
            Laser_DMG = Laser_DMG * 2;
            Turret_DMG = Turret_DMG * 2;
            Missile_DMG = Missile_DMG * 2;
            if (!Boss)
            {
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);

            }
        }
    }

    IEnumerator Hit()
    {
        hitDet.SetActive(true);
        hit = true;
        hitTime = 2;
        yield return new WaitForSeconds(.1f);
        hitDet.SetActive(false);

    }
}
