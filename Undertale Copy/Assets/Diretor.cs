using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diretor : MonoBehaviour
{

    [SerializeField]
    private GameObject MenuEsc = null;
    private bool estaPausado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            estaPausado = !estaPausado;
            MenuEsc.SetActive(estaPausado);    
        }
    }
}
