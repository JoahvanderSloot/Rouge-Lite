using UnityEngine;

public class Settings : MonoBehaviour
{
    private static Settings instance;

    public static Settings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Settings>();

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
}
