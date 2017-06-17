using System;
using UnityEngine;

    public class Controls : MonoBehaviour
    {
        private TPPMovement movement; 
        public Transform playerCamera;                  
        private Vector3 pcamForward;             
        private Vector3 movementVector;
        private bool willJump;                      
        public string player;
        public GolfHit gh;
        public float speedDecreaseOnAim;

        private void Start()
        {
            movement = GetComponent<TPPMovement>();
        }


        private void Update()
        {
            if (!willJump)
            {
                willJump = Input.GetButtonDown(player + "Jump");
            }
        }


        private void FixedUpdate()
        {
            float h = Input.GetAxis(player + "Horizontal");
            float v = Input.GetAxis(player + "Vertical");

        if (gh.aiming)
            v *= speedDecreaseOnAim;
            
            if (playerCamera != null)
            {
                pcamForward = Vector3.Scale(playerCamera.forward, new Vector3(1, 0, 1)).normalized;
                movementVector = v * pcamForward + h * playerCamera.right;
            }
            else
            {
                movementVector = v * Vector3.forward + h * Vector3.right;
            }

            movement.Move(movementVector, willJump);
            willJump = false;
        }
    }
