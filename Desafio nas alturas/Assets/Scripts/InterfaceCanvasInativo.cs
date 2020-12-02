using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceCanvasInativo : MonoBehaviour
{
    [SerializeField]
    private GameObject fundo = null;
    [SerializeField]
    private Text quantidadeParaReviver = null;
    private Canvas canvas;

    private void Awake()
    {
        this.canvas = GetComponent<Canvas>();
    }

    public void Mostrar(Camera camera)
    {
        this.fundo.SetActive(true);
        this.canvas.worldCamera = camera;
    }

    public void Sumir()
    {
        this.fundo.SetActive(false);
    }

    public void AtualizarTexto(int quantidadeReviver)
    {
        this.quantidadeParaReviver.text = quantidadeReviver.ToString();
    }

}
