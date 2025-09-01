using System;
using Sirenix.OdinInspector;
using UnityEngine;

public partial class Player{
    [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 4.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float sprintSpeedMod = 1.5f;
    float speedMod = 1;
    
    float stamina = 1;
    
    [BoxGroup("Stamina")][ShowInInspector]public float Stamina => stamina;
    [BoxGroup("Stamina")][SerializeField] float staminaUseRate = 0.15f;
    [BoxGroup("Stamina")][SerializeField] float staminaRegenRate = 0.1f;

    CharacterController characterController;
    IMover firstPersonController;

    void SetFirstPersonController(){
        if (firstPersonController == null){
            return;
        }
        firstPersonController.MoveSpeed = MoveSpeed;
        firstPersonController.SprintSpeed = SprintSpeed;
    }
    public float MoveSpeed(){
        stamina += staminaRegenRate * Time.deltaTime;
        stamina = Mathf.Min(1, stamina);
        return moveSpeed * speedMod;
    }
    public float SprintSpeed()
    {
        var canSprint = stamina > 0;
        if (!canSprint){
            return moveSpeed * speedMod;
        }
        stamina -= staminaUseRate * Time.deltaTime;
        return moveSpeed * speedMod * sprintSpeedMod;
    }
    public Transform GetTransform(){
        return transform;
    }

    public void SetPos(Vector3 position)
    {
        // Debug.Log("SetPosLocal pos " + position);
        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;
    }
    public void ModSpeed(float mod)
    {
        speedMod *= mod;
    }

    public LevelNode GetContainingNode()
    {
        Collider[] colliders = new Collider[1];
        Physics.OverlapBoxNonAlloc(transform.position, 0.1f * Vector3.one, colliders, Quaternion.identity, LayerMask.GetMask("Level Node"));
        var node = General.GetComponentFromCollider<LevelNode>(colliders[0]);
        if (!node){
            Debug.LogWarning("Player node is null", this);
        }
        return node;
    }
}
