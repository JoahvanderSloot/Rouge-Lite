using System.Collections;
using UnityEngine;

public class Bat : EnemyBase
{
    [Header("Slime")]
    [SerializeField] private Sprite m_wingsOpen;
    [SerializeField] private Sprite m_wingsClose;

    [SerializeField] private float m_direction = 1f;
    [SerializeField] private float m_amplitude = 0.5f;
    [SerializeField] private float m_frequency = 2f;
    [SerializeField] private LayerMask m_collisionLayer;
    [SerializeField] private float m_collisionCheckDistance = 0.1f;
    [SerializeField] private float m_forceMultiplier = 5f;

    private float m_offset;

    new void Start()
    {
        base.Start();
        m_offset = transform.position.y;
        StartCoroutine(FlyUpAndDown());
        StartCoroutine(SwitchSprites());

        float _yLevelFactor = Settings.Instance.settings.m_YLevel / 4f;

        m_enemySpeed = Mathf.Clamp(3f - _yLevelFactor, 2f, 5f);
        m_EnemyHP = Mathf.Clamp(1f - _yLevelFactor, 1f, 5f);
    }

    new void Update()
    {
        base.Update();
        MoveTowardsPlayer();
    }

    private IEnumerator FlyUpAndDown()
    {
        while (true)
        {
            if ((transform.position.y >= 2f && CheckCollision(Vector2.up)) || CheckCollision(Vector2.down))
            {
                m_direction = -1f;
            }
            else if (transform.position.y <= 2f && CheckCollision(Vector2.down))
            {
                m_direction = 1f;
            }

            float _newY = m_offset + Mathf.Sin(Time.time * m_frequency) * m_amplitude * m_direction;
            float _forceY = (_newY - transform.position.y) * m_forceMultiplier;

            m_rb.AddForce(new Vector2(0, _forceY));

            yield return new WaitForFixedUpdate();
        }
    }

    private bool CheckCollision(Vector2 _direction)
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, _direction, m_collisionCheckDistance, m_collisionLayer);
        return _hit.collider != null;
    }

    private IEnumerator SwitchSprites()
    {
        while (true)
        {
            m_renderer.sprite = m_wingsOpen;
            yield return new WaitForSeconds(0.2f);
            m_renderer.sprite = m_wingsClose;
            yield return new WaitForSeconds(0.2f);
        }
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Brick"))
        {
            m_direction *= -1f;
            m_offset = transform.position.y;
        }
    }
}
