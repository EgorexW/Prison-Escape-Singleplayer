using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTrap : MonoBehaviour, ITrap
{
    [SerializeField] float timeToLock = 60f;
    
    Room room;

    float lockTime = float.NegativeInfinity;
    bool active = false;
    
    public void Activate()
    {
        lockTime = Time.time + timeToLock;
        active = true;
    }

    void Update()
    {
        if (!active){
            return;
        }
        if (Time.time > lockTime){
            Lock();
        }
    }

    void Lock()
    {
        var doorLocks = room.doorway.GetDoorLocks();
        foreach (var doorLock in doorLocks){
            doorLock.Lock();
        }
        var keycardReaders = room.doorway.GetKeycardReaders();
        foreach (var keycardReader in keycardReaders){
            keycardReader.Corrupted = true;
        }
    }

    public void SetRoom(Room room)
    {
        this.room = room;
    }

    public bool Eligable(Room room)
    {
        return true;
    }

    public float GetLockTime()
    {
        return lockTime;
    }

    public bool IsActive()
    {
        return active;
    }
}
