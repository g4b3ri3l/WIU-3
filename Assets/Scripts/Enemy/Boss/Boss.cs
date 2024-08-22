using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private EnemyStats Stats;  // Reference to ScriptableObject with common enemy data
    private string Name;
    public float health, damage;

    private float fixedRotation = 0;

    [SerializeField] private EnemyHealthBar HealthBar;


    protected override void Awake()
    {
        HealthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    protected override void Start()
    {
        Name = Stats.enemyName;
        health = Stats.maxHealth;
        damage = Stats.damage;

        HealthBar.UpdateHealthBar(health, Stats.maxHealth);


    }

    protected override void Update()
    {
        //Vector3 eulerAngles = transform.eulerAngles;
        //transform.eulerAngles = new Vector3(fixedRotation, fixedRotation, eulerAngles.z);
    }

    public override void TakeDamage(float amount)
    {
        if (HealthBar != null)
        {

            health -= amount;
            if (health <= 0)
            {
                health = 0;
            }
            HealthBar.UpdateHealthBar(health, Stats.maxHealth);
        }
    }

    public override void Die()
    {
        Debug.Log($"{Stats.enemyName} has died!");
        //for (int i = 0; i < itemdrops.Length; i++)
        //{
        //    var item = Instantiate(itemdrops[i], transform.position, Quaternion.identity);
        //    item.GetComponent<Interactable>().player = player;
        //}
        Destroy(gameObject);  // Default death behavior
    }
}
