﻿using UnityEngine;
using UnityEngine.AI;

namespace JasonRPG
{
    public class Mover : IMover
    {
        private readonly Player _player;
        private readonly CharacterController _characterController;

        public Mover(Player player)
        {
            _player = player;
            _characterController = _player.GetComponent<CharacterController>();
            _player.GetComponent<NavMeshAgent>().enabled = false;
        }

        public void Tick()
        {
            Vector3 movementInput = new Vector3(_player.ControllerInput.Horizontal, 0, _player.ControllerInput.Vertical);
            Vector3 movement = _player.transform.rotation * movementInput;
            _characterController.SimpleMove(movement);
        }
    }
}