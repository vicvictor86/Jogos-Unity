using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCode : MonoBehaviour
{

    private int limite;
    private SpriteRenderer spriteR;
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private GameObject bomb = null;

    // Start is called before the first frame update
    void Start()
    {
        limite = 0;
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = sprites[0];
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > 4 && collision.relativeVelocity.magnitude < 10)
        {
            if(limite < sprites.Length - 1)
            {
                limite++;
                spriteR.sprite = sprites[limite];
            }
            else if (limite == sprites.Length - 1)
            {
                Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if(collision.relativeVelocity.magnitude > 12 && collision.gameObject.CompareTag("Player"))
        {
            Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
