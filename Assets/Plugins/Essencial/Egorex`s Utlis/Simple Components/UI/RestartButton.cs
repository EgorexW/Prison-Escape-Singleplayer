using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : PlayButton
{
    void Awake(){
        sceneName = SceneManager.GetActiveScene().name;
    }
}
