using System;
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
        if (PlayerScore > HighScore)
        {
            var data = new SaveData
            {
                Name = PlayerName,
                Score = PlayerScore
            };

            var json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public string LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveData>(json);

            HighScoreName = data.Name;
            HighScore = data.Score;

            var hsString = $"Best Score : { HighScore } Name : { HighScoreName }";

            return hsString;
        }
        return string.Empty;
    }
}
