using UnityEngine;

public class Zombie : EnemyBase
{
    [Header("Zombie")]
    [SerializeField] Sprite m_idleSprite;
    [SerializeField] Sprite m_attackSprite;

    new void Start()
    {
        base.Start();

        float _yLevelFactor = Settings.Instance.settings.m_YLevel / 4f;

        m_enemySpeed = Mathf.Clamp(1f - _yLevelFactor, 1f, 5.5f);
        m_EnemyHP = Mathf.Clamp(3f - _yLevelFactor, 1f, 20f);
        m_enemyDamage = Mathf.Clamp(Mathf.Round(3f - _yLevelFactor), 1f, 7f);
    }

    new void Update()
    {
        base.Update();
        if(m_DamageTaken || m_attack)
        {
            m_renderer.sprite = m_attackSprite;
        }
        else
        {
            m_renderer.sprite= m_idleSprite;
        }

        MoveTowardsPlayer();
    }
}