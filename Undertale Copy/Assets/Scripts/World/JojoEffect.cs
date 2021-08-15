using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JojoEffect : MonoBehaviour
{

    [SerializeField] private SpriteRenderer player = null;
    [SerializeField] private SpriteRenderer toriel = null;
    [SerializeField] private SpriteRenderer backGround = null;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartingBattle()
    {
        GameObject.Find("SoundSystem").GetComponent<SoundSystem>().PlayAudio("GiornoTheme");
        StartCoroutine(Blink(2f));
    }
 
    private IEnumerator Blink(float waitTime) 
    {
        float endTime = Time.time + waitTime;
        float seconds = .2f;
        float totalBlink = 0;
        
        while(Time.time < endTime){
            player.enabled = false;
            toriel.enabled = false;
            backGround.enabled = false;
            
            yield return new WaitForSeconds(seconds);
            
            player.enabled = true;
            toriel.enabled = true;
            backGround.enabled = true;
            
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
