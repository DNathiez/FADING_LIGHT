using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CHARACTER_CONTROLLER : MonoBehaviour
{
   //Component
   private Rigidbody rb;
   
   [Header("Input")] 
   private Vector2 mousePosition;
   
   [Header("Movement")]
   public float currentSpeed;
   private bool moving;
   private Vector3 dir;
   public float rotationSpeed;
   private float rotationAngle;
   private bool rotating;
   
   [Header("Targeting")] 
   public Transform target;
   
   [Header("Jump")] 
   private bool canJump;
   public float jumpSpeed;
   public float jumpLenght;
   

   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      if (moving)
      {
         MovePlayer();
      }
      
      rotating = rotationAngle != 0;
      
      if (rotating)
      {
         RotatePlayer();
      }
      
      GroundDetection();
   }

   private void MovePlayer()
   {
      Vector3 moveDir = transform.right * dir.x + transform.forward * dir.z;
      transform.position += (moveDir * currentSpeed * Time.deltaTime);
   }

   private void RotatePlayer()
   {
      Vector3 rotation = new Vector3(0, rotationAngle, 0);
      rotation = rotation.normalized;
      transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
   }

   private void GroundDetection()
   {
      RaycastHit hit;
      if (Physics.Raycast(transform.position, Vector3.down, out hit))
      {
         float distanceToHit = Vector3.Distance(transform.position, hit.point);
            
         if (distanceToHit <= 1.1)
         {
            canJump = true;
         }
         else
         {
            canJump = false;
         }
      }
      
      //if(GetComponent<SpecialGround>) set the special ground modification to the player moving behaviour
   }
   
   
   //Invoke Unity Events
   
   public void GetDirection(InputAction.CallbackContext ctx)
   {
      //Get the input movement direction
      
      if (ctx.performed)
      {
         dir = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
         moving = true;
      }

      if (ctx.canceled)
      {
         dir = Vector3.zero;
         moving = false;
      }
   }

   //Check if can't switch these two in one by using a Vector2 or something like
   public void GetLeftRotation(InputAction.CallbackContext ctx)
   {
      if (ctx.performed)
      {
         rotationAngle -= 1;
      }

      if (ctx.canceled)
      {
         rotationAngle += 1;
      }
   }
   
   public void GetRightRotation(InputAction.CallbackContext ctx)
   {
      if (ctx.performed)
      {
         rotationAngle += 1;
      }

      if (ctx.canceled)
      {
         rotationAngle -= 1;
      }
   }

   public void ReadMousePosition(InputAction.CallbackContext ctx)
   {
      mousePosition = ctx.ReadValue<Vector2>();
   }

   public void Jump()
   {
      if (canJump)
      {
         rb.AddForce(new Vector3(0, jumpLenght, 0) * jumpSpeed, ForceMode.Impulse);
      }
   }

   public void SelectTarget(InputAction.CallbackContext ctx)
   {
      if (ctx.started)
      {
         Ray ray = Camera.main.ScreenPointToRay(mousePosition);
         RaycastHit mouseHit;
      
         if (Physics.Raycast(ray, out mouseHit))
         {
            if (mouseHit.transform.CompareTag("Targetable"))
            {
               target = mouseHit.transform;
            }
            else
            {
               target = null;
            }
         }
      
         HUD_MANAGER.instance.UpdateTargetHUD();
      }
      
   }
   
   
}
