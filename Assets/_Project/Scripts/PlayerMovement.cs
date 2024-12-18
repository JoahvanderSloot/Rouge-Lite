using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_controllerCursorSpeed;
    [SerializeField] InputActionReference m_moveInput;
    [SerializeField] InputActionReference m_cursorInput;
    Coroutine m_jumpLoop;

    public bool m_HasJumped;

    bool m_lookLeft;

    public Rigidbody2D m_Rb;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
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

            m_Rb.linearVelocity = new Vector2(_data.x * m_speed, m_Rb.linearVelocity.y);

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

    public void Jump(CallbackContext _context)
    {
        if (_context.performed)
        {
            if (m_jumpLoop == null)
            {
                m_jumpLoop = StartCoroutine(JumpLoop());
            }
        }
        else if (_context.canceled)
        {
            if (m_jumpLoop != null)
            {
                StopCoroutine(m_jumpLoop);
                m_jumpLoop = null;
            }
        }
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            if (!m_HasJumped && !Settings.Instance.settings.m_Paused)
            {
                m_Rb.linearVelocity = new Vector2(m_Rb.linearVelocity.x, m_jumpForce);
                m_HasJumped = true;

                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
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
