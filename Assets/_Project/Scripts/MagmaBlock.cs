using System.Collections;
using UnityEngine;

public class MagmaBlock : MonoBehaviour
{
    Coroutine m_damagePlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(m_damagePlayer == null)
            {
                m_damagePlayer = StartCoroutine(DamagePlayer(collision.gameObject.GetComponent<PlayerAttack>()));
            }
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
