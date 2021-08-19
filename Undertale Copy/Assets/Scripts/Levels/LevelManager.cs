using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static void NextLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Fase 0"))
        {
            SceneManager.LoadScene("FloweyLevel");
        }
    }

    public static void PreviousLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("FloweyLevel"))
        {
            SceneManager.LoadScene("Fase 0");
        }
    }
}
