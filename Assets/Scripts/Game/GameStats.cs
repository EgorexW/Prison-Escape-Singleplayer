using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats i;

    public float gameTime;
    public float normalDamageTaken;
    public float pernamentDamageTaken;
    public float metersWalked;
    public int uniqueRoomsEntered;

    HashSet<Room> roomsList = new HashSet<Room>();

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
        uniqueRoomsEntered++;
    }

    void OnMove(Vector3 arg0)
    {
        metersWalked += arg0.magnitude;
    }

    void OnDamage(Damage damage)
    {
        normalDamageTaken += Mathf.Max(damage.damage, 0);
        pernamentDamageTaken += Mathf.Max(damage.pernamentDamage, 0);
    }

    void BeforeEndGame()
    {
        gameTime = GameManager.i.gameTimeManager.GameTime;
    }
}