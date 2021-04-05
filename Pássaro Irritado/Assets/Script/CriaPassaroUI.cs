 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriaPassaroUI : MonoBehaviour
{

    public GameObject[] passaros;
    public GameObject nascE, nascD;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TiroPassaro", 2f, 2f);
    }

    void TiroPassaro()
    {
        if(gameObject.name == "NascPointE")
        {
           Instantiate(passaros[Random.Range(0, 3)], transform.position, Quaternion.identity);
        }
        if(gameObject.name == "NascPointD")
        {
            Instantiate(passaros[Random.Range(0, 3)], transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
}
