using UnityEngine;

namespace MyCell.CoreSystem.CoreComponent
{
    public class ParticleManager : CoreComponent
    {
        private Transform _particleContainer;

        private Movement _movement;
        public Movement Movement => _movement ? _movement : core.GetCoreComponent(ref _movement);

        protected override void Awake()
        {
            base.Awake();

            _particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
        }

        public GameObject StartParticlesWithRandomRotation(GameObject particlesPrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            return StartParticles(particlesPrefab, transform.position, randomRotation);
        }

        public GameObject StartParticlesWithRandomRotation(GameObject particlesPrefab, Vector2 offset)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            return StartParticles(particlesPrefab, FindOffset(offset), randomRotation);
        }

        public GameObject StartParticles(GameObject particlesPrefab)
        {
            return StartParticles(particlesPrefab, transform.position, Quaternion.identity);
        }

        public GameObject StartParticles(GameObject particlesPrefab, Vector2 offset)
        {
            return StartParticles(particlesPrefab, FindOffset(offset), Quaternion.identity);
        }

        public GameObject StartParticles(GameObject particlesPrefab, Vector2 position, Quaternion rotation)
        {
            return Instantiate(particlesPrefab, position, rotation, _particleContainer);
        }

        private Vector2 FindOffset(Vector2 offset)
        {
            offset.x *= Movement.CurrentFaceDirection;

            return transform.position + (Vector3)offset;
        }
    }

}