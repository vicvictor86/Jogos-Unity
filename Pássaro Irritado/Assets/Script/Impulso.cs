using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{
    private Rigidbody2D passaro;
    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        passaro = GetComponent<Rigidbody2D>();
        direction = passaro.transform.position.x < 0 ? 1 : -1;
        passaro.AddForce(new Vector2(Random.Range(5, 11) * direction, Random.Range(5, 11)),ForceMode2D.Impulse);
    }
}
