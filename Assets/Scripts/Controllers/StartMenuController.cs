using Assets.Scripts.Managers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    public class StartMenuController : MonoBehaviour
    {
        public TMP_InputField PlayerNameInput;
        public GameObject ErrorTextGameObject;

        private TextMeshProUGUI _errorText;

        private void Start()
        {
            _errorText = ErrorTextGameObject.GetComponent<TextMeshProUGUI>();
        }

        public void StartGame()
        {
            if (isValidName(PlayerNameInput.text))
            {
                AudioManager.Instance.PlayStartGameSound();
                SceneManager.LoadScene(1);
            }
            else
                AudioManager.Instance.PlayErrorSound();
        }

        private bool isValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                SetErrorMessage("Name can't be empty");
            else if (name.Length < 3)
                SetErrorMessage("Name must be at least 3 characters long");
            else
            {
                HighScoreManager.Instance.CurrentPlayerName = PlayerNameInput.text;
                return true;
            }

            return false;
        }

        private void SetErrorMessage(string errorMessage)
        {
            _errorText.text = errorMessage;
            ErrorTextGameObject.SetActive(true);
        }
    }
}
