using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public bool txtAtivo;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;
    public static LevelManager instance;
    
    
    public void Awake()
    {
        ZPlayerPrefs.Initialize("12345678", "passarotiltado");
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        ListaAdd();
    }

    void ListaAdd()
    {
        foreach(Level level in levelList)
        {
            GameObject btnNovo = Instantiate(botao) as GameObject;
            BotaoLevel btnNew = btnNovo.GetComponent<BotaoLevel>();
            btnNew.levelTxtBTN.text = level.levelText;

            if (ZPlayerPrefs.GetInt("Level" + btnNew.levelTxtBTN.text) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
                level.txtAtivo = true;
            }

            btnNew.desbloqueadoBTN = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable = level.habilitado;
            btnNew.GetComponentInChildren<TextMeshProUGUI>().enabled = level.txtAtivo;
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + btnNew.levelTxtBTN.text));

            if(level.desbloqueado == 1)
            {
                //condições para estrelas
                btnNew.estrela1.enabled = true;
                btnNew.estrela2.enabled = true;
                btnNew.estrela3.enabled = true;
            }
            btnNovo.transform.SetParent(localBtn, false);
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

}
