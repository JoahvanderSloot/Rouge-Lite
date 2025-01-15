using System.Collections;
using UnityEngine;

public class Slime : EnemyBase
{
    [SerializeField] float m_jumpForce;
    [SerializeField] Sprite m_idleSprite;
    [SerializeField] Sprite m_hitSprite;
    [SerializeField] Sprite m_attackSprite;

    new void Start()
    {
        base.Start();
        StartCoroutine(Jump());

        float _yLevelFactor = Settings.Instance.settings.m_YLevel / 4f;

        m_enemySpeed = Mathf.Clamp(1f - _yLevelFactor, 1f, 5f);
        m_EnemyHP = Mathf.Clamp(3f - _yLevelFactor, 1f, 10f);
        m_enemyDamage = Mathf.Clamp(Mathf.Round(1f - _yLevelFactor), 1f, 3f);
    }

    new void Update()
    {
        base.Update();
        MoveTowardsPlayer();

        if (m_attack)
        {
            m_renderer.sprite = m_attackSprite;
        }
        else if(m_DamageTaken)
        {
            m_renderer.sprite = m_hitSprite;
        }
        else
        {
            m_renderer.sprite = m_idleSprite;
        } 
    }

    private IEnumerator Jump()
    {
        while (true)
        {
            m_rb.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Force);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
