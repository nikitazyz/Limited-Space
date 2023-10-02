using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystem
{
    public class LevelSwitch : MonoBehaviour
    {
        [SerializeField] private Transform _transitionScreen;

        private static LevelSwitch _instance;

        public static LevelSwitch Instance => _instance;
        private bool _switching;

        public bool Switching => _switching;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void LoadLevelSwitch()
        {
            if (!_instance)
            {
                var levelSwitch = Instantiate(Resources.Load<LevelSwitch>("LevelSwitch"));
                DontDestroyOnLoad(levelSwitch.gameObject);
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void Switch(string path)
        {
            if (_switching)
            {
                return;
            }
            Debug.Log($"Switch to: {path}");

            _switching = true;
            _transitionScreen.localPosition = new Vector3(1000, 0, 0);
            _transitionScreen.DOLocalMoveX(0, 1).SetEase(Ease.InQuad).OnComplete(() =>
            {
                SceneManager.LoadScene(path);
                _transitionScreen.DOLocalMoveX(-1000, 1).SetEase(Ease.OutQuad).OnComplete(() => _switching = false);
            });
        }
    }
}