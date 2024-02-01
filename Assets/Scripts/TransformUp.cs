using UnityEngine;

namespace TowerDefense
{
    public class TransformUp : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.up = Vector2.up;
        }
    }
}