using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("General Objects")]
    GameManager m_gameManager;

    [Header("Main Items")]
    [SerializeField] List<Image> m_mainItems;
    [SerializeField] Image m_selected;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        m_selected.transform.position = m_mainItems[(int)m_gameManager.m_currentItem].transform.position;
    }
}
