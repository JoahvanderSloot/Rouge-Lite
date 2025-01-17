using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Own Atributes")]
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

    [Header("Player")]
    GameObject m_player;
    [HideInInspector] public bool m_PlayerInRange = false;
    Transform m_playerTransform;
    [SerializeField] float m_detectionRange;

    [Header("Drops")]
    bool m_hasDropped;
    [SerializeField] GameObject m_pickupPref;
    [SerializeField] GameObject m_zombiePref;
    [SerializeField] GameObject m_wormPref;
    [SerializeField] GameObject m_slimePref;
    [SerializeField] GameObject m_batPref;
    [SerializeField] GameObject m_fireGiantPref;

    private void Start()
    {
        m_box = GetComponent<BoxCollider2D>();
        m_hasDropped = false;
        m_IsHover = false;
        m_BrokeBlock = false;
        m_LadderPlaced = false;
        m_box.isTrigger = false;
        m_hover.SetActive(m_IsHover);
        m_brick.SetActive(!m_BrokeBlock);
        m_ladder.SetActive(m_LadderPlaced);

        NewHP();
        UpdateBreakScale();

        m_player = GameObject.FindWithTag("Player");
        if (m_player != null)
        {
            m_playerTransform = m_player.transform;
        }
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void Update()
    {   
        if (m_HP <= 0)
        {
            m_BrokeBlock = true;
            m_box.isTrigger = true;
            m_break.SetActive(false);
            gameObject.layer = default;
            if (!m_hasDropped)
            {
                ChoseDrop();
            }
        }

        if (m_PlayerInRange)
        {
            UpdateBreakScale();

            m_hover.SetActive(m_IsHover);
            m_brick.SetActive(!m_BrokeBlock);
            m_ladder.SetActive(m_LadderPlaced);
        }
        else
        {
            m_hover.SetActive(false);
        }

        if (m_playerTransform != null)
        {
            float _distanceToPlayer = Vector3.Distance(transform.position, m_playerTransform.position);
            m_PlayerInRange = _distanceToPlayer <= m_detectionRange;
        }
        else
        {
            m_PlayerInRange = false;
        }
    }

    private void UpdateBreakScale()
    {
        if (m_MaxHP <= 1f)
        {
            m_break.transform.localScale = new Vector3(0f, 0f, 0f);
            return;
        }

        float _clampedHP = Mathf.Max(1f, m_HP);
        float _healthPercentage = (_clampedHP - 1f) / (m_MaxHP - 1f);
        float _scaleValue = Mathf.Lerp(2.5f, 0f, _healthPercentage);
        m_break.transform.localScale = new Vector3(_scaleValue, _scaleValue, _scaleValue);
    }

    private void ChoseDrop()
    {
        float[] _baseDropRates = new float[]
        {
        25f, // Gem (most common)
        21f, // Zombie
        17f, // Slime
        15f,  // Bat
        15f,  // Worm
        7f   // Fire Giant (rarest)
        };

        float _noDropBaseWeight = 150f;

        float _adjustedNoDropWeight = _noDropBaseWeight + Mathf.Abs(Settings.Instance.settings.m_YLevel * 10f);

        float[] _dropRates = new float[_baseDropRates.Length + 1];
        _baseDropRates.CopyTo(_dropRates, 0);
        _dropRates[_dropRates.Length - 1] = _adjustedNoDropWeight;

        float _totalWeight = 0f;
        foreach (float _weight in _dropRates)
        {
            _totalWeight += _weight;
        }

        float _randomValue = Random.Range(0f, _totalWeight);
        float _cumulativeWeight = 0f;

        for (int i = 0; i < _dropRates.Length; i++)
        {
            _cumulativeWeight += _dropRates[i];
            if (_randomValue <= _cumulativeWeight)
            {
                switch (i)
                {
                    case 0:
                        Instantiate(m_pickupPref, transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(m_zombiePref, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(m_slimePref, transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(m_batPref, transform.position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(m_wormPref, transform.position, Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(m_fireGiantPref, transform.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
                break;
            }
        }

        m_hasDropped = true;
    }

    private void NewHP()
    {
        m_MaxHP = Mathf.Max(2, Mathf.CeilToInt(-Settings.Instance.settings.m_YLevel / 5f)); 
        m_HP = m_MaxHP;
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
            _playerMovement.m_IsOnLadder = false;
        }
    }
}