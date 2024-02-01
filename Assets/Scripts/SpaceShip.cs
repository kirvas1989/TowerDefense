using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [Header("Space Ship")]

        /// <summary>
        /// ����� ��� �������������� ��������� � ������.
        /// </summary>
        [SerializeField] private float m_Mass;

        #region Movement

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float m_Thrust;
        public float Thrust => m_Thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float m_Mobility;
        public float Mobility => m_Mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ ��������. � ��������/���.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        private float m_MaxVelocityBackup;

        #endregion

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        [SerializeField] private AudioSource m_SoundHit;

        #region Public API
        /// <summary>
        /// ���������� �������� �����. �� -1.0 �� +1.0.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ �����. �� -1.0 �� +1.0.
        /// </summary>
        public float TorqueControl { get; set; }

        public void HalfMaxLinearVelocity()
        {
            m_MaxVelocityBackup = m_MaxLinearVelocity;
            m_MaxLinearVelocity /= 2;
        }

        public void RestoreMaxLinearVelocity()
        {
            m_MaxLinearVelocity = m_MaxVelocityBackup;
        }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigidbody.mass = m_Mass;
            m_Rigidbody.inertia = 1;
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        #endregion

        /// <summary>
        /// ����� ���������� ��� ������� ��� ��������.
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigidbody.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force); // �������� ������.
            m_Rigidbody.AddForce(-m_Rigidbody.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force); // ����������� ������������ ��������
            m_Rigidbody.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force); // �������� �������.
            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force); // ����, ��������������� �������� ( == ������������ �������� ��������).
        }

        #region ��������

        [SerializeField] private Turret[] m_Turrets;

        public UnityEvent FireEvent;

        /// <summary>
        /// TODO: �������� ��������� �����-��������.
        /// ������������ ��.
        /// </summary>
        /// <param name="mode"></param>
        public void Fire(TurretMode mode)
        {
            return;
        }

        /// <summary>
        /// TODO: �������� ��������� �����-��������.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawAmmo(int count)
        {
            return true;
        }

        /// <summary>
        /// TODO: �������� ��������� �����-��������.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }

        #endregion

        new public void Use(EnemyAsset asset)
        {
            base.Use(asset);

            m_MaxLinearVelocity = asset.moveSpeed;
        }

        /// <summary>
        /// ���������� ��������.
        /// </summary>
        public void SlowDown(float speed)
        {
            m_MaxLinearVelocity = speed;                
        }
    }
}
