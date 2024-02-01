using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ����� ���� ������������� �������� �� �����.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// �������� ������� ��� ������������.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;
    }
}
