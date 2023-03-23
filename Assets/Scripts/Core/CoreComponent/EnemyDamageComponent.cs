using System;
using UnityEngine;
using MyCell.CoreSystem.CoreComponent;
using MyCell.Interfaces;
using MyCell.Structs;
using MyCell.Modifiers;
using MyCell.Enemy;
using BehaviorDesigner.Runtime;

public class EnemyDamageComponent : CoreComponent, IDamageable
{
    public event Action<GameObject> OnDamage;
    private GameObject _CurrentEnemyGo;
    private BehaviorTree _behaviorTree;
    private Animator _anim;
    private EnemyAnimationHandler _animationHandler;

    // [SerializeField] private GameObject damageParticles;

    public ModifierContainer<DamageModifier, DamageData> DamageModifiers { get; private set; } =
        new ModifierContainer<DamageModifier, DamageData>();

    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);

    private CollisionScene CollisionScene => collisionScene ?? core.GetCoreComponent(ref collisionScene);

    private Movement Movement => movement ?? core.GetCoreComponent(ref movement);

    // private ParticleManager ParticleManager => particleManager ?? core.GetCoreComponent(ref particleManager);

    private Movement movement;
    private Stats stats;
    private CollisionScene collisionScene;
    // private ParticleManager particleManager;

    protected override void Awake()
    {
        base.Awake();
        _CurrentEnemyGo = core.transform.parent.gameObject;
        _behaviorTree = _CurrentEnemyGo.GetComponent<BehaviorTree>();
        _anim = _CurrentEnemyGo.GetComponent<Animator>();
        _animationHandler = _CurrentEnemyGo.GetComponent<EnemyAnimationHandler>();
    }

    public void Damage(DamageData data)
    {
        
        OnDamage?.Invoke(data.Source);

        var modifiedData = DamageModifiers.ApplyModifiers(data);
        // print($"{core.Parent.name} Damage by {modifiedData.DamageAmount}");
        if (modifiedData.DamageAmount <= 0.0f) return;

        Stats?.Health.Decrease(modifiedData.DamageAmount);
        // ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
        // TODO: 敌人闪烁暗淡红光
    }

    private void OnEnable() {
        _animationHandler.OnAnimationFinished += EnableBehaviorTree;
    }

    private void OnDisable() {
        _animationHandler.OnAnimationFinished += EnableBehaviorTree;
    }

    public void Critical()
    {
        Debug.Log("Critical");
        _behaviorTree.enabled = false;
        _anim.SetBool("Move", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Dizzy", true);
        // TODO: 暴击闪金光或者其他特效
    }

    public void EnableBehaviorTree()
    {
        _anim.SetBool("Dizzy", false);
        _behaviorTree.enabled = true;
    }
}