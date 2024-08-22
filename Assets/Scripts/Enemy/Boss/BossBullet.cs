using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float delay = 3f;

    private void OnEnable()
    {
        Invoke("Destroy", delay);
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
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

            if (collision.CompareTag("Ground")) Destroy(gameObject);

        }
    }
}
