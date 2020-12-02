using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AviaoSinglePlayer : MonoBehaviour
{

    private Rigidbody2D fisica;
    [SerializeField]
    private float forca = 0;
    private DiretorSinglePlayer diretor = null;

    private Vector3 posicaoInicial;
    private bool devoImpulsionar;
    private Animator animacao = null;

    [SerializeField]
    private UnityEvent AoPassarPeloObstaculo = null;

    private void Awake()
    {
        this.fisica = this.GetComponent<Rigidbody2D>();
        this.posicaoInicial = this.transform.position;
        this.animacao = this.GetComponent<Animator>();
    }
    public void Start()
    {
        this.diretor = FindObjectOfType<DiretorSinglePlayer>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.devoImpulsionar = true;
        }
        this.animacao.SetFloat("VelocidadeY", this.fisica.velocity.y);
    }

    private void FixedUpdate()
    {
        if (devoImpulsionar)
        {
            this.Impulsionar();
        }
    }

    public void Reiniciar()
    {
        this.transform.position = this.posicaoInicial;
        this.fisica.simulated = true;
    }

    private void Impulsionar()
    {
        this.fisica.velocity = Vector2.zero;
        this.fisica.AddForce(Vector2.up * this.forca, ForceMode2D.Impulse);
        this.devoImpulsionar = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        this.fisica.simulated = false;
        this.diretor.FinalizarJogo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.AoPassarPeloObstaculo.Invoke();
    }
}