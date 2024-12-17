using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] float m_climbSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //make player climb
        }
    }
}
