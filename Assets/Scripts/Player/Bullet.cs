using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float delay = 0.5f;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.transform.GetComponent<Enemy>();

            enemy.TakeDamage(10);

            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
