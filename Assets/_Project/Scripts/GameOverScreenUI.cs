using TMPro;
using UnityEngine;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_yLevelHighscore;

    private void Start()
    {
        m_yLevelHighscore.text = "Depth highscore: " + Settings.Instance.settings.m_YHighScore.ToString();
    }
}
