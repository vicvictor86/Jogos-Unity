using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTxtInfo : MonoBehaviour
{

    private Vector3 pos;
    private RectTransform rt;
    private bool libera;
    private GameObject btnBlock, nomeGame;

    [SerializeField]private RectTransform uiRect1, uiRect2;

    // Start is called before the first frame update
    void Awake()
    {
        uiRect1 = GameObject.FindGameObjectWithTag("canvasBack").GetComponent<RectTransform>();
        uiRect2 = GameObject.FindGameObjectWithTag("infoTxt").GetComponent<RectTransform>();
        btnBlock = GameObject.FindGameObjectWithTag("btnBlock");
        btnBlock.SetActive(false);
        nomeGame = GameObject.FindGameObjectWithTag("nomeGame");

        rt = GetComponent<RectTransform>();
        pos = rt.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (libera)
        {
            transform.Translate(0, 1 * Time.deltaTime, 0);
        }
        else
        {
            rt.anchoredPosition = pos;
        }

        if(!RectOverLap(uiRect1, uiRect2))
        {
            rt.anchoredPosition = pos;
        }
    }

    public void LiberaMov()
    {
        btnBlock.SetActive(true);
        nomeGame.SetActive(false);
        libera = true;
        ConfigMenu.instance.clicked = false;
        ConfigMenu.instance.anim.Play("ConfigAnimReverse");
    }

    public void BlockMov()
    {
        nomeGame.SetActive(true);
        btnBlock.SetActive(false);
        libera = false;
    }

    bool RectOverLap(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }
}
