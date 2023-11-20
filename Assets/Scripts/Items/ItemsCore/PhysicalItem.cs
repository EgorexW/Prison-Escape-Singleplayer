using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Analytics;

public class PhysicalItem : NetworkBehaviour, IItemObserver
{
    [SerializeField] Vector3 equipPos;
    [SerializeField] Vector3 equipRotation;
    [SerializeField] float equipScaleChange = 1;
    [SerializeField] float equipTime = 0.5f;
    new Rigidbody rigidbody;
    const float ContinuousCollisionDetectionTimeOnThrow = 2f;

#if UNITY_EDITOR
    [Button]
    void CopyFromTransform(){
        equipPos = transform.localPosition;
        equipRotation = transform.localRotation.eulerAngles;
    }
#endif
    public override void OnStartServer()
    {
        base.OnStartServer();
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Use(ICharacter character, bool alternative = false)
    {

    }
    [ServerRpc(RequireOwnership = false)]
    public void ServerOnPickup(Transform root)
    {
        gameObject.SetActive(false);
        NetworkObject.SetParent(root.GetComponent<NetworkObject>());
        transform.SetParent(root);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;     
        rigidbody.isKinematic = true; 
    }
    [ServerRpc(RequireOwnership = false)]
    public void ServerOnEquip()
    {
        gameObject.SetActive(true);
        transform.LeanMoveLocal(equipPos, equipTime);
        transform.localRotation = Quaternion.Euler(equipRotation);
        transform.localScale *= equipScaleChange;
    }
    [ServerRpc(RequireOwnership = false)]
    public void ServerOnDeequip()
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; 
        transform.localScale *= 1 / equipScaleChange;  
    }
    [ServerRpc(RequireOwnership = false)]
    public void ServerOnDrop(Vector3 force)
    {
        gameObject.SetActive(true);   
        transform.SetParent(null);
        NetworkObject.UnsetParent();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode.Impulse); 
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        General.CallAfterSeconds(() => rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete, ContinuousCollisionDetectionTimeOnThrow);
    }

    public void OnPickup(ICharacter character)
    {
        ServerOnPickup(character.GetAimTransform());
    }

    public void OnEquip(ICharacter character)
    {
        ServerOnEquip();
    }

    public void OnDeequip(ICharacter character)
    {
        ServerOnDeequip();
    }

    public void OnDrop(ICharacter character, Vector3 force)
    {
        ServerOnDrop(force);
    }

    public void HoldUse(ICharacter character, bool alternative = false)
    {

    }

    public void StopUse(ICharacter character, bool alternative = false)
    {

    }
}
