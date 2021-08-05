using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{//UNDERTALE COPY

    public Rigidbody2D rigid;

    public float speed;
    public int life;
    public static Vector3 tempoPosition;

    //Movimentação
    private float movimentoH;
    private float movimentoV;
    private Vector3 firstPosition;

    public Diretor diretor;
    public GameObject battleButtons;

    public double power;
    void Start()
    {
        
    }

 
    void Update()
    {

        if(diretor.isFighting)
        {
            movimentoPlayerHeart();
        }
      
        //Fazer as bolas de fogo da toriel caindo e dando dano

    }


    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Colidiu com " + collision2D.gameObject.tag);
        if(collision2D != null)
        {
            rigid.velocity = new Vector2(0, 0); 
        }
    }


    public void OnCollisionExit2D(Collision2D collision2D)
    {
        Debug.Log("Deixou de colidir com " + collision2D.gameObject.tag);
    }


    private void movimentoPlayerHeart()
    {   
        movimentoH = Input.GetAxisRaw("Horizontal");
        movimentoV = Input.GetAxisRaw("Vertical");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(movimentoH * speed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, movimentoV * speed);
    }

    public void Damage(int dano)
    {
        life -= dano;
    }

    public double GetPower()
    {
        return this.power;
    }
}
