using UnityEngine;

namespace Blockit.Level
{
  // Checking the greatest height of the figure (whether the figure stands so that it is tallest and not lying).

  public class IsTallDetector
  {
    public bool IsTallResolver(Transform t)
    {
      float[] scales = new float[3];                  // largest scale vectors

      float xScale = t.localScale.x;
      float yScale = t.localScale.y;
      float zScale = t.localScale.z;

      int count = 0;

      if (xScale >= yScale && xScale >= zScale) { scales[0] = xScale; count++; }
      if (yScale >= xScale && yScale >= zScale) { scales[1] = yScale; count++; }
      if (zScale >= xScale && zScale >= yScale) { scales[2] = zScale; count++; }

      bool isTall = false;

      if (count < 3)   // the solid is not a cube, so you can check if the solid is currently tall
      {
        if (scales[0] != 0)
          if (Vector3.Distance(t.right, Vector3.up) < 0.1f || Vector3.Distance(t.right, -Vector3.up) < 0.1f)
            isTall = true;

        if (scales[1] != 0)
          if (Vector3.Distance(t.up, Vector3.up) < 0.1f || Vector3.Distance(t.up, -Vector3.up) < 0.1f)
            isTall = true;

        if (scales[2] != 0)
          if (Vector3.Distance(t.forward, Vector3.up) < 0.1f || Vector3.Distance(t.forward, -Vector3.up) < 0.1f)
            isTall = true;
      }

      return isTall;
    }
  }
}