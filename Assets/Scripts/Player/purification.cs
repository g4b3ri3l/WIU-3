using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purification : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float radius = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pollution") || collision.CompareTag("Ground"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Pollution"))
                {
                    Destroy(collider.gameObject);
                }

            }

            GameObject effect = Instantiate(explosion,
                                                gameObject.transform.position,
                                                Quaternion.identity).gameObject;
            Destroy(effect, 1.0f);
            Destroy(gameObject);

        }
    }


}
