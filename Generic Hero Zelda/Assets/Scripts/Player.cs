using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Grid grid;

    public Transform firePoint;
    public float atkRadius;
    public LayerMask enemyLayer;

    Vector2 mov;
    public float dashSpeed;
    public bool directionRight;

    // Update is called once per frame
    void Update()
    {
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");
        SwordAttack();
        Dash();
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * moveSpeed * Time.fixedDeltaTime);
    }

    public void SwordAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //isAttacking = true;
            //animator.SetInteger("transition", 3);
            //AudioSource.PlayOneShot(sfx);

            Collider2D hit = Physics2D.OverlapCircle(firePoint.position, atkRadius, enemyLayer);
            if (hit)
            {
                Debug.Log("dor");
            }

            //StartCoroutine(OnDaming(hit));

            //StartCoroutine(OnAttacking());
        }
    }

    public void ArrowAttack()
    {

    }

    public void Dash()
    {
        //Não dar dash infinito
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Force);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector2.down * dashSpeed, ForceMode2D.Force);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector2.up * dashSpeed, ForceMode2D.Force);
            }
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, atkRadius);
    }
}
