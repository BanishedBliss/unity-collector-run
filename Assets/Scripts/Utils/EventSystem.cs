using System;
using Collectibles;

namespace Utils
{ 
    /**
     * Central event system
     */
    public static class EventSystem
    {
        public static event Action<int, CollectibleItem> OnCollectibleCollected;
        public static void CollectibleCollected(int points, CollectibleItem collectible) => OnCollectibleCollected?.Invoke(points, collectible);
    }
}
