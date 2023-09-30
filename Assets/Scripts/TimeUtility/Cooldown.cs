using System;
using System.Collections;
using UnityEngine;

namespace TimeUtility
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _time;
        public float Timer { get; private set; }
        public bool isPaused { get; set; }

        private Coroutine _coroutine;

        public Cooldown()
        {
        }

        public Cooldown(float time)
        {
            _time = time;
        }

        public void Start(MonoBehaviour agent)
        {
            if (Timer > 0)
            {
                return;
            }
            Timer = _time;
            _coroutine = agent.StartCoroutine(Routine());
        }

        public void Stop()
        {
            Timer = 0;
        }

        private IEnumerator Routine()
        {
            while (Timer > 0)
            {
                yield return null;
                if (isPaused)
                {
                    continue;
                }
                Timer -= Time.deltaTime;
            }
            Timer = 0;
        }
    }
}