using UnityEngine;

namespace Blockit.Level
{
  // Support for hidden platforms.

  abstract public class HiddenPlatform : MonoBehaviour
  {
    public abstract void ShowPlatform();
    public abstract void HidePlatform();
  }
}