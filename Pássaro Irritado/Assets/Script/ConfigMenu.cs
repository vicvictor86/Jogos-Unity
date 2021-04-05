using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMenu : MonoBehaviour
{

    public static ConfigMenu instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Animator anim;
    public bool clicked;

    public void ClickedButton()
    {
        if (clicked)
        {
            anim.Play("ConfigAnimReverse");
        }
        else
        {
            anim.Play("ConfigAnim");
        }
        clicked = !clicked;
    }
}
