using UnityEngine;

namespace Blockit.Level
{
  public class Trap : MonoBehaviour
  {
    private Rigidbody rb;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other) => rb.useGravity = true;
  }
}