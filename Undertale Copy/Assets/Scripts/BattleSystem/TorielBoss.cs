using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorielBoss : MonoBehaviour
{
    [Header("Toriel Informations")]
    [SerializeField] private double life;
    [SerializeField] private int powerOfToriel = 0;
    [SerializeField] private GameObject fireToriel = null;
    [SerializeField] private int convincing = 0;

    [Header("Region Of Fire")]
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(970, 656, 0);
        size = new Vector3(250, 78, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Diretor").GetComponent<Diretor>().IsFighting())
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

    public int GetPowerOfToriel()
    {
        return this.powerOfToriel;
    }

    public double GetLife()
    {
        return this.life;
    }

    public int GetConvincing()
    {
        return this.convincing;
    }

    public void Convince(int number)
    {
        this.convincing -= number;
    }
}
