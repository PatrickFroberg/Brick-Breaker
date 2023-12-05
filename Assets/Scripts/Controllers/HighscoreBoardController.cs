using Assets.Scripts.Managers;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public  class HighscoreBoardController : MonoBehaviour
    {
        public GameObject HighScoreBoardGameObject;
        public GameObject HighScoreRowPrefab;
        public Transform HighScoreRowParent;

        public void ToogleHighScoreBoard()
        {
            AudioManager.Instance.PlayBtnSound();
            HighScoreBoardGameObject.SetActive(!HighScoreBoardGameObject.activeSelf);

            if (HighScoreBoardGameObject.activeSelf == true)
                SetHighScoreBoardData();
        }

        private void SetHighScoreBoardData()
        {
            var highScores = HighScoreManager.Instance.GetHighScores();

            if (highScores.FirstOrDefault() != null)
            {
                ClearHighScoreBoard();

                for (int i = 0; i < highScores.Count; i++)
                {
                    Models.HighScoreModel highscore = highScores[i];
                    GameObject rowPrefab = Instantiate(HighScoreRowPrefab, HighScoreRowParent);
                    TextMeshProUGUI[] texts = rowPrefab.GetComponentsInChildren<TextMeshProUGUI>();

                    texts[0].text = (i + 1).ToString();
                    texts[1].text = highscore.Name;
                    texts[2].text = highscore.Score.ToString();
                }
            }
        }

        private void ClearHighScoreBoard()
        {
            foreach (Transform item in HighScoreRowParent)
                Destroy(item.gameObject);
        }
    }
}
