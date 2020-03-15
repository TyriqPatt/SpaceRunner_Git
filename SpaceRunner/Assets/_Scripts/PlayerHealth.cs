using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public Slider HealthSlider;
    public float MaxRollCdwn;
    public float CurRollCdwn;
    public Slider RollSlider;
    public float MaxTurretCdwn;
    public float CurTurretCdwn;
    public Slider TurretSlider;
    public GameObject TurretFill;
    public GameObject Turret;
    public GameObject TurretBar;
    public float MaxMissileCdwn;
    public float CurMissileCdwn;
    public Slider MissileSlider;
    public GameObject Missile;
    public GameObject MissileBar;
    public float Laser_DMG;
    public float Curve_DMG;
    public float DOT_DMG;
    public float Asteroid_DMG;
    public float Beam_DMG;
    public float Mine_Dmg;
    public float Gball_Dmg;
    public float HealAmount;
    public GameObject hitDet;
    public GameObject healthDet;
    public GameObject ShieldDet;
    public GameObject AttackDet;
    public GameObject EnergyDet;
    public static float DmgMultiplier = 1;
    public bool StartCdwn;
    public LowHealthIndicator[] LowHp;
    Flight_Controller fc;
    public GameObject PlayerExplosions;
    public AudioSource thrusterExplosion;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthSlider.value = CalculatedHealth();

        CurRollCdwn = MaxRollCdwn;
        RollSlider.value = RollCooldown();

        CurTurretCdwn = MaxTurretCdwn;
        TurretSlider.value = TurretCooldown();

        CurMissileCdwn = MaxMissileCdwn;
        MissileSlider.value = MissileCooldown();

        fc = GetComponentInParent<Flight_Controller>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DealDamage(100);
        }

        if (CurRollCdwn <= MaxRollCdwn)
        {
            CurRollCdwn += Time.deltaTime;
            RollSlider.value = RollCooldown();
            if (CurRollCdwn >= MaxRollCdwn)
            {
                CurRollCdwn = MaxRollCdwn;
                RollSlider.value = RollCooldown();
            }
        }
        if (CurTurretCdwn <= MaxTurretCdwn && StartCdwn)
        {
            CurTurretCdwn += Time.deltaTime;
            TurretSlider.value = TurretCooldown();
            if (CurTurretCdwn >= MaxTurretCdwn)
            {
                CurTurretCdwn = MaxTurretCdwn;
                TurretSlider.value = TurretCooldown();
                StartCdwn = false;
            }
        }
        if (CurMissileCdwn <= MaxMissileCdwn)
        {
            CurMissileCdwn += Time.deltaTime;
            MissileSlider.value = MissileCooldown();
            if (CurMissileCdwn >= MaxMissileCdwn)
            {
                CurMissileCdwn = MaxMissileCdwn;
                MissileSlider.value = MissileCooldown();
            }
        }
    }

    public void DealDamage(float DamageValue)
    {
        CurrentHealth -= DamageValue;
        HealthSlider.value = CalculatedHealth();
        StartCoroutine(Hit());
        if (CurrentHealth <= 0)
        {
            Die();
        }
        else if (CurrentHealth <= 25)
        {
            
            for (int i = 0; i < LowHp.Length; i++)
            {
                LowHp[i].enabled = true;
            }
        }
    }

    public void Heal(float HealValue)
    {
        CurrentHealth += HealValue;
        HealthSlider.value = CalculatedHealth();
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (CurrentHealth > 25)
        {
            
            for (int i = 0; i < LowHp.Length; i++)
            {
                LowHp[i].enabled = false;
            }
        }
    }

    void Die()
    {
        CurrentHealth = 0;
        fc.States = 5;
        PlayerExplosions.SetActive(true);
        StartCoroutine(delayexplosion());
    }

    float CalculatedHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    public float RollCooldown()
    {
        return CurRollCdwn / MaxRollCdwn;
    }

    public float TurretCooldown()
    {
        return CurTurretCdwn / MaxTurretCdwn;
    }

    public float MissileCooldown()
    {
        return CurMissileCdwn / MaxMissileCdwn;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyLaser")
        {
            DealDamage(Laser_DMG);
        }
        else if (other.gameObject.tag == "Curvelaser")
        {
            DealDamage(Curve_DMG);
        }
        else if (other.gameObject.tag == "SpaceMine")
        {
            DealDamage(Mine_Dmg);
        }
        else if (other.gameObject.tag == "Stun")
        {
            StartCoroutine(DOT(3));
        }
        else if (other.gameObject.tag == "Asteroid")
        {
            DealDamage(Asteroid_DMG);
        }
        else if (other.gameObject.tag == "G_Ball")
        {
            transform.parent.GetComponent<Flight_Controller>()._speed = .5f;
            transform.parent.GetComponent<Flight_Controller>()._RollSpeed = 4f;
            other.transform.parent.GetComponent<ShotBehavior>().speed = 20;
        }
        else if (other.gameObject.tag == "Pickup")
        {
            PowerUp PU = other.GetComponent<PowerUp>();
            if (PU.AttackPickUp)
            {
                StartCoroutine(AttackPowerUp());
            }
            else if (PU.HealthPickUp)
            {
                StartCoroutine(HealthPowerUp());
            }
            else if (PU.EnergyPickUp)
            {
                StartCoroutine(EnergyPowerUp());
            }
            else if (PU.ShieldPickup)
            {
                StartCoroutine(ShieldsPowerUp());
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            DealDamage(Beam_DMG);
        }
        else if (other.gameObject.tag == "G_Ball")
        {
            DealDamage(Gball_Dmg);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "G_Ball")
        {
            transform.parent.GetComponent<Flight_Controller>()._speed = 1f;
            transform.parent.GetComponent<Flight_Controller>()._RollSpeed = 10f;
            other.transform.parent.GetComponent<ShotBehavior>().speed = 35;
        }
    }

    IEnumerator Hit()
    {
        hitDet.SetActive(true);
        yield return new WaitForSeconds(.15f);
        hitDet.SetActive(false);

    }

    IEnumerator delayexplosion()
    {
        
        yield return new WaitForSeconds(.2f);
        thrusterExplosion.enabled = true;
        thrusterExplosion.Play();
        yield return new WaitForSeconds(.8f);
        thrusterExplosion.enabled = true;
        thrusterExplosion.Play();

    }

    IEnumerator DOT(float TotalTime)
    {
        DealDamage(DOT_DMG);
        yield return new WaitForSeconds(.4f);
        if (TotalTime > 0)
        {
            if (TotalTime > 1)
            {
                StartCoroutine(DOT(TotalTime -= 1));
            }
            
        }

    }

    public IEnumerator HealthPowerUp()
    {
        healthDet.SetActive(true);
        yield return new WaitForSeconds(1f);
        healthDet.SetActive(false);
    }

    public IEnumerator ShieldsPowerUp()
    {
        ShieldDet.SetActive(true);
        yield return new WaitForSeconds(1f);
        ShieldDet.SetActive(false);
    }

    public IEnumerator EnergyPowerUp()
    {
        EnergyDet.SetActive(true);
        yield return new WaitForSeconds(1f);
        EnergyDet.SetActive(false);
    }

    public IEnumerator AttackPowerUp()
    {
        AttackDet.SetActive(true);
        yield return new WaitForSeconds(1f);
        AttackDet.SetActive(false);
    }

    public void ToggleAbility_Turret()
    {
        Turret.SetActive(false);
        TurretBar.SetActive(false);
    }

    public void ToggleAbility_Missile()
    {
        Missile.SetActive(false);
        MissileBar.SetActive(false);
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("WinScene");

    }

    public void CallDelayEnumerator()
    {
        StartCoroutine(Delay()); 
    }
}
