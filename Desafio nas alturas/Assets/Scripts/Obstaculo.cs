using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{

    [SerializeField]
    private VariavelCompartilhadaFloat velocidade = null;
    [SerializeField]
    private float variacaoy = 0;

    private void Awake()
    {
        this.transform.Translate(Vector3.up * Random.Range(-variacaoy, variacaoy));
    }

    private void Update()
    {
        this.transform.Translate(Vector3.left * this.velocidade.valor * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("parede"))
        {
            this.Destruir();
        }
        
    }

    public void Destruir()
    {
        GameObject.Destroy(this.gameObject);
    }
}
