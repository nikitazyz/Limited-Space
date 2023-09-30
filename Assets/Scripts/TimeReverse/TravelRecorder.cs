using System;
using System.Collections.Generic;
using UnityEngine;

namespace TimeReverse
{
    public class TravelRecorder : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _snapshotsPerUnit = 1;

        private List<Snapshot> _snapshots = new List<Snapshot>();

        public bool IsRecording { get; private set; }
        
        public void StartRecord()
        {
            IsRecording = true;
            _oldPosition = _player.position;
        }

        public void StopRecord()
        {
            IsRecording = false;
        }

        public void Clear()
        {
            _snapshots.Clear();
        }

        public Snapshot[] Snapshots => _snapshots.ToArray();

        private Vector3 _oldPosition;
        private void Update()
        {
            if (!IsRecording)
            {
                return;
            }
            Vector3 position = _player.position;

            if (Vector3.Distance(position, _oldPosition) > 1/_snapshotsPerUnit)
            {
                _snapshots.Add(new Snapshot() {Position = position});
                _oldPosition = position;
            }
        }

        public struct Snapshot
        {
            public Vector2 Position;
        }
    }
}
