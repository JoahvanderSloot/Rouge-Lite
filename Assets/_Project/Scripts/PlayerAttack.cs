using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerAttack : MonoBehaviour
{
    GameManager m_gameManager;

    [Header("Crossbow")] 
    public GameObject m_CrossBowRotation;
    [SerializeField] GameObject m_arrowPref;
    [SerializeField] LayerMask m_ignoreRaycast;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
        Settings.Instance.settings.m_PlayerHP = Settings.Instance.settings.m_MaxHP;
    }

    public void Attack(CallbackContext _context)
    {
        if (!Settings.Instance.settings.m_Paused && _context.performed)
        {
            switch (m_gameManager.m_currentItem)
            {
                case GameManager.Item.Pickaxe:
                    PickaxeAttack();
                    break;

                case GameManager.Item.Ladder:
                    LadderPlace();
                    break;

                case GameManager.Item.Crossbow:
                    CrossbowShoot();
                    break;
            }
        }
    }

    private void PickaxeAttack()
    {
        RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, m_ignoreRaycast);

        if (_hit.collider != null && _hit.collider.CompareTag("Brick"))
        {
            Block _block = _hit.collider.gameObject.GetComponent<Block>();
            if (_block.m_IsHover && _block.m_PlayerInRange)
            {
                if(Settings.Instance.settings.m_PlayerMiningSpeed >= _block.m_HP)
                {
                    _block.m_IsHover = false;
                }
                _block.m_HP -= Settings.Instance.settings.m_PlayerMiningSpeed;
            }

        }
    }

    private void LadderPlace()
    {
        RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, m_ignoreRaycast);

        if (_hit.collider != null && _hit.collider.CompareTag("Brick"))
        {
            Block _block = _hit.collider.gameObject.GetComponent<Block>();
            if (_block.m_BrokeBlock && _block.m_PlayerInRange && !_block.m_LadderPlaced)
            {
                _block.m_LadderPlaced = true;
            }
        }
    }

    private void CrossbowShoot()
    {
        Vector3 _arrowSpawn = new Vector3(transform.position.x, transform.position.y, -1);
        Instantiate(m_arrowPref, _arrowSpawn, Quaternion.identity);
    }

    public void RemoveLadder()
    {
        RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, m_ignoreRaycast);

        if (_hit.collider != null && _hit.collider.CompareTag("Brick"))
        {
            Block _block = _hit.collider.gameObject.GetComponent<Block>();
            if (_block.m_BrokeBlock && _block.m_PlayerInRange && _block.m_LadderPlaced)
            {
                _block.m_LadderPlaced = false;
            }
        }
    }
}
