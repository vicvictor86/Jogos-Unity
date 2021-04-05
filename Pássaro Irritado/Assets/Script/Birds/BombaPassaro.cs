using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaPassaro : MonoBehaviour
{
    private Rigidbody2D passaroRb;
    public bool libera = false;
    public int trava = 0;
    public GameObject bomba;

    // Start is called before the first frame update
    void Start()
    {
        passaroRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && passaroRb.isKinematic == false && trava == 0)
        {
            libera = true;
            trava = 1;
            GameObject bombaEffect = Instantiate(bomba, transform.position, Quaternion.identity);
            Destroy(bombaEffect, 1);
            Destroy(gameObject);
        }
    }
}
