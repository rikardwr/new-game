using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInput : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool attack;
    public bool block;
    public bool takeDamage;
    [Header("Movement Settings")]
    public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
#endif

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
        // Debug.Log("NewInput -> OnMove");
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnAttack(InputValue value)
    {
        attackInput(value.isPressed);
        // Debug.Log("newInput => OnAttack");
    }

    public void OnBlock(InputValue value)
    {
        blockInput(value.isPressed);
        // Debug.Log("newInput => OnBlock");
    }



    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void attackInput(bool newAttackState)
    {
        attack = newAttackState;
        Debug.Log("NewInput => attackInput");
    }
    
    public void blockInput(bool newBlockState)
    {
        block = newBlockState;
        Debug.Log("NewInput => blockInput");

    }
}