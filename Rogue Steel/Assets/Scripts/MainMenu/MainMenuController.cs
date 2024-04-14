using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public GameData gamedata;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
        GameData.InventorySceneStartType = "New";
    }

    public void LoadGameDialogyes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
            GameData.InventorySceneStartType = "Load";
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }

}
