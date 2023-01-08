using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameLogic
{
    public class GameScenario : Singleton<GameScenario>
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _failButton;
        [SerializeField] private Button _winButton;
        
        

        private void Start()
        {
            _resetButton.onClick.AddListener(ResetScene);
            _failButton.onClick.AddListener(ResetScene);
            _winButton.onClick.AddListener(ResetScene);

            _resetButton.gameObject.SetActive(true);
            _failButton.gameObject.SetActive(false);
            _winButton.gameObject.SetActive(false);
        }

        public void WinState()
        {
            _resetButton.gameObject.SetActive(false);
            _failButton.gameObject.SetActive(false);
            _winButton.gameObject.SetActive(true);
        }
        public void FailState()
        {
            _resetButton.gameObject.SetActive(false);
            _failButton.gameObject.SetActive(true);
            _winButton.gameObject.SetActive(false);
        }

        private void ResetScene()
        {
            _resetButton.gameObject.SetActive(true);
            _failButton.gameObject.SetActive(false);
            _winButton.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
