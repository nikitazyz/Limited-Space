using System;
using UnityEngine;

namespace Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer _player;
        private void Awake()
        {
            if (_player)
            {
                Destroy(gameObject);
                return;
            }

            _player = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
