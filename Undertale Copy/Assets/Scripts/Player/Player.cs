using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class Player : MonoBehaviour
{//UNDERTALE COPY

    [Header("Player Informations")]
    [SerializeField] private float speed;
    
    
    [Header("GameObjects")]
    [SerializeField] private GameObject battleScreen = null;
    [SerializeField] private SoundSystem soundSystem = null;
    [SerializeField] private JojoEffect jojoEffect = null;

    private Rigidbody2D rig;
    private float movimentoH;
    private float movimentoV;

    private bool alreadyBattle = false;

    void Start()
    { 
        speed = 100;
        rig = this.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        MovementPlayer();
        AnimationMovement();
    }

    private void AnimationMovement()
    {
        if (movimentoH > 0)
        {
            GetComponent<Animator>().SetBool("MovingRight", true);
            GetComponent<Animator>().SetBool("MovingLeft", false);
        }
        if (movimentoH < 0)
        {
            GetComponent<Animator>().SetBool("MovingLeft", true);
            GetComponent<Animator>().SetBool("MovingRight", false);
        }
        if (movimentoV > 0)
        {
            GetComponent<Animator>().SetBool("MovingUp", true);
            GetComponent<Animator>().SetBool("MovingDown", false);
        }
        if (movimentoV < 0)
        {
            GetComponent<Animator>().SetBool("MovingDown", true);
            GetComponent<Animator>().SetBool("MovingUp", false);
        }
        if (movimentoH == 0)
        {
            GetComponent<Animator>().SetBool("MovingRight", false);
            GetComponent<Animator>().SetBool("MovingLeft", false);
        }
        if (movimentoV == 0)
        {
            GetComponent<Animator>().SetBool("MovingUp", false);
            GetComponent<Animator>().SetBool("MovingDown", false);
        }
    }

    private void MovementPlayer()
    {
        movimentoH = Input.GetAxisRaw("Horizontal");
        movimentoV = Input.GetAxisRaw("Vertical");

        rig.velocity = new Vector2(movimentoH * speed, movimentoV * speed);
    }
    
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Toriel") && alreadyBattle == false)
        {
            jojoEffect.StartingBattle();

            float timeFlick = 2f;
            await new WaitForSeconds(timeFlick);
            
            AudioClip audioBattle = soundSystem.PlayAudio("StartingBattle");
            
            await new WaitForSeconds(audioBattle.length);
            
            battleScreen.SetActive(true);
            GameObject.Find("OpenWorldScreen").SetActive(false);
            alreadyBattle = true;
        }
    }
}
