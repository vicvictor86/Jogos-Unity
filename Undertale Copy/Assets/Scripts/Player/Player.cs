using System;
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
    [SerializeField] private SpawnManager spawnManager = null;

    private Rigidbody2D rig;
    private float movimentoH;
    private float movimentoV;
    private Animator animator;
    
    private bool alreadyBattle = false;

    private void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    
    private void Update()
    {
        MovementPlayer();
        AnimationMovement();
    }

    private void AnimationMovement()
    {
        animator.SetFloat("VelocityH", movimentoH);
        animator.SetFloat("VelocityV", movimentoV);
        animator.SetFloat("FacingRight",movimentoH);
        animator.SetFloat("FacingUp", movimentoV);
        
        if(movimentoH == 0 && movimentoV == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }
    }

    private void MovementPlayer()
    {
        movimentoH = Input.GetAxisRaw("Horizontal");
        movimentoV = Input.GetAxisRaw("Vertical");

        rig.velocity = new Vector2(movimentoH * speed, movimentoV * speed);
    }
    
    private async void OnTriggerEnter2D(Collider2D colliderOther)
    {
        if (colliderOther.CompareTag("NextLevel"))
        {
            spawnManager.nextLevel = true;
            LevelManager.NextLevel();
        }

        if (colliderOther.CompareTag("PreviousLevel"))
        {
            spawnManager.nextLevel = false;
            LevelManager.PreviousLevel();
        }
        
        if (colliderOther.gameObject.CompareTag("Enemy") && alreadyBattle == false)
        {
            soundSystem.PauseAudios();
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
