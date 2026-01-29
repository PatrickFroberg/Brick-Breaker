using Assets.Scripts.Managers;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
            StartCoroutine(FocusNameInputNextFrame());
        }

        public void OnStartGameButtonPushed(InputAction.CallbackContext callbackContext)
        {
            // Important if this action can fire multiple phases
            if (!callbackContext.performed) 
                return;

            Debug.Log("Start Game Button Pushed");
            StartGame();
        }

        public void StartGame()
        {
            if (isValidName(PlayerNameInput.text))
            {
                AudioManager.Instance.PlayStartGameSound();
                SceneManager.LoadScene(1);
            }
            else
            {
                AudioManager.Instance.PlayErrorSound();
                StartCoroutine(FocusNameInputNextFrame());
            }
        }

        private IEnumerator FocusNameInputNextFrame()
        {
            yield return null; // wait one frame so EventSystem finishes button selection

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(PlayerNameInput.gameObject);

            PlayerNameInput.Select();
            //PlayerNameInput.ActivateInputField();
            //PlayerNameInput.caretPosition = PlayerNameInput.text.Length;
        }

        private bool isValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                SetErrorMessage("Enter a name.");
            else if (name.Length < 3 || name.Length > 12)
                SetErrorMessage("Name must be 3–12 characters.");
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
