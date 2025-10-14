using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class SpawnableRoom : MonoBehaviour
{
    [GetComponent][SerializeField] public DoorwayConfig doorway;
    
    [SerializeField] Transform joinPoint;
    [SerializeField] Transform dirPoint;
    [SerializeField] public RoomTrait[] traits;
    public bool discovered;
    public string Name => doorway.roomName;

    public void SetPos(Vector3 joinPos, Vector3 dir)
    {
        SetJoinPos(joinPos);
        SetDir(dir);
        SetJoinPos(joinPos);
    }

    void SetJoinPos(Vector3 joinPos)
    {
        transform.position = joinPos - (joinPoint.position - transform.position);
    }

    void SetDir(Vector3 dir)
    {
        var currentDir = dirPoint.position - joinPoint.position;
        var rotation = Quaternion.FromToRotation(currentDir, dir);
        ;
        transform.rotation *= rotation;
    }
}