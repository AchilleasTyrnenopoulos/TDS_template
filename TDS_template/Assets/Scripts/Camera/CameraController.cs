using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        //get the virtual camera component
        var vc = GetComponent<CinemachineVirtualCamera>();
        //get the camera target transform of the player character game object
        Transform cameraTarget = GameObject.FindGameObjectWithTag("Player").transform.Find(Strings.CameraTarget);
        //set the follow property of the virtual camera
        vc.Follow = cameraTarget;
    }
}
