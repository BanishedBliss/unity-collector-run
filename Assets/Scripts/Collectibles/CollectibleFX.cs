using UnityEngine;

namespace Collectibles
{
    public class CollectibleFX: MonoBehaviour
    {
        public ParticleSystem ParticleSystem()
        {
            return gameObject.GetComponent<ParticleSystem>();
        }
    }
}