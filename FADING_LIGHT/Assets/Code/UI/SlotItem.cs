using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IDropHandler
{
    private CanvasGroup canvasG;

    private Button button;

    public CAPACITY capacity;
    
    public InputAction actionButton;

    private CHARACTER_CONTROLLER player;

    private void Awake()
    {
        canvasG = GetComponent<CanvasGroup>();
        button = GetComponent<Button>();
        player = FindObjectOfType<CHARACTER_CONTROLLER>();
    }

    private void OnEnable()
    {
        actionButton.Enable();
    }

    private void OnDisable()
    {
        actionButton.Disable();
    }

    private void Update()
    {
        actionButton.started += context => UseItem();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void UseItem()
    {
        if (capacity != null)
        {
            player.UseCapacity(capacity);
        }
    }
}
