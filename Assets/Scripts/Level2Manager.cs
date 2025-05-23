using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    public Text buoyCounterText;
    public string nextSceneName = "MainMenu";

    private int remainingBuoys;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        remainingBuoys = GameObject.FindGameObjectsWithTag("Buoy").Length;
        UpdateBuoyText();
    }

    void UpdateBuoyText()
    {
        buoyCounterText.text = $"Buoys left: {remainingBuoys}";
    }

    public void OnBuoyCollected()
    {
        remainingBuoys--;
        UpdateBuoyText();

        if (remainingBuoys <= 0)
            CompleteLevel();
    }

    private void CompleteLevel()
    {
        float elapsed = Time.time - startTime;
        SaveRecord(elapsed);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }

    private void SaveRecord(float elapsedTime)
    {
        LevelRecord record = new LevelRecord {
            levelName = "Level2",
            completionTime = elapsedTime
        };

        string json = JsonUtility.ToJson(record, true);
        string path = Path.Combine(Application.persistentDataPath, "level_records.json");

        // Если файла не было — создаём новый JSON-массив
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "[" + json + "]");
        }
        else
        {
            // Добавляем новый объект в существующий массив
            string existing = File.ReadAllText(path).Trim();
            // Убираем закрывающую ] и добавляем , + новый объект + ]
            existing = existing.Substring(0, existing.Length - 1) + "," + "\n" + json + "\n" + "]";
            File.WriteAllText(path, existing);
        }
        Debug.Log($"Record saved to {path}");
    }
}
