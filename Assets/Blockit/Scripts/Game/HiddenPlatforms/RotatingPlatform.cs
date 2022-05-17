using System.Collections;
using UnityEngine;

namespace Blockit.Level
{
  // Support for rotating platforms.

  public class RotatingPlatform : HiddenPlatform
  {
    [SerializeField] private float rotX = 0;
    [SerializeField] private float rotZ = -90;

    private Quaternion destRot;
    private Quaternion startRot;

    private void Awake()
    {
      Quaternion deltaRot = Quaternion.Euler(new Vector3(rotX, 0, rotZ));

      destRot = transform.rotation * deltaRot;   // modyfikacja obecnej rotacji
      startRot = transform.rotation;
    }

    public override void ShowPlatform() => StartCoroutine(Show());
    public override void HidePlatform() => StartCoroutine(Hide());

    private IEnumerator Show()
    {
      while (transform.rotation != destRot)
      {
        transform.rotation = Quaternion.Lerp(transform.rotation, destRot, 0.1f);
        yield return null;
      }
    }

    private IEnumerator Hide()
    {
      while (transform.rotation != startRot)
      {
        transform.rotation = Quaternion.Lerp(transform.rotation, startRot, 0.1f);
        yield return null;
      }
    }
  }
}