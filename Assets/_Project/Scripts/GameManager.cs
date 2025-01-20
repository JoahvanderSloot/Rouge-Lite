using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject m_player;
    PlayerMovement m_movement;
    [SerializeField] List<GameObject> m_mouseIcons;
    int m_currentIcon;

    [Header("Gems")]
    public int m_BlueGemCount;
    public int m_GreenGemCount;
    public int m_RedGemCount;
    public int m_YellowGemCount;

    [Header("Other")]
    [SerializeField] Image m_gameOverIMG;
    float m_gameOverTimer = 0;

    public enum Item
    {
        Pickaxe,
        Ladder,
        Crossbow
    }
    public Item m_currentItem;

    private void Start()
    {
        m_movement = m_player.GetComponent<PlayerMovement>();
        m_currentItem = Item.Pickaxe;
        Settings.Instance.settings.m_Paused = false;

        m_GreenGemCount = 0;
        m_BlueGemCount = 0;
        m_RedGemCount = 0;
        m_YellowGemCount = 0;

        m_gameOverIMG.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Settings.Instance.settings.m_Paused)
        {
            Time.timeScale = 1f;
            Inventory();
        }
        else
        {
            Time.timeScale = 0f;
        }

        Settings.Instance.settings.m_YLevel = Convert.ToInt32(m_player.transform.position.y);
        if(Settings.Instance.settings.m_YLevel < Settings.Instance.settings.m_YHighScore)
        {
            Settings.Instance.settings.m_YHighScore = Settings.Instance.settings.m_YLevel;
        }

        if(Settings.Instance.settings.m_PlayerHP > Settings.Instance.settings.m_MaxHP)
        {
            Settings.Instance.settings.m_PlayerHP = Settings.Instance.settings.m_MaxHP;
        }

        if (Settings.Instance.settings.m_PlayerHP <= 0)
        {
            m_gameOverIMG.gameObject.SetActive(true);
            Color _color = m_gameOverIMG.color;
            m_gameOverTimer += Time.deltaTime * 3f;
            _color.a = m_gameOverTimer;
            m_gameOverIMG.color = _color;

            if(m_gameOverTimer >= 3)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    private void Inventory()
    {
        m_currentIcon = (int)m_currentItem;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            m_currentItem = Item.Pickaxe;
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            m_currentItem = Item.Ladder;
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            m_currentItem = Item.Crossbow;
        }

        float _scroll = Mouse.current.scroll.ReadValue().y;
        if (_scroll > 0f)
        {
            SwitchItem(-1);
        }
        else if (_scroll < 0f)
        {
            SwitchItem(1);
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftShoulder.wasPressedThisFrame)
            {
                SwitchItem(-1);
            }
            else if (Gamepad.current.rightShoulder.wasPressedThisFrame)
            {
                SwitchItem(1);
            }
        }

        for (int i = 0; i < m_mouseIcons.Count; i++)
        {
            if (i == m_currentIcon)
            {
                m_mouseIcons[i].SetActive(true);
                m_mouseIcons[i].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
            else
            {
                m_mouseIcons[i].SetActive(false);
            }
        }
    }

    private void SwitchItem(int _direction)
    {
        int _itemCount = System.Enum.GetValues(typeof(Item)).Length;
        m_currentItem = (Item)(((int)m_currentItem + _direction + _itemCount) % _itemCount);
    }
}
