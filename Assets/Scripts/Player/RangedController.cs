using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float firePoint;

    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ranged Attack
        if (Input.GetMouseButtonDown(1))
        {
            //SoundManager.instance.PlaySound(shoot);
            //rangecontroller.Shoot();
            //animator.SetBool("Shoot", true);


            var aimTargetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var AimDirection = (aimTargetPosition - (Vector2)transform.position).normalized;
            var bulletGO = Instantiate(bullet, transform.position + (Vector3)AimDirection, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(AimDirection * speed, ForceMode2D.Impulse);
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
       //Gizmos.DrawWireSphere(firePoint, 0.5f);
    }


    //public void Shoot()
    //{
    //    GameObject bulletGO = Instantiate(bullet, firePoint.position, firePoint.rotation);
    //    Rigidbody2D bulletRigidbody = bulletGO.GetComponent<Rigidbody2D>();
    //    bulletRigidbody.AddForce(new Vector2(speed * transform.localScale.x, 0.0f), ForceMode2D.Impulse);
    //}


    /*

       var aimTargetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
       var AimDirection = (aimTargetPosition - (Vector2)transform.position).normalized;
       var bullet = Instantiate(bulletPrefab, transform.position + (Vector3)AimDirection * 0.6f, Quaternion.identity);
       bullet.GetComponent<Rigidbody2D>().AddForce(AimDirection*4, ForceMode2D.Impulse);
*/
}
