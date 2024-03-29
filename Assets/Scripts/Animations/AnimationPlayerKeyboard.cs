using UnityEngine;

namespace TowerDefense
{
    public class AnimationPlayerKeyboard : MonoBehaviour
    {
        [SerializeField] private KeyCode m_Key;
        [SerializeField] private AnimationBase m_Target;

        private void Update()
        {
            if (Input.GetKeyDown(m_Key))
                m_Target.StartAnimation(true);
        }
    }
}
