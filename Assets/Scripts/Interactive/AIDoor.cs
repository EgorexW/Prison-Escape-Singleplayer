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
    [SerializeField] float noticedScore = 1;

    MainAI mainAI;
    float baseDuration;

    public GameObject GameObject => gameObject;
    void Awake()
    {
        door.onOpen.AddListener(OnDoorOpen);
        workingLights.ForEach(light => light.SetActive(false));
        baseDuration = door.holdDuration;
    }

    public AIObjectStats Stats => stats;

    public void SetActive(bool active)
    {
        if (active){
            door.Close();
        }
        // door.SetLockState(active);
        door.holdDuration = active ? longDurationMultiplier * baseDuration : baseDuration;
        workingLights.ForEach(light => light.SetActive(active));
    }


    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
    }

    void OnDoorOpen()
    {
        mainAI.PlayerNoticed(noticedScore);
    }
}