using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_controllerCursorSpeed;
    [SerializeField] InputActionReference m_moveInput;
    [SerializeField] InputActionReference m_cursorInput;

    public bool m_HasJumped;

    bool m_lookLeft;

    Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Move();

        if (Gamepad.all.Count > 0)
        {
            MoveCursorWithGamepad();
        }

        if (m_lookLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Move()
    {
        if (!Settings.Instance.settings.m_Paused)
        {
            Vector2 _data = m_moveInput.action.ReadValue<Vector2>();

            m_rb.linearVelocity = new Vector2(_data.x * m_speed, m_rb.linearVelocity.y);

            if (_data.x > 0)
            {
                m_lookLeft = false;
            }
            else if (_data.x < 0)
            {
                m_lookLeft = true;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void Jump()
    {
        if(!m_HasJumped && !Settings.Instance.settings.m_Paused)
        {
            m_rb.linearVelocity = new Vector2(m_rb.linearVelocity.x, m_rb.linearVelocity.y + m_jumpForce);
            m_HasJumped = true;
        }
    }

    public void Pause()
    {
        Settings.Instance.settings.m_Paused = !Settings.Instance.settings.m_Paused;
    }

    public void MoveCursorWithGamepad()
    {
        Vector2 gamepadInput = m_cursorInput.action.ReadValue<Vector2>();

        float scaleFactor = m_controllerCursorSpeed / 100;

        Vector3 currentCursorPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 newCursorPosition = currentCursorPosition + new Vector3(
            gamepadInput.x * scaleFactor,
            gamepadInput.y * scaleFactor,
            0f
        );

        // This makes the cursor move with a controller (so you can use the wormhole on controller too)
        Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(newCursorPosition));
    }
}
