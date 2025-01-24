using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PointerEnter()
    {
        transform.localScale = new Vector2(1.05f, 1.05f);
    }

    public void PointerExit()
    {
        transform.localScale = Vector2.one;
    }

    public void ContinueGame()
    {
        Settings.Instance.settings.m_Paused = false;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Main");
        Settings.Instance.settings.m_PlayerHP = 1;
        Settings.Instance.settings.m_PlayerSpeed = 1;
        Settings.Instance.settings.m_PlayerMiningSpeed = 1;
        Settings.Instance.settings.m_PlayerDamage = 1;
        Settings.Instance.settings.m_MaxHP = 50;
    }

    public void ContinueOldGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
        Settings.Instance.settings.m_Paused = false;
    }

    public void QuitGame()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
             Application.Quit();
#elif (UNITY_WEBGL)
             Application.OpenURL("https://joahvds.itch.io/mine-adventure");
#endif
    }
}
