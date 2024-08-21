using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float delay = 1.5f;
    private void Start()
    {
        Destroy(this.gameObject, delay);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
                player.TakeDamage(20f);
                Destroy(this.gameObject);
            }
        }
    }
}
