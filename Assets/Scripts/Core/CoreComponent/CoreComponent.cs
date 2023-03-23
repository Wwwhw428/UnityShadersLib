using UnityEngine;

namespace MyCell.CoreSystem.CoreComponent
{
    public class CoreComponent : MonoBehaviour
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null)
                Debug.LogError("There is no core on parent");

            core.AddCoreComponent(this);
        }

        public virtual void LogicUpdate() { }
    }
}
