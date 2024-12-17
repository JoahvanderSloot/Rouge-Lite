using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement m_playerMovement;
    [SerializeField] LayerMask m_groundCheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & m_groundCheck) != 0)
        {
            m_playerMovement.m_HasJumped = false;
        }
    }
}
