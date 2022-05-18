using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PLAYER_MANAGER : MonoBehaviour
{
    private PlayerInputs inputs;

    //Component
    private Rigidbody rb;

    [Header("State")] 
    public int maxHealth;
    public int currentHealth; //
    public int maxMana;
    public int currentMana; //
    
    [Header("Level")]
    public int currentLevel;
    public int expRequirement;
    public int expCurrent;
    
    [Header("Movement")] public float speed;
    public float currentSpeed; //
    Vector3 direction; //

    public float jumpSpeed;
    public float jumpLenght;
    public bool canJump; //

    public float rotationSpeed;
    public bool rotate; //
    private Vector3 rotationAngle;

    public Transform currentTarget; //

    public List<SpellList> spellList;

    [Serializable] public class SpellList
    {
        public CAPACITY spell;
        public int levelRequirement;
        public bool onCooldown;
    }
    
    private void Awake()
    {
        inputs = new PlayerInputs();
        rb = GetComponent<Rigidbody>();
        
        Initialize();
    }

    private void Update()
    {
        //Movement
        inputs.Normal.Move.performed += context => direction = new Vector3(context.ReadValue<Vector2>().x, 0,context.ReadValue<Vector2>().y);
        inputs.Normal.Move.canceled += _ => direction = Vector3.zero;
        Move(direction);

        //Rotation
        inputs.Normal.RotateLeft.performed += context => rotationAngle = new Vector3(0, -1, 0);
        inputs.Normal.RotateLeft.performed += context => rotate = true; 
        inputs.Normal.RotateLeft.canceled += context => rotate = false;
        inputs.Normal.RotateRight.performed += context => rotationAngle =new Vector3(0, 1, 0);
        inputs.Normal.RotateRight.performed += context => rotate = true; 
        inputs.Normal.RotateRight.canceled += context => rotate = false;
        
        Rotate(rotationAngle);
        
        //Jump
        
        inputs.Normal.Jump.started += context => Jump(new Vector3(0, jumpLenght, 0));
        
        //Mouse

        Vector3 mousePosition = inputs.Normal.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit mouseHit;

        if (Physics.Raycast(ray, out mouseHit))
        {
            if (mouseHit.transform.CompareTag("Targetable"))
            {
                if (inputs.Normal.Select.IsPressed())
                {
                    currentTarget = mouseHit.transform;
                    
                }
            }
            else
            {
                if (inputs.Normal.Select.IsPressed())
                {
                    currentTarget = null;
                    
                }            
            }
        }
        
        //Detection
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
    }

    void Move(Vector3 dir)
    {
        dir = transform.right * dir.x + transform.forward * dir.z;
        transform.position += (dir * currentSpeed * Time.deltaTime);
        
    }

    void Jump(Vector3 jumpDir)
    {
        if (canJump)
        {
            rb.AddForce(jumpDir * jumpSpeed, ForceMode.Impulse);
        }
    }

    void Rotate(Vector3 rotation)
    {
        if (rotate)
        {
            rotation = rotation.normalized;
            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        }
    }

    public void UseCapacity(CAPACITY capacityToUse)
    {
        foreach (SpellList s in spellList)
        {
            if (s.spell == capacityToUse)
            {
                if (!s.onCooldown)
                {
                    if (currentTarget == null)
                    {
                        currentTarget = transform;
                    }
                    
                    s.spell.UseCapacity(this, currentTarget);
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

    void Initialize()
    {
        currentSpeed = speed;
    }

    #region INITILISATION
    
    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
    
    #endregion

    
}
