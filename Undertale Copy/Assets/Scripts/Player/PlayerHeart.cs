using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{//UNDERTALE COPY

    private Rigidbody2D rigid;

    [Header("Player Informations")]
    [SerializeField] private float speed = 0;
    [SerializeField] private int life = 0;
    [SerializeField] private int lifeMax = 0;
    [SerializeField] private double power = 0.0;
    [SerializeField] private UISystem uiSystem = null;
    
    private float movimentoH;
    private float movimentoV;
    private Diretor diretor;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        diretor = FindObjectOfType<Diretor>();
    }

    private void Update()
    {
        if(diretor == null || diretor.IsFighting())
        {
            MovimentoPlayerHeart();
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D != null)
        {
            rigid.velocity = new Vector2(0, 0); 
        }
    }

    private void MovimentoPlayerHeart()
    {   
        movimentoH = Input.GetAxisRaw("Horizontal");
        movimentoV = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2(movimentoH * speed, movimentoV * speed);
    }

    public void Damage(int damage)
    {
        life -= damage;
        uiSystem.LifeBarDegrease(damage);
        uiSystem.TextChange();
    }

    public void Heal(int regen)
    {
        life += regen;
        if (life > lifeMax)
        {
            life = lifeMax;
        }
        
        uiSystem.LifeBarAddition(regen);
        uiSystem.TextChange();
    }

    public double GetPower()
    {
        return this.power;
    }

    public void SetPower(double powerd)
    {
        this.power = powerd;
    }

    public int GetLife()
    {
        return this.life;
    }
    
    public int GetLifeMax()
    {
        return this.lifeMax;
    }
}
