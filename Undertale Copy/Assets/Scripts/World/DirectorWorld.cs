using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Start()
    {
        GameObject parent = GameObject.Find("World");
        worldObjects.Clear();
        foreach (Transform child in parent.transform)
        {
            worldObjects.Add(child.gameObject);
        }
        
        worldObjects.Add(player);
        
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy != null)
        {
            worldObjects.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
