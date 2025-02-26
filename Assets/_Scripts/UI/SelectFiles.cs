using UnityEngine;
using UnityEngine.UI;

public class SelectFiles : MonoBehaviour
{
    private GameObject _button; //Files selectionné

    public static SelectFiles Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectedButton(GameObject button)
    {
        if (_button != null) { _button.GetComponent<Image>().color = Color.white; }
        _button = button;
        _button.GetComponent<Image>().color = Color.gray;
    }

    public void StartGame()
    {
        if (_button == null) { return; }
        string Files = _button.GetComponent<Files>().filesNames;
        //SaveManager.Instance.LoadGame();
    }

    public void EraseFiles()
    {

    }
}
