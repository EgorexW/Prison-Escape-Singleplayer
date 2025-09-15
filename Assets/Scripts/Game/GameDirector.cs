using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameDirector : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    [BoxGroup("References")] [Required] public GameTime gameTime;
    [FormerlySerializedAs("facilitySwitches")] [BoxGroup("References")] [Required] public FacilityObjects facilityObjects;
    [BoxGroup("References")] [Required] public LevelNodes levelNodes;
    [BoxGroup("References")][Required] public FacilityAnnouncements facilityAnnouncements;

    [SerializeField] bool log;

    public static GameDirector i{ get; private set; }
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

    #region Debug

    void Log(string msg)
    {
        if (log){
            Debug.Log(msg, this);
        }
    }

    #endregion
}