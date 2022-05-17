using UnityEngine;
using System.Collections;

namespace Blockit.Level
{
    public class SwitchHide : Switch
    {
        protected override void DoAction(Collider other)
        {
            if (tallRequired)                                       // If a vertical orientation is suggested,
                StartCoroutine(Detect(other.transform));            // only the vertical orientation of the block is considered.
            else                                                    // If a horizontal orientation is suggested,
                hiddenPlatform.HidePlatform();                      // we take into account any orientation of the block.
        }

        private IEnumerator Detect(Transform t)
        {
            yield return new WaitForSeconds(1f);

            IsTallDetector detector = new IsTallDetector();

            if (detector.IsTallResolver(t) == tallRequired)
                hiddenPlatform.HidePlatform();
        }
    }
}