using UnityEngine;
using System.Collections;

namespace Broniek.Stuff.Cameras
{
  // The class responsible for zooming in and out of the camera from the protagonist.

  public class CameraZoom : MonoBehaviour
  {
    public static CameraZoom Instance { get; private set; }

    private Vector3 resetPosition;
    private Quaternion resetRotation;
    private Camera cam;

    private void Awake()
    {
      Instance = this;

      resetPosition = transform.position;
      resetRotation = transform.rotation;

      cam = GetComponent<Camera>();
    }

    private void Update() => SetZoom();

    private void SetZoom()
    {
      float scroll = Input.mouseScrollDelta.y;
      scroll = Mathf.Clamp(scroll, -1f, 1f);

      if (!cam.orthographic)
        transform.Translate(scroll * transform.forward, Space.World);
      else
      {
        float size = cam.orthographicSize - scroll;
        cam.orthographicSize = Mathf.Clamp(size, 1f, Mathf.Infinity);
      }
    }

    public void ResetCamera() => StartCoroutine(SmoothReset());

    private IEnumerator SmoothReset()
    {
      while ((transform.position - resetPosition).magnitude > 0.2f)
      {
        transform.position = Vector3.Lerp(transform.position, resetPosition, 0.005f);
        transform.rotation = Quaternion.Lerp(transform.rotation, resetRotation, 0.005f);

        yield return null;
      }
    }
  }
}