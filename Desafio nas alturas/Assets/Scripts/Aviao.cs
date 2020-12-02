using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Aviao : MonoBehaviour
{

    private Rigidbody2D fisica;
    [SerializeField]
    private float forca = 0;
    [SerializeField]
    private UnityEvent AoBater = null;

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

    private void Update()
    {
        this.animacao.SetFloat("VelocidadeY", this.fisica.velocity.y);
    }

    private void FixedUpdate()
    {
        if (devoImpulsionar)
        {
            this.Impulsionar();
        }
    }

    public void DarImpulso()
    {
        this.devoImpulsionar = true;
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
        this.AoBater.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.AoPassarPeloObstaculo.Invoke();
    }
}
