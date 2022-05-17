using UnityEngine;
using System.Collections;

namespace Blockit.Level
{
    public class SwitchShow : Switch
    {
        protected override void DoAction(Collider other)
        {
            StartCoroutine(Detect(other.transform));
        }

        private IEnumerator Detect(Transform t)
        {
            yield return new WaitForSeconds(0.1f);

            IsTallDetector detector = new IsTallDetector();

            if (detector.IsTallResolver(t) == tallRequired)
                hiddenPlatform.ShowPlatform();
        }
    }
}