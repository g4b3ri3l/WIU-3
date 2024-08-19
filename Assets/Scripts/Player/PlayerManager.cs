using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] private float health;
    [SerializeField] AudioSource DamageAudioSource;

    private void Start()
    {
        health = 100f;
    }

    private void Update()
    {
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
    }

    public void LoadData(GameData data)
    {
        this.health = data.health;
        this.transform.position = data.playerPos;
    }

    public void SaveData(ref GameData data)
    {
        data.health = this.health;
        data.playerPos = this.transform.position;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        DamageAudioSource.Play();
    }

    public void Die()
    {
        // TODO: death logic, switch scenes, i.e
        this.gameObject.SetActive(false);
        Debug.Log("Player has died");
    }
}