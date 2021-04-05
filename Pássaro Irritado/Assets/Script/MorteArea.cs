using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteArea : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Passa"))
        {
            Destroy(collision.gameObject);
        }
    }
}
