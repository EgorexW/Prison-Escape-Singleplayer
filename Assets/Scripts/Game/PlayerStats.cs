using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] PrefabList roomsList;

    void Start()
    {
        OnStartGame();
        GameManager.i.gameEnder.beforeLoseGame.AddListener(BeforeLoseGame);
        GameManager.i.gameEnder.beforeWinGame.AddListener(BeforeWinGame);
        GameManager.i.roomsManager.onPlayerEnteredRoom.AddListener(OnPlayerEnteredRoom);
    }

    void OnPlayerEnteredRoom(Room room)
    {
        var originReferenceHolder = room.GetComponent<PrefabListIndexHolder>();
        if (originReferenceHolder == null){
            Debug.LogWarning("Room origin reference holder is null", room);
            return;
        }
        var roomIndex = originReferenceHolder.prefabListIndex;
        // Debug.Log($"Saving last room entered index: {roomIndex}", this);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LastRoomEntered, roomIndex);
    }

    void BeforeWinGame()
    {
        AddToPlayerPref(PlayerPrefsKeys.GamesWon, 1);
        Debug.Log("Games Won: " + PlayerPrefs.GetInt(PlayerPrefsKeys.GamesWon, 0));
    }

    void BeforeLoseGame()
    {
        AddToPlayerPref(PlayerPrefsKeys.GamesLost, 1);
        Debug.Log("Games Lost: " + PlayerPrefs.GetInt(PlayerPrefsKeys.GamesLost, 0));
    }

    void OnStartGame()
    {
        AddToPlayerPref(PlayerPrefsKeys.GamesStarted, 1);
        Debug.Log("Games Started: " + PlayerPrefs.GetInt(PlayerPrefsKeys.GamesStarted, 0));
    }

    public void AddToPlayerPref(string key, int value, int defaultValue = 0)
    {
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key, defaultValue) + value);
    }
}