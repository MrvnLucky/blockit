using UnityEngine;

namespace Blockit.Level
{
    // Simple Template Method pattern with Hollywood Principle (Inversion of Control)

    abstract public class Switch : MonoBehaviour
    {
        [SerializeField] protected HiddenPlatform hiddenPlatform;   // associated platform
        [SerializeField] protected bool tallRequired = true;        // The condition for activating the switch.

        protected void OnTriggerEnter(Collider other)
        {
            DoAction(other);
        }

        abstract protected void DoAction(Collider other);
    }
}