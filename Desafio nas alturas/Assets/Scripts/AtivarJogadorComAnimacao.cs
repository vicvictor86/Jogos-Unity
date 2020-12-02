using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtivarJogadorComAnimacao : MonoBehaviour
{
    [SerializeField]
    private UnityEvent aoAcabarAnimacao = null;

    public void AtivarJogador()
    {
        this.aoAcabarAnimacao.Invoke();
    }
}
