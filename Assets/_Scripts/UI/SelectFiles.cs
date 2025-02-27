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
            SaveManager.Instance.CurrentFileUse = Files;
            SaveManager.Instance.LoadGame(Files);
            LoadScene.Instance.ChangeScene("EmrysScene");
        }
        else
        {
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
            Debug.Log("Test");
            for (int i = 0; i < saveFiles.Length; i++)
            {
                Debug.Log("boucle");
                string fileName = Path.GetFileName(saveFiles[i]);
                buttons[i].filesNames = fileName;
                buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fileName;
            }
        }
    }
}
