using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallbackTriggerCollider : MonoBehaviour
{
    UnityAction<Collider2D> callback;

    void Awake(){
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
    public void SetCallback(UnityAction<Collider2D> callback){
        this.callback = callback;
    }
    void OnTriggerEnter2D(Collider2D other){
        callback.Invoke(other);
    }
}
