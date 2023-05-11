using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class HighScoreData
    {
        public List<HighScoreModel> HighScores;

        public HighScoreData(List<HighScoreModel> highScoreModels)
        {
            HighScores = highScoreModels;
        }
    }
}