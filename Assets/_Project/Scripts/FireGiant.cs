using System.Collections;
using UnityEngine;

public class FireGiant : EnemyBase
{
    [Header("Fire Giant")]
    [SerializeField] GameObject m_fireBall;
    [SerializeField] Transform m_fireBallSpawn;
    [SerializeField] float m_fireBallSpeed;

    [SerializeField] Sprite m_idleSprite;
    [SerializeField] Sprite m_shootingSprite;

    new void Start()
    {
        base.Start();
        StartCoroutine(ShootFireBall());

        float _yLevelFactor = Settings.Instance.settings.m_YLevel / 4f;

        m_EnemyHP = Mathf.Clamp(15f - _yLevelFactor, 15f, 75f);
        m_enemyDamage = Mathf.Clamp(Mathf.Round(5f - _yLevelFactor), 5f, 20f);
    }

    new void Update()
    {
        base.Update();
    }

    IEnumerator ShootFireBall()
    {
        while (true)
        {
            m_renderer.sprite = m_shootingSprite;
            yield return new WaitForSeconds(m_fireBallSpeed / 2);
            GameObject _fireBallObj = Instantiate(m_fireBall, m_fireBallSpawn.position, Quaternion.identity);
            FireBall _fireBallScript = _fireBallObj.GetComponent<FireBall>();
            _fireBallScript.m_Damage = m_enemyDamage * 1.5f;
            m_renderer.sprite = m_idleSprite;
            yield return new WaitForSeconds(m_fireBallSpeed / 2);
        }
    }
}