using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Settings.Instance.settings.m_PlayerHP += 2;
            PlayerAttack _playerAttack = collision.gameObject.GetComponent<PlayerAttack>();
            _playerAttack.StartPlayerFlash(Color.green);

            Destroy(gameObject);
        }
    }
}
