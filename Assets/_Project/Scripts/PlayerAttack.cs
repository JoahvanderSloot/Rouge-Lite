using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void Attack()
    {
        if (!Settings.Instance.settings.m_Paused)
        {
            Debug.Log("Attack");
        }
    }
}
