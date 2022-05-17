using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Blockit.Persistent;

namespace Blockit
{
  public class MainMenu : MonoBehaviour
  {
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button lastGameBtn;
    [SerializeField] private Button highestGameBtn;
    [SerializeField] private Button infoGameBtn;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject infoPanel;

    public static int levelNumber;

    private void Awake()
    {
      newGameBtn.onClick.AddListener(NewGame);
      lastGameBtn.onClick.AddListener(LastGame);
      highestGameBtn.onClick.AddListener(HighestGame);
      infoGameBtn.onClick.AddListener(InfoGame);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.M))
        SwitchPanels(menuPanel.activeSelf ? false : true, false);
    }


    private void NewGame()
    {
      // Destorys scene then load scene no. 1
      TryUnloadScenes();
      LoadScene(1);
    }

    private void LastGame()
    {
      if (PlayerPrefs.HasKey("LastLevel"))
      {
        TryUnloadScenes();
        LoadScene(PlayerPrefs.GetInt("LastLevel"));
      }
    }

    private void HighestGame()
    {
      if (PlayerPrefs.HasKey("HighestLevel"))
      {
        TryUnloadScenes();
        LoadScene(PlayerPrefs.GetInt("HighestLevel"));
      }
    }

    private void LoadScene(int level)
    {
      levelNumber = level;
      SceneManager.LoadScene("Persistent", LoadSceneMode.Additive);
      SwitchPanels(false, false);
    }

    private void TryUnloadScenes()
    {
      if (SceneManager.sceneCount == 3)
      {
        SceneManager.UnloadSceneAsync("Persistent");
        SceneManager.UnloadSceneAsync("Level" + GameController.levelNumber);
      }
    }

    private void InfoGame()
    {
      infoPanel.SetActive(true);
    }

    private void SwitchPanels(bool menu, bool info)
    {
      menuPanel.gameObject.SetActive(menu);
      infoPanel.gameObject.SetActive(info);
    }


  }
}