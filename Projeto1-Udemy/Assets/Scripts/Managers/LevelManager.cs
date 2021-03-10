using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

    void ListaAdd()
    {
        foreach(Level level in levelList)
        {
            //Criação do botão
            GameObject btnNovo = Instantiate(botao) as GameObject;
            btnNovo.transform.SetParent(localBtn, false);

            //Características do botão 
            if (PlayerPrefs.GetInt("Level"+level.levelText) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
            }

            btnNovo.GetComponentInChildren<Text>().text = level.levelText;
            btnNovo.GetComponentInChildren<Text>().enabled = level.habilitado;
            btnNovo.GetComponent<Button>().interactable = level.habilitado;

            //Lógica para acessar as fases
            btnNovo.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + level.levelText));
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    void Start()
    {
        ListaAdd();
    }
}
