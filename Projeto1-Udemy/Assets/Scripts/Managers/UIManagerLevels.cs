using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerLevels : MonoBehaviour
{

    [SerializeField]private Text moedasLevel = null;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.UpdateScore();
        moedasLevel.text = PlayerPrefs.GetInt("moedasSave").ToString();
    }
}
