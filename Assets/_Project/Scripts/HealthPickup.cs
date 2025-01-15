using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Settings.Instance.settings.m_PlayerHP += 2;
            PlayerAttack _playerAttack = collision.gameObject.GetComponent<PlayerAttack>();
            StartCoroutine(_playerAttack.FlashColor(Color.green));

            SpriteRenderer _sprite = gameObject.GetComponent<SpriteRenderer>();
            _sprite.enabled = false;
            Collider2D _collider = gameObject.GetComponent<Collider2D>();
            _collider.enabled = false;

            Destroy(gameObject, Settings.Instance.settings.m_PlayerDamageTick * 2);
        }
    }
}
