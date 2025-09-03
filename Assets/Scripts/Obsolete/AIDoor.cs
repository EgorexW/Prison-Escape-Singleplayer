using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(DoorObsolete))]
public class AIDoor : MonoBehaviour, IAIObject
{
    [FormerlySerializedAs("door")] [GetComponent] [SerializeField] DoorObsolete doorObsolete;

    [SerializeField] List<GameObject> workingLights;

    [SerializeField] float longDurationMultiplier = 3;
    [SerializeField] AIObjectStats stats;
    [SerializeField] Discovery noticedScore = new Discovery(){ score = 1};

    GameDirector gameDirector;
    float baseDuration;

    public GameObject GameObject => gameObject;
    public bool IsActive{ get; private set; }

    void Awake()
    {
        workingLights.ForEach(light => light.SetActive(false));
        baseDuration = doorObsolete.holdDuration;
    }

    public AIObjectStats Stats => stats;

    public void SetActive(bool active)
    {
        IsActive = active;
        if (active){
            doorObsolete.Close();
        }
        // door.SetLockState(active);
        doorObsolete.holdDuration = active ? longDurationMultiplier * baseDuration : baseDuration;
        workingLights.ForEach(light => light.SetActive(active));
    }


    public void Init(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }
}