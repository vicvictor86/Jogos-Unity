using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerOfAttack : MonoBehaviour
{
    [SerializeField] private float velocityOfPointer = 0;
    private bool canMove = true;
    private int quantityOfCollision = 0;

    private GameObject attackLevel;
    //Fazer Herança da classe enemy
    private Enemy enemyActual;
    private PlayerHeart playerHeart;

    private Collider2D lastCollision;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
        attackLevel = GameObject.Find("AttackLevel(Clone)");
        enemyActual = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        playerHeart = GameObject.Find("PlayerHeart").GetComponent<PlayerHeart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AttackPointer();
        }

        if (canMove)
        {
            MovePointer();
        }
    }

    private void MovePointer()
    {
        rig.velocity = new Vector2(velocityOfPointer, rig.velocity.y);
    }

    private void CleanBattleField()
    {
        Destroy(attackLevel);
        GameObject.Find("Diretor").GetComponent<Diretor>().PrepareToFight();
    }

    public void AttackPointer()
    {
        canMove = false;
        rig.velocity = new Vector2(0, 0);

        //Chamar função pra saber o dano
        double damageReduce = DamageReduce();
        double powerOfPlayer = playerHeart.GetPower();

        enemyActual.TakeDamage(powerOfPlayer - Math.Truncate(powerOfPlayer * damageReduce));
        //Animação da toriel tomando dano
        CleanBattleField();
        Destroy(this.gameObject);
    }

    private double DamageReduce()
    {
        switch (lastCollision.gameObject.name)
        {
            case "PerfectScore":
                return 0;
            case "HighScore":
                return 0.2;
            case "MediumScore":
                return 0.5;
            case "LowScore":
                return 0.8;
            default:
                return 1;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        lastCollision = collision;
        
        if (collision.gameObject.name.Equals("BattleFieldSprite"))
        {
            CollisionWithBattleField();
        }
    }

    private void CollisionWithBattleField()
    {
        if (quantityOfCollision == 0)
        {
            quantityOfCollision++;
        }
        else
        {
            CleanBattleField();
            Destroy(this.gameObject);
        }
    }
}
