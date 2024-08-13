using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    float delay = 0.5f;
    Health enemyhp;

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
            enemyhp = collision.transform.GetComponent<Health>();

            enemyhp.TakeDamage(20);

            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
