using UnityEngine;

namespace ECM2.Examples
{
    public sealed class SimpleCameraController : MonoBehaviour
    {
        #region PUBLIC FIELDS

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _distanceToTarget = 10.0f;

        [SerializeField]
        private float _smoothTime = 0.1f;

        [SerializeField]
        private Transform blockRight, blockLeft, blockUp, blockDown;
        [SerializeField] float value;
        #endregion

        #region FIELDS

        private Vector3 _followVelocity;

        #endregion

        #region PROPERTIES

        public Transform target
        {
            get => _target;
            set => _target = value;
        }

        public float distanceToTarget
        {
            get => _distanceToTarget;
            set => _distanceToTarget = Mathf.Max(0.0f, value);
        }
        public float smoodTime
        {
            get => _smoothTime;
            set => _smoothTime = value;
        }
        #endregion

        #region MONOBEHAVIOUR

        public void OnValidate()
        {
            distanceToTarget = _distanceToTarget;
        }

        public void Start()
        {
            transform.position = target.position - transform.forward * distanceToTarget;
        }

        public void LateUpdate()
        {
            if (target == null)
            {
                return;
            }
            //if (target.position.x >= (blockRight.position.x - value) || target.position.x <= (blockLeft.position.x + value) || target.position.y >= blockUp.position.y || target.position.y <= blockDown.position.y)
            //{
            //    Debug.LogError(11111);
            //    return;
            //}
            Vector3 targetPosition = target.position - transform.forward * distanceToTarget;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _followVelocity, _smoothTime);
        }

        #endregion

    }
}