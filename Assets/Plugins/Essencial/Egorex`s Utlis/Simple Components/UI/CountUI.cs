using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [HideInInspector] public UnityEvent<int> onUpdate;

    public void UpdateUI(int count){
        onUpdate.Invoke(count);
        UpdateUI(count.ToString());
    }
    public void UpdateUI(string textToShow){
        text.text = textToShow;
    } 
}
