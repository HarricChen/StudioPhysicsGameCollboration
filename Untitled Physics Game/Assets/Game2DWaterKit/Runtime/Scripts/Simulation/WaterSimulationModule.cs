namespace Game2DWaterKit.Simulation
{
    using Game2DWaterKit.Main;
    using Game2DWaterKit.Mesh;
    using UnityEngine;

    public class WaterSimulationModule
    {
        private const float updateSimulationMinimumAbsoluteVelocityThreshold = 0.001f;

        private Game2DWater _waterObject;
        private WaterMainModule _mainModule;
        private WaterMeshModule _meshModule;

        private float _damping;
        private float _stiffness;
        private float _stiffnessSquareRoot;
        private float _spread;
        private float _firstCustomBoundary;
        private float _secondCustomBoundary;
        private bool _isUsingCustomBoundaries;

        private float _leftCustomBoundary;
        private float _rightCustomBoundary;
        private float _surfaceHeighestPointDeltaHeight;
        private float[] _velocities;
        private bool _updateSimulation;

        public WaterSimulationModule(Game2DWater waterObject, WaterSimulationModuleParameters parameters)
        {
            _waterObject = waterObject;

            _damping = parameters.Damping;
            _stiffness = parameters.Stiffness;
            _spread = parameters.Spread;
            _firstCustomBoundary = parameters.FirstCustomBoundary;
            _secondCustomBoundary = parameters.SecondCustomBoundary;
            _isUsingCustomBoundaries = parameters.IsUsingCustomBoundaries;

            _stiffnessSquareRoot = Mathf.Sqrt(_stiffness);
            _leftCustomBoundary = Mathf.Min(_firstCustomBoundary, _secondCustomBoundary);
            _rightCustomBoundary = Mathf.Max(_firstCustomBoundary, _secondCustomBoundary);
        }

        #region Properties
        public float Damping { get { return _damping; } set { _damping = Mathf.Clamp01(value); } }
        public float Spread { get { return _spread; } set { _spread = Mathf.Clamp(value, 0f, float.MaxValue); } }
        public float Stiffness
        {
            get { return _stiffness; }

            set
            {
                _stiffness = Mathf.Clamp(value, 0f, float.MaxValue);
                _stiffnessSquareRoot = Mathf.Sqrt(_stiffness);
            }
        }
        public bool IsUsingCustomBoundaries { get { return _isUsingCustomBoundaries; }
            set
            {
                if (_isUsingCustomBoundaries == value)
                    return;

                _isUsingCustomBoundaries = value;
                _meshModule.RecomputeMeshData();
            }
        }
        public float FirstCustomBoundary
        {
            get { return _firstCustomBoundary; }
            set
            {
                float halfWaterWidth = _mainModule.Width * 0.5f;
                _firstCustomBoundary = Mathf.Clamp(value, -halfWaterWidth, halfWaterWidth);

                _leftCustomBoundary = Mathf.Min(_firstCustomBoundary, _secondCustomBoundary);
                _rightCustomBoundary = Mathf.Max(_firstCustomBoundary, _secondCustomBoundary);
            }
        }
        public float SecondCustomBoundary
        {
            get { return _secondCustomBoundary; }
            set
            {
                float halfWaterWidth = _mainModule.Width * 0.5f;
                _secondCustomBoundary = Mathf.Clamp(value, -halfWaterWidth, halfWaterWidth);

                _leftCustomBoundary = Mathf.Min(_firstCustomBoundary, _secondCustomBoundary);
                _rightCustomBoundary = Mathf.Max(_firstCustomBoundary, _secondCustomBoundary);
            }
        }
        public float LeftBoundary { get { return _isUsingCustomBoundaries ? LeftCustomBoundary : _mainModule.Width * -0.5f; } }
        public float RightBoundary { get { return _isUsingCustomBoundaries ? RightCustomBoundary : _mainModule.Width * 0.5f; } }

        internal WaterMainModule MainModule { get { return _mainModule; } }
        internal float[] Velocities { get { return _velocities; } }
        internal float LeftCustomBoundary { get { return Mathf.Clamp(_leftCustomBoundary, -_mainModule.Width * 0.5f, _mainModule.Width * 0.5f); } }
        internal float RightCustomBoundary { get { return Mathf.Clamp(_rightCustomBoundary, -_mainModule.Width * 0.5f, _mainModule.Width * 0.5f); } }
        internal bool IsControlledByLargeWaterAreaManager { get; set; }
        internal WaterSimulationModule NextWaterSimulationModule { get; set; }
        internal WaterSimulationModule PreviousWaterSimulationModule { get; set; }
        internal float SurfaceHeighestPoint { get { return _mainModule.Height * 0.5f + _surfaceHeighestPointDeltaHeight; } }
        internal float StiffnessSquareRoot { get { return _stiffnessSquareRoot; } }

        private float LastSurfaceVertexHeight { get { return _meshModule.Vertices[_meshModule.SurfaceVerticesCount - 1].y; } }
        private float SecondSurfaceVertexHeight { get { return _meshModule.Vertices[1].y; } }
        private bool UpdateSimulation
        {
            get
            {
                if (_updateSimulation)
                    return true;

                if (IsControlledByLargeWaterAreaManager)
                {
                    if (PreviousWaterSimulationModule != null && PreviousWaterSimulationModule._updateSimulation)
                        return true;

                    if (NextWaterSimulationModule != null && NextWaterSimulationModule._updateSimulation)
                        return true;
                }

                return false;
            }
        }
        #endregion

        #region Methods

        internal void Initialize()
        {
            _mainModule = _waterObject.MainModule;
            _meshModule = _waterObject.MeshModule;

            ResetSimulation();
            _meshModule.OnRecomputeMesh += ResetSimulation;
            _surfaceHeighestPointDeltaHeight = 0f;
        }
        
        internal void PhysicsUpdate(float deltaTime)
        {
            if (!UpdateSimulation)
                return;

            Vector3[] vertices = _meshModule.Vertices;
            int surfaceVerticesCount = _meshModule.SurfaceVerticesCount;

            int firstVertexIndex = _isUsingCustomBoundaries ? 1 : 0;
            int lastVertexIndex = _isUsingCustomBoundaries ? surfaceVerticesCount - 2 : surfaceVerticesCount - 1;

            float firstVertexHeight;
            float lastVertexHeight;

            if (!IsControlledByLargeWaterAreaManager || _isUsingCustomBoundaries)
            {
                firstVertexHeight = vertices[firstVertexIndex].y;
                lastVertexHeight = vertices[lastVertexIndex].y;
            }
            else
            {
                if (PreviousWaterSimulationModule != null)
                {
                    firstVertexHeight = PreviousWaterSimulationModule.LastSurfaceVertexHeight;
                    vertices[0].y = firstVertexHeight;
                    firstVertexIndex++;
                }
                else
                {
                    firstVertexHeight = vertices[firstVertexIndex].y;
                }

                if (NextWaterSimulationModule != null)
                    lastVertexHeight = NextWaterSimulationModule.SecondSurfaceVertexHeight;
                else
                    lastVertexHeight = vertices[lastVertexIndex].y;
            }

            IterateSimulation(firstVertexIndex, lastVertexIndex, firstVertexHeight, lastVertexHeight, deltaTime);
        }

        internal void MarkVelocitiesArrayAsChanged()
        {
            _updateSimulation = true;
        }

        internal void ResetSimulation()
        {
            _surfaceHeighestPointDeltaHeight = 0f;

            if (_velocities == null || _velocities.Length != _meshModule.SurfaceVerticesCount)
            {
                _velocities = new float[_meshModule.SurfaceVerticesCount];
            }
            else
            {
                float waterPositionOfRest = _mainModule.Height * 0.5f;
                var vertices = _meshModule.Vertices;
                for (int i = 0, imax = _velocities.Length; i < imax; i++)
                {
                    _velocities[i] = 0f;
                    vertices[i].y = waterPositionOfRest;
                }
                _meshModule.UpdateMeshData();
            }
        }
        
        private void IterateSimulation(int startSurfaceVertexIndex, int endSurfaceVertexIndex, float firstSurfaceVertexHeight, float lastSurfaceVertexHeight, float deltaTime)
        {
            var vertices = _meshModule.Vertices;

            float dampingFactor = _damping * 2f * _stiffnessSquareRoot;
            float spreadFactor = _spread * _meshModule.SubdivisionsPerUnit;
            float waterPositionOfRest = _mainModule.Height * 0.5f;

            float currentVertexPosition = vertices[startSurfaceVertexIndex].y;
            float previousVertexPosition = firstSurfaceVertexHeight;
            float nextVertexPosition;

            float surfaceHeighestPoint = currentVertexPosition;
            _updateSimulation = false;

            for (int i = startSurfaceVertexIndex; i <= endSurfaceVertexIndex; i++)
            {
                nextVertexPosition = i + 1 <= endSurfaceVertexIndex ? vertices[i + 1].y : lastSurfaceVertexHeight;

                float velocity = _velocities[i];
                float restoringForce = _stiffness * (waterPositionOfRest - currentVertexPosition);
                float dampingForce = -dampingFactor * velocity;
                float spreadForce = spreadFactor * (previousVertexPosition - currentVertexPosition + nextVertexPosition - currentVertexPosition);

                previousVertexPosition = currentVertexPosition;

                velocity += (restoringForce + dampingForce + spreadForce) * deltaTime;
                currentVertexPosition += velocity * deltaTime;

                _velocities[i] = velocity;
                vertices[i].y = currentVertexPosition;

                if (currentVertexPosition > surfaceHeighestPoint)
                    surfaceHeighestPoint = currentVertexPosition;

                currentVertexPosition = nextVertexPosition;
                
                _updateSimulation |= velocity < -updateSimulationMinimumAbsoluteVelocityThreshold || velocity > updateSimulationMinimumAbsoluteVelocityThreshold;
            }

            _surfaceHeighestPointDeltaHeight = surfaceHeighestPoint - waterPositionOfRest;
            _meshModule.UpdateMeshData();
        }

        #endregion

        #region Editor Only Methods
        #if UNITY_EDITOR
        internal void Validate(WaterSimulationModuleParameters parameters)
        {
            Damping = parameters.Damping;
            Stiffness = parameters.Stiffness;
            Spread = parameters.Spread;
            bool recomputeMesh = parameters.FirstCustomBoundary != FirstCustomBoundary || parameters.SecondCustomBoundary != SecondCustomBoundary;
            FirstCustomBoundary = parameters.FirstCustomBoundary;
            SecondCustomBoundary = parameters.SecondCustomBoundary;
            IsUsingCustomBoundaries = parameters.IsUsingCustomBoundaries;
            if (recomputeMesh)
                _meshModule.RecomputeMeshData();
        }
        #endif
        #endregion
    }

    public struct WaterSimulationModuleParameters
    {
        public float Damping;
        public float Stiffness;
        public float Spread;
        public float FirstCustomBoundary;
        public float SecondCustomBoundary;
        public bool IsUsingCustomBoundaries;
    }
}
