using System;
using UnityEngine;

public class CameraMenuMove : MonoBehaviour
{
    [SerializeField] Vector3 targetPos = new(0, 1.5f, 0);
    [SerializeField] float moveTime = 1;
    
    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    public void StartSequence()
    {
        ResetPosition();
        Move();
    }
    public void ResetPosition()
    {
        LeanTween.cancel(gameObject);
        transform.position = startPos;
    }
    public void Move()
    {
        transform.LeanMove(targetPos, moveTime);
    }
}