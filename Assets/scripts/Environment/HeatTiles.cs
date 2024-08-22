using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatTiles : MonoBehaviour
{
    public float damage = 1f; 
    public float damageInterval = 1f; 

    private Coroutine damageCoroutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null)
            {
                damageCoroutine = StartCoroutine(ApplyDamage(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator ApplyDamage(PlayerManager player)
    {
        while (true)
        {
            player.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval); 
        }
    }
}
