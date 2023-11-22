using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public partial class Character{
    [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 4.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float sprintSpeedMod = 1.5f;
    float speedMod = 1;

    CharacterController characterController;
    IMover firstPersonController;

    void SetFirstPersonController(){
        if (firstPersonController == null){
            return;
        }
        firstPersonController.MoveSpeed = GetMoveSpeed;
        firstPersonController.SprintSpeed = GetSprintSpeed;
    }
    public float GetMoveSpeed(){
        return moveSpeed * speedMod;
    }
    public float GetSprintSpeed(){
        return GetMoveSpeed() * sprintSpeedMod;
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
}
