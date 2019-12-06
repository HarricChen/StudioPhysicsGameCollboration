namespace Game2DWaterKit.Main
{
    using Game2DWaterKit.Mesh;
    using Game2DWaterKit.Material;
    using UnityEngine;

    public abstract class MainModule
    {
        #region Variables
        internal event System.Action OnDepthChange;

        protected Transform _transform;
        protected MeshModule _meshModule;
        protected MaterialModule _materialModule;
        protected Vector2 _size;

        private float _zRotation;
        private float _depth;
        private Vector3 _position;
        private Vector3 _lossyScale;
        private Vector3 _upDirection;
        private Vector3 _forwardDirection;
        private Matrix4x4 _worldToLocalMatrix;
        private Matrix4x4 _localToWorldMatrix;
        #endregion


        #region Properties
        public float Width { get { return _size.x; } }
        public float Height { get { return _size.y; } }
        public Vector3 Position { get { return _position; } set { _transform.position = value; } }
        public Matrix4x4 LocalToWorldMatrix { get { return _localToWorldMatrix; } }
        public Matrix4x4 WorldToLocalMatrix { get { return _worldToLocalMatrix; } }
        internal Transform Transform { get { return _transform; } }
        internal float ZRotation { get { return _zRotation; } }
        internal float Depth { get { return _depth; } }
        internal Vector3 UpDirection { get { return _upDirection; } }
        internal Vector3 ForwardDirection { get { return _forwardDirection; } }
        internal bool IsVisible { get; set; }
        #endregion

        #region Methods

        internal Vector3 TransformPointLocalToWorldNoRotation(Vector3 point)
        {
            Vector3 result;
            result.x = point.x * _lossyScale.x + _position.x;
            result.y = point.y * _lossyScale.y + _position.y;
            result.z = point.z * _lossyScale.z + _position.z;
            return result;
        }

        public Vector3 TransformPointLocalToWorld(Vector3 point)
        {
            return _localToWorldMatrix.MultiplyPoint3x4(point);
        }

        public Vector3 TransformPointWorldToLocal(Vector3 point)
        {
            return _worldToLocalMatrix.MultiplyPoint3x4(point);
        }

        public Vector3 TransformVectorLocalToWorld(Vector3 vector)
        {
            return _localToWorldMatrix.MultiplyVector(vector);
        }

        public Vector3 TransformVectorWorldToLocal(Vector3 vector)
        {
            return _worldToLocalMatrix.MultiplyVector(vector);
        }

        public void SetSize(Vector2 newSize, bool recomputeMesh = false)
        {
            if (newSize.x > 0f && newSize.y > 0f)
            {
                _size = newSize;
                if (recomputeMesh)
                    _meshModule.RecomputeMeshData();
                _materialModule.UpdateAspectRatio();
            }
        }

        protected void UpdateCachedTransformInformation()
        {
            _localToWorldMatrix = _transform.localToWorldMatrix;
            _worldToLocalMatrix = _transform.worldToLocalMatrix;
            _upDirection = _transform.up;
            _forwardDirection = _transform.forward;
            _position = _transform.position;
            _zRotation = _transform.rotation.eulerAngles.z;
            _depth = _position.z;
            Vector3 oldLossyScale = _lossyScale;
            _lossyScale = _transform.lossyScale;
            if(_lossyScale != oldLossyScale)
                _materialModule.UpdateAspectRatio();
        }

        internal void Update()
        {
            if (_transform.hasChanged)
            {
                _transform.hasChanged = false;

                float oldDepth = _position.z;

                UpdateCachedTransformInformation();

                if (oldDepth != _position.z && OnDepthChange != null)
                    OnDepthChange.Invoke();
            }
        }

#if UNITY_EDITOR
        internal void Validate(Vector2 newSize)
        {
            if (newSize != _size)
                SetSize(newSize, true);

            UpdateCachedTransformInformation();
        }
#endif

        #endregion
    }
}
