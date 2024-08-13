using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFish : Enemy
{
    private string Name;
    private float damage;
    private float maxHealth;
    private float currHealth;



    [SerializeField] private EnemyHealthBar HealthBar;
    private float fixedRotation = 0;

    [SerializeField] private EnemyStats swordFishStats;

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


}

   

