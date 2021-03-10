using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Bola
    [SerializeField]
    private GameObject[] bola = null;
    public int chutesBola = 2;
    public int bolasEmCena = 0;
    public Transform pos = null;

    public bool win;
    public int tiro = 0;
    //public int ondeEstou = 0;

    public bool jogoComecou;
    private bool adsUmaVez = false;

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
        pos = GameObject.Find("PosStart").GetComponent<Transform>();
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if(OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 7 && OndeEstou.instance.fase != 2)
        {
            pos = GameObject.Find("PosStart").GetComponent<Transform>();
            StartGame();
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        ScoreManager.instance.GameStartScoreM();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        NascBolas();

        if(chutesBola <= 0 && win == false)
        {
            GameOver();
        }

        if(win == true && bolasEmCena == 1 && OndeEstou.instance.fase != 7)
        {
            WinGame();
        }
    }
    
    void NascBolas()
    {
        if(OndeEstou.instance.fase >= 3 && OndeEstou.instance.fase != 7)
        {
            if(chutesBola > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= 0.05f)
            {
                //Seleciona qual dos prefabs existente, de acordo com o índice do vetor de prefabs
                Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
        else
        {
            if (chutesBola > 0 && bolasEmCena == 0 && OndeEstou.instance.fase != 7)
            {
                Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
    }

    void GameOver()
    {
        UIManager.instance.GameOverUI();
        jogoComecou = false;
        if(adsUmaVez == false)
        {
            AdsUnity.instance.ShowAds();
            adsUmaVez = true;
        }    
    }
    void WinGame()
    {
        UIManager.instance.WinGameUI();
        jogoComecou = false;
    }

    void StartGame()
    {
        jogoComecou = true;
        chutesBola = 2;
        bolasEmCena = 0;
        win = false;
        UIManager.instance.StartUI();
        adsUmaVez = false;
        Time.timeScale = 1;
    }
}
