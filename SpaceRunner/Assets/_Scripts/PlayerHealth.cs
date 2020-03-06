using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    }

    private void Update()
    {
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
    }

    public void Heal(float HealValue)
    {
        CurrentHealth += HealValue;
        HealthSlider.value = CalculatedHealth();
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    void Die()
    {
        CurrentHealth = 0;
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
        if (other.gameObject.tag == "Curvelaser")
        {
            DealDamage(Curve_DMG);
        }
        if (other.gameObject.tag == "SpaceMine")
        {
            DealDamage(Mine_Dmg);
        }
        if (other.gameObject.tag == "Stun")
        {
            StartCoroutine(DOT(3));
        }
        if (other.gameObject.tag == "Asteroid")
        {
            DealDamage(Asteroid_DMG);
        }
        if (other.gameObject.tag == "G_Ball")
        {
            transform.parent.GetComponent<Flight_Controller>()._speed = .5f;
            transform.parent.GetComponent<Flight_Controller>()._RollSpeed = 4f;
            other.transform.parent.GetComponent<ShotBehavior>().speed = 20;
        }
        if (other.gameObject.tag == "Pickup")
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

        if (other.gameObject.tag == "G_Ball")
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
}
