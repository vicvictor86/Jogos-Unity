using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsUnity : MonoBehaviour, IUnityAdsListener
{
    public static AdsUnity instance;

    [SerializeField] string _gameID = "4038741";
    [SerializeField] string myPlacementID = "rewardedVideo";
    [SerializeField] private Button btnAds;
    public bool adsBtnAcionado = false;

    public void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameID);
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += PegaBtn;
    }

    void PegaBtn(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 7)
        {         
            btnAds = GameObject.Find("AdsBtn").GetComponent<Button>();
            btnAds.onClick.AddListener(AdsBtn);
        }
    }

    public void OnUnityAdsDidFinish(string PlacementID, ShowResult ShowResult)
    {
        if(ShowResult == ShowResult.Finished && PlacementID == myPlacementID)
        {
            ScoreManager.instance.ColetaMoedas(100);
            Debug.Log("Você ganhou 100 moedas");
        }
        else if(ShowResult == ShowResult.Skipped)
        {
            Debug.Log("Você pulou o anúncio");
        }
        else if(ShowResult == ShowResult.Failed)
        {
            Debug.LogWarning("Failed");
        }
    }

    void AdsBtn()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
            adsBtnAcionado = true;
        }
    }

    public void ShowAds()
    {
        if(PlayerPrefs.HasKey("AdsUnity"))
        {
            if (PlayerPrefs.GetInt("AdsUnity") == 3)
            {                
                if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                }

                PlayerPrefs.SetInt("AdsUnity", 1);
            }
            else
            {
                PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("AdsUnity", 1);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ready Ads: " + placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Start Ads: " + placementId);
    }
}
