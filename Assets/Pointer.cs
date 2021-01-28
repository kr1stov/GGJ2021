using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    private PlayerController _player;

    private Vector3 _mousPosInWorld;
    private Vector2 _position;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _mousPosInWorld = Camera.main.ScreenToWorldPoint(_position);
        _mousPosInWorld.z = 0;
        var dir = _mousPosInWorld - _player.transform.position;
        var finalPos = _player.transform.position + dir.normalized;

        // finalPos.x = (int) finalPos.x;
        // finalPos.x += 0.5f;
        // finalPos.y = (int) finalPos.y;
        // finalPos.y += 0.5f;
        //
        // finalPos.z = (int) finalPos.z;

        transform.position = finalPos;
        // transform.position = _mousPosInWorld;

    }
    
    public void OnPosition(InputAction.CallbackContext context)
    {
        _position = context.ReadValue<Vector2>();
    }
}
