using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/Input Reader", fileName = "InputReader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions
{
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction AttackEvent;
    public event UnityAction InteractionEvent;

    private float interactionCooldownTimer;
    public bool isInteracting { get; set; }

    private Controls control; 

    private void OnEnable()
    {
        if(control == null)
        {

            control = new Controls();
            control.Gameplay.SetCallbacks(this);
        }
        control.Enable();
    }


    private void OnDisable()
    {
        control.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(AttackEvent != null && context.performed)
        {
            AttackEvent.Invoke();
        }
    }


    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (InteractionEvent != null && context.performed)
        {
            InteractionEvent.Invoke();
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

}
