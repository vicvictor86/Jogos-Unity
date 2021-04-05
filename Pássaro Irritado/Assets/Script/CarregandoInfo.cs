using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarregandoInfo : MonoBehaviour
{

    public TextMeshProUGUI txtComp;

    public void BtnClick()
    {
        StartCoroutine(LoadGameProg());
    }

    IEnumerator LoadGameProg()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
   
        while (!async.isDone)
        {
            txtComp.enabled = true;
            yield return null;
        }
    }
}
