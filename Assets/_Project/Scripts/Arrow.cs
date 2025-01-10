using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float m_shootForce;
    GameObject m_player;
    PlayerAttack m_playerAttack;
    Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player");
        m_playerAttack = m_player.GetComponent<PlayerAttack>();

        Destroy(gameObject, 5);

        transform.rotation = m_playerAttack.m_CrossBowRotation.transform.rotation;

        Vector3 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0;

        Vector2 _shootDirection = ((Vector2)_mousePosition - (Vector2)transform.position).normalized;

        m_rb.AddForce(_shootDirection * m_shootForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
