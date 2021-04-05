using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{

    public Text text;
    public Touch toque;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            toque = Input.GetTouch(0);

            switch (toque.phase)
            {
                case TouchPhase.Began:
                    text.text = "Began";
                    break;
                case TouchPhase.Ended:
                    text.text = "Ended";
                    break;
                case TouchPhase.Moved:
                    text.text = "Moved";
                    break;
                case TouchPhase.Stationary:
                    text.text = "Stationary";
                    break;
                case TouchPhase.Canceled:
                    text.text = "Canceled";
                    break;
            }
        }
    }
}
