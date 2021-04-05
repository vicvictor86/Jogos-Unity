using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaManager : MonoBehaviour
{
    [SerializeField] private GameObject bombaFX = null;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bola"))
        {
            Instantiate(bombaFX, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            StartCoroutine(Vida());
        }
    }

    IEnumerator Vida()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject bombaObject = GameObject.Find("BombaFX(Clone)").gameObject;
        Destroy(bombaObject);
        Destroy(this.gameObject);
    }
}
