using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleComputador : MonoBehaviour
{
    [SerializeField]
    private float intervalo = 0;
    private Aviao aviao;
    void Start()
    {
        this.aviao = GetComponent<Aviao>();
        StartCoroutine(this.Impulsionar());
    }

    
    private IEnumerator Impulsionar()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalo);
            this.aviao.DarImpulso();
        }
        
    }
}
