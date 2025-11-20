using Sirenix.OdinInspector;
using UnityEngine;

public class RoomsMenu : UIElement
{
    [BoxGroup("References")][Required][SerializeField] ObjectsFactory roomsFactory;
    [BoxGroup("References")][Required][SerializeField] PrefabList roomsList;
    [BoxGroup("References")][Required][SerializeField] RoomBackground roomBackground;
    
    public override void Show()
    {
        base.Show();
        var roomCount = roomsList.prefabs.Count;
        roomsFactory.Clear();
        for (var i = 0; i < roomCount; i++){
            var unlocked = PlayerPrefs.GetInt(PlayerPrefsKeys.RoomDiscoveredPrefix + i, 0) == 1;
            var roomObject = roomsFactory.AddObject();
            if (!unlocked){
                roomObject.GetComponent<RoomMenuObject>().NotUnlocked();
                continue;
            }
            var room = roomsList.prefabs[i];
            roomObject.GetComponent<RoomMenuObject>().SetRoom(room);
        }
    }
    
    public void RoomSelected(GameObject room)
    {
        roomBackground.SpawnRoom(room);
        Hide();
    }
}