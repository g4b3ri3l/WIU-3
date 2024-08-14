using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBeam : MonoBehaviour
{
    float delay = 0.5f;
    Vector3 expand = new Vector3(0.2f, 0, 0);
    Health enemyhp;

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
            enemyhp = collision.transform.GetComponent<Health>();

            enemyhp.TakeDamage(20);

            //Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }

}
