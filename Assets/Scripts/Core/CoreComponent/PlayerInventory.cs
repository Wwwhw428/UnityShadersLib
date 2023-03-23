using MyCell.Interfaces;
using MyCell.SO.Weapon;
using MyCell.SO.EventChannels;
using MyCell.Weapon;
using UnityEngine;

namespace MyCell.CoreSystem.CoreComponent
{
    public class PlayerInventory : CoreComponent
    {
        public WeaponDataSo[] weapons;

        private Interaction interaction;

        private Interaction Interaction => interaction ? interaction : core.GetCoreComponent(ref interaction);

        [SerializeField] private WeaponChangedEventChannel InventoryChangeChannel;
        [SerializeField] private WeaponPickupEventChannel WeaponPickupChannel;

        private WeaponPickup weaponPickup;

        private bool _inited = false;

        public void SetWeapon(WeaponDataSo data, CombatInput input)
        {
            if (weaponPickup != null)
            {
                weaponPickup.SetContext(weapons[(int)input]);
                weaponPickup = null;
            }

            weapons[(int)input] = data;
            InventoryChangeChannel.RaiseEvent(this, new WeaponChangedEventArgs(data, input));
        }

        private void Start()
        {
            Interaction.OnInteract += HandleInteraction;
            Debug.Log("Inventory init Weapon ###############################");
            // TODO: 只设置了一个武器 另一个武器有问题
            for (var i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] != null)
                    InventoryChangeChannel.RaiseEvent(this, new WeaponChangedEventArgs(weapons[i], (CombatInput)i));
            }
        }

        private void HandleInteraction(IInteractable context)
        {
            if (context is not WeaponPickup) return;

            weaponPickup = context as WeaponPickup;
            var data = weaponPickup.GetInteractionContext() as WeaponDataSo;

            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] == null)
                {
                    SetWeapon(data, (CombatInput)i);
                    return;
                }
            }

            WeaponPickupChannel.RaiseEvent(this, new WeaponPickupEventArgs(
                data,
                weapons[0],
                weapons[1]
            ));
        }

        private void OnDisable()
        {
            interaction.OnInteract += HandleInteraction;
        }
    }
}