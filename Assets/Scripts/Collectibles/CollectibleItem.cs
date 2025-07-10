using UnityEngine;
using Utils;

namespace Collectibles
{
    public class CollectibleItem : MonoBehaviour
    {
        [Header("Collectible Properties")]
        [SerializeField] private int pointsReward = 1;

        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, что это игрок
            if (other.CompareTag("Player"))
            {
                EventSystem.CollectibleCollected(pointsReward, this);
            }
        }
    }
}
