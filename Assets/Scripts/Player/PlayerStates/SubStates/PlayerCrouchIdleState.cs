using UnityEngine;
using Wwwhw.SO.Player;

public class PlayerCrouchIdleState : PlayerGroundStates
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetColliderHeight(PlayerData.CrouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        Player.SetColliderHeight(PlayerData.StandColliderHeigh);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jumpInput)
        {
            if (CollisionScene.Floor.gameObject.tag == "Platform")
            {
                Player.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            Player.InputHandler.UseJumpInput();
        }
        if (inputX != 0)
            StateMachine.ChangeState(Player.CrouchMoveState);
        else if (inputY > -0.01f && !isTouchingCeiling)
            StateMachine.ChangeState(Player.IdleState);
    }
}
