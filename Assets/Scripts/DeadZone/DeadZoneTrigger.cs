
using System;
using UnityEngine;

namespace DeadZone
{
    public class DeadZoneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<Player.Player>();
            if (player)
            {
                player.IsDead = true;
            }
        }
    }
}