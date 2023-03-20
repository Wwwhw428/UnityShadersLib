using System.Collections;
using UnityEngine;

namespace Wwwhw.Player.States
{
    public class PlayerIdleState : PlayerGroundState
    {
        private int _xInput;
        private int _yInput;

        public PlayerIdleState(Player player, PlayerInputHandler inputHandler, PlayerStateMachine stateMachine, Animator anim, string animBoolName) : base(player, inputHandler, stateMachine, anim, animBoolName)
        {

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_xInput != 0 || _yInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                // TODO: set velocity = 0 and this need a component controll movement of player
            }
        }

        public override void DoCheck()
        {
            base.DoCheck();

            _xInput = inputHandler.InputX;
            _yInput = inputHandler.InputY;
        }
    }
}