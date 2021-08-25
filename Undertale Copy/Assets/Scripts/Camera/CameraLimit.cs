using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Transform player;

    [SerializeField] private float[] xClamp = new float[2];
    [SerializeField] private float[] yClamp = new float[2];

    private void Update()
    {
        Vector3 playerPos = player.position;

        float posX = Mathf.Clamp(playerPos.x, xClamp[0], xClamp[1]);
        float posY = Mathf.Clamp(playerPos.y, yClamp[0], yClamp[1]);

        transform.position = Vector3.Lerp(transform.position, 
                            new Vector3(posX, posY, transform.position.z), speed * Time.deltaTime);
    }
}
