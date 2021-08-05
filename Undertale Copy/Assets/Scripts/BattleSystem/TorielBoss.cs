using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorielBoss : MonoBehaviour
{

    public double life;
    public GameObject fireToriel;

    public Vector3 center;
    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        center = new Vector3(634, 505, 0);
        size = new Vector3(250, 78, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    public void Attack()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        Instantiate(fireToriel, pos, Quaternion.identity);
    }

    public void TakeDamage(double damage)
    {
        this.life -= damage;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(center, size);
    }
}
