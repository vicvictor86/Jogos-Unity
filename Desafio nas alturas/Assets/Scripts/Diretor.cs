using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diretor : MonoBehaviour
{
    private Aviao aviao = null;
    private Pontuacao pontuacao = null;
    private InterfaceGameOver interfaceGameOver = null;

    public void Start()
    {
        this.aviao = GameObject.FindObjectOfType<Aviao>();
        this.pontuacao = GameObject.FindObjectOfType<Pontuacao>();
        this.interfaceGameOver = GameObject.FindObjectOfType<InterfaceGameOver>();
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
        Time.timeScale = 1;
        this.interfaceGameOver.EsconderInterface();
        this.DestruirObstaculos();
        this.aviao.Reiniciar();
        this.pontuacao.Reiniciar();
    }

    private void DestruirObstaculos()
    {
        Obstaculo[] obstaculos = GameObject.FindObjectsOfType<Obstaculo>();
        foreach(Obstaculo obstaculo in obstaculos)
        {
            obstaculo.Destruir();
        }
    }
}
