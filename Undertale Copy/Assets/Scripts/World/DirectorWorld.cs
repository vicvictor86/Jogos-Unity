﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorWorld : MonoBehaviour
{
    public static DirectorWorld instance;
    
    [Header("Player Informations")] [SerializeField]
    public GameObject player;
    [SerializeField] public int lifePlayer = 0;
    [SerializeField] public int lifeMax = 0 ;
    [SerializeField] public double power = 0.0 ;

    [SerializeField] public GameObject battleScreenPrefab = null;
    [SerializeField] public List<GameObject> worldObjects = new List<GameObject>();

    public int indexOfScene = 0;
    
    public GameObject battleScreenActual = null;
    public GameObject JojoEffectActual = null;
    public SoundSystem soundSystem = null;
    public GameObject spawnManager = null;

    [Header("Talk Objects")]
    public GameObject conversationWithNpc;
    public bool isReading;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += DefineObjectsDirectorWorld;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DefineObjectsDirectorWorld(Scene scene, LoadSceneMode mode)
    {
        battleScreenActual = GameObject.Find("BattleScreen");
        if (battleScreenActual)
        {
            battleScreenActual.SetActive(false);
        }

        soundSystem = GameObject.Find("SoundSystem").GetComponent<SoundSystem>();
        JojoEffectActual = GameObject.Find("JojoEffect");
        spawnManager = GameObject.Find("SpawnManager");

        GameObject parent = GameObject.FindWithTag("World");
        worldObjects.Clear();
        if (parent)
        {
            foreach (Transform child in parent.transform)
            {
                worldObjects.Add(child.gameObject);
            }
        }
    }

    public void SetBattleScreen(bool boolean)
    {
        battleScreenActual.SetActive(boolean);
    }

    public async void StartJojoEffect()
    {
        JojoEffectActual.GetComponent<JojoEffect>().StartingBattle();
    }

    public void PauseAudios()
    {
        soundSystem.GetComponent<SoundSystem>().PauseAudios();
    }
    
    public AudioClip PlayAudio(string sound)
    {
        return soundSystem.GetComponent<SoundSystem>().PlayAudio(sound);
    }

    public void SetNextLevel(bool boolean)
    {
        spawnManager.GetComponent<SpawnManager>().nextLevel = boolean;
    }
}