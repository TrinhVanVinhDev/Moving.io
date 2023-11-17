using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform playerTransf;
    private Vector3 offSetPosCam = new Vector3(0, 20f, -10f);
    private Vector3 offSetRotCam = new Vector3(45f, 0f, 0f);
    private Vector3 offSetPosCamOnChangeEquip = new Vector3(7.5f, 1f, 1f);
    private Vector3 offSetRotCamOnChangeEquip = new Vector3(0f, -90f, 0f);
    private Vector3 offSetPosCamOnHome = new Vector3(0f, 3f, 10f);
    private Vector3 offSetRotCamOnHome = new Vector3(10f, 180f, 0f);

    private Quaternion rotationLast;
    private Vector3 positionLast;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransf.position + positionLast;
        transform.rotation = rotationLast;
    }

    public void CameraInGamePlay()
    {
        positionLast = offSetPosCam;
        rotationLast = Quaternion.Euler(offSetRotCam);
    }

    public void CameraInMenuChangeEquip()
    {
        positionLast = offSetPosCamOnChangeEquip;
        rotationLast = Quaternion.Euler(offSetRotCamOnChangeEquip);
    }

    public void CameraInMenuHome()
    {
        positionLast = offSetPosCamOnHome;
        rotationLast = Quaternion.Euler(offSetRotCamOnHome);
    }
}
