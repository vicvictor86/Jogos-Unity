using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeObstaculosSinglePlayer : MonoBehaviour
{
    [SerializeField]
    private float tempoParaGerarFacil = 0;
    [SerializeField]
    private float tempoParaGerarDificil = 0;
    private ControleDeDificuldade controleDeDificuldade;
    //Prefabs
    [SerializeField]
    private GameObject manualDeInstrucoes = null;
    private float cronometro;

    //Chamado na criação do objeto
    private void Awake()
    {
        this.cronometro = this.tempoParaGerarFacil;
    }

    private void Start()
    {
        this.controleDeDificuldade = GameObject.FindObjectOfType<ControleDeDificuldade>();
    }

    void Update()
    {
        //Faz a contagem do tempo
        this.cronometro -= Time.deltaTime;

        //Ver se o tempo que passou foi o necessário
        if (this.cronometro < 0)
        {
            //Instância um novo objeto, que segue o prefab da referência, e é criado na posição do GameObject
            //Sem receber rotação
            GameObject.Instantiate(this.manualDeInstrucoes, this.transform.position, Quaternion.identity);
            //Reseta o tempo do cronômetro
            this.cronometro = Mathf.Lerp(this.tempoParaGerarFacil,
                                         this.tempoParaGerarDificil,
                                         this.controleDeDificuldade.Dificuldade);
        }



    }
}