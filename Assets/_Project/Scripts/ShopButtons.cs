using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
    public bool m_ShopIsOpen;
    [SerializeField] bool m_OpenShopButton;
    GameManager m_gameManager;
    [SerializeField] int m_collor;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
    }

    public void PointerEnter()
    {
        if (!m_ShopIsOpen && !Settings.Instance.settings.m_Paused)
        {
            transform.localScale = new Vector2(1.05f, 1.05f);
        }
    }

    public void PointerExit()
    {
        transform.localScale = Vector2.one;
    }

    public void OpenShop()
    {
        if (!Settings.Instance.settings.m_Paused || m_ShopIsOpen)
        {
            transform.localScale = Vector2.one;
            if (m_ShopIsOpen)
            {
                Settings.Instance.settings.m_Paused = false;
            }
            m_ShopIsOpen = !m_ShopIsOpen;
        }
    }

    private void Update()
    {
        if (m_OpenShopButton)
        {
            if(!m_ShopIsOpen)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Settings.Instance.settings.m_Paused = true;
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                m_ShopIsOpen=false;
            }
        }
        else
        {
            Button _thisButton = gameObject.GetComponent<Button>();
            switch (m_collor)
            {
                case 0:
                    if(m_gameManager.m_BlueGemCount <= 0)
                    {
                        m_gameManager.m_BlueGemCount = 0;

                        _thisButton.image.color = Color.red;
                    }
                    else
                    {
                        _thisButton.image.color = Color.white;
                    }
                    break;
                case 1:
                    if (m_gameManager.m_GreenGemCount <= 0)
                    {
                        m_gameManager.m_GreenGemCount = 0;

                        _thisButton.image.color = Color.red;
                    }
                    else
                    {
                        _thisButton.image.color = Color.white;
                    }
                    break;
                case 2:
                    if (m_gameManager.m_RedGemCount <= 0)
                    {
                        m_gameManager.m_RedGemCount = 0;

                        _thisButton.image.color = Color.red;
                    }
                    else
                    {
                        _thisButton.image.color = Color.white;
                    }
                    break;
                case 3:
                    if (m_gameManager.m_YellowGemCount <= 0)
                    {
                        m_gameManager.m_YellowGemCount = 0;

                        _thisButton.image.color = Color.red;
                    }
                    else
                    {
                        _thisButton.image.color = Color.white;
                    }
                    break;
            }
        }
    }

    public void BlueBuy()
    {
        if(m_gameManager.m_BlueGemCount > 0 && Settings.Instance.settings.m_PlayerSpeed < 10)
        {   
            PlayerMovement _playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            m_gameManager.m_BlueGemCount--;
            Settings.Instance.settings.m_PlayerSpeed += 0.25f;
            _playerMovement.m_Speed = Settings.Instance.settings.m_PlayerSpeed + 5;

            if (Settings.Instance.settings.m_PlayerSpeed == 10)
            {
                Button _thisButton = gameObject.GetComponent<Button>();
                _thisButton.interactable = false;
            }
        }
    }

    public void GreenBuy()
    {
        if(m_gameManager.m_GreenGemCount > 0 && Settings.Instance.settings.m_MaxHP < 100)
        {
            m_gameManager.m_GreenGemCount--;
            Settings.Instance.settings.m_MaxHP++;
            if(Settings.Instance.settings.m_MaxHP == 100)
            {
                Button _thisButton = gameObject.GetComponent<Button>();
                _thisButton.interactable = false;
            }
        }
    }

    public void RedBuy()
    {
        if(m_gameManager.m_RedGemCount > 0 && Settings.Instance.settings.m_PlayerDamage < 25)
        {
            m_gameManager.m_RedGemCount--;
            Settings.Instance.settings.m_PlayerDamage++;
            if (Settings.Instance.settings.m_PlayerDamage == 25)
            {
                Button _thisButton = gameObject.GetComponent<Button>();
                _thisButton.interactable = false;
            }
        }
    }

    public void YellowBuy()
    {
        if(m_gameManager.m_YellowGemCount > 0)
        {
            m_gameManager .m_YellowGemCount--;
            Settings.Instance.settings.m_PlayerMiningSpeed++;
        }
    }
}
