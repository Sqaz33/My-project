using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro; // если вы используете TextMeshPro

public class MainMenuManager : MonoBehaviour
{
    public GameObject confirmationPanel;
    public TextMeshProUGUI confirmationText;
    public Button confirmYesButton;
    public Button confirmNoButton;

    private System.Action onConfirmAction;

    public Dropdown levelDropdown;
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;

    public GameObject leaderboardEntryPrefab;
    public Transform leaderboardContainer;

    private string jsonFilePath;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        jsonFilePath = Path.Combine(Application.persistentDataPath, "level_records.json");
        settingsButton.onClick.AddListener(() => SceneManager.LoadScene("Settings"));

        LoadLeaderboard();
        
        startButton.onClick.AddListener(() => ShowConfirmation("Вы действительно хотите начать игру?", StartGame));
        quitButton.onClick.AddListener(() => ShowConfirmation("Вы действительно хотите выйти из игры?", QuitGame));

        confirmYesButton.onClick.AddListener(() =>
        {
            confirmationPanel.SetActive(false);
            onConfirmAction?.Invoke();
        });

        confirmNoButton.onClick.AddListener(() => confirmationPanel.SetActive(false));
        confirmationPanel.SetActive(false);
    }

    void StartGame()
    {
        string selected = levelDropdown.options[levelDropdown.value].text;
        string sceneName = selected switch
        {
            "Уровень 1" => "Level1",
            "Уровень 2" => "Level2",
            _ => "Level1"
        };

        SceneManager.LoadScene(sceneName);
    }

    void LoadLeaderboard()
    {
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogWarning($"Leaderboard file not found: {jsonFilePath}");
            return;
        }

        string json = File.ReadAllText(jsonFilePath);
        if (string.IsNullOrWhiteSpace(json) || json.Length < 2) return;

        List<LevelRecord> records = JsonUtilityWrapper.FromJsonList<LevelRecord>(json);

        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject); // очистка панели
        }

        foreach (var record in records)
        {
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
            entry.transform.Find("LevelText").GetComponent<Text>().text = record.levelName;
            entry.transform.Find("PlayerText").GetComponent<Text>().text = record.playerName;
            entry.transform.Find("ScoreText").GetComponent<Text>().text = record.completionTime.ToString("F2") + "s";
            entry.gameObject.SetActive(true);
        }
    }


    void ShowConfirmation(string message, System.Action onConfirm)
    {
        confirmationText.text = message;
        confirmationPanel.SetActive(true);
        onConfirmAction = onConfirm;
    }
    
    void QuitGame()
    {
        Application.Quit();
    }


}
