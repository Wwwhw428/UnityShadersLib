using UnityEngine;
using Wwwhw.Player.States;

namespace Wwwhw.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerStateMachine _stateMachine;
        private PlayerInputHandler _inputHandler;
        private Animator _anim;

        #region player states
        public PlayerIdleState IdleState;
        public PlayerMoveState MoveState;

        #endregion

        private void Awake()
        {
            _stateMachine = GetComponent<PlayerStateMachine>();
            _anim = GetComponent<Animator>();

            IdleState = new PlayerIdleState(this, _inputHandler, _stateMachine, _anim, "Idle");
            MoveState = new PlayerMoveState(this, _inputHandler, _stateMachine, _anim, "Move");
        }

        private void Start()
        {
            _stateMachine.InitStateMachine(IdleState);
        }
    }
}