using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiretorMultiplayer : Diretor
{
    [SerializeField]
    private int quantidadeParaReviver = 0;
    private bool alguemMorto;
    private int pontosDesdeAMorte = 0;
    private Jogador[] jogadores;
    private InterfaceCanvasInativo interfaceInativo = null;

    protected override void Start()
    {
        base.Start();
        this.jogadores = GameObject.FindObjectsOfType<Jogador>();
        this.interfaceInativo = GameObject.FindObjectOfType<InterfaceCanvasInativo>();
    }

    public void AlguemMorreu(Camera camera)
    {
        if (this.alguemMorto)
        {
            this.interfaceInativo.Sumir();
            this.FinalizarJogo();
        }
        else
        {
            this.alguemMorto = true;
            this.pontosDesdeAMorte = 0;
            this.interfaceInativo.AtualizarTexto(this.quantidadeParaReviver);
            this.interfaceInativo.Mostrar(camera);
        }
        
    }

    public override void ReiniciarJogo()
    {
        base.ReiniciarJogo();
        this.ReviverJogadores();
    }

    public void ReviverSePrecisar()
    {
        if (this.alguemMorto)
        {
            this.pontosDesdeAMorte++;
            this.interfaceInativo.AtualizarTexto(this.quantidadeParaReviver - this.pontosDesdeAMorte);
            if(this.pontosDesdeAMorte >= quantidadeParaReviver)
            {
                this.interfaceInativo.Sumir();
                this.ReviverJogadores();
            }
        }
    }  

    private void ReviverJogadores()
    {
        this.alguemMorto = false;
        foreach(var jogador in this.jogadores)
        {
            jogador.Ativar();
        }
    }

}
