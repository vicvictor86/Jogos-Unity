using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuIniciar : MonoBehaviour
{
    private Animator barraAnim;
    private Animator infoAnim;
    private bool sobe;
    private AudioSource musica;
    public Sprite somLigado, somDesligado;
    private Button btnSom;

    public void Start()
    {
        infoAnim = GameObject.FindGameObjectWithTag("InfoTag").GetComponent<Animator>() as Animator;
        musica = GameObject.Find("AudioManager").GetComponent<AudioSource>() as AudioSource;
        btnSom = GameObject.Find("SOM").GetComponent<Button>() as Button;
    }

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void AnimaMenu()
    {
        barraAnim = GameObject.FindGameObjectWithTag("BarraAnimTag").GetComponent<Animator>();

        if(sobe == false)
        {
            barraAnim.Play("Move_UP_UI");
            sobe = true;
        }
        else
        {
            barraAnim.Play("Move_UP_UI_Inverse");
            sobe = false;
        }
    }

    public void AnimaInfo()
    {
        infoAnim.Play("AnimaInfo");
    }

    public void AnimaInfoInverse()
    {
        infoAnim.Play("AnimaInfoInverse");
    }

    public void LigaDesligaSom()
    {
        musica.mute = !musica.mute;

        if (musica.mute)
        {
            btnSom.image.sprite = somDesligado;
        }
        else
        {
            btnSom.image.sprite = somLigado;
        }
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/victor.emmanuel.121/");
    }
}
