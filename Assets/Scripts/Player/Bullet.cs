using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float delay = 0.5f;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.name);

        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                Debug.Log("Enemy component found: " + enemy.name);
                enemy.TakeDamage(10);
            }
            else
            {
                Debug.LogError("Enemy component not found on: " + collision.name);
            }
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground")) Destroy(gameObject);
    }
}
