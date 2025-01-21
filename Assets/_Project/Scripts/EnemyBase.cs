using System.Collections;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Enemy Base")]
    public float m_EnemyHP;
    public float m_knockBackForce;
    protected bool m_attack;
    protected GameObject m_player;
    protected Rigidbody2D m_rb;
    protected SpriteRenderer m_renderer;
    [SerializeField] protected float m_enemySpeed;
    [SerializeField] protected float m_enemyDamage;
    [SerializeField] protected float m_enemyAttackSpeed;
    [SerializeField] protected GameObject m_deathParticles;
    [SerializeField] protected GameObject m_hpPickup;
    private Coroutine m_damageCoroutine;
    public bool m_DamageTaken = false;
    private float m_tickTimer;

    protected virtual void Start()
    {
        m_renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player");
    }

    protected virtual void Update()
    {
        if (m_EnemyHP <= 0)
        {
            OnDeath();
        }

        if (m_DamageTaken)
        {
            SpriteRenderer _sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            _sprite.color = Color.red;
            m_tickTimer = m_tickTimer + Time.deltaTime;
            m_attack = false;

            if (m_tickTimer > 0.25f)
            {
                m_DamageTaken = false;
                _sprite.color = Color.white;
                Rigidbody2D _rb = gameObject.GetComponent<Rigidbody2D>();
                _rb.linearVelocity = Vector2.zero;
                m_tickTimer = 0;
            }
        }
    }

    protected virtual void OnDeath()
    {
        DropHealthPickup();
        Instantiate(m_deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected virtual void DropHealthPickup()
    {
        int _dropHP = Random.Range(0, 7);
        if (_dropHP == 5 && m_hpPickup != null)
        {
            Instantiate(m_hpPickup, transform.position, Quaternion.identity);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_damageCoroutine == null)
            {
                m_damageCoroutine = StartCoroutine(DamagePlayer(collision.gameObject));
                m_attack = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!m_attack)
            {
                m_attack = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_damageCoroutine != null)
            {
                StopCoroutine(m_damageCoroutine);
                m_damageCoroutine = null;
                m_attack = false;
            }
            m_rb.linearVelocityX = 0;
        }
    }

    protected virtual IEnumerator DamagePlayer(GameObject _player)
    {
        while (true)
        {
            if (Settings.Instance != null && Settings.Instance.settings != null)
            {
                Settings.Instance.settings.m_PlayerHP -= m_enemyDamage;

                PlayerAttack _playerAttack = _player.GetComponent<PlayerAttack>();
                _playerAttack.StartPlayerFlash(Color.red);
            }
            yield return new WaitForSeconds(m_enemyAttackSpeed);
        }
    }

    public void MoveTowardsPlayer()
    {
        Vector3 _currentPosition = transform.position;
        Vector3 _playerPosition = m_player.transform.position;
        Vector3 _targetPosition = new Vector3(_playerPosition.x, _currentPosition.y, _currentPosition.z);

        transform.position = Vector3.MoveTowards(_currentPosition, _targetPosition, Time.deltaTime * m_enemySpeed);

        if (_targetPosition.x - _currentPosition.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_targetPosition.x - _currentPosition.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
