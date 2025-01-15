using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TunnelWorm : EnemyBase
{
    [SerializeField] float m_burstSpeed;
    [SerializeField] Sprite m_idleSprite;
    [SerializeField] Sprite m_attackSprite;
    bool m_isSpawned;

    new void Start()
    {
        m_isSpawned = false;
        base.Start();
        StartCoroutine(BurstAtPlayer());

        float _yLevelFactor = Settings.Instance.settings.m_YLevel / 4f;

        m_enemySpeed = Mathf.Clamp(150f - _yLevelFactor, 150f, 300f);
        m_EnemyHP = Mathf.Clamp(5f - _yLevelFactor, 5f, 25f);
        m_enemyDamage = Mathf.Clamp(Mathf.Round(2f - _yLevelFactor), 2f, 10f);
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
    }

    IEnumerator BurstAtPlayer()
    {
        while (true)
        {
            MoveTowardsPlayer();
            yield return new WaitForSeconds(m_burstSpeed);
            m_isSpawned = true;
        }
    }
}