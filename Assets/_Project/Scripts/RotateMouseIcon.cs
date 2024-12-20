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

        Vector3 _newDirection = Vector3.RotateTowards(transform.forward, _targetDirection, 100, 0.0f);
        Vector3 _rotation = new Vector3(0, 0, _newDirection.y);

        Vector3 _eulerRotate = Quaternion.LookRotation(_rotation).ToEuler();
        _eulerRotate.x = 0;
        _eulerRotate.y = 0;
        transform.rotation = Quaternion.LookRotation(_eulerRotate);
    }
}
