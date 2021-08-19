using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class JojoEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer player = null;
    [SerializeField] private SpriteRenderer enemy = null;
    [SerializeField] private SpriteRenderer backGround = null;

    [SerializeField] private GameObject spawnLetters = null;
    [SerializeField] private GameObject jojoLettersPrefab = null;

    private GameObject jojoLettersGameObject = null;
    private Vector3 positionFinalJojoLetters = new Vector3(224.3f, 375.5f, -516.2276f);
    
    // Start is called before the first frame update
    void Start()
    {
        player = DirectorWorld.instance.player.GetComponent<SpriteRenderer>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<SpriteRenderer>();
        backGround = GameObject.FindWithTag("BackGround").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jojoLettersGameObject != null && jojoLettersGameObject.transform.position == positionFinalJojoLetters)
        {
            Destroy(jojoLettersGameObject);
        }
    }

    public async void StartingBattle()
    {
        GameObject.Find("SoundSystem").GetComponent<SoundSystem>().PlayAudio("GiornoTheme");
        StartCoroutine(Blink(2f));
        
        await new WaitForSeconds(2f);
        
        jojoLettersGameObject = Instantiate(jojoLettersPrefab, spawnLetters.transform.position, Quaternion.identity);
        backGround.enabled = false;
    }
 
    private IEnumerator Blink(float waitTime) 
    {
        float endTime = Time.time + waitTime;
        float seconds = .2f;
        float totalBlink = 0;
        
        while(Time.time < endTime){
            /*player.enabled = false;
            enemy.enabled = false;
            backGround.enabled = false;*/

            foreach (GameObject gameObjectInScene in DirectorWorld.instance.worldObjects)
            {
                if (gameObjectInScene.GetComponent<SpriteRenderer>() != null)
                {
                    gameObjectInScene.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            
            yield return new WaitForSeconds(seconds);
            
            /*player.enabled = true;
            enemy.enabled = true;
            backGround.enabled = true;*/
            
            
            foreach (GameObject gameObjectInScene in DirectorWorld.instance.worldObjects)
            {
                if (gameObjectInScene.GetComponent<SpriteRenderer>() != null)
                {
                    gameObjectInScene.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            yield return new WaitForSeconds(seconds);
            
            totalBlink++;
            if (totalBlink == 3)
            {
                seconds = .1f;
            }
            else if (totalBlink == 12)
            {
                seconds = .07f;
            }
        }
    }
}
