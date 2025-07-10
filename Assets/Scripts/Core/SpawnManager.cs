using Collectibles;
using UnityEngine;
using Utils;

namespace Core
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private CollectibleItem collectiblePrefab;
        [SerializeField] private float spawnAreaWidth = 20f;
        [SerializeField] private float spawnAreaHeight = 20f;
        [SerializeField] private float spawnHeight = 1f;
    
        [Header("Spawn Timing")]
        [SerializeField] private int initialCollectiblesCount = 1;
        [SerializeField] private float spawnIntervalSecs = 3f;
        [SerializeField] private int maxCollectiblesOnScene = 5;
        
        [Header("Collectible FX")]
        [SerializeField] private CollectibleFX collectEffect;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip collectSound;

        private bool _isSpawning;
        private float _nextSpawnTime;
        private float _spawnTimeRemaining;
        
        private PrefabPool<CollectibleItem> _collectiblePool;
        private PrefabPool<CollectibleFX> _collectibleFXPool;

        private void Awake()
        {
            InitializePools();
            EventSystem.OnCollectibleCollected += RemoveCollectible;
        }
    
        private void Update()
        {
            if (_isSpawning && Time.time >= _nextSpawnTime)
            {
                SpawnCollectible();
                _nextSpawnTime = Time.time + spawnIntervalSecs;
            }
        }
        
        /**
         * Creates object pools to reduce initialization costs.
         */
        private void InitializePools()
        {
            if (collectiblePrefab != null)
            {
                _collectiblePool = new PrefabPool<CollectibleItem>(
                    prefab:collectiblePrefab, 
                    initialSize: maxCollectiblesOnScene, 
                    maxSize: maxCollectiblesOnScene
                );
            }
            
            if (collectEffect != null)
            {
                _collectibleFXPool = new PrefabPool<CollectibleFX>(
                    prefab: collectEffect, 
                    initialSize: maxCollectiblesOnScene,
                    maxSize: maxCollectiblesOnScene
                );
            }
        }
        
        /**
         * Fired the game starts.
         */
        public void StartSpawning()
        {
            SpawnInitialCollectibles();
            _nextSpawnTime = Time.time + spawnIntervalSecs;
            _isSpawning = true;
        }

        /**
         * Fired whenever the game is unpaused.
         */
        public void ResumeSpawning()
        {
            _nextSpawnTime = Time.time + _spawnTimeRemaining;
            _isSpawning = true;
        }
    
        /**
         * Fired to stop or pause spawning.
         */
        public void StopSpawning()
        {
            _isSpawning = false;
            _spawnTimeRemaining = _nextSpawnTime - Time.time;
        }
    
        /**
         * Clear all collectibles from scene.
         */
        public void ClearSpawn()
        {
            _collectiblePool.ReturnAll();
            _collectibleFXPool.ReturnAll();
        }

        /**
         * Collect event handling.
         */
        private void RemoveCollectible(int _, CollectibleItem collectible)
        {
            if (collectEffect != null)
            {
                CollectibleFX effect = _collectibleFXPool.Get();
                effect.transform.position = collectible.transform.position;
                effect.ParticleSystem().Emit(5);
            }
            
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            
            _collectiblePool.Return(collectible);
        }
    
        /**
         * Creates initial set of collectibles on level start.
         */
        private void SpawnInitialCollectibles()
        {
            for (int i = 0; i < initialCollectiblesCount; i++)
            {
                SpawnCollectible();
            }
        }
        
        private void SpawnCollectible()
        {
            CollectibleItem collectible = _collectiblePool.Get();
            collectible.transform.position = GetRandomSpawnPosition();
        }
    
        /**
         * Get a random position to spawn the collectible at.
         */
        private Vector3 GetRandomSpawnPosition()
        {
            float x = Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f);
            float z = Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f);
            
            return new Vector3(transform.position.x + x, spawnHeight, transform.position.z + z);
        }
    
        /**
         * Spawn area editor visualisation.
         */
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                new Vector3(transform.position.x, spawnHeight, transform.position.z),
                new Vector3(spawnAreaWidth, 0.1f, spawnAreaHeight)
            );
        }
    }
}
   
