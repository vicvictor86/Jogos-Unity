using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllBtnMenu : MonoBehaviour
{
    public Animator btnAnim;
    private bool chave = true;

    public void EventoClickG()
    {
        chave = !chave;

        if(chave == false)
        {
            btnAnim.Play("LogoAnim");
        }
        else
        {
            btnAnim.Play("LogoAnimInverse");
        }
    }

    public void FuturosJogos()
    {
        Application.OpenURL("https://github.com/vicvictor86/Jogos-Unity");
    }
}
