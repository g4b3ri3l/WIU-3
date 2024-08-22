using UnityEditor;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private EnemyStats stats;  // Reference to ScriptableObject with common enemy data


    protected string enemyName;
    protected float currentHealth, attackDamage;

    protected EnemyHealthBar healthBar;

    protected virtual void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    protected virtual void Start()
    {
        enemyName = stats.enemyName;
        currentHealth = stats.maxHealth;
        attackDamage = stats.damage;

        healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);

    }

    protected virtual void Update()
    {
        
    }

    public virtual void TakeDamage(float amount)
    {   
        currentHealth -= amount;
        healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }

    }
    public virtual void Die()
    {
        Debug.Log($"{stats.enemyName} has died!");
        Destroy(gameObject);  // Default death behavior
    }


}