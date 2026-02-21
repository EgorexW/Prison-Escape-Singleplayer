using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    
    [SerializeField] bool sendMessageInsteadOfDeactivate = false;

    [FoldoutGroup("Events")] public UnityEvent<GameObject> onCreateObject = new();

    readonly List<GameObject> activeObjs = new();
    readonly Queue<GameObject> inactiveObjs = new();
    
    void Awake()
    {
        SetPrefab();
        prefab.SetActive(false);
    }

    void OnValidate()
    {
        SetPrefab();
    }

    void SetPrefab()
    {
        if (prefab != null){
            return;
        }
        if (transform.childCount <= 0){
            return;
        }
        prefab = transform.GetChild(0).gameObject;
    }

    public void SetCount(int count)
    {
        while (activeObjs.Count > count) RemoveObject();
        while (activeObjs.Count < count) AddObject();
    }

    public GameObject AddObject()
    {
        if (inactiveObjs.Count < 1){
            CreateObjectUI();
        }
        var obj = inactiveObjs.Dequeue();
        obj.SetActive(true);
        activeObjs.Add(obj);
        obj.SendMessage("OnObjectPoolActivate", SendMessageOptions.DontRequireReceiver);
        return obj;
    }

    public void RemoveObject(GameObject obj = null)
    {
        var iconUI = activeObjs[^1];
        if (obj == null){
            obj = activeObjs[^1];
        }
        if (obj == null || !activeObjs.Contains(obj)){
            return;
        }
        if (sendMessageInsteadOfDeactivate){
            Debug.Log("Sending OnObjectPoolDeactivate to " + obj.name, obj);
            obj.SendMessage("OnObjectPoolDeactivate", SendMessageOptions.RequireReceiver);
        }
        else{
            obj.SetActive(false);
        }
        activeObjs.Remove(obj);
        inactiveObjs.Enqueue(obj);
    }

    public void Clear()
    {
        SetCount(0);
    }

    public List<GameObject> GetActiveObjs()
    {
        return new List<GameObject>(activeObjs);
    }

    protected virtual GameObject CreateObjectUI()
    {
        var newObj = Instantiate(prefab, transform);
        inactiveObjs.Enqueue(newObj);
        onCreateObject.Invoke(newObj);
        newObj.SendMessage("OnObjectPoolCreate", SendMessageOptions.DontRequireReceiver);
        return newObj;
    }

    #region Show/Hide

    public void Hide()
    {
        // onHide.Invoke();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // onShow.Invoke();
        gameObject.SetActive(true);
    }

    #endregion
}