using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ReloadSceneOnStopServer : NetworkBehaviour
{
    public override void OnStopServer()
    {
        base.OnStopServer();
        if (!gameObject.activeInHierarchy){
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
