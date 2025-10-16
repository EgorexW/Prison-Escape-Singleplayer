using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;

    [FormerlySerializedAs("gameTime")] [BoxGroup("References")] [Required] public GameTimeManager gameTimeManager;

    [FormerlySerializedAs("facilityObjects")]
    [FormerlySerializedAs("facilitySwitches")]
    [BoxGroup("References")]
    [Required]
    public FacilityTriggers facilityTriggers;

    [BoxGroup("References")] [Required] public LevelNodes levelNodes;
    [BoxGroup("References")] [Required] public FacilityAnnouncements facilityAnnouncements;
    [BoxGroup("References")] [Required] public GameEnder gameEnder;
    [BoxGroup("References")][Required] public RoomsManager roomsManager;

    public static GameManager i{ get; private set; }
    public Player Player => player;

    void Awake()
    {
        if (i != null && i != this){
            Debug.LogWarning("There was another instance", this);
            Destroy(gameObject);
            return;
        }
        i = this;
        // DontDestroyOnLoad(gameObject);
    }
}