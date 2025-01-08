using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("General Objects")]
    GameManager m_gameManager;
    GameObject m_player;

    [Header("Main Items")]
    [SerializeField] List<Image> m_mainItems;
    [SerializeField] Image m_selected;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI m_yLevelText;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
        m_player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        m_selected.transform.position = m_mainItems[(int)m_gameManager.m_currentItem].transform.position;
        m_yLevelText.text = "Y: " + Mathf.Round(m_player.transform.position.y).ToString();
    }
}
