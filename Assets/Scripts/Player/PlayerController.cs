using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float jumpHeight = 2.5f;
    public float gravity = -20f;
    public float _sprintTime = 7f;
    public float _maxSprintTime = 7f;
    public float _sprintRecoveryRate = 2f;   // Скорость восстановления выносливости

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float rotationSmoothTime = 0.1f;

    public Slider staminaSlider;

    private Vector3 _velocity;
    private float _smoothVelocity;
    private bool _isSprinting;
    private bool _wasSprinting;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleGravity();
        HandleRotation();

        // Если удерживаем Shift — вызываем спринт
        if (_isSprinting)
        {
            Sprint();
        }

        if (!_isSprinting && _sprintTime < _maxSprintTime)
        {
            _sprintTime += _sprintRecoveryRate * Time.deltaTime;
            Debug.Log($"[Stamina] Recovering: {_sprintTime}");
            if (_sprintTime > _maxSprintTime)
            {
                _sprintTime = _maxSprintTime;
            }
        }


        if (_isSprinting)
        {
            _wasSprinting = true;
        }

        // Обновление UI
        if (staminaSlider != null)
        {
            staminaSlider.value = _sprintTime / _maxSprintTime;
        }

        _controller.Move(_velocity * Time.deltaTime);
    }


    public void SetMoveInput(Vector2 input) => _moveInput = input;
    public void SetLookInput(Vector2 input) => _lookInput = input;

    public void SetSprint(bool isSprinting)
    {
        Debug.Log($"[SetSprint] Called with: {isSprinting}");
        if (isSprinting && _sprintTime > 0)
        {
            _isSprinting = true;
            _wasSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }
    }

    public void Jump()
    {
        if (_controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (_velocity.y < 0)
        {
            _velocity.y += gravity * Time.deltaTime * 1.5f;
        }
    }

    private void HandleMovement()
    {
        if (!_isSprinting) 
        {
            Move(moveSpeed);
        }
    }

    public void Sprint()
    {
        if (_sprintTime > 0)
        {
            Move(moveSpeed * sprintMultiplier);
            _sprintTime -= Time.deltaTime;
            if (_sprintTime < 0) _sprintTime = 0;
        }
        else
        {
            _isSprinting = false; 
        }
    }

    private void Move(float speed)
    {
        Vector3 direction = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        _controller.Move(direction * speed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravity * Time.deltaTime;
        _velocity.y = Mathf.Clamp(_velocity.y, -50f, 50f);
    }

    private void HandleRotation()
    {
        float mouseX = _lookInput.x * mouseSensitivity;
        float currentYRotation = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            transform.eulerAngles.y + mouseX,
            ref _smoothVelocity,
            rotationSmoothTime
        );
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}
