using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed;
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_controllerCursorSpeed;
    [SerializeField] InputActionReference m_moveInput;
    [SerializeField] InputActionReference m_cursorInput;
    Coroutine m_jumpLoop;
    public bool m_HasJumped;
    public bool m_IsOnLadder;
    bool m_lookLeft;
    [HideInInspector] public Rigidbody2D m_Rb;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        m_Speed += Settings.Instance.settings.m_PlayerSpeed;
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

            m_Rb.linearVelocity = new Vector2(_data.x * m_Speed, m_Rb.linearVelocity.y);

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

        if(m_Rb.linearVelocityY > 16)
        {
            m_Rb.linearVelocityY = 16;
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
            StopCoroutine(m_jumpLoop);
            m_jumpLoop = null;
        }
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            if (!m_IsOnLadder)
            {
                if (!m_HasJumped && !Settings.Instance.settings.m_Paused)
                {
                    m_Rb.linearVelocity = new Vector2(m_Rb.linearVelocity.x, m_jumpForce);
                    m_HasJumped = true;

                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                m_Rb.linearVelocity = new Vector2(m_Rb.linearVelocity.x, m_jumpForce);
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
        Vector2 _gamepadInput = m_cursorInput.action.ReadValue<Vector2>();

        float _scaleFactor = m_controllerCursorSpeed / 100;

        Vector3 _currentCursorPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 _newCursorPosition = _currentCursorPosition + new Vector3(
            _gamepadInput.x * _scaleFactor,
            _gamepadInput.y * _scaleFactor,
            0f
        );

        Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(_newCursorPosition));
    }
}
