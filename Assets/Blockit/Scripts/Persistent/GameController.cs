using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Blockit.Persistent
{
  public class GameController : MonoBehaviour
  {
    [SerializeField] private Text levelTxt;
    private static GameController Instance { get; set; }
    public static int levelNumber;
    private static string prefix = "Level";

    private void Awake()
    {
      Instance = this;
      levelNumber = MainMenu.levelNumber;
      SceneManager.LoadScene(prefix + levelNumber, LoadSceneMode.Additive);
    }

    public static void LoadNextLevel()
    {
      SceneManager.UnloadSceneAsync(prefix + levelNumber);

      int levelsCount = SceneManager.sceneCountInBuildSettings - 2;
      levelNumber = (levelNumber % levelsCount) + 1;
      SceneManager.LoadScene(prefix + levelNumber, LoadSceneMode.Additive);

      Instance.levelTxt.text = "Level: " + levelNumber.ToString();
      Instance.AutoSave();
    }

    private void AutoSave()
    {
      PlayerPrefs.SetInt("LastLevel", levelNumber);

      if (PlayerPrefs.GetInt("HighestLevel") < levelNumber)
        PlayerPrefs.SetInt("HighestLevel", levelNumber);
    }

    public static void ReloadLevel()
    {
      SceneManager.UnloadSceneAsync(prefix + levelNumber);
      SceneManager.LoadScene(prefix + levelNumber, LoadSceneMode.Additive);
    }
  }
}