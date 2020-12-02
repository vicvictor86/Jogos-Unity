using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    private Carrossel[] cenario;
    private GeradorDeObstaculos GeradorDeobstaculos;
    private Aviao aviao;
    private bool estouMorto;

    private void Start()
    {
        this.cenario = this.GetComponentsInChildren<Carrossel>();
        this.GeradorDeobstaculos = this.GetComponentInChildren<GeradorDeObstaculos>();
        this.aviao = this.GetComponentInChildren<Aviao>();
    }

    public void Desativar()
    {
        this.estouMorto = true;
        GeradorDeobstaculos.Parar();
        foreach(var carrosel in this.cenario)
        {
            carrosel.enabled = false;
        }
    }

    public void Ativar()
    {
        if(this.estouMorto == true)
        {
            this.aviao.Reiniciar();
            this.GeradorDeobstaculos.Recomecar();
            foreach (var carrosel in this.cenario)
            {
                carrosel.enabled = true;
            }
        }
        estouMorto = false;
    }
}
