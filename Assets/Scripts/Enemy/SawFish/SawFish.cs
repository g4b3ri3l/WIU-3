using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SawFish : Enemy
{
    [SerializeField] private EnemyStats stats;  // Reference to ScriptableObject with common enemy data

    private string Name;
    public float currHealth, damage;

    private float fixedRotation = 0;


    [SerializeField] private EnemyHealthBar HealthBar;

    protected override void Awake()
    {
        HealthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    protected override void Start()
    {
        Name = stats.enemyName;
        currHealth = stats.maxHealth;
        damage = stats.damage;

        HealthBar.UpdateHealthBar(currHealth, stats.maxHealth);


    }

    protected override void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(fixedRotation, fixedRotation, eulerAngles.z);
    }

    public override void TakeDamage(float amount)
    {
        if (HealthBar != null)
        {
            
            currHealth -= amount;
            if (currHealth <= 0)
            {
                currHealth = 0;
            }
            HealthBar.UpdateHealthBar(currHealth, stats.maxHealth);
        }
    }

    public override void Die()
    {
        Debug.Log($"{stats.enemyName} has died!");
        Destroy(gameObject);  // Default death behavior
    }
 

}
