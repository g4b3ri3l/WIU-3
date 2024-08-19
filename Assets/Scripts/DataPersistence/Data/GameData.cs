using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float health;
    public Vector3 playerPos;
    public List<Item> items;
    public List<Equipment> equipment;

    public GameData()
    {
        this.health = 100;
        this.playerPos = new Vector3(-106.5f, -60.1f, 0f);
        this.items = new List<Item>();
        this.equipment = new List<Equipment>();
    }
}
