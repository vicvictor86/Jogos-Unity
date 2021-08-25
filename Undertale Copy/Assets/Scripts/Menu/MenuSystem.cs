using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Fase 0");
    }

    public void Config()
    {
        Debug.Log("Entrou no config");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
