using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireToriel : MonoBehaviour
{

    private int powerOfToriel;

    // Start is called before the first frame update
    void Start()
    {
        powerOfToriel = GameObject.Find("Toriel").GetComponent<TorielBoss>().GetPowerOfToriel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHeart>().Damage(powerOfToriel);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("EndOfFire"))
        {
            Destroy(this.gameObject);
        }
    }
}
