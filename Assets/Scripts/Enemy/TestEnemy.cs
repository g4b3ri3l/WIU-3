using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TestEnemy : Enemy
{
    [SerializeField] private EnemyStats testStats;  // Reference to ScriptableObject with common enemy data

    private string testEnemyName;
    public float testCurrentHealth, testAttackDamage;

    private float fixedRotation = 0;


    [SerializeField] private EnemyHealthBar testHealthBar;

    protected override void Awake()
    {
        testHealthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    protected override void Start()
    {
        testEnemyName = testStats.enemyName;
        testCurrentHealth = testStats.maxHealth;
        testAttackDamage = testStats.damage;

        testHealthBar.UpdateHealthBar(testCurrentHealth, testStats.maxHealth);


    }

    protected override void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(fixedRotation, fixedRotation, eulerAngles.z);
    }

    public override void TakeDamage(float amount)
    {
        if (testHealthBar != null)
        {
            
            testCurrentHealth -= amount;
            if (testCurrentHealth <= 0)
            {
                testCurrentHealth = 0;
            }
            testHealthBar.UpdateHealthBar(testCurrentHealth, testStats.maxHealth);
        }
    }

    public override void Die()
    {
        Debug.Log($"{testStats.enemyName} has died!");
        Destroy(gameObject);  // Default death behavior
    }
 

}
