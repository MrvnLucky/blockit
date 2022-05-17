using UnityEngine;
using System.Collections;

namespace Blockit.Level
{
  // Support for sliding platforms.

  public class SlidingPlatform : HiddenPlatform
  {
    [SerializeField] private float posX = 0;
    [SerializeField] private float posY = 0;
    [SerializeField] private float posZ = -1;

    private Vector3 destPos;
    private Vector3 startPos;

    private void Awake()
    {
      destPos = transform.position + new Vector3(posX, posY, posZ);
      startPos = transform.position;
    }

    public override void ShowPlatform() => StartCoroutine(Show());
    public override void HidePlatform() => StartCoroutine(Hide());

    private IEnumerator Show()
    {
      while (transform.position != destPos)
      {
        transform.position = Vector3.Lerp(transform.position, destPos, 0.1f);
        yield return null;
      }
    }

    private IEnumerator Hide()
    {
      while (transform.position != startPos)
      {
        transform.position = Vector3.Lerp(transform.position, startPos, 0.1f);
        yield return null;
      }
    }
  }
}