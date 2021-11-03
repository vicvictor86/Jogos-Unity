using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloweyAttack : MonoBehaviour
{

    public GameObject floweyAttackPrefab;

    [Header("Area of Attack")] 
    public GameObject regionLeft;
    public GameObject regionCenter;
    public GameObject regionRight;
    public GameObject regionSideTop;
    public GameObject regionSideCenter;
    public GameObject regionSideBottom;
    
    private Vector3 centerLeft;
    private Vector3 centerRight;
    private Vector3 centerCenter;
    private Vector3 centerSideTop;
    private Vector3 centerSideCenter;
    private Vector3 centerSideBottom;
    
    private Vector3 sizeLeft;
    private Vector3 sizeCenter;
    private Vector3 sizeRight;
    private Vector3 sizeSideTop;
    private Vector3 sizeSideCenter;
    private Vector3 sizeSideBottom;

    // Start is called before the first frame update
    void Start()
    {
        centerLeft = regionLeft.transform.position;
        sizeLeft = regionLeft.transform.lossyScale;
        centerCenter = regionCenter.transform.position;
        sizeCenter = regionCenter.transform.lossyScale;
        centerRight = regionRight.transform.position;
        sizeRight = regionRight.transform.lossyScale;
        
        centerSideTop = regionSideTop.transform.position;
        sizeSideTop = regionSideTop.transform.lossyScale;
        centerSideCenter = regionSideCenter.transform.position;
        sizeSideCenter = regionSideCenter.transform.lossyScale;
        centerSideBottom = regionSideBottom.transform.position;
        sizeSideBottom = regionSideBottom.transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    private Vector3 SpawnShot(Vector3 center, Vector3 size, bool sidesLr)
    {
        if (sidesLr)
        {
            int leftOrRight = Random.Range(0, 2);
            leftOrRight = leftOrRight == 1 ? 1 : -1;
            return center + new Vector3((size.x * leftOrRight)/2, Random.Range(-size.y / 2, size.y / 2), 0);
        }
        return center + new Vector3(Random.Range(-size.x / 2, size.x / 2), size.y / 2, 0);
    }

    private void Attack()
    {
        //Falta ajeitar a scale do tamanho do prefab e a posição de spawn pq não ta spawnando no local certo
        int region = Random.Range(0, 9);
        Vector3 pos = new Vector3();
        bool spawnInBottom = false;
        bool spawnSide = false;
        
        switch (region)
        {
            case 0:
                pos = SpawnShot(centerLeft, sizeLeft, false);
                break;
            case 1:
                pos = SpawnShot(centerCenter, sizeCenter, false);
                break;
            case 2:
                pos = SpawnShot(centerRight, sizeRight, false);
                break;
            case 3:
                pos = SpawnShot(centerLeft, -sizeLeft, false);
                spawnInBottom = true;
                break;
            case 4:
                pos = SpawnShot(centerCenter, -sizeCenter, false);
                spawnInBottom = true;
                break;
            case 5:
                pos = SpawnShot(centerRight, -sizeRight, false);
                spawnInBottom = true;
                break;
            case 6:
                pos = SpawnShot(centerSideTop, sizeSideTop, true);
                spawnSide = true;
                break;
            case 7:
                pos = SpawnShot(centerSideCenter, sizeSideCenter, true);
                spawnSide = true;
                break;
            case 8:
                pos = SpawnShot(centerSideBottom, sizeSideBottom, true);
                spawnSide = true;
                break;
        }
        
        GameObject floweyShotInstantiate = Instantiate(floweyAttackPrefab, pos, Quaternion.identity, regionLeft.transform);
        
        if (spawnInBottom)
        {
            floweyShotInstantiate.GetComponent<Rigidbody2D>().gravityScale *= -1;
            Vector3 shotLocalScale = floweyShotInstantiate.transform.localScale;
            floweyShotInstantiate.transform.localScale = new Vector3(shotLocalScale.x, shotLocalScale.y * -1);
        }
        else if (spawnSide)
        {
            Rigidbody2D rigFloweyShot = floweyShotInstantiate.GetComponent<Rigidbody2D>();
            rigFloweyShot.gravityScale = 0;

            if (pos.x > centerCenter.x)
            {
                rigFloweyShot.AddForce(new Vector2(-200f, 0), ForceMode2D.Impulse);
                floweyShotInstantiate.transform.Rotate(0,0, -90);
            }
            else
            {
                rigFloweyShot.AddForce(new Vector2(200f, 0), ForceMode2D.Impulse);
                floweyShotInstantiate.transform.Rotate(0,0, 90);
            }
        }

    }

    //Caso seja necessário visualizar a área designada, descomentar esse bloco
    public void OnDrawGizmos()
    {
        //Gizmos.DrawCube(centerLeft, sizeLeft);
        //Gizmos.DrawCube(centerCenter, sizeCenter);
        //Gizmos.DrawCube(centerRight, sizeRight);
    }
}
