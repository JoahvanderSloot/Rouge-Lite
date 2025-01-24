using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadCursorController : MonoBehaviour
{
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";
    [SerializeField] private float cursorSpeed = 1000f;

    private Vector2 cursorPosition;

    private void Start()
    {
        cursorPosition = Mouse.current.position.ReadValue();
    }

    private void Update()
    {
        MoveCursorWithGamepad();
    }

    private void MoveCursorWithGamepad()
    {
        float _horizontalInput = Input.GetAxis(horizontalAxis);
        float _verticalInput = Input.GetAxis(verticalAxis);
        Vector2 _gamepadInput = new Vector2(_horizontalInput, _verticalInput);

        if (_gamepadInput == Vector2.zero)
            return;

        Vector2 _scaledInput = _gamepadInput * cursorSpeed * Time.deltaTime;

        cursorPosition += _scaledInput;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);

        Mouse.current.WarpCursorPosition(cursorPosition);
    }
}
