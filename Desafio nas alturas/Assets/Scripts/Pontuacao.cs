using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{  
    [SerializeField]
    private Text textoPontuacao = null;
    [SerializeField]
    public int Pontos { get; private set; }
    private AudioSource audioPontuacao = null;

    public void Awake()
    {
        this.audioPontuacao = GetComponent<AudioSource>();
    }

    public void Reiniciar()
    {
        this.Pontos = 0;
        this.textoPontuacao.text = this.Pontos.ToString();
    }
    public void AdicionarPontos()
    {
        Pontos++;
        this.audioPontuacao.Play();
        this.textoPontuacao.text = this.Pontos.ToString();
    }

    public void SalvarRecorde()
    {
        if(this.Pontos > PlayerPrefs.GetInt("recorde"))
        {
            PlayerPrefs.SetInt("recorde", this.Pontos);
        }
    }
}
