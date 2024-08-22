using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bombScript : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;  
    [SerializeField] AudioClip BoomClip;
    [SerializeField] GameObject explosion;
    [SerializeField] float radius = 5;


    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    var enemy = collider.transform.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        Debug.Log("Enemy component found: " + enemy.name);
                        enemy.TakeDamage(30);
                    }
                    else
                    {
                        Debug.LogError("Enemy component not found on: " + collision.name);
                    }
                }

                //var direction = (collider.transform.position - transform.position);
                //if (collider.GetComponent<Rigidbody>() != null)
                //{
                //    collider.GetComponent<Rigidbody>().AddForce(direction * 4, ForceMode.Impulse);
                //}
            }

            GameObject effect = Instantiate(explosion,
                                                gameObject.transform.position,
                                                Quaternion.identity).gameObject;
            Destroy(effect, 1.0f);
            Destroy(gameObject);

            audioSource.PlayOneShot(BoomClip);
        }
    }
}
