using Cinemachine;
using UnityEngine;

public class CameraHandler: ICameraHandler
{
    private CinemachineFreeLook _freeLookCamera;
    public Camera GetCam()
    {
        return Camera.main;
    }

    public void SetCamFollow(Transform lookPoint)
    {
         _freeLookCamera = GetFreeLookCam();
         _freeLookCamera.Follow = lookPoint;
         _freeLookCamera.LookAt = lookPoint;
    }

    public void FreeLookCamSettings()
    {
        
    }
    public CinemachineFreeLook GetFreeLookCam()
    {
        var camObject = GameObject.FindGameObjectWithTag("FreeLookCam");
        return camObject.GetComponent<CinemachineFreeLook>();
    }
}

public interface ICameraHandler
{
    Camera GetCam();
    CinemachineFreeLook GetFreeLookCam();
    public void SetCamFollow(Transform lookPoint);
}
