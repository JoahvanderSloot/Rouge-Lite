using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject m_player;
    PlayerMovement m_movement;
    [SerializeField] List<GameObject> m_mouseIcons;
    int m_currentIcon;

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
