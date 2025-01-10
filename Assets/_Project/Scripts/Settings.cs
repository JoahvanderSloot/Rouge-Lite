using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private static Settings instance;

    public static Settings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Settings>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SettingsSingleton");
                    instance = singletonObject.AddComponent<Settings>();
                }
            }
            return instance;
        }
    }

    public PlayerSettings settings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Scene _scene = SceneManager.GetActiveScene();
        if (_scene.name == "Main")
        {
            if (!settings.m_Paused)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
        }
        else
        {
            Cursor.visible = true;
            settings.m_Paused = false;
        }
    }
}
