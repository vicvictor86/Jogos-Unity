using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veloz : MonoBehaviour
{
    public Rigidbody2D passaroRb;
    public bool libera = false;
    public int trava = 0;

    // Start is called before the first frame update
    void Start()
    {
        passaroRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && passaroRb.isKinematic == false && trava == 0)
        {
            libera = true;
            trava = 1;
        }
    }

    public void FixedUpdate()
    {
        if (libera)
        {
            passaroRb.velocity *= 2.5f;
            libera = false;
        }
    }
}
