using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Weapon;
using MyCell.SO.EventChannels;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    private readonly CombatInput _inputIndex;
    private WeaponChangedEventChannel _weaponChangedEventChannel;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine statesMachine,
        PlayerData_SO playerData,
        string animBoolName,
        Weapon weapon,
        WeaponChangedEventChannel inventoryChannel,
        CombatInput inputIndex
        ) : base(player, statesMachine, playerData, animBoolName)
    {
        this._weapon = weapon;
        this._inputIndex = inputIndex;
        _weaponChangedEventChannel = inventoryChannel;
        _weaponChangedEventChannel.OnEvent += HandleWeaponChange;
        Debug.Log($"{inputIndex} init  #############################");
        _weapon.OnExit += ExitHandler;
        _weapon.Init(Core);
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocity(0, 0);
        _weapon.SetInput(Player.InputHandler.AttackInputsHold[(int) _inputIndex]);
        _weapon.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _weapon.SetInput(Player.InputHandler.AttackInputsHold[(int) _inputIndex]);

        Movement.SetVelocity(0, 0);
    }

    public void ExitHandler()
    {
        isAbilityDone = true;
    }

    public void HandleWeaponChange(object sender, WeaponChangedEventArgs context)
    {
        if (context.WeaponInput == _inputIndex)
        {
            // Important that state changes happens first so Exit is called for the currently equipped weapon
            if (StateMachine.CurrentState == this)
                StateMachine.ChangeState(Player.IdleState);

            // _weapon.Init(Core);
            _weapon.WeaponData = context.WeaponData;
        }
    }
}
