using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace VFX.Nature
{
    public class PlatformLeaves : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _visualEffect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player.Player _))
            {
                return;
            }
            
            _visualEffect.Play();
            List<AudioClip> clips = new List<AudioClip>(_audioClips);
            if (_audioSource.clip != null)
            {
                clips.Remove(_audioSource.clip);
            }

            AudioClip clip = clips[Random.Range(0, clips.Count)];
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
