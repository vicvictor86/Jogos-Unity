using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceGameOverSinglePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject imagemGameOver = null;
    [SerializeField]
    private Text pontuacaoRecorde = null;

    [SerializeField]
    private Image imagemMedalha = null;
    [SerializeField]
    private Sprite imagemMedalhaBronze = null;
    [SerializeField]
    private Sprite imagemMedalhaPrata = null;
    [SerializeField]
    private Sprite imagemMedalhaOuro = null;

    private PontuacaoSinglePlayer pontuacao = null;
    private int recorde = 0;

    public void Start()
    {
        this.pontuacao = GameObject.FindObjectOfType<PontuacaoSinglePlayer>();
    }

    public void MostrarInterface()
    {
        this.AtualizarInterfaceGrafica();
        this.imagemGameOver.SetActive(true);
    }

    public void EsconderInterface()
    {
        this.imagemGameOver.SetActive(false);
    }

    private void AtualizarInterfaceGrafica()
    {
        this.recorde = PlayerPrefs.GetInt("recordesingleplayer");
        this.pontuacaoRecorde.text = recorde.ToString();
        this.VerificarCorMedalha();
    }

    private void VerificarCorMedalha()
    {
        if (this.pontuacao.Pontos > this.recorde - 2)
        {
            this.imagemMedalha.sprite = this.imagemMedalhaOuro;
        }
        else if (this.pontuacao.Pontos > this.recorde / 2)
        {
            this.imagemMedalha.sprite = this.imagemMedalhaPrata;
        }
        else
        {
            this.imagemMedalha.sprite = this.imagemMedalhaBronze;
        }
    }
}
