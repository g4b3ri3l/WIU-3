using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D _rigid;

    float hori;
    float vert;
    bool isfacingRight = true;
    [SerializeField] public float speed = 8f;

    bool canDash = true;
    bool isDashing;
    [SerializeField] float dashPower = 3f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown = 1f;

    private PlayerManager playerManager;

    [SerializeField] float stamDrain = 0.5f;
    [SerializeField] float stamRecov = 1f;

    float localScaleX;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        localScaleX = transform.localScale.x;
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && playerManager.stamina > 10)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (vert > 0)
        {
            playerManager.stamina -= stamDrain;
        }
        else
        {
            playerManager.stamina += stamRecov;

            if (playerManager.stamina >= playerManager.maxStamina)
            {
                playerManager.stamina = playerManager.maxStamina;
            }
        }

        if (playerManager.stamina <= 0)
        {
            playerManager.stamina = 0;
            vert = 0;
        }

        _rigid.velocity = new Vector2(hori * speed, vert * speed);
    }

    void Flip()
    {
        if (isfacingRight && hori < 0f || !isfacingRight && hori > 0f)
        {
            isfacingRight = !isfacingRight;
            localScaleX *= -1f;
        }


        transform.localScale = new Vector3(
            Mathf.Lerp(transform.localScale.x, localScaleX, Time.deltaTime * 25f), 
            transform.localScale.y, transform.localScale.z);
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float oggrav = _rigid.gravityScale;
        playerManager.stamina -= 10;
        _rigid.gravityScale = 0.0f;
        _rigid.velocity = new Vector2(_rigid.velocity.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        _rigid.gravityScale = oggrav;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}