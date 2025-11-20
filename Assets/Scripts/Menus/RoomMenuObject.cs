using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class RoomMenuObject : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] TextMeshProUGUI roomNameText;
    [BoxGroup("References")][Required][SerializeField] Button selectButton;
    [BoxGroup("References")][Required][SerializeField] RoomsMenu roomsMenu;
    
    [SerializeField] string notUnlockedText = "???";
    
    GameObject room;

    void Awake()
    {
        selectButton.onClick.AddListener(OnSelected);
    }

    void OnSelected()
    {
        if (room == null){
            Debug.LogWarning("Room is null, cannot select.", this);
        }
        roomsMenu.RoomSelected(room);
    }

    public void NotUnlocked()
    {
        roomNameText.text = notUnlockedText;
    }

    public void SetRoom(GameObject room)
    {
        this.room = room;
        var roomComponent = room.GetComponent<Room>();
        roomNameText.text = roomComponent.roomName;
    }
}