using System;
using Nrjwolf.Tools.AttachAttributes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class RoomNameDisplay : MonoBehaviour
{
    [GetComponent] [SerializeField] TextMeshPro text;

    void Start()
    {
        text.text = General.GetRootComponent<Room>(transform).roomName;
    }
}
