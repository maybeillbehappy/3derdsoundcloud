using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Build;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class StepSystem
{
    public AudioClip FstStep, SndStep;
    [HideInInspector]
    public AudioClip PreviousStep;
    public void SoundStepFunc(AudioSource audioSrc)
    {
        audioSrc.pitch = Random.Range(0.8f, 0.95f);
        if (PreviousStep == FstStep)
        {
            audioSrc.PlayOneShot(SndStep);
            PreviousStep = SndStep;
        }
        else
        {
            audioSrc.PlayOneShot(FstStep);
            PreviousStep = FstStep;
        }
    }
}
public class MoveControl : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinCamera;
    CharacterController characterController;
    AudioSource audioSource;
    private bool _stepFlag;
    private float _currentSpeed;
    private Vector2 _moveInput;
    private Vector3 _move;



    [Header("Moving Settings")]
    public float Speed;
    public float RunSpeed;
    [Header("Step sound system")] 
    public StepSystem stepSystem;
    public float StepTimer;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentSpeed = Speed;
        stepSystem.PreviousStep = stepSystem.FstStep;
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
        if (_moveInput.magnitude > 0.35f && !_stepFlag) 
        {
            _stepFlag = true;
            StartCoroutine(StepPlayer());
        }
        characterController.Move(_move * _currentSpeed * Time.deltaTime);
    }

    IEnumerator StepPlayer()
    {
        stepSystem.SoundStepFunc(audioSource);
        yield return new WaitForSeconds((_currentSpeed == Speed) ? StepTimer : StepTimer / 2);
        _stepFlag = false;
    }    
}
