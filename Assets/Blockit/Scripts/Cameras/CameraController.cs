using UnityEngine;

namespace Broniek.Stuff.Cameras
{
  // Camera controller dealing with player inputs.

  public class CameraController : MonoBehaviour
  {
    private CameraRotation vr;
    private bool rotAllowed;

    private void Awake() => vr = new CameraRotation(10);
    private void Update() => RotateCamera();

    private void RotateCamera()
    {
      if (Input.GetMouseButtonDown(1)) rotAllowed = true;
      if (Input.GetMouseButtonUp(1)) rotAllowed = false;

      if (rotAllowed)
        vr.OrbitCamera(transform.position);
    }
  }
}