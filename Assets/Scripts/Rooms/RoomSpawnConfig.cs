using UnityEngine;

public class RoomSpawnConfig : MonoBehaviour
{
    [SerializeField] Transform roomCenter;
    [SerializeField] Transform joinPoint;
    [SerializeField] Transform dirPoint;

    public void Spawn(Vector3 joinPos, Vector3 dir)
    {
        SetJoinPos(joinPos);
        SetDir(dir);
        SetJoinPos(joinPos);
    }

    void SetJoinPos(Vector3 joinPos)
    {
        roomCenter.position = joinPos - (joinPoint.position - roomCenter.position);
    }

    void SetDir(Vector3 dir)
    {
        var currentDir = dirPoint.position - joinPoint.position;
        var rotation = Quaternion.FromToRotation(currentDir, dir);
        roomCenter.rotation *= rotation;
    }
}