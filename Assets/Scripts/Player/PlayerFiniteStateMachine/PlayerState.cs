using Unity.VisualScripting;
using UnityEngine;

namespace Wwwhw.Player.States
{
    public class PlayerState : MonoBehaviour
    {
        protected Player player;
        protected PlayerStateMachine stateMachine;
        protected PlayerInputHandler inputHandler;

        private Animator _anim;
        private string _animBoolName;

        public PlayerState(Player player, PlayerInputHandler inputHandler, PlayerStateMachine stateMachine, Animator anim, string animBoolName)
        {
            this.player = player;
            this.inputHandler = inputHandler;
            this.stateMachine = stateMachine;
            this._anim = anim;
            _animBoolName = animBoolName;
        } 
        public void Enter()
        {
            _anim.SetBool(_animBoolName, true);
        }

        public void Exit()
        {
            _anim.SetBool(_animBoolName, false);
        }

        public virtual void LogicUpdate()
        {
            DoCheck();
        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void DoCheck()
        {

        }
    }
}