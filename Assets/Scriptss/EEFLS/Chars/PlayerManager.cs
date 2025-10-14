using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
        playerStats?.InitializePlayerStats();
    }
}