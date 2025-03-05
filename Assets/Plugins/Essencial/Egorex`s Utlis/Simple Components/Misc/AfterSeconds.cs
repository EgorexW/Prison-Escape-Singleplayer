using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfterSeconds : MonoBehaviour
{
    [SerializeField] UnityEvent call;
    [SerializeField] float time;

    public void StartCounting(){
        General.CallAfterSeconds(call.Invoke, time);
    }
}
