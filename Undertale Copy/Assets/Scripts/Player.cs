using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{//UNDERTALE COPY

    public int life;
    public float speed;
    public int power;

    public static Vector3 tempPosition;

    private Rigidbody2D rig;

    private float movimentoH;
    private float movimentoV;


    void Start()
    {

        life = 10;
        speed = 100;
        rig = this.GetComponent<Rigidbody2D>();
        power = 5;
    }

    
    void Update()
    {
        MovementPlayer();
        AnimationMovement();
    }

    private void AnimationMovement()
    {
        if (movimentoH > 0)
        {
            GetComponent<Animator>().SetBool("MovingRight", true);
        }
        if (movimentoH < 0)
        {
            GetComponent<Animator>().SetBool("MovingLeft", true);
        }
        if (movimentoV > 0)
        {
            GetComponent<Animator>().SetBool("MovingUp", true);
        }
        if (movimentoV < 0)
        {
            GetComponent<Animator>().SetBool("MovingDown", true);
        }
        if (movimentoH == 0)
        {
            GetComponent<Animator>().SetBool("MovingRight", false);
            GetComponent<Animator>().SetBool("MovingLeft", false);
        }
        if (movimentoV == 0)
        {
            GetComponent<Animator>().SetBool("MovingUp", false);
            GetComponent<Animator>().SetBool("MovingDown", false);
        }
    }

    private void MovementPlayer()
    {
        movimentoH = Input.GetAxisRaw("Horizontal");
        movimentoV = Input.GetAxisRaw("Vertical");

        rig.velocity = new Vector2(movimentoH * speed, rig.velocity.y);
        rig.velocity = new Vector2(rig.velocity.x, movimentoV * speed);
    }

    public int GetPower()
    {
        return this.power;
    }
}
