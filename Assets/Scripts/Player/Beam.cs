using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    Vector3 expand = new Vector3(0.2f, 0, 0);
    Enemy enemy;

    RectTransform RectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, delay);

        RectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        RectTransform.transform.localScale += expand;

        if (RectTransform.transform.localScale.x >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.transform.GetComponent<Enemy>();

            enemy.TakeDamage(20);

            //Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
