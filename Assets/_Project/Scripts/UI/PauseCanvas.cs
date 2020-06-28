using UnityEngine;

namespace JasonRPG.UI
{
    public class PauseCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        private void Awake()
        {
            _panel.SetActive(false);
            GameStateMachine.OnGameStateChanged += Handle_GameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateMachine.OnGameStateChanged -= Handle_GameStateChanged;
        }

        private void Handle_GameStateChanged(IState state)
        {
           _panel.SetActive(state is Paused);
        }
    }
}
