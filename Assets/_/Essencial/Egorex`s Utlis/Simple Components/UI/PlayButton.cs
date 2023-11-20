using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class PlayButton : MonoBehaviour
{
    [SerializeField][Scene] protected string sceneName = "BossFight";
    [SerializeField] protected bool async = false;
    public virtual void Play(){
        
        if (async){
            SceneManager.LoadSceneAsync(sceneName);
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }
#if UNITY_EDITOR
    void Reset()
    {
        if (!TryGetComponent<Button>(out Button button)){
            return;
        }
        for(int i = 0; i < button.onClick.GetPersistentEventCount(); i++){
            if (button.onClick.GetPersistentMethodName(i) == "Play"){
                return;
            }
        }
        UnityEventTools.AddPersistentListener(button.onClick, Play);
    }
#endif
}
