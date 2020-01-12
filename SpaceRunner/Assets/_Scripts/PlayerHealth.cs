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
    public float MaxMissileCdwn;
    public float CurMissileCdwn;
    public Slider MissileSlider;
    public bool invulnerable;
    public float Laser_DMG;
    public float Curve_DMG;
    public float Asteroid_DMG;
    public float Beam_DMG;
    public float Mine_Dmg;
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
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "Curvelaser")
        {
            DealDamage(Curve_DMG);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "SpaceMine")
        {
            DealDamage(Mine_Dmg);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "Stun")
        {
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "Asteroid")
        {
            DealDamage(Asteroid_DMG);
            StartCoroutine(Hit());
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
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        hitDet.SetActive(true);
        yield return new WaitForSeconds(.15f);
        hitDet.SetActive(false);

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
}
