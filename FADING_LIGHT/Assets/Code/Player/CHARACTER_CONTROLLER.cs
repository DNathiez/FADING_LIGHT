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

   [Header("State")] 
   public CLASS currentClass;
   public int currentLevel;
   public int currentExperience;
   private int experienceNecessary;
   
   public int currentHealth;

   [Header("Stats")]
   public int attackPower;
   public int magicPower;
   public int physicalResistance;
   public int magicalResistance;
   public int cooldownReduce;
   public int endurance;
   public int armor;
   public int strength;
   public int agility;
   public int intellect;
   public int haste;
   public int mastery;
   public int spirit;
   public int versatility;
   public int criticalStrike;
   
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
   
   [Header("Spell")]
   public List<SpellList> spellList;

   [Header("Gear")] 
   public ITEM_WEAPON currentWeapon;

   [Serializable] public class SpellList
   {
      public CAPACITY spell;
      public int levelRequirement;
      public bool onCooldown;
   }
   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Start()
   {
      Initialize();
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
   
   //Using Method
   public void UseCapacity(CAPACITY capacityToUse)
   {
      foreach (SpellList s in spellList)
      {
         if (s.spell == capacityToUse)
         {
            if (!s.onCooldown)
            {
               if (target == null)
               {
                  target = transform;
               }
                    
               s.spell.UseCapacity(this, target);
               StartCoroutine(SetCooldown(s));
            }
            else
            {
               Debug.Log(s.spell + " is already on cooldown");
            }
         }
      }
   }

    
   IEnumerator SetCooldown(SpellList spell)
   {
      Debug.Log(spell.spell.name + " on cooldown, is cooldown is about " + spell.spell.cooldown + " seconds");
      spell.onCooldown = true;
      yield return new WaitForSeconds(spell.spell.cooldown);
      Debug.Log(spell.spell.name + " cooldown is over");
      spell.onCooldown = false;
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
   
   //Gearing
   void ChangeWeapon(ITEM_WEAPON newWeapon)
   {
      foreach (CLASS.WeaponUsable wu in currentClass.weaponsUsable)
      {
         if (newWeapon.weaponType == wu.weaponType && newWeapon.handType == wu.handType)
         {
            currentWeapon = newWeapon;
         }
         else
         {
            Debug.LogError("You can't equip this.");
         }
      }
   }

   void ChangeGear()
   {
      
   }

   void UseConsumable()
   {
      
   }
   
   //Maths
   void CalculateStats()
   {
      attackPower = Mathf.FloorToInt(strength * ((currentWeapon.minAttackDamage + currentWeapon.maxAttackDamage) / 2) * currentClass.apFactor);
      magicPower = Mathf.FloorToInt(intellect * ((currentWeapon.minAttackDamage + currentWeapon.maxAttackDamage) / 2) * currentClass.mpFactor);
      physicalResistance = Mathf.FloorToInt((armor + versatility + mastery) * currentClass.physicResistFactor);
      magicalResistance = Mathf.FloorToInt((armor + versatility + spirit) * currentClass.magicResistFactor);
   }
   
   //Initialiaze

   void Initialize()
   {
      //Set class stats
      endurance = currentClass.endurance;
      armor = currentClass.armor;
      strength = currentClass.strength;
      agility = currentClass.agility;
      intellect = currentClass.intellect;
      haste = currentClass.haste;
      mastery = currentClass.mastery;
      spirit = currentClass.spirit;
      versatility = currentClass.versatility;
      criticalStrike = currentClass.criticalStrike;
      
      CalculateStats();
   }
}
