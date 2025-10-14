using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RoomChooser))]
public class RoomGenerator : MonoBehaviour
{
    const int GenerationTries = 10;
    [SerializeField] [HideIf("setSeedBasedOnGameNr")] Optional<int> seed;
    [SerializeField] bool setSeedBasedOnGameNr = true;

    public void Generate()
    {
        if (setSeedBasedOnGameNr){
            var seedValue = PlayerPrefs.GetInt("Games Started", 0);
            Random.InitState(seedValue);
        }
        else if (seed){
            Random.InitState(seed);
        }
        var roomChooser = GetComponent<RoomChooser>();
        for (var i = 0; i < GenerationTries; i++){
            var choosenRooms = roomChooser.ChooseRooms();
            if (choosenRooms != null){
                GenerateRooms(choosenRooms);
                return;
            }
        }
        Debug.LogError("Generation Failed", this);
    }

    void GenerateRooms(Dictionary<RoomSpawner, GameObject> matchedRoomWithSpawner)
    {
        foreach (var match in matchedRoomWithSpawner) match.Key.SpawnRoom(match.Value);
    }
}