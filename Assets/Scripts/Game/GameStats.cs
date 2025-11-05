using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Stats
{
    public float gameTime;
    public float normalDamageTaken;
    public float pernamentDamageTaken;
    public float metersWalked;
    public int uniqueRoomsEntered;
    public int objectsDestroyed;
}

public class GameStats : MonoBehaviour
{
    public static GameStats i;

    readonly HashSet<Room> roomsList = new();
    
    
    [ShowInInspector] Stats stats = new();

    void Awake()
    {
        if (i != null && i != this){
            Destroy(i.gameObject);
        }
        i = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameManager.i.gameEnder.beforeEndGame.AddListener(BeforeEndGame);
        GameManager.i.Player.playerHealth.Health.onDamage.AddListener(OnDamage);
        GameManager.i.Player.onMove.AddListener(OnMove);
        GameManager.i.roomsManager.onPlayerEnteredRoom.AddListener(OnPlayerEnteredRoom);
    }

    void OnPlayerEnteredRoom(Room arg0)
    {
        if (!roomsList.Add(arg0)){
            return;
        }
        stats.uniqueRoomsEntered++;
    }

    void OnMove(Vector3 arg0)
    {
        stats.metersWalked += arg0.magnitude;
    }

    void OnDamage(Damage damage)
    {
        stats.normalDamageTaken += Mathf.Max(damage.lightDamage, 0);
        stats.pernamentDamageTaken += Mathf.Max(damage.heavyDamage, 0);
    }

    void BeforeEndGame()
    {
        stats.gameTime = GameManager.i.gameTimeManager.GameTime;
    }

    public void OnObjectDestroyed(Destroyable destroyable)
    {
        stats.objectsDestroyed ++;
    }

    public Stats GetStats()
    {
        return stats;
    }
}