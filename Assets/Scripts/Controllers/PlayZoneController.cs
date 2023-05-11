using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(Collider))]
    public class PlayZoneController : MonoBehaviour
    {
        private MainManager _mainManager;
        private void Start()
        {
            _mainManager = GameObject.Find(nameof(MainManager)).GetComponent<MainManager>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (_mainManager.Bricks <= 0)
                _mainManager.SetLevel();
        }
    }
}
