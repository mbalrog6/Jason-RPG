using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JasonRPG
{
    public class GameStateMachine : MonoBehaviour
    {

        private static GameStateMachine _instance;
        private StateMachine _stateMachine;
        public static event Action<IState> OnGameStateChanged;
        public Type GetCurrentStateType => _stateMachine.CurrentState.GetType();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this; 
            DontDestroyOnLoad(gameObject);
            
            _stateMachine = new StateMachine();
            _stateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state);

            var menu = new Menu();
            var loading = new Loadlevel();
            var play = new Play();
            var paused = new Paused();

            _stateMachine.SetState(menu);

            _stateMachine.AddTransition(loading, play, loading.OperationsFinished);
            _stateMachine.AddTransition( paused, menu, () => RestartButton.Pressed );
            _stateMachine.AddTransition(paused, play, () => PlayerInput.Instance.PausePressed);
            _stateMachine.AddTransition(play, paused, () => PlayerInput.Instance.PausePressed);
            
            _stateMachine.AddTransition( menu, loading, () => PlayButton.LevelToLoad != null);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
        
    }

    public class Menu : IState
    {
        public void Tick()
        {
        }

        public void OnEnter()
        {
            PlayButton.LevelToLoad = null;
            SceneManager.LoadSceneAsync("Menu");
        }

        public void OnExit()
        {
        }
    }

    public class Loadlevel : IState
    {
        public bool OperationsFinished() => _operations.TrueForAll(t => t.isDone);
        private List<AsyncOperation> _operations = new List<AsyncOperation>();

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _operations.Add(SceneManager.LoadSceneAsync(PlayButton.LevelToLoad));
            _operations.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
        }

        public void OnExit()
        {
            _operations.Clear();
        }
    }

    public class Paused : IState
    {
        public static bool Active { get; private set; }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            Active = true; 
            Time.timeScale = 0f;
        }

        public void OnExit()
        {
            Active = false;
            Time.timeScale = 1f;
        }
    }

    public class Play : IState
    {
        public void Tick()
        {
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}