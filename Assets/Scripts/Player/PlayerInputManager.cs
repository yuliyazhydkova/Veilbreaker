using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController _playerController;

    [Inject]
    public void Construct(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void OnMove(InputValue value)
    {
        _playerController.SetMoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        _playerController.SetLookInput(value.Get<Vector2>());
    }

    public void OnSprint(InputValue value)
    {
        float input = value.Get<float>(); // Для кнопки: 1 - нажата, 0 - отпущена
        bool isPressed = input > 0.5f;    // Превращаем в булевое значение
        Debug.Log($"[OnSprint] Sprint Input: {input}, IsPressed: {isPressed}");
        _playerController.SetSprint(isPressed);
    }


    public void OnJump(InputValue value)
    {
        _playerController.Jump();
    }
}
