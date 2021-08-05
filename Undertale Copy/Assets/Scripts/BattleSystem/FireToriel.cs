using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireToriel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHeart>().Damage(1);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("EndOfFire"))
        {
            Destroy(this.gameObject);
        }
    }
}
