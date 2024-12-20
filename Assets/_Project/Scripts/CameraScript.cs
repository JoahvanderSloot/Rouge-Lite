using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] float m_followSpeed;
    [SerializeField] float m_offsetDistance;

    PlayerMovement m_playerMovement;
    Vector3 m_offset;
    Transform m_playerTransform;

    private void Start()
    {
        m_playerMovement = m_player.GetComponent<PlayerMovement>();
        m_playerTransform = m_player.transform;

        m_offset = transform.position - m_playerTransform.position;
    }

    private void Update()
    {
        if (m_playerMovement == null || m_playerTransform == null) return;

        Vector3 _movementDirection = new Vector3(-m_playerMovement.m_Rb.linearVelocity.x, -m_playerMovement.m_Rb.linearVelocity.y, 0);
        bool _isMoving = _movementDirection.magnitude > 0.1f;

        Vector3 _targetOffset = m_offset;

        if (_isMoving)
        {
            _targetOffset = m_offset - _movementDirection.normalized * m_offsetDistance;
        }

        Vector3 _targetPosition = m_playerTransform.position + _targetOffset;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, m_followSpeed * Time.deltaTime);
    }
}
