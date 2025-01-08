using UnityEngine;

public class IconHover : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameManager _gameManager = FindFirstObjectByType<GameManager>();
        if (collision.CompareTag("Brick"))
        {
            Block _blockScript = collision.gameObject.GetComponent<Block>();

            if (_gameManager.m_currentItem == GameManager.Item.Pickaxe)
            {
                if (!_blockScript.m_BrokeBlock)
                {
                    _blockScript.m_IsHover = true;
                }
            }
            else if(_gameManager.m_currentItem == GameManager.Item.Ladder)
            {
                _blockScript.m_IsHover = true;
            }
        }
        else
        {
            Block _blockScript = collision.gameObject.GetComponent<Block>();
            _blockScript.m_IsHover = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager _gameManager = FindFirstObjectByType<GameManager>();
        if (collision.CompareTag("Brick"))
        {
            Block _blockScript = collision.gameObject.GetComponent<Block>();
            _blockScript.m_IsHover = false;
        }
    }
}
