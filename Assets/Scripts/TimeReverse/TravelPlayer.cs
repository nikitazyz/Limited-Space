using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace TimeReverse
{
    public class TravelPlayer : MonoBehaviour
    {
        public event Action PlayEnd;
        
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private TravelRecorder _travelRecorder;
        [SerializeField] private float _time = 3;

        private Coroutine _playingCoroutine;
        public bool IsPlaying { get; private set; }
        
        public void StartPlay()
        {
            var snapshots = _travelRecorder.Snapshots;
            if (snapshots.Length == 0)
            {
                _travelRecorder.Clear();
                PlayEnd?.Invoke();
                return;
            }
            _playingCoroutine = StartCoroutine(Play(snapshots));
        }

        IEnumerator Play(TravelRecorder.Snapshot[] snapshots)
        {
            IsPlaying = true;
            float interval = _time / snapshots.Length;
            _player.isKinematic = true;
            foreach (var snapshot in snapshots.Reverse())
            {
                float time = interval;
                Vector3 startPos = _player.position;
                while (time > 0)
                {
                    _player.position = Vector3.Lerp(startPos, snapshot.Position, 1-time/interval);
                    time -= Time.deltaTime;
                    yield return null;
                }
            }
            _player.isKinematic = false;
            IsPlaying = false;
            _travelRecorder.Clear();
            PlayEnd?.Invoke();
        }
    }
}