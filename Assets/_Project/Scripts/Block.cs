using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject m_brick;
    [SerializeField] GameObject m_ladder;
    [SerializeField] GameObject m_hover;
    [SerializeField] GameObject m_break;
    [SerializeField] float m_MaxHP;
    public float m_HP;
    [HideInInspector] public bool m_IsHover;
    [HideInInspector] public bool m_BrokeBlock;
    [HideInInspector] public bool m_LadderPlaced;
    BoxCollider2D m_box;

    private void Start()
    {
        m_box = GetComponent<BoxCollider2D>();
        m_IsHover = false;
        m_BrokeBlock = false;
        m_LadderPlaced = false;
        m_box.isTrigger = false;
        m_break.transform.localScale = Vector3.zero;
        m_HP = m_MaxHP;
    }

    private void Update()
    {
        if (m_HP <= 0)
        {
            m_BrokeBlock = true;
            m_box.isTrigger = true;
            m_break.SetActive(false);
            gameObject.layer = default;
            ChoseDrop();
        }

        UpdateBreakScale();

        m_hover.SetActive(m_IsHover);
        m_brick.SetActive(!m_BrokeBlock);
        m_ladder.SetActive(m_LadderPlaced);
    }

    private void UpdateBreakScale()
    {
        float _clampedHP = Mathf.Max(1f, m_HP);
        float _healthPercentage = (_clampedHP - 1f) / (m_MaxHP - 1f);
        float _scaleValue = Mathf.Lerp(2.5f, 0f, _healthPercentage);
        m_break.transform.localScale = new Vector3(_scaleValue, _scaleValue, _scaleValue);
    }


    private void ChoseDrop()
    {
        //drop logic
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement _playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (m_LadderPlaced)
            {
                _playerMovement.m_IsOnLadder = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement _playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (m_LadderPlaced)
            {
                _playerMovement.m_IsOnLadder = false;
            }
        }
    }
}
