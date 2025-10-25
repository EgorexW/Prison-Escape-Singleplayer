using UnityEngine;

public class CameraMenuMove : MonoBehaviour
{
    [SerializeField] Vector3 targetPos = new(0, 1.5f, 0);
    [SerializeField] float moveTime = 1;

    void Start()
    {
        transform.LeanMove(targetPos, moveTime);
    }
}