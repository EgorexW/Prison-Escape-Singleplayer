using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public enum CompareType{
    Bigger,
    Smaller,
    Equal
}
public class CheckPlayerPref : MonoBehaviour
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
    [HideIf("IsString")]
    [SerializeField] CompareType compareType = CompareType.Equal;
    [ShowIf("IsString")]
    [SerializeField] string text;
    [SerializeField] bool IsString{
        get {
            return prefType == PrefType.String;
        }
    }
    [Foldout("Events")]
    public UnityEvent onTrue;
    [Foldout("Events")]
    public UnityEvent onFalse;
    [Foldout("Events")]
    public UnityEvent<bool> onCheck;

    public void Check(){
        GetCheck();
    }
    public void Check(UnityAction<bool> callback){
        GetCheck(callback);
    }
    public bool GetCheck(UnityAction<bool> callback = null){
        bool result = false;
        switch (prefType){
            case PrefType.Int:
                result = CheckInt();
                break;
            case PrefType.Float:
                result = CheckFloat();
                break;
            case PrefType.String:
                result = CheckString();
                break;
        }
        if (callback != null){
            callback.Invoke(result);
        }
        if (result){
            onTrue.Invoke();
        } else {
            onFalse.Invoke();
        }
        onCheck.Invoke(result);
        return result;
    }
    public bool CheckInt(){
        int value = PlayerPrefs.GetInt(prefName, 0);
        switch (compareType){
            case CompareType.Bigger:
                return value > nrInt;
            case CompareType.Smaller:
                return value < nrInt;
            case CompareType.Equal:
                return value == nrInt;
        }
        return false;
    }
    public bool CheckFloat(){
        float value = PlayerPrefs.GetFloat(prefName, 0f);
        switch (compareType){
            case CompareType.Bigger:
                return value > nrInt;
            case CompareType.Smaller:
                return value < nrInt;
            case CompareType.Equal:
                return value == nrInt;
        }
        return false;
    }
    public bool CheckString(){
        string value = PlayerPrefs.GetString(prefName, "");
        return value == text;
    }
}
