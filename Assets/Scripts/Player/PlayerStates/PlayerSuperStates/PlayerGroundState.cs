using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wwwhw.Player.States
{
    public class PlayerGroundState : PlayerState
    {
        public PlayerGroundState(Player player, PlayerInputHandler inputHandler, PlayerStateMachine stateMachine, Animator anim, string animBoolName) : base(player, inputHandler, stateMachine, anim, animBoolName)
        {

        }

    }
}
