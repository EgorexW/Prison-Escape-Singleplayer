using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LightZone : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameInit gameInit;
    
    [GetComponent][SerializeField] BoxCollider boxCollider;
    
    [BoxGroup("References")][Required][SerializeField] Material material;
    [BoxGroup("References")][Required][SerializeField] Color color;

    void Awake()
    {
        gameInit.onFinish.AddListener(Work);
    }

    void Work()
    {
        var bounds = boxCollider.bounds;
        var colliders = General.OverlapBounds(bounds);
        foreach (var collider in colliders)
        {
            var ceilingLight = General.GetComponentFromCollider<CeilingLight>(collider);
            if (ceilingLight == null){
                continue;
            }
            ceilingLight.SetLight(material, color);
        }
    }
}
