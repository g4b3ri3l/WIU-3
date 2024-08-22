using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public float health;
    public float stamina;
    public float damage;
    public int hp_up;
    public int stam_up;
    public int dmg_up;
    public float shield;
    public bool shieldActive;
    public Vector3 playerPos;
    public List<Equipment> equipment;
    public string levelname;
    public bool level1, level2, level3; 
    public GameData()
    {

        this.health = 100;
        this.stamina = 100;
            this.damage = 100;
            this.hp_up = 0;
        this.stam_up = 0;
        this.dmg_up = 0;
        this.shield = 0;
        this.shieldActive = false;
        level1 = level2 = level3 = false;
        this.levelname = null;
        this.playerPos = Vector3.zero;
        this.equipment = new List<Equipment>();
    }
}
