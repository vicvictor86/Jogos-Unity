using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{//UNDERTALE COPY

    public static float speed;
    public static int life;
    public static Vector3 tempoPosition;

    //Movimentação
    private float movimentoH;
    private float movimentoV;
    private Vector3 firstPosition;

    //Logica dos cliques
    public bool clickFight;
    public bool clickAct;
    public bool clickItem;
    public bool clickMercy;





    void Start()
    {
        life = Player.life;
        speed = 300;
    }

 
    void Update()
    {
        movimentoPlayerHeart();
      
        //Fazer as bolas de fogo da toriel caindo e dando dano

    }


    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Colidiu com " + collision2D.gameObject.tag);
    }


    public void OnCollisionExit2D(Collision2D collision2D)
    {
        Debug.Log("Deixou de colidir com " + collision2D.gameObject.tag);
    }


    private void movimentoPlayerHeart()
    {
        movimentoH = Input.GetAxis("Horizontal");
        movimentoV = Input.GetAxis("Vertical");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(movimentoH * speed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, movimentoV * speed);
    }
    
}
