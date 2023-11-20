using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using NaughtyAttributes;
using UnityEngine;

public class Door : NetworkBehaviour, IDoor
{
    [SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0);
    [SerializeField] float rotationAngle = -90;
    [SerializeField] float moveTime = 0.75f;
    [SerializeField] bool beginOpen = false;
    [SerializeField] Optional<float> autoCloseTime;
    [SerializeField] bool colliderWhenOpen = false;
    
    float lastMoveStartedTime = 0;
    Vector3 startRotation;
    Collider[] colliders;
    [ReadOnly][SyncVar] bool lockState;
    [ShowNativeProperty] bool Opened => transform.rotation.eulerAngles != startRotation;

    void Awake(){
        colliders = GetComponentsInChildren<Collider>();
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        startRotation = transform.rotation.eulerAngles;
        if (beginOpen){
            LocalOpen();
        }
    }
    void Update()
    {
        if (base.IsServer)
        {
            ServerUpdate();
        }
        ResolveColliders();
    }
    [ServerRpc(RequireOwnership = false)]
    public void LockState(bool lockState){
        this.lockState = lockState;
    }
    private void ResolveColliders()
    {
        if (colliderWhenOpen)
        {
            return;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = Opened;
        }
    }

    void ServerUpdate(){
        if (!autoCloseTime){
            return;
        }
        if (!CanChangeState()){
            return;
        }
        if (Time.time - autoCloseTime - moveTime < lastMoveStartedTime){
            return;
        }
        LocalClose();
    }
    public virtual bool CanCharacterUse(ICharacter character, bool onInteract){
        return true;
    }
    public bool CanChangeState(){
        if (Time.time - lastMoveStartedTime < moveTime)
        {
            return false;
        }
        if (lockState){
            return false;
        }
        return true;
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void ChangeState()
    {
        if (!CanChangeState()){
            return;
        }
        if (Opened)
        {
            LocalClose();
            return;
        }
        LocalOpen();
    }
    [ServerRpc(RequireOwnership = false)]
    public void Open(){
        if (!CanChangeState()){
            return;
        }
        LocalOpen();
    }
    [ServerRpc(RequireOwnership = false)]
    public void Close(){
        if (!CanChangeState()){
            return;
        }
        LocalClose();
    }
    private void LocalOpen()
    {
        if (!base.IsServer){
            return;
        }
        if (Opened){
            return;
        }
        Rotate(rotationAngle);
    }
    private void LocalClose()
    {
        if (!base.IsServer){
            return;
        }
        if (!Opened)
        {
            return;
        }
        Rotate(-rotationAngle);
    }

    private void Rotate(float angle)
    {
        if (!base.IsServer){
            return;
        }
        LeanTween.cancel(gameObject);
        lastMoveStartedTime = Time.time;
        transform.LeanRotateAroundLocal(rotationAxis.normalized, angle, moveTime);
    }

    public virtual void Interact(ICharacter character){
        if (!CanCharacterUse(character, true)){
            return;
        }
        ChangeState();
    }
    public float GetHoldDuration(){
        return 0;
    }
}
