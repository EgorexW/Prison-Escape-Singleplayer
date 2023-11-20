using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Cinemachine;

//This is made by Bobsi Unity - Youtube
public class PlayerNetworking : NetworkBehaviour
{
    [SerializeField] Transform cameraRoot;


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            CinemachineVirtualCameraBase camera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
            camera.Follow = cameraRoot;
        }
    }
}