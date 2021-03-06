﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject weaponCam;
    [SerializeField] TextMeshProUGUI healthText;

    float walkSpeed = 10f;
    float crouchSpeed = 5f;
    float runSpeed = 15f;
    float jumpSpeed = 5f;
    float gravityForce = 20f;

    float inputX, inputY;
    float inputSetX, inputSetY;
    float antiBumpFactor = 0.75f;

    bool isCrouching = false;
    bool isRunning = false;
    bool isAlive = true;
    bool isGrounded = true;
    bool isMoving;

    [Range(0, 100)]
    public int playerHealth = 100;

    CharacterController charController;
    Vector3 movingDirection = Vector3.zero;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        movementSpeed = 10f;
        isMoving = false;
    }
    void Update()
    {

        if (isAlive)
        {
            UpdateHealth();
            PlayerMovement();
            PlayerCrouch();
            PlayerRun();
        }
    }
    private string UpdateHealth()
    {
        return healthText.text = "+" + playerHealth.ToString();
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!isCrouching)
            {
                movingDirection.y = jumpSpeed;

            }
        }
    }
    private void PlayerRun()
    {
        if (!isCrouching)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                isRunning = true;
                movementSpeed = runSpeed;
            }
            else
            {
                isRunning = false;
                movementSpeed = walkSpeed;
            }
        }

    }
    private void PlayerCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                isCrouching = false;
            }
            else
            {
                isCrouching = true;
            }

            CrouchPosition();
        }


    }
    private void CrouchPosition()
    {
        if (isCrouching)
        {
            Vector3 crouchPos = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
            weaponCam.transform.position = crouchPos;
            movementSpeed = crouchSpeed;
        }
        else
        {
            Vector3 nonCrouchPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            weaponCam.transform.position = nonCrouchPos;
            movementSpeed = walkSpeed;
        }
    }
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
            {
                inputSetY = 1f;
            }
            else
            {
                inputSetY = -1f;
            }

        }
        else
        {
            inputSetY = 0f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                inputSetX = -1f;
            }
            else
            {
                inputSetX = 1f;
            }


        }
        else
        {
            inputSetX = 0f;
        }

        inputY = Mathf.Lerp(inputY, inputSetY, Time.deltaTime * 12f);
        inputX = Mathf.Lerp(inputX, inputSetX, Time.deltaTime * 12f);


        if (isGrounded)
        {
            movingDirection = new Vector3(inputX, -antiBumpFactor, inputY);
            movingDirection = transform.TransformDirection(movingDirection) * movementSpeed;

            PlayerJump();

        }
        movingDirection.y -= gravityForce * Time.deltaTime;

        isGrounded = (charController.Move(movingDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

        isMoving = charController.velocity.magnitude > 0.15f;
    }
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(3);

        }
    }
}
