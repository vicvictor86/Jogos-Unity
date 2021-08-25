using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public bool nextLevel = false;
    private GameObject player = null;
    public static SpawnManager instance = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += PositionPlayer;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void PositionPlayer(Scene scene, LoadSceneMode mode)
    {
        Transform localSpawn = null;
       // Debug.Log(nextLevel);
        localSpawn = nextLevel ? GameObject.FindWithTag("SpawnPointBegin").transform : GameObject.FindWithTag("SpawnPointBack").transform;
        player = GameObject.FindWithTag("Player");

        player.transform.position = localSpawn.position;
    }
}
