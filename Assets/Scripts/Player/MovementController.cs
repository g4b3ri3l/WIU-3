using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D _rigid;

    [SerializeField] float hori;
    [SerializeField] float vert;
    [SerializeField] float speed = 8f;
    [SerializeField] bool isfacingRight = true;

    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower = 3f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashStam = 20f;

    [SerializeField] float stamina = 100f;
    [SerializeField] float stamDrain = 0.5f;
    [SerializeField] float stamRecov = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
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
            stamina -= stamDrain;
        }
        else
        {
            stamina += stamRecov;

            if (stamina >= 100f)
            {
                stamina = 100f;
            }
        }

        _rigid.velocity = new Vector2(hori * speed, vert * speed);
    }

    void Flip()
    {
        if (isfacingRight && hori < 0f || !isfacingRight && hori > 0f)
        {
            Vector3 localscale = transform.localScale;
            isfacingRight = !isfacingRight;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        stamina -= dashStam;
        float oggrav = _rigid.gravityScale;
        _rigid.gravityScale = 0.0f;
        _rigid.velocity = new Vector2(_rigid.velocity.x * dashPower, _rigid.velocity.y * dashPower);
        yield return new WaitForSeconds(dashTime);
        _rigid.gravityScale = oggrav;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}