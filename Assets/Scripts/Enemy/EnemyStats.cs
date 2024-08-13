using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float damage;
    public float chaseSpeed;
    public float patrolSpeed;
    public float chaseRange;
    public float attackRange;
}