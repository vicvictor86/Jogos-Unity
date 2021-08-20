using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Informations Enemy")]
    [SerializeField] protected double life;
    [SerializeField] protected int powerOfEnemy = 0;
    [SerializeField] protected int convincing = 0;
    protected string nameEnemy = "";
    
    [Header("Region Of Fire")]
    [SerializeField] protected Vector3 center;
    [SerializeField] protected Vector3 size;
    
    public int GetPowerEnemy()
    {
        return this.powerOfEnemy;
    }

    public virtual string GetName()
    {
        return this.nameEnemy;
    }
    
    public double GetLife()
    {
        return this.life;
    }

    public int GetConvincing()
    {
        return this.convincing;
    }

    public void Convince(int number)
    {
        this.convincing -= number;
    }
    
    public void TakeDamage(double damage)
    {
        this.life -= damage;
    }

    public virtual string TextBeggin()
    {
        return null;
    }

    public virtual string TextConviced()
    {
        return null;
    }

    public virtual string TextNoConviced()
    {
        return null;
    }

    public virtual string TextTalking()
    {
        return null;
    }
}
