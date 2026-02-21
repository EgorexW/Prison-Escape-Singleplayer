using System;
using UnityEngine;

public class EditorOnlyGameObject : MonoBehaviour
{
    bool destroy = false;
    
    void Awake()
    {
        if (destroy){
            Destroy(gameObject);
            return;
        }
        gameObject.SetActive(false);
    }
}
