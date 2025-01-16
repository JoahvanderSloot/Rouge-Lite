using UnityEngine;

public class FireBall : MonoBehaviour
{
    [HideInInspector] public float m_Damage;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Settings.Instance.settings.m_PlayerHP -= m_Damage;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            Block _block = collision.gameObject.GetComponent<Block>();
            if (!_block.m_BrokeBlock)
            {
                _block.m_HP -= m_Damage;
                _block.m_BrokeBlock = true;
                _block.m_PlayerInRange = true;
                Destroy(gameObject);
            }
        }
    }
}
