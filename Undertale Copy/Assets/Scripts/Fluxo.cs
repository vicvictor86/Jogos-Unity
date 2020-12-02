using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fluxo : MonoBehaviour
{
    public void CarregarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CarregarFase1()
    {
        SceneManager.LoadScene("Fase 1");
    }

    public void CarregarFase2()
    {
        SceneManager.LoadScene("BattleTest");
    }
}
