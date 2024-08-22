using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField] GameObject bomb, purificationBomb;
    [SerializeField] Transform firePoint;
    [SerializeField] float speed;
   
    [SerializeField] Item bomb1,bomb2;
    [SerializeField] float inputBuffer;
    float bufferTimer = 0;

    [SerializeField] float offset;

    [SerializeField] AudioSource audioSource;  // Reference to the AudioSource component
    [SerializeField] AudioClip shootingClip;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        bufferTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && bomb1.itemCount > 0)
        {
            if (bufferTimer >= inputBuffer)
            {
                bufferTimer = 0;
                bomb1.itemCount--;
                
                // Play the shooting audio
                audioSource.PlayOneShot(shootingClip);

                // Normal Bullets
                Vector2 aimTargetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 AimDirection = (aimTargetPosition - (Vector2)rb.transform.position).normalized;

                firePoint.transform.position = AimDirection;

                var zAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;

                GameObject bulletGO = Instantiate(bomb, firePoint.position + transform.position, Quaternion.Euler(0, 0, zAngle));
                Rigidbody2D bulletRigidbody = bulletGO.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(AimDirection * speed, ForceMode2D.Impulse);
                bulletGO.GetComponent<bombScript>().audioSource = audioSource;
            }
        }
        if (Input.GetKeyDown(KeyCode.T) && bomb2.itemCount > 0)
        {
            if (bufferTimer >= inputBuffer)
            {
                bomb2.itemCount--;
                bufferTimer = 0;

                // Play the shooting audio
                audioSource.PlayOneShot(shootingClip);

                // Normal Bullets
                Vector2 aimTargetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 AimDirection = (aimTargetPosition - (Vector2)rb.transform.position).normalized;

                firePoint.transform.position = AimDirection;

                var zAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;

                GameObject bulletGO = Instantiate(purificationBomb, firePoint.position + transform.position, Quaternion.Euler(0, 0, zAngle));
                Rigidbody2D bulletRigidbody = bulletGO.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(AimDirection * speed, ForceMode2D.Impulse);
                bulletGO.GetComponent<purification>().audioSource = audioSource;
            }
        }
    }


}
