using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] public float health;
    [SerializeField] public float stamina;
    [SerializeField] public float damage;
    [SerializeField] public float maxHp;
    [SerializeField] public float maxStamina;
    public bool alive = true;

    public bool shieldActive;
    [SerializeField] public float shield;

    [SerializeField] int hp_up;
    [SerializeField] int stam_up;
    [SerializeField] int dmg_up;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageClip;
    [SerializeField] private float damageSoundCooldown = 0.5f;
    private float damageSoundCooldownTimer = 0f;

    [SerializeField] public float pollutionAmount;

    [SerializeField] Equipment armor, bullet;

    private void Start()
    {
        health = 100f;
        maxHp = 100f;
        stamina = 100f;
        maxStamina = 100f;
        damage = 10f;

        shieldActive = false;
        shield = 0;

        hp_up = 0;
        stam_up = 0;
        dmg_up = 0;
    }

    

    public void HealthUP()
    {
        hp_up++;
        health = 100f + hp_up * 10f;
        maxHp = health;
    }

    public void StaminaUP()
    {
        stam_up++;
        stamina = 100f + stam_up * 10f;
        maxStamina = stamina;
    }

    public void DamageUP()
    {
        dmg_up++;
        damage = 10f + dmg_up;
    }

    private void Update()
    {
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        if (pollutionAmount >0f)
        pollutionAmount -= Time.deltaTime * 2f;

        // Update the cooldown timer
        if (damageSoundCooldownTimer > 0)
        {
            damageSoundCooldownTimer -= Time.deltaTime;
        }
    }

    public void LoadData(GameData data)
    {
        this.health = data.health;
        this.transform.position = data.playerPos;

        this.hp_up = data.hp_up;
        this.dmg_up = data.dmg_up;
        this.stam_up = data.stam_up;

        this.shield = data.shield;
        this.shieldActive = data.shieldActive;


        this.stamina = 100f + this.stam_up * 10f;
        this.maxStamina = this.stamina;

        this.maxHp = 100f + this.hp_up * 10f;

        this.damage = 1f + this.dmg_up;

    }

    public void SaveData(ref GameData data)
    {
        data.health = this.health;
        data.playerPos = this.transform.position;
        data.shield = this.shield;
        data.shieldActive = this.shieldActive;

        data.hp_up =this.hp_up;
        data.dmg_up = this.dmg_up;
        data.stam_up = this.stam_up;
    }

    public void TakeDamage(float dmg)
    {
        if (shield > 0 && shieldActive)
        {
            shield -= dmg;
            if (shield <= 0)
            {
                shield = 0;
                shieldActive = false;
                armor.itemCount--;
            }
        }
        else health -= dmg;


        if (damageSoundCooldownTimer <= 0f)
        {
            audioSource.PlayOneShot(damageClip);
            damageSoundCooldownTimer = damageSoundCooldown; // Reset the cooldown timer
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health> maxHp)
        {
            health = maxHp;
        }
    }

    public void Shield()
    {
        shieldActive = true;
        if (shield <= 0)
        {
            shield += 40;
        }
    }
    public void ShieldOff()
    {
        shieldActive = false;
    }


    public void Die()
    {
        // TODO: death logic, switch scenes, i.e
        alive = false;
        this.gameObject.SetActive(false);
        Debug.Log("Player has died");
    }
}