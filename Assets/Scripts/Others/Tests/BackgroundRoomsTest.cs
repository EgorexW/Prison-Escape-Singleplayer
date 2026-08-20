// AI Generated code

using UnityEngine;
using UnityEngine.TestTools; // for [UnityTest]
using NUnit.Framework;       // for [Test], Assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // for IEnumerator

public class BackgroundRoomsTest
{
    public static IEnumerable<int> RoomIndexes()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return i;
        }
    }

    [UnityTest]
    public IEnumerator SpawnRoom_NoRuntimeErrors([ValueSource(nameof(RoomIndexes))] int roomIndex)
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            SceneManager.LoadScene("Main Menu");
            yield return null; // Wait a frame for the scene to load
            yield return null; // Wait a frame for the scene to load
        }
        
        // Grab the helper from the test scene
        var helper = Object.FindAnyObjectByType<MainMenuTestHelper>();
        Assert.IsNotNull(helper, "RoomTestHelper not found in the scene");

        var rooms = helper.roomPrefabs;
        var roomBackground = helper.roomBackground;
        
        if (roomIndex >= rooms.prefabs.Count)
        {
            Assert.Ignore($"Room index {roomIndex} is out of range. Total rooms: {rooms.prefabs.Count}");
        }
        
        var prefab = rooms.prefabs[roomIndex];
        
            Debug.Log($"Spawning {prefab.name}");
            
            try
            {
                roomBackground.SpawnRoom(prefab);
            }
            catch (System.Exception e)
            {
                Assert.Fail($"Instantiation failed for '{prefab.name}': {e}");
            }

            // Wait a few frames to catch runtime errors
            int framesToWait = 300;
            for (int i = 0; i < framesToWait; i++)
                yield return null;
    }
}