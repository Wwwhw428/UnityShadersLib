using MyCell.CoreSystem;
using MyCell.CoreSystem.CoreComponent;
using UnityEngine;

public class PlayerStates
{
    protected Core Core;
    public Player Player;
    public PlayerStateMachine StateMachine;
    public PlayerData_SO PlayerData;
    protected Movement Movement
    {
        get => _movement ?? Core.GetCoreComponent(ref _movement);
    }

    private Movement _movement;
    private string _animBoolName;
    protected float startTime;
    private bool _isExitingState;
    protected bool animationFinished;

    public PlayerStates(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName)
    {
        this.Player = player;
        this.StateMachine = statesMachine;
        this.PlayerData = playerData;
        this._animBoolName = animBoolName;
        this.Core = player.Core;
    }

    public virtual void AnimationTrigger()
    {
        animationFinished = false;
    }

    public virtual void AnimationFinishedTrigger()
    {
        animationFinished = true;
    }

    public virtual void Enter()
    {
        Docheck();
        Player.Anim.SetBool(_animBoolName, true);
        Debug.Log("Enter " + _animBoolName);
        startTime = Time.time;

        animationFinished = false;
        _isExitingState = false;
    }

    public virtual void Exit()
    {
        Player.Anim.SetBool(_animBoolName, false);
        Debug.Log("Exit " + _animBoolName);

        _isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        Docheck();
    }

    public virtual void PhysicsUpdate()
    {
        Docheck();
    }

    public virtual void Docheck()
    {
    }
}
