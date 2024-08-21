using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

                if (player.pollutionAmount >= 50) player.TakeDamage(0.1f);
                else player.pollutionAmount += Time.deltaTime * 7f;
            }
        }
    }
}
