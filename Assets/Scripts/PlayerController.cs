using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlayerController : MonoBehaviour
{
    public CircleCollider2D groundCheck;
    public LayerMask groundLayers;

    public float moveSpeed;

    private Vector3 _direction;

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.transform.position, groundCheck.radius, groundLayers) == null;
    
    private void Update()
    {
        Move(_direction);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
        DebugUtil.Log($"direction:{_direction}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnDig(InputAction.CallbackContext context)
    {
        
    }


    private void Move(Vector2 dir)
    {
        if (_direction.sqrMagnitude < 0.01)
            return;
        
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var move = Vector3.right * scaledMoveSpeed;
        transform.position += move * scaledMoveSpeed;
    }
    
}
