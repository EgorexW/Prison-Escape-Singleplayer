using System;
using UnityEngine;

public class KeycardUploader : Equipment
{
    void Awake()
    {
        destroyOnUse = false;
    }

    protected override void Apply(Player player)
    {
        var heldItem = player.GetHeldItem();
        if (heldItem == null){
            // Debug.Log("No item held, cannot upload keycard.");
            return;
        }
        if (!heldItem.TryGetComponent(out Keycard keycard)){
            // Debug.Log("Held item is not a keycard, cannot upload.");
            return;
        }
        destroyOnUse = true;
        UploadKeycard(keycard);
        // player.RemoveItem(heldItem);
        // Destroy(heldItem.gameObject);
    }

    void UploadKeycard(Keycard keycard)
    {
        int count = 0;
        var levelNodesRoomNodes = GameManager.i.levelNodes.RoomNodes.Copy();
        levelNodesRoomNodes.Shuffle();
        foreach (var roomNode in levelNodesRoomNodes){
            var accessLevel = roomNode.room.doorway?.accessLevel;
            if (accessLevel == null){
                continue;
            }
            if (!keycard.ReadKeycard(accessLevel))
            {
                continue;
            }
            roomNode.room.doorway.GetDoorLocks().ForEach(doorLock => doorLock.Unlock());
            count++;
            keycard.OnAccessGranted();
            // Debug.Log($"Unlocked doors with access level {accessLevel} in room {roomNode.room.name}", roomNode.room);
        }
        var annoucement = new FacilityAnnouncement(){
            message = "Unlocked " + count + " doors with access level: " + keycard.AccessLevel.displayName
        };
        GameManager.i.facilityAnnouncements.AddAnnouncement(annoucement);
    }
}
