using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class HighScoreManager : MonoBehaviour
    {
        public static HighScoreManager Instance;

        public string CurrentPlayerName;

        private List<HighScoreModel> _highScores;

        private const string _FILE_PATH = "/highscorefile.json";

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public List<HighScoreModel> GetHighScores()
        {
            LoadHighScore();

            return _highScores.ToList();
        }

        public string GetHighScoreText()
        {
            LoadHighScore();

            if (_highScores.FirstOrDefault() == null)
                return "No Highscore!";

            return $"#1 {_highScores.FirstOrDefault().Name}: {_highScores.FirstOrDefault().Score}";
        }

        public int GetHighScore()
        {
            LoadHighScore();

            if (_highScores.FirstOrDefault() == null)
                return 0;

            return _highScores.FirstOrDefault().Score;
        }

        public void SetHighScore(int score)
        {
            SaveHighScore(score);
        }

        private void SaveHighScore(int score)
        {
            HighScoreModel data = new HighScoreModel()
            {
                Score = score,
                Name = CurrentPlayerName
            };

            _highScores.Add(data);

            File.WriteAllText(Application.persistentDataPath + _FILE_PATH, JsonUtility.ToJson(new HighScoreData(_highScores)));
        }

        private void LoadHighScore()
        {
            string path = Application.persistentDataPath + _FILE_PATH;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

                if (data != null)
                    _highScores = data.HighScores.OrderByDescending(x => x.Score).ToList();
                else
                    _highScores = new List<HighScoreModel>();

            }
            else
                _highScores = new List<HighScoreModel>();
        }
    }
}