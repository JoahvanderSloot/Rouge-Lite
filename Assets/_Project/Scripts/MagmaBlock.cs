using System.Collections;
using UnityEngine;

public class MagmaBlock : MonoBehaviour
{
    Coroutine m_damagePlayer;
    [SerializeField] LayerMask m_ground;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & m_ground) != 0)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_damagePlayer == null)
            {
                m_damagePlayer = StartCoroutine(DamagePlayer(collision.gameObject.GetComponent<PlayerAttack>()));
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase _enemyBase = collision.gameObject.GetComponent<EnemyBase>();
            if (_enemyBase.m_knockBackForce != 0)
            {
                _enemyBase.m_EnemyHP = 0;
            }
        }
        else
        {
            Destroy(collision.gameObject);
        }

        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocityX = 0;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(m_damagePlayer != null)
            {
                StopCoroutine(m_damagePlayer);
                m_damagePlayer = null;
            }
        }
    }

    IEnumerator DamagePlayer(PlayerAttack _player)
    {
        while (true)
        {
            Settings.Instance.settings.m_PlayerHP--;
            _player.StartPlayerFlash(Color.red);
            yield return new WaitForSeconds(Settings.Instance.settings.m_PlayerDamageTick);
        }
    }
}
