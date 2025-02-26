using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectFiles : MonoBehaviour
{
    private GameObject _button; //Files selectionné

    public static SelectFiles Instance;

    private void Start()
    {
        //Chercher dans le persisent data path les noms de fichier pour Update les texts dans le menu
    }

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

    public async void StartGame()
    {
        if (_button == null) { return; }
        string Files = _button.GetComponent<Files>().filesNames;
        if(SaveFileExists(Files))
        {
            Debug.Log("On Load");
            SaveManager.Instance.LoadGame(Files);
        }
        else
        {
            _button.transform.GetChild(0).gameObject.SetActive(false); //text false
            _button.transform.GetChild(1).gameObject.SetActive(true); //InputField active
            await WaitForEnterKey();
            _button.GetComponent<Files>().filesNames = _button.transform.GetChild(1).GetComponent<TMP_InputField>().text;
            LoadScene.Instance.ChangeScene("EmrysScene");
            //Il faudrait permettre au SaveManager de se rappeler de la variable Files après le changement de scene comme ça je peux regarder dans quel nom de fichier je peux save
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

    private async Task WaitForEnterKey()
    {
        while (!Input.GetKeyDown(KeyCode.Return)) //Enter
        {
            await Task.Yield();
        }
    }

    public bool SaveFileExists(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".xml");
        return File.Exists(filePath);
    }
}
