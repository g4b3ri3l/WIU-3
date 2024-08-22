using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeController : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] float colliderDist;
    [SerializeField] BoxCollider2D box;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float cooldowntimer = 0;
    PlayerManager playerManager;

    [SerializeField] AudioSource audioSource;  
    [SerializeField] AudioClip StabClip;

    private void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
    }

    Enemy enemy;

    void Update()
    {
        cooldowntimer += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (cooldowntimer >= attackCooldown)
            {
                //SoundManager.instance.PlaySound(punch);
                //animator.SetBool("Melee", true);
                if (EnemyInSight())
                {
                    audioSource.PlayOneShot(StabClip);
                    cooldowntimer = 0;
                    enemy.TakeDamage(playerManager.damage);
                }
            }
        }
        //else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    animator.SetBool("Melee", false);
        //}
        
    }

    bool EnemyInSight()
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(box.bounds.center + transform.right * range * transform.localScale.x * colliderDist, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z), 0, Vector2.right, 0, enemyLayer);
        if (hit.Length < 1)
        {
            return false;
        }
        else
        {
            foreach (var col in hit)
            {
                if (col.collider != null)
                {
                    Debug.Log(col.collider != null);
                    enemy = col.transform.GetComponent<Enemy>();
                }
                else
                {
                    return false;
                }

            }
        }
        return hit != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDist, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z));
    }
}
