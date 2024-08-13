using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeController : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] float colliderDist;
    [SerializeField] int damage;
    [SerializeField] BoxCollider2D box;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float cooldowntimer = 0;

    Health enemyHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            cooldowntimer += Time.deltaTime;
            if (cooldowntimer >= attackCooldown)
            {
                //SoundManager.instance.PlaySound(punch);
                //animator.SetBool("Melee", true);
                if (EnemyInSight())
                {
                    cooldowntimer = 0;
                    enemyHP.TakeDamage(1);
                }
            }
        }
        //else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    animator.SetBool("Melee", false);
        //}
        

        ////Melee Attack
        //if (Input.GetMouseButton(0))
        //{
        //    if (EnemyInSight())
        //    {
        //        cooldowntimer += Time.deltaTime;
        //        if (cooldowntimer >= attackCooldown)
        //        {
        //            cooldowntimer = 0;
        //            enemyHP.TakeDamage(10);
        //            //SoundManager.instance.PlaySound(punch);
        //            //animator.SetBool("Melee", true);
        //        }
        //    }
        //}
        ////else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        ////{
        ////    animator.SetBool("Melee", false);
        ////}
    }

    bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * colliderDist, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z), 0, Vector2.right, 0, enemyLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider != null);
            enemyHP = hit.transform.GetComponent<Health>();
        }

        

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDist, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z));
    }
}
