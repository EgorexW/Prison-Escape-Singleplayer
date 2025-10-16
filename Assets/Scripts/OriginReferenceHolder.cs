using System;
using UnityEngine;
using UnityEngine.Serialization;

public class OriginReferenceHolder : MonoBehaviour
{
    [SerializeField] public GameObject origin;

    void OnValidate()
    {
        origin = gameObject;
    }
}