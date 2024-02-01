using UnityEngine;

namespace TowerDefense
{
    public class SortingOrderController : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            spriteRenderer.sortingOrder = -(int)(spriteRenderer.bounds.min.y * 100);
        }
    }
}