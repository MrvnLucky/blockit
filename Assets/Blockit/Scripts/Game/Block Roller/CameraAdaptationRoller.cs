using UnityEngine;

namespace Blockit.Level
{
  // Another cuboid rolling scenario: take note of the position of the camera.

  public class CameraAdaptationRoller : UniversalBlockRoller
  {
    private Transform mainCamTrans;

    private new void Awake()
    {
      base.Awake();
      mainCamTrans = Camera.main.transform;
    }

    protected override Vector3[] ResolveDirection()     // roll directions depend on the camera viewing direction
    {
      Vector3 vector = mainCamTrans.forward;
      Vector3 normal = Vector3.up;
      Vector3 project = Vector3.ProjectOnPlane(vector, normal);               // The vector projection onto the xz plane.
      float angle = Vector3.SignedAngle(Vector3.forward, project, normal);    // The angle between the vector obtained from the projection and the global forward vector.

      Vector3[] directions = new Vector3[4];

      if (angle > -45 && angle < 45)
      {
        directions[0] = Vector3.right;
        directions[1] = -Vector3.right;
        directions[2] = Vector3.forward;
        directions[3] = -Vector3.forward;
      }
      else if (angle > 45 && angle < 135)
      {
        directions[0] = -Vector3.forward;
        directions[1] = Vector3.forward;
        directions[2] = Vector3.right;
        directions[3] = -Vector3.right;
      }
      else if (angle > -135 && angle < -45)
      {
        directions[0] = Vector3.forward;
        directions[1] = -Vector3.forward;
        directions[2] = -Vector3.right;
        directions[3] = Vector3.right;
      }
      else //if ((angle > 135 && angle <= 180) || (angle < -135 && angle >= -180))
      {
        directions[0] = -Vector3.right;
        directions[1] = Vector3.right;
        directions[2] = -Vector3.forward;
        directions[3] = Vector3.forward;
      }

      return directions;
    }
  }
}