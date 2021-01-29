using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    public static EventHandler<Vector3Int> sandTileDugAway;
    public static EventHandler<int> interactedWithItem;


    public GameSettings gameSettings;
    public CircleCollider2D groundCheck;
    public BoxCollider2D groundCheckBox;

    public LayerMask groundLayers;
    
    private Vector3 _move;
    private bool _isRunning = false;
    private bool _jumped = false;
    private Rigidbody2D _rb;
    private Pointer _pointer;

    private bool IsNextToObject => _currentInteractableItem != null;
    private InteractableItem _currentInteractableItem = null;
    
    [FormerlySerializedAs("foreground")] public Tilemap sandTilemap;

    // public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.transform.position, groundCheck.radius, groundLayers) != null;
    public bool IsGrounded => Physics2D.OverlapBox(groundCheckBox.transform.position, groundCheckBox.size,  groundLayers) != null;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pointer = FindObjectOfType<Pointer>();
    }

    private void Update()
    {
        if (SessionManager.Instance.menuController.gameIsPaused)
            return;
        
        Move(_move);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _currentInteractableItem = other.GetComponent<InteractableItem>();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _currentInteractableItem = null;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
        // DebugUtil.Log($"direction:{_move}");
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            _isRunning = true;
        else if (context.canceled)
            _isRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            Jump();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(IsNextToObject)
            InteractWith(_currentInteractableItem);
        
        if (context.started)
            Dig();
    }

    private void Move(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.01)
            return;
        
        var scaledMoveSpeed = (_isRunning ? gameSettings.runSpeed : gameSettings.moveSpeed) * Time.deltaTime;
        var move = new Vector3(dir.x, dir.y, 0) * scaledMoveSpeed;
        transform.position += move;
    }

    private void Dig()
    {
        var t = transform;
        var position = t.position;
        var positionInt = new Vector3Int((int) position.x, (int) position.y, (int) position.z);
        // var digPosition = positionInt + new Vector3Int(1, -1, 0);
        var pt = _pointer.transform;
        var ptPos = pt.position;
        Vector3Int digPosition = Vector3Int.zero;
        digPosition.x = ptPos.x > 0 ? (int) ptPos.x : (int) (ptPos.x - 1);
        digPosition.y = ptPos.y > 0 ? (int) ptPos.y : (int) (ptPos.y - 1);
        digPosition.z = 0;

        sandTileDugAway?.Invoke(this, digPosition);
    }

    private void Jump()
    {
        if(IsGrounded)
            _rb.AddForce(Vector2.up*gameSettings.jumpHeight, ForceMode2D.Impulse);
    }

    private void InteractWith(InteractableItem current)
    {
        interactedWithItem?.Invoke(this, current.gameObject.GetInstanceID());
    }
    
}
