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
    private GameObject conversationInScene;
    private bool canMove = true;
    
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (canMove)
        {
            MovementPlayer();
        }
        else if(DirectorWorld.instance.isReading)
        {
            rig.velocity = Vector2.zero;
            movement = Vector2.zero;
            AnimationMovement();
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DialogueSystem.instance.NextSentence();
            }
        }
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
        
        if (colliderOther.gameObject.CompareTag("Enemy") && alreadyBattle == false)
        {
            canMove = false;
            conversationInScene = colliderOther.gameObject.GetComponent<Enemy>().ContactWithPlayer(); 
            //StartingBattle();
        }
    }

    public async void StartingBattle()
    {
        DirectorWorld.instance.PauseAudios();
        DirectorWorld.instance.StartJojoEffect();

        float timeFlick = 2f;
        await new WaitForSeconds(timeFlick);
            
        AudioClip audioBattle = DirectorWorld.instance.PlayAudio("StartingBattle");
            
        await new WaitForSeconds(audioBattle.length);
            
        DirectorWorld.instance.SetBattleScreen(true);
        GameObject.Find("Diretor").GetComponent<Diretor>().CatchWorldScreen();
        
        canMove = true;
        GameObject.Find("World").SetActive(false);
        
        alreadyBattle = true;
    }
}
