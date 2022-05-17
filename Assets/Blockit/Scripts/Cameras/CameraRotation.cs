using UnityEngine;

namespace Broniek.Stuff.Cameras
{
  // Performing the appropriate camera orbiting around the indicated point.

  public class CameraRotation
  {
    private readonly float rotationSpeed = 10;
    private readonly Transform camTransform;
    private Vector3 lastCenter;                     // midpoint in the last game frame (to detect midpoint change)

    public CameraRotation(float rotationSpeed)
    {
      this.rotationSpeed = rotationSpeed;
      camTransform = Camera.main.transform;
    }

    public void OrbitCamera(Vector3 center)
    {
      float posX = Input.GetAxis("Mouse X");
      float posY = Input.GetAxis("Mouse Y");

      EnsureTargetDirection(center);

      camTransform.RotateAround(center, Vector3.up, rotationSpeed * posX);            // Orbit around a fixed axis of rotation.
      camTransform.RotateAround(center, camTransform.right, -rotationSpeed * posY);   // Orbit around a variable axis of rotation.
    }

    private void EnsureTargetDirection(Vector3 center)                                  // keep the view direction
    {
      if (Vector3.Distance(lastCenter, center) > 0f)                                  // change of the center of the sphere
        camTransform.LookAt(center);                                                // look towards the center

      lastCenter = center;
    }
  }
}