using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

enum PrefType{
    Int,
    Float,
    String
}
public class SetPlayerPref : MonoBehaviour
{
    [SerializeField] string prefName;
    [SerializeField] PrefType prefType;
    [ShowIf("IsInt")]
    [SerializeField] int nrInt;
    [SerializeField] bool IsInt{
        get {
            return prefType == PrefType.Int;
        }
    }
    [ShowIf("IsFloat")]
    [SerializeField] float nrFloat;
    [SerializeField] bool IsFloat{
        get {
            return prefType == PrefType.Float;
        }
    }
    [ShowIf("IsString")]
    [SerializeField] string text;
    public bool IsString{
        get {
            return prefType == PrefType.String;
        }
    }

    public void Set(){
        switch (prefType){
            case PrefType.Int:
                SetInt();
                break;
            case PrefType.Float:
                SetFloat();
                break;
            case PrefType.String:
                SetString();
                break;
        }
    }
    void SetInt(){
        PlayerPrefs.SetInt(prefName, nrInt);
    }
    void SetFloat(){
        PlayerPrefs.SetFloat(prefName, nrFloat);
    }
    void SetString(){
        PlayerPrefs.SetFloat(prefName, nrFloat);
    }
}
