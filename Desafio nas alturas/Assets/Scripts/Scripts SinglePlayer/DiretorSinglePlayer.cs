using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiretorSinglePlayer : MonoBehaviour
{
    private AviaoSinglePlayer aviao = null;
    private PontuacaoSinglePlayer pontuacao = null;
    private InterfaceGameOver interfaceGameOver = null;
    private ControleDeDificuldade controleDeDificuldade = null;

    public void Start()
    {
        this.aviao = GameObject.FindObjectOfType<AviaoSinglePlayer>();
        this.pontuacao = GameObject.FindObjectOfType<PontuacaoSinglePlayer>();
        this.interfaceGameOver = GameObject.FindObjectOfType<InterfaceGameOver>();
        this.controleDeDificuldade = GameObject.FindObjectOfType<ControleDeDificuldade>();
    }
    //Freeza o jogo
    public void FinalizarJogo()
    {
        Time.timeScale = 0;
        this.pontuacao.SalvarRecorde();
        this.interfaceGameOver.MostrarInterface();
    }
    //Volta tudo aos momentos iniciais
    public void ReiniciarJogo()
    {
        controleDeDificuldade.ZerarDificuldade();
        Time.timeScale = 1;
        this.interfaceGameOver.EsconderInterface();
        this.DestruirObstaculos();
        this.aviao.Reiniciar();
        this.pontuacao.Reiniciar();
    }

    private void DestruirObstaculos()
    {
        Obstaculo[] obstaculos = GameObject.FindObjectsOfType<Obstaculo>();
        foreach (Obstaculo obstaculo in obstaculos)
        {
            obstaculo.Destruir();
        }
    }
}