using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    int m_randomPickup;
    [SerializeField] List<GameObject> m_pickupIcons;
    GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();

        m_randomPickup = Random.Range(0, 4);
        foreach (var _item in m_pickupIcons)
        {
            _item.SetActive(false);
        }
        m_pickupIcons[m_randomPickup].SetActive(true);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch(m_randomPickup)
            {
                case 0:
                    m_gameManager.m_BlueGemCount++;
                    break;
                case 1:
                    m_gameManager.m_GreenGemCount++;
                    break;
                case 2:
                    m_gameManager.m_RedGemCount++;
                    break;
                case 3:
                    m_gameManager.m_YellowGemCount++;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
