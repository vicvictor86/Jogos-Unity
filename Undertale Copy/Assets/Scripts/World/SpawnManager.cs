using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public bool nextLevel = false;
    private GameObject player = null;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += PositionPlayer;
    }
    
    void PositionPlayer(Scene scene, LoadSceneMode mode)
    {
        Transform localSpawn = null;
        localSpawn = nextLevel ? GameObject.FindWithTag("SpawnPointBegin").transform : GameObject.FindWithTag("SpawnPointBack").transform;
        player = GameObject.Find("Player");

        player.transform.position = localSpawn.position;
    }
}
