using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 1.5f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -9.81f;
    private bool canMove = true;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            return; 
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float moveForward = Input.GetAxis("Vertical") * playerSpeed;
        float moveSide = Input.GetAxis("Horizontal") * playerSpeed;
        
        Vector3 move = new Vector3(moveSide, 0, moveForward);

        if(moveForward > 0 )
        {
            move = transform.TransformDirection(move);
        }else{
            move = new Vector3(moveSide, 0, moveForward*3);
            move = transform.TransformDirection(move);
        }

        //Vector3 move = new Vector3(moveSide, 0, moveForward);
    // move = transform.TransformDirection(move);

        if (move != Vector3.zero && groundedPlayer)
        {
            // Move the player
            controller.Move(move * Time.deltaTime);

            // Calculate target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(move);

            // Smoothly rotate towards the target rotation
            float rotationSpeed = 1f; // Adjust this value to control rotation speed
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if(!AnimatorIsPlaying("Walk")){
                animator.Play("Walk");
            }           
        } else if(move == Vector3.zero){
             if(!AnimatorIsPlaying("Idle")){
                animator.Play("Idle");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            if(!AnimatorIsPlaying("Jump")){
                animator.Play("Jump");
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public void FreezeMovement()
    {
        canMove = false;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }
}