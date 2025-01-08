using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_block;
    [SerializeField] GameObject m_bedrock;
    [SerializeField] GameObject m_cornerBedrock;
    [SerializeField] int m_rows;
    [SerializeField] int m_columns;
    [SerializeField] float m_spacing;

    private void Start()
    {
        GenerateGrid();
        GenerateBedrockRim();
    }

    private void GenerateGrid()
    {
        float _gridWidth = (m_columns - 1) * m_spacing;
        float _gridHeight = (m_rows - 1) * m_spacing;

        Vector3 _topMiddlePosition = Vector3.zero;

        Vector3 _topLeftPosition = _topMiddlePosition - new Vector3(_gridWidth / 2, 0, 0);

        for (int _row = 0; _row < m_rows; _row++)
        {
            for (int _col = 0; _col < m_columns; _col++)
            {
                Vector3 _blockPosition = _topLeftPosition + new Vector3(_col * m_spacing, -_row * m_spacing, 0);

                Instantiate(m_block, _blockPosition, Quaternion.identity, transform);
            }
        }
    }

    private void GenerateBedrockRim()
    {
        float _gridWidth = (m_columns - 1) * m_spacing;
        float _gridHeight = (m_rows - 1) * m_spacing;

        Vector3 _topMiddlePosition = Vector3.zero;

        Vector3 _topLeftPosition = _topMiddlePosition - new Vector3(_gridWidth / 2, 0, 0);

        for (int _row = 0; _row <= m_rows; _row++)
        {
            Vector3 _leftPosition = _topLeftPosition + new Vector3(-m_spacing, -_row * m_spacing, 0);
            Vector3 _rightPosition = _topLeftPosition + new Vector3((_gridWidth + m_spacing), -_row * m_spacing, 0);

            if (_row == 0)
            {
                Instantiate(m_cornerBedrock, _leftPosition, Quaternion.identity, transform);
                Instantiate(m_cornerBedrock, _rightPosition, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(m_bedrock, _leftPosition, Quaternion.identity, transform);
                Instantiate(m_bedrock, _rightPosition, Quaternion.identity, transform);
            }
        }

        for (int _col = -1; _col <= m_columns; _col++)
        {
            Vector3 _bottomPosition = _topLeftPosition + new Vector3(_col * m_spacing, -(m_rows * m_spacing), 0);
            Instantiate(m_bedrock, _bottomPosition, Quaternion.identity, transform);
        }
    }
}
