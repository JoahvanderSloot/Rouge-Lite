using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo.asset", menuName = "Game Info", order = 0)]
public class PlayerSettings : ScriptableObject
{
    public bool m_Paused;
    public float m_PlayerHP;
    public float m_PlayerSpeed;
    public float m_PlayerMiningSpeed;
    public float m_PlayerDamage;
    public int m_YLevel;
    public int m_YHighScore;
    public float m_MaxHP;
    public float m_PlayerDamageTick;
}
