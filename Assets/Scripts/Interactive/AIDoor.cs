using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Door))]
public class AIDoor : MonoBehaviour, IAIObject
{
    [GetComponent] [SerializeField] Door door;

    [SerializeField] List<GameObject> workingLights;

    [SerializeField] float longDurationMultiplier = 3;
    [SerializeField] AIObjectStats stats;
    [SerializeField] Discovery noticedScore = new Discovery(){ score = 1};

    AIDirector aiDirector;
    float baseDuration;

    public GameObject GameObject => gameObject;
    public bool IsActive{ get; private set; }

    void Awake()
    {
        door.onOpen.AddListener(OnDoorOpen);
        workingLights.ForEach(light => light.SetActive(false));
        baseDuration = door.holdDuration;
    }

    public AIObjectStats Stats => stats;

    public void SetActive(bool active)
    {
        IsActive = active;
        if (active){
            door.Close();
        }
        // door.SetLockState(active);
        door.holdDuration = active ? longDurationMultiplier * baseDuration : baseDuration;
        workingLights.ForEach(light => light.SetActive(active));
    }


    public void Init(AIDirector aiDirector)
    {
        this.aiDirector = aiDirector;
    }

    void OnDoorOpen()
    {
        AIDirector.i.PlayerDiscovery(noticedScore);
    }
}