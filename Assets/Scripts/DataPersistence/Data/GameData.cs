using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float health;
    public float stamina;
    public float damage;
    public int hp_up;
    public int stam_up;
    public int dmg_up;
    public Vector3 playerPos;
    public List<Item> items;
    public List<Equipment> equipment;

    public GameData()
    {
        this.health = 100;
        this.stamina = 100;
        this.damage = 100;
        this.hp_up = 0;
        this.stam_up = 0;
        this.dmg_up = 0;
        this.playerPos = new Vector3(-106.5f, -60.1f, 0f);
        this.items = new List<Item>();
        this.equipment = new List<Equipment>();
    }
}
