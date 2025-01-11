using UnityEngine;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
    bool m_shopIsOpen;
    [SerializeField] bool m_OpenShopButton;
    GameManager m_gameManager;
    [SerializeField] int m_collor;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
    }

    public void PointerEnter()
    {
        if(m_shopIsOpen == false)
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
        transform.localScale = Vector2.one;
        m_shopIsOpen = !m_shopIsOpen;
    }

    private void Update()
    {
        if (m_OpenShopButton)
        {
            if(!m_shopIsOpen)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                Settings.Instance.settings.m_Paused = false;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Settings.Instance.settings.m_Paused = true;
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
        if(m_gameManager.m_BlueGemCount > 0)
        {
            m_gameManager.m_BlueGemCount--;
            Settings.Instance.settings.m_PlayerSpeed++;
        }
    }

    public void GreenBuy()
    {
        if(m_gameManager.m_GreenGemCount > 0)
        {
            m_gameManager.m_GreenGemCount--;
            Settings.Instance.settings.m_MaxHP++;
        }
    }

    public void RedBuy()
    {
        if(m_gameManager.m_RedGemCount > 0)
        {
            m_gameManager.m_RedGemCount--;
            Settings.Instance.settings.m_PlayerDamage++;
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
