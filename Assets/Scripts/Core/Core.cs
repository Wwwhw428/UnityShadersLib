using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyCell.CoreSystem
{
    public class Core : MonoBehaviour
    {
        public readonly List<CoreComponent.CoreComponent> CoreComponents = new List<CoreComponent.CoreComponent>();
        [field: SerializeField] public GameObject Parent { get; private set; }
        public Transform EntityTransform { get; private set; }


        public void LogicUpdate()
        {
            foreach (CoreComponent.CoreComponent coreComponent in CoreComponents)
                coreComponent.LogicUpdate();
        }
        public void AddCoreComponent(CoreComponent.CoreComponent component)
        {
            if (!CoreComponents.Contains(component))
                CoreComponents.Add(component);
        }

        private T GetCoreComponent<T>() where T : CoreComponent.CoreComponent
        {
            var comp = CoreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent.CoreComponent
        {
            value = GetCoreComponent<T>();

            return value;
        }
    }
}
