using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;  // Reference to ScriptableObject with common enemy data

    protected string enemyName;
    protected float currentHealth, attackDamage;

    protected virtual void Start()
    {
        enemyName = stats.enemyName;
        currentHealth = stats.maxHealth;
        attackDamage = stats.damage;
    }

    protected virtual void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{stats.enemyName} has died!");
        Destroy(gameObject);  // Default death behavior
    }

    // Abstract methods to be implemented by child classes
    protected abstract void Attack();
}