using UnityEngine;

namespace Blockit.Persistent
{
  public class PlayerDeath : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
        GameController.ReloadLevel();
    }
  }


}