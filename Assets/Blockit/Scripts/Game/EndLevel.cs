using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Blockit.Persistent;

namespace Blockit.Level
{
    public class EndLevel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // SoundManager.GetSoundEffect(1, 1f);
            SceneManager.LoadScene("MainMenu");
        }
    }

}
