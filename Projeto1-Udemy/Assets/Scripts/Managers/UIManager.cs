using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private Text pontosUI, bolasUI;
    [SerializeField]private GameObject losePainel, winPainel, pausePainel;
    
    //Pause
    [SerializeField] private Button pauseBtn, pauseBtnReturn, MenuPause, PlayAgainPause;
    //Lose
    [SerializeField] private Button btnNovamenteLose, btnLevelLose;
    //Win
    [SerializeField] private Button btnLevelWin, btnNovamenteWin, btnAvancarWin;

    public int moedasAntes, moedasDepois, resultado;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
        PegaDados();
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        PegaDados();
    }

    void PegaDados()
    {
        if (OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 7 && OndeEstou.instance.fase != 2)
        {
            //Elementos da UI
            pontosUI = GameObject.Find("Numero_Moedas").GetComponent<Text>();
            bolasUI = GameObject.Find("bolasUI").GetComponent<Text>();

            //Paineis
            losePainel = GameObject.Find("LosePainel");
            winPainel = GameObject.Find("WinPainel");
            pausePainel = GameObject.Find("PausePainel");

            //Botões Pause
            pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
            pauseBtnReturn = GameObject.Find("Despausar").GetComponent<Button>();
            MenuPause = GameObject.Find("MenuPause").GetComponent<Button>();
            PlayAgainPause = GameObject.Find("JogarNovamentePause").GetComponent<Button>();

            //Botões lose
            btnNovamenteLose = GameObject.Find("NovamenteLose").GetComponent<Button>();
            btnLevelLose = GameObject.Find("MenuFasesLose").GetComponent<Button>();

            //Botões Win
            btnLevelWin = GameObject.Find("MenuFasesWin").GetComponent<Button>();
            btnNovamenteWin = GameObject.Find("NovamenteWin").GetComponent<Button>();
            btnAvancarWin = GameObject.Find("AvancarWin").GetComponent<Button>();

            //Eventos

            //Eventos pause
            pauseBtn.onClick.AddListener(Pause);
            pauseBtnReturn.onClick.AddListener(Despausar);
            PlayAgainPause.onClick.AddListener(JogarNovamente);
            MenuPause.onClick.AddListener(Levels);

            //Eventos lose
            btnNovamenteLose.onClick.AddListener(JogarNovamente);
            btnLevelLose.onClick.AddListener(Levels);

            //Eventos win
            btnLevelWin.onClick.AddListener(Levels);
            btnNovamenteWin.onClick.AddListener(JogarNovamente);
            btnAvancarWin.onClick.AddListener(ProximaFase);

            moedasAntes = PlayerPrefs.GetInt("moedasSave");
        }
    }

    public void StartUI()
    {
        LigaDesligaPainel();
    }
    
    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.chutesBola.ToString();
        moedasDepois = ScoreManager.instance.moedas;
    }
    public void GameOverUI()
    {
        losePainel.SetActive(true);
    }

    public void WinGameUI()
    {
        winPainel.SetActive(true);
    }

    public void PauseGameUI()
    {
        pausePainel.SetActive(true);
    }

    void LigaDesligaPainel()
    {
        StartCoroutine(DesligaPainel());
    }

    public void Pause()
    {
        PauseGameUI();
        pausePainel.GetComponent<Animator>().Play("MoveUI_Pause");
        Time.timeScale = 0;
    }

    public void Despausar()
    {
        pausePainel.GetComponent<Animator>().Play("MoveUI_Despause");
        Time.timeScale = 1;
        StartCoroutine(EsperaDespause());
    }

    IEnumerator EsperaDespause()
    {
        yield return new WaitForSeconds(0.8f);
        pausePainel.SetActive(false);
    }

    IEnumerator DesligaPainel()
    {
        yield return new WaitForSeconds(0.001f);
        losePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);
    }

    public void JogarNovamente()
    {
        if (!GameManager.instance.win && AdsUnity.instance.adsBtnAcionado == true)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            AdsUnity.instance.adsBtnAcionado = false;
        }
        else if(!GameManager.instance.win && AdsUnity.instance.adsBtnAcionado == false)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = moedasDepois - moedasAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
        }
        else
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = 0;
        }
    }

    void Levels()
    {
        if (GameManager.instance.win == false && AdsUnity.instance.adsBtnAcionado == true)
        {
            AdsUnity.instance.adsBtnAcionado = false;
            SceneManager.LoadScene(1);
        }
        else if(GameManager.instance.win == false && AdsUnity.instance.adsBtnAcionado == false)
        {
            resultado = moedasDepois - moedasAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
            SceneManager.LoadScene(1);
        }
        else
        {
            resultado = 0;
            SceneManager.LoadScene(1);
        }
    }

    void ProximaFase()
    {
        if(GameManager.instance.win == true)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);
        }
    }
}
