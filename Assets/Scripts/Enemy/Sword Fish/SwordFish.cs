using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SwordFish : Enemy
{
    private string Name;
    private float damage;
    private float maxHealth;
    private float currHealth;

    [SerializeField] GameObject[] itemdrops;
    [SerializeField] private EnemyHealthBar HealthBar;
    private float fixedRotation = 0;
    [SerializeField] Transform player;
    [SerializeField] private EnemyStats swordFishStats;

    [SerializeField] AudioSource audioSource;  // Reference to the AudioSource component
    [SerializeField] AudioClip StabClip;

    protected override void Awake()
    {
        HealthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    protected override void Start()
    {
        Name = swordFishStats.enemyName;
        damage = swordFishStats.damage;
        maxHealth = swordFishStats.maxHealth;
        currHealth = maxHealth;
    }

    protected override void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(fixedRotation, fixedRotation, eulerAngles.z);
    }

    public override void TakeDamage(float amount)
    {
        // Implementation for SwordFish
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log($"{swordFishStats.enemyName} has died!");
        for (int i = 0; i < itemdrops.Length; i++)
        {
            var item = Instantiate(itemdrops[i], transform.position, Quaternion.identity);
            item.GetComponent<Interactable>().player = player;
        }
        Destroy(gameObject);  // Default death behavior
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(StabClip);
            PlayerManager player = collision.GetComponent<PlayerManager>();
            player.TakeDamage(damage);
        }
    }
}

   

