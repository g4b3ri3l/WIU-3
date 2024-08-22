using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;

    [SerializeField] private float startAngle = 90f, endAngle = 270f;

    private Vector2 moveDirection;

    [SerializeField] private EnemySpawner boss;

    [SerializeField] AudioSource audioSource;  // Reference to the AudioSource component
    [SerializeField] AudioClip shootingClip;
    [SerializeField] private float damageSoundCooldown = 0.5f;
    private float damageSoundCooldownTimer = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (boss.phase == 2)
            InvokeRepeating("Fire", 0f, 2f);
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        if (damageSoundCooldownTimer > 0)
        {
            damageSoundCooldownTimer -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;
<<<<<<< Updated upstream
        if (damageSoundCooldownTimer <= 0f)
        {
            audioSource.PlayOneShot(shootingClip);
            damageSoundCooldownTimer = damageSoundCooldown; // Reset the cooldown timer
        }
=======
/*        if (damageSoundCooldownTimer <= 0f)
        {
            audioSource.PlayOneShot(shootingClip);
            damageSoundCooldownTimer = damageSoundCooldown; // Reset the cooldown timer
        }*/
>>>>>>> Stashed changes
        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BossBullet>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }
}
