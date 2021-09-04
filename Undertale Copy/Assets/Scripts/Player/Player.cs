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
    private GameObject conversationInScene;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (DirectorWorld.instance.playerCanMove)
        {
            MovementPlayer();
        }
        if(DirectorWorld.instance.isReading)
        {
            StopPlayerMovement();
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DialogueSystem.instance.NextSentence();
            }
        }
    }

    private void StopPlayerMovement()
    {
        rig.velocity = Vector2.zero;
        movement = Vector2.zero;
        AnimationMovement();
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
    
    private void OnTriggerEnter2D(Collider2D colliderOther)
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
        
        if (colliderOther.gameObject.CompareTag("Enemy") && DirectorWorld.instance.playerAlreadyBattle == false)
        {
            DirectorWorld.instance.playerCanMove = false;
            conversationInScene = colliderOther.gameObject.GetComponent<Enemy>().ContactWithPlayer();
        }
    }

    public async void StartingBattle()
    {
        StopPlayerMovement();
        
        DirectorWorld.instance.PauseAudios();
        DirectorWorld.instance.StartJojoEffect();

        float timeFlick = 2f;
        await new WaitForSeconds(timeFlick);
        AudioClip audioBattle = DirectorWorld.instance.PlayAudio("StartingBattle", false);
        await new WaitForSeconds(audioBattle.length);
            
        DirectorWorld.instance.ChangeScreen("World", "BattleScreenParent");

        float timeRemaingToFirstAudio = 27f;
        await new WaitForSeconds(timeRemaingToFirstAudio);
        DirectorWorld.instance.PlayAudio("JojoThemeComplete", true);
    }
}
