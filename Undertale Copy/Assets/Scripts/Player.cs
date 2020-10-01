using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{//UNDERTALE COPY

    public static int life;
    public float speed;
    public static Vector3 tempPosition;

    private float movimentoH;
    private float movimentoV;


    void Start()
    {

        life = 10;
        speed = 100;

    }

    
    void Update()
    {

        //Função que irá movimentar o personagem
        movimentoPlayer();

        //Animações do personagem

        if(movimentoH > 0)
        {
            GetComponent<Animator>().SetBool("MovingRight", true);
        }
        if(movimentoH < 0)
        {
            GetComponent<Animator>().SetBool("MovingLeft", true);
        }
        if(movimentoV > 0)
        {
            GetComponent<Animator>().SetBool("MovingUp", true);
        }
        if(movimentoV < 0)
        {
            GetComponent<Animator>().SetBool("MovingDown", true);
        }
        if(movimentoH == 0)
        {
            GetComponent<Animator>().SetBool("MovingRight", false);
            GetComponent<Animator>().SetBool("MovingLeft", false);
        }
        if(movimentoV == 0)
        {
            GetComponent<Animator>().SetBool("MovingUp", false);
            GetComponent<Animator>().SetBool("MovingDown", false);
        }





    }

    private void movimentoPlayer()
    {
        movimentoH = Input.GetAxis("Horizontal");
        movimentoV = Input.GetAxis("Vertical");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        //Alteração na movimentação para n ter aceleração do movimento
        //Testar dps o método do video e ver qual fica melhor
        rigidbody.velocity = new Vector2(movimentoH * speed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, movimentoV * speed);
        Debug.Log(movimentoV);
    }

}
