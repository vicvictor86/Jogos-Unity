using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerOfAttack : MonoBehaviour
{
    public float velocityOfPointer;
    public bool canMove = true;
    private int quantityOfCollision = 0;

    public GameObject attackLevel;
    //Fazer Herança da classe enemy
    public TorielBoss toriel;
    public PlayerHeart playerHeart;

    public Collider2D lastCollision;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
        attackLevel = GameObject.Find("AttackLevel(Clone)");
        toriel = GameObject.Find("Toriel").GetComponent<TorielBoss>();
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

    public void MovePointer()
    {
        rig.velocity = new Vector2(velocityOfPointer, rig.velocity.y);
    }

    public void CleanBattleField()
    {
        Destroy(attackLevel.gameObject);
        GameObject.Find("Diretor").GetComponent<Diretor>().PrepareToFight();
    }

    public void AttackPointer()
    {
        canMove = false;
        rig.velocity = new Vector2(0, 0);

        //Chamar função pra saber o dano
        double damageReduce = DamageReduce();
        double powerOfPlayer = playerHeart.GetPower();

        toriel.TakeDamage(powerOfPlayer - Math.Truncate(powerOfPlayer * damageReduce));
        //Animação da toriel tomando dano
        CleanBattleField();
        Destroy(this.gameObject);
    }

    public double DamageReduce()
    {
        if (lastCollision.gameObject.name.Equals("PerfectScore"))
        {
            return 0;
        }
        else if (lastCollision.gameObject.name.Equals("HighScore"))
        {
            return 0.2;
        }
        else if (lastCollision.gameObject.name.Equals("MediumScore"))
        {
            return 0.5;
        }
        else if (lastCollision.gameObject.name.Equals("LowScore"))
        {
            return 0.8;
        }
        else
        {
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

    public void CollisionWithBattleField()
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
