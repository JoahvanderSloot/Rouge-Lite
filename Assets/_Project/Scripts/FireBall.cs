using UnityEngine;

public class FireBall : MonoBehaviour
{
    [HideInInspector] public float m_Damage;
    [SerializeField] float m_speed = 5f;
    private GameObject m_player;
    private Vector2 m_direction;

    void Start()
    {
        Destroy(gameObject, 5);
        m_player = GameObject.FindWithTag("Player");

        if (m_player != null)
        {
            Vector2 _playerPosition = m_player.transform.position;
            Vector2 _fireballPosition = transform.position;
            m_direction = (_playerPosition - _fireballPosition).normalized;

            GetComponent<Rigidbody2D>().linearVelocity = m_direction * m_speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack _playerAttack = collision.gameObject.GetComponent<PlayerAttack>();
            _playerAttack.StartPlayerFlash(Color.red);
            Settings.Instance.settings.m_PlayerHP -= m_Damage;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            Block _block = collision.gameObject.GetComponent<Block>();
            if (!_block.m_BrokeBlock)
            {
                _block.m_HP -= m_Damage;
                _block.m_BrokeBlock = true;
                _block.m_PlayerInRange = true;
                Destroy(gameObject);
            }
        }
    }
}
