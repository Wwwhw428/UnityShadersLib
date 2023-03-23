using System;
using UnityEngine;
using MyCell.CoreSystem.CoreComponent;
using MyCell.Interfaces;
using MyCell.Structs;
using MyCell.Modifiers;

public class DamageComponent : CoreComponent, IDamageable
{
    public event Action<GameObject> OnDamage;

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

    public void Damage(DamageData data)
    {
        
        OnDamage?.Invoke(data.Source);

        var modifiedData = DamageModifiers.ApplyModifiers(data);
        // print($"{core.Parent.name} Damage by {modifiedData.DamageAmount}");

        if (modifiedData.DamageAmount <= 0.0f) return;

        Stats?.Health.Decrease(modifiedData.DamageAmount);
        // ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
    }
}