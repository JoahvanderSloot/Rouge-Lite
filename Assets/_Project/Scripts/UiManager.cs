using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("General Objects")]
    GameManager m_gameManager;
    GameObject m_player;
    [SerializeField] Slider m_HPslider;
    [SerializeField] GameObject m_escMenu;
    [SerializeField] ShopButtons m_shopOnAndOff;

    [Header("Main Items")]
    [SerializeField] List<Image> m_mainItems;
    [SerializeField] Image m_selected;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI m_yLevelText;

    [SerializeField] TextMeshProUGUI m_blueGemText;
    [SerializeField] TextMeshProUGUI m_greenGemText;
    [SerializeField] TextMeshProUGUI m_redGemText;
    [SerializeField] TextMeshProUGUI m_yellowGemText;

    [SerializeField] TextMeshProUGUI m_speedText;
    [SerializeField] TextMeshProUGUI m_hpText;
    [SerializeField] TextMeshProUGUI m_damageText;
    [SerializeField] TextMeshProUGUI m_miningSpeedText;

    [SerializeField] TextMeshProUGUI m_healthBarrText;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
        m_player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        m_selected.transform.position = m_mainItems[(int)m_gameManager.m_currentItem].transform.position;
        m_yLevelText.text = "Y: " + Settings.Instance.settings.m_YLevel.ToString();

        m_blueGemText.text = m_gameManager.m_BlueGemCount.ToString();
        m_greenGemText.text = m_gameManager.m_GreenGemCount.ToString();
        m_redGemText.text = m_gameManager.m_RedGemCount.ToString();
        m_yellowGemText.text = m_gameManager.m_YellowGemCount.ToString();

        m_speedText.text = "Speed<br>" + Settings.Instance.settings.m_PlayerSpeed.ToString();
        m_hpText.text = "Health<br>" + Settings.Instance.settings.m_MaxHP.ToString();
        m_damageText.text = "Strength<br>" + Settings.Instance.settings.m_PlayerDamage.ToString();
        m_miningSpeedText.text = "Pickaxe <br>Strength <br>" + Settings.Instance.settings.m_PlayerMiningSpeed.ToString();
        m_healthBarrText.text = Settings.Instance.settings.m_PlayerHP.ToString() + "/" + Settings.Instance.settings.m_MaxHP.ToString();

        m_HPslider.value = Settings.Instance.settings.m_PlayerHP;
        m_HPslider.maxValue = Settings.Instance.settings.m_MaxHP;

        if (!m_shopOnAndOff.m_ShopIsOpen)
        {
            m_escMenu.SetActive(Settings.Instance.settings.m_Paused);
        }
        else
        {
            m_escMenu.SetActive(false);
        }
    }
}
