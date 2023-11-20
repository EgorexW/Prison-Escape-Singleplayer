using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Analytics;

public class PhysicalItem : MonoBehaviour, IItemObserver
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
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Use(ICharacter character, bool alternative = false)
    {

    }
    public void OnPickup(ICharacter character)
    {
        gameObject.SetActive(false);
        transform.SetParent(character.GetAimTransform());
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;     
        rigidbody.isKinematic = true; 
    }

    public void OnEquip(ICharacter character)
    {
        gameObject.SetActive(true);
        transform.LeanMoveLocal(equipPos, equipTime);
        transform.localRotation = Quaternion.Euler(equipRotation);
        transform.localScale *= equipScaleChange;
    }

    public void OnDeequip(ICharacter character)
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; 
        transform.localScale *= 1 / equipScaleChange; 
    }

    public void OnDrop(ICharacter character, Vector3 force)
    {
        gameObject.SetActive(true);   
        transform.SetParent(null);
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode.Impulse); 
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        General.CallAfterSeconds(() => rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete, ContinuousCollisionDetectionTimeOnThrow);

    }

    public void HoldUse(ICharacter character, bool alternative = false)
    {

    }

    public void StopUse(ICharacter character, bool alternative = false)
    {

    }
}
