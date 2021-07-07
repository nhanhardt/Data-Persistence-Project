using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string PlayerName;
    public int PlayerScore;
    public string HighScoreName;
    public int HighScore;
    public InputField NameText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    class SaveData
    {
        public string Name;
        public int Score;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData
        {
            Name = PlayerName,
            Score = PlayerScore
        };

        string json = JsonUtility.ToJson(data);

        Debug.Log(json);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public string LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScoreName = data.Name;
            HighScore = data.Score;

            //Debug.Log("Name is " + data.Name);
            //Debug.Log("Score is " + data.Score);

            var hsString = $"Best Score : { HighScore } Name : { HighScoreName }";
            Debug.Log(hsString);

            //HighScoreText.text = hsString;
            return hsString;
        }
        return string.Empty;
    }
}
