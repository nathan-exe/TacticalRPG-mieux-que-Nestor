using System.IO;
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
        if(SaveFileExists(Files))
        {
            Debug.Log("On Load");
            //SaveManager.Instance.LoadGame(Files);
        }
        else
        {
            LoadScene.Instance.ChangeScene("EmrysScene");
            //Il faudrait permettre au SaveManager de se rappelr de la variable Files après le changement de scene
        }
    }

    public void EraseFiles()
    {
        if (_button == null) { return; }
        string Files = _button.GetComponent<Files>().filesNames;
        string filePath = Path.Combine(Application.persistentDataPath, Files + ".xml");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        _button.GetComponent<Image>().color = Color.white;
        _button = null;
    }

    public bool SaveFileExists(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".xml");
        return File.Exists(filePath);
    }
}
