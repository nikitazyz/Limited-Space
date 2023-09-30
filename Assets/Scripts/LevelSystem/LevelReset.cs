using System;
using TimeReverse;
using UnityEngine;

namespace LevelSystem
{
    public class LevelReset : MonoBehaviour
    {
        public static event Action LevelRestarted;
        public static event Action LevelRestartStarted;
        
        [SerializeField] private Player.Player _player;
        [SerializeField] private TravelRecorder _travelRecorder;
        [SerializeField] private TravelPlayer _travelPlayer;

        private void Awake()
        {
            _travelPlayer.PlayEnd += () =>
            {
                LevelRestarted?.Invoke();
                _travelRecorder.StartRecord();
                _player.gameObject.layer = LayerMask.NameToLayer("Player");
                _player.enabled = true;
            };
        }

        private void Start()
        {
            _travelRecorder.StartRecord();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LevelRestartStarted?.Invoke();
                _player.gameObject.layer = LayerMask.NameToLayer("PlayerVanish");
                _player.enabled = false;
                _travelRecorder.StopRecord();
                _travelPlayer.StartPlay();
            }
        }
    }
}