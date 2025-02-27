using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SelectFiles : MonoBehaviour
{
    private GameObject _button; //Files selectionné
    [SerializeField] GameObject file;

    public static SelectFiles Instance;

    private void Start()
    {
        LoadSaveFiles();
        //Chercher dans le persisent data path les noms de fichier pour Update les texts dans le menu
        file.SetActive(false);
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
        if (SaveFileExists(Files))
        {
            Debug.Log("On Load");
            SaveManager.Instance.LoadGame(Files);
            LoadScene.Instance.ChangeScene("EmrysScene");
        }
        else
        {
            //_button.transform.GetChild(0).gameObject.SetActive(false); //text false
            //_button.transform.GetChild(1).gameObject.SetActive(true); //InputField active
            //await WaitForEnterKey();
            //_button.GetComponent<Files>().filesNames = _button.transform.GetChild(1).GetComponent<TMP_InputField>().text;
            SaveManager.Instance.CurrentFileUse = Files;
            LoadScene.Instance.ChangeScene("EmrysScene");
        }
    }

    public void EraseFiles()
    {
        if (_button == null) { return; }
        string Files = _button.GetComponent<Files>().filesNames;
        string filePath = Path.Combine(Application.persistentDataPath, "Saves", Files);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        _button.GetComponent<Image>().color = Color.white;
        _button.transform.GetChild(0).GetComponent<TMP_Text>().text = "New Game";
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
        string filePath = Path.Combine(Application.persistentDataPath, "Saves", fileName);
        Debug.Log(filePath);
        return File.Exists(filePath);
    }

    private void LoadSaveFiles()
    {
        string savesPath = Path.Combine(Application.persistentDataPath, "Saves");

        if (Directory.Exists(savesPath))
        {
            string[] saveFiles = Directory.GetFiles(savesPath);
            Files[] buttons = FindObjectsOfType<Files>();
            string fileName = Path.GetFileName(saveFiles[0]);
            buttons[0].filesNames = fileName;
            buttons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fileName;
        }
    }
}
