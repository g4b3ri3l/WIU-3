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
        //this.playerPos = new Vector3(-106.5f, -60.1f, 0f);
        if (SceneManager.GetActiveScene().name == "level1 1")
        {

            this.playerPos = new Vector3(-122.66f, -54.59f, 0f);
        }
        if (SceneManager.GetActiveScene().name == "level2 2")
        {
            this.playerPos = new Vector3(-110.4f, -58.2f, 0f);

        }
        this.equipment = new List<Equipment>();
    }
}
