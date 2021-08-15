using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldSprite : MonoBehaviour
{
    [SerializeField] private Diretor diretor = null;

    public void EndBattle()
    {
        diretor.StartEndBattle();
    }
}
