using System;
using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class Player : MonoBehaviour
{//UNDERTALE COPY

    [Header("Player Informations")]
    [SerializeField] private float speed;

    private Rigidbody2D rig;
    private Vector2 movement;
    private Animator animator;
    
    private bool alreadyBattle = false;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        MovementPlayer();
    }

    private void AnimationMovement()
    {
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);       
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void MovementPlayer()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        rig.velocity = new Vector2(movement.x * speed, movement.y * speed);
        AnimationMovement();
    }
    
    private async void OnTriggerEnter2D(Collider2D colliderOther)
    {
        if (colliderOther.CompareTag("NextLevel"))
        {
            DirectorWorld.instance.SetNextLevel(true);
            LevelManager.NextLevel();
        }

        if (colliderOther.CompareTag("PreviousLevel"))
        {
            DirectorWorld.instance.SetNextLevel(false);
            LevelManager.PreviousLevel();
        }
        
        if (colliderOther.gameObject.CompareTag("Enemy") && alreadyBattle == false)
        {
            DirectorWorld.instance.PauseAudios();
            DirectorWorld.instance.StartJojoEffect();

            float timeFlick = 2f;
            await new WaitForSeconds(timeFlick);
            
            AudioClip audioBattle = DirectorWorld.instance.PlayAudio("StartingBattle");
            
            await new WaitForSeconds(audioBattle.length);
            
            DirectorWorld.instance.SetBattleScreen(true);
            GameObject.Find("Diretor").GetComponent<Diretor>().CatchWorldScreen();
            GameObject.Find("World").SetActive(false);
            alreadyBattle = true;
        }
    }
}
