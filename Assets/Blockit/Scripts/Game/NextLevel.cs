using UnityEngine;
using Blockit.Persistent;

namespace Blockit.Level
{
  // Loading successive game scenes periodically.

  public class NextLevel : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      // SoundManager.GetSoundEffect(1, 1f);
      GameController.LoadNextLevel();
    }
  }
}