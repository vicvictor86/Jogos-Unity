using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeclaPressionada : MonoBehaviour
{
    [SerializeField]
    private KeyCode tecla = 0;
    [SerializeField]
    private UnityEvent aoPressionarTecla = null;

    void Update()
    {
        if (Input.GetKeyDown(this.tecla))
        {
            this.aoPressionarTecla.Invoke();
        }
    }
}
