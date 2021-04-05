using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [SerializeField] private Transform objE = null, objD = null, passaro = null;


    // Update is called once per frame
    void Update()
    {
        Vector3 poscam = transform.position;
        poscam.x = passaro.position.x;
        poscam.x = Mathf.Clamp(poscam.x, objE.position.x, objD.position.x);
        transform.position = poscam;
    }
}
