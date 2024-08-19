using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerData : MonoBehaviour, IDataPersistance
{
    [SerializeField] private float health;
    [SerializeField] private float stamina;
    [SerializeField] private float damage;

    [SerializeField] int hp_up;
    [SerializeField] int stam_up;
    [SerializeField] int dmg_up;

    private void Start()
    {
        health = 100f;
        stamina = 100f;
        damage = 1f;

        hp_up = 0;
        stam_up = 0;
        dmg_up = 0;
    }

    public void HealthUP()
    {
        hp_up++;
        health = 100f + hp_up * 10f;
    }

    public void StaminaUP()
    {
        stam_up++;
        stamina = 100f + stam_up * 10f;
    }

    public void DamageUP()
    {
        dmg_up++;
        damage = 100f + dmg_up * 10f;
    }

    public void LoadData(GameData data)
    {
        this.health = data.health;
        this.transform.position = data.playerPos;

        this.stamina = data.stamina;
        this.damage = data.damage;
    }

    public void SaveData(ref GameData data)
    {
        data.health = this.health;
        data.playerPos = this.transform.position;
    }
}