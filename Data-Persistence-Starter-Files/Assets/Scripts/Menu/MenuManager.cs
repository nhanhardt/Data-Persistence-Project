using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string Name;
    public int Score;
    //public Text HighScoreText;
    public InputField NameText;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //LoadColor();
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
            Name = Name,
            Score = Score
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

            Name = data.Name;
            Score = data.Score;

            //Debug.Log("Name is " + data.Name);
            //Debug.Log("Score is " + data.Score);

            var hsString = $"Best Score : { Score } Name : { Name }";
            //Debug.Log(hsString);

            //HighScoreText.text = hsString;
            return hsString;
        }
        return string.Empty;
    }
}
