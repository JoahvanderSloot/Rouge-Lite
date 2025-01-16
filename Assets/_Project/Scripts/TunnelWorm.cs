using System.Collections;
using UnityEngine;

public class TunnelWorm : EnemyBase
{
    [Header("Worm")]
    [SerializeField] float m_burstSpeed;
    [SerializeField] float m_speedAmplitude;
    [SerializeField] Sprite m_idleSprite;
    [SerializeField] Sprite m_attackSprite;
    bool m_isSpawned;

    float m_baseSpeed;

    new void Start()
    {
        m_isSpawned = false;
        base.Start();

        float _yLevelFactor = Settings.Instance.settings.m_YLevel / 4f;

        m_baseSpeed = Mathf.Clamp(3f - _yLevelFactor, 2f, 5f);
        m_enemySpeed = m_baseSpeed;
        m_EnemyHP = Mathf.Clamp(5f - _yLevelFactor, 5f, 25f);
        m_enemyDamage = Mathf.Clamp(Mathf.Round(2f - _yLevelFactor), 2f, 10f);

        StartCoroutine(SpawnDelay());
    }

    new void Update()
    {
        base.Update();

        if (m_DamageTaken || m_attack || !m_isSpawned)
        {
            m_renderer.sprite = m_attackSprite;
        }
        else
        {
            m_renderer.sprite = m_idleSprite;
        }

        UpdateSpeed();
        MoveTowardsPlayer();
    }

    void UpdateSpeed()
    {
        float _speedBoost = Mathf.Sin(Time.time * m_burstSpeed) * m_speedAmplitude;
        m_enemySpeed = m_baseSpeed + _speedBoost;
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.25f);
        m_isSpawned = true;
    }
}
