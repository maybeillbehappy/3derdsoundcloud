using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControl : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] CinemachineCamera cinCamera;

    [Header("Moving Settings")]
    private float _currentSpeed;
    private Vector2 _moveInput;
    private Vector3 _move;
    public float Speed;
    public float RunSpeed;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentSpeed = Speed;
    }
    void OnMove(InputValue input)
    {
        Debug.Log("OnMove is detected");
        _moveInput = new Vector2(input.Get<Vector2>().x, input.Get<Vector2>().y);
    }
    void OnLook(InputValue input)
    {
    }
    void OnSprint(InputValue input)
    {
        _currentSpeed = (input.isPressed) ? RunSpeed : Speed;
    }
    void Update()
    {   
        _move =  (_moveInput.y * cinCamera.transform.forward + _moveInput.x * cinCamera.transform.right);
        _move.y = 0;
        characterController.Move(_move * _currentSpeed * Time.deltaTime);
    } 
        
}
