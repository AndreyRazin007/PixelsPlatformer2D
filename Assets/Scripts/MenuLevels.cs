using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevels : MonoBehaviour
{
    private Button[] _buttons;
    public GameObject _levelButtons;

    private void Awake()
    {
        ButtonsToArray();

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel; ++i)
        {
            _buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level_" + levelID;
        SceneManager.LoadScene(levelName);
    }

    private void ButtonsToArray()
    {
        int childCount = _levelButtons.transform.childCount;
        _buttons = new Button[childCount];

        for (int i = 0; i < childCount; ++i)
        {
            _buttons[i] = _levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
