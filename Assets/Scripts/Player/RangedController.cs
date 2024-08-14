using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject beam;
    [SerializeField] Transform firePoint;
    [SerializeField] float speed;

    [SerializeField] float attackCooldown;
    [SerializeField] float cooldowntimer = 0;

    [SerializeField] float inputBuffer;
    [SerializeField] float bufferTimer = 0;

    [SerializeField] float offset;

    Rigidbody2D rb;

    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            attacking = true;
        }

        if (attacking && Input.GetMouseButtonUp(1))
        {
            bufferTimer += Time.deltaTime;
            if (bufferTimer >= inputBuffer)
            {
                bufferTimer = 0;
                cooldowntimer = 0;

                //Normal Bullets
                Vector2 aimTargetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 AimDirection = (aimTargetPosition - (Vector2)rb.transform.position).normalized;

                firePoint.transform.position = AimDirection;

                GameObject bulletGO = Instantiate(bullet, firePoint.position + transform.position, bullet.transform.rotation);
                Rigidbody2D bulletRigidbody = bulletGO.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(AimDirection * speed, ForceMode2D.Impulse);
            }
            attacking = false;
        }
        else if (attacking && cooldowntimer >= attackCooldown)
        {
            //Charge testBeam
            cooldowntimer = 0;
            Vector2 aimTargetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 AimDirection = (aimTargetPosition - (Vector2)rb.transform.position).normalized;

            firePoint.transform.position = AimDirection;

            var zAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;

            GameObject beamGO = Instantiate(beam, firePoint.position * offset + transform.position, Quaternion.Euler(0, 0, zAngle));
            attacking = false;
        }
        else if (attacking)
        {
            cooldowntimer += Time.deltaTime;
        }

        //else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    animator.SetBool("Shoot", false);
        //}
    }

    private void OnDrawGizmos()
    {
        // Set the gizmos color
        Gizmos.color = Color.red;

        // Draw a gizmos at the attackPoint within given range
        Gizmos.DrawWireSphere(firePoint.position, 0.5f);
    }
}
