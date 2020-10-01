using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{

    [SerializeField]
    private VariavelCompartilhadaFloat velocidade = null;
    [SerializeField]
    private float variacaoy = 0;
    private Vector3 posicaoAviao;

    private Pontuacao pontuacao;
    private bool pontuou = false;

    private void Awake()
    {
        this.transform.Translate(Vector3.up * Random.Range(-variacaoy, variacaoy));
    }

    public void Start()
    {
        this.posicaoAviao = GameObject.FindObjectOfType<Aviao>().transform.position;
        this.pontuacao = GameObject.FindObjectOfType<Pontuacao>();
    }

    private void Update()
    {
        this.transform.Translate(Vector3.left * this.velocidade.valor * Time.deltaTime);
        if(!pontuou && this.transform.position.x < this.posicaoAviao.x)
        {
            pontuou = true;
            this.pontuacao.AdicionarPontos();
        }
    }

    public void OnTriggerEnter2D(Collider2D outro)
    {
        this.Destruir();
    }

    public void Destruir()
    {
        GameObject.Destroy(this.gameObject);
    }
}
