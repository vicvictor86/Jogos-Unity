using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ControleDeDificuldade : MonoBehaviour
{

    private float tempoPasssado = 0;
    [SerializeField]
    private float tempoParaDificuldadeMaxima = 0;
    public float Dificuldade {get; private set;}

    private void Update()
    {
        this.tempoPasssado += Time.deltaTime;
        this.Dificuldade = this.tempoPasssado / this.tempoParaDificuldadeMaxima;
        this.Dificuldade = Mathf.Min(1, this.Dificuldade);
    }

    public void ZerarDificuldade()
    {
        this.tempoPasssado = 0;
    }

}
