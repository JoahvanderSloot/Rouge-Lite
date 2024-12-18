using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Attack()
    {
        if (!Settings.Instance.settings.m_Paused)
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
        Debug.Log("SWING");
    }

    private void LadderPlace()
    {
        Debug.Log("PLACE");
    }

    private void CrossbowShoot()
    {
        Debug.Log("SHOOT");
    }
}
