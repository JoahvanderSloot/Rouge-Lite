using UnityEngine;

public class RotateMouseIcon : MonoBehaviour
{
    GameObject m_player;

    private void Start()
    {
        m_player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector3 _targetDirection = m_player.transform.position - transform.position;

        float _angle = Mathf.Atan2(-_targetDirection.y, -_targetDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }
}
