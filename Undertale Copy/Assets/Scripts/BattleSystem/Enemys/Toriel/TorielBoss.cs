using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorielBoss : Enemy
{
    [Header("Toriel Informations")]
    [SerializeField] private GameObject fireToriel = null;

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

    private void Attack()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        Instantiate(fireToriel, pos, Quaternion.identity);
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(center, size);
    }

    public override string GetName()
    {
        return "Toriel";
    }
    
    public override string TextBeggin()
    {
        return "Toriel ta tiltada contigo";
    }

    public override string TextConviced()
    {
        return "Toriel nao ta mais tiltada, ces fizeram as pazes";
    }

    public override string TextNoConviced()
    {
        return "Toriel ainda ta tiltada contigo, hora da porrada";
    }

    public override string TextTalking()
    {
        return "Você tenta conversar com Toriel, sem sucesso.";
    }
}
