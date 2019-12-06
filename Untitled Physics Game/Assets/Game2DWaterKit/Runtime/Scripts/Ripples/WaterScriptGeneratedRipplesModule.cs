namespace Game2DWaterKit.Ripples
{
    using Game2DWaterKit.Mesh;
    using Game2DWaterKit.Ripples.Effects;
    using Game2DWaterKit.Simulation;
    using Game2DWaterKit.Main;
    using UnityEngine;

    public class WaterScriptGeneratedRipplesModule
    {
        #region Variables
        private readonly Transform _ripplesEffectsRoot;
        private readonly WaterRipplesParticleEffect _particleEffect;
        private readonly WaterRipplesSoundEffect _soundEffect;

        private float _minimumDisturbance;
        private float _maximumDisturbance;

        private Game2DWater _waterObject;
        private WaterMainModule _mainModule;
        private WaterMeshModule _meshModule;
        private WaterSimulationModule _simulationModule;
        #endregion

        public WaterScriptGeneratedRipplesModule(Game2DWater waterObject, WaterScriptGeneratedRipplesModuleParameters parameters, Transform ripplesEffectsRootParent)
        {
            _waterObject = waterObject;

            _minimumDisturbance = parameters.MinimumDisturbance;
            _maximumDisturbance = parameters.MaximumDisturbance;

            _ripplesEffectsRoot = CreateRipplesEffectsRoot(ripplesEffectsRootParent);

            _particleEffect = new WaterRipplesParticleEffect(parameters.ParticleEffectParameters, _ripplesEffectsRoot);
            _soundEffect = new WaterRipplesSoundEffect(parameters.SoundEffectParameters, _ripplesEffectsRoot);
        }

        #region Properties
        public WaterRipplesParticleEffect ParticleEffect { get { return _particleEffect; } }
        public WaterRipplesSoundEffect SoundEffect { get { return _soundEffect; } }
        public float MaximumDisturbance { get { return _maximumDisturbance; } set { _maximumDisturbance = Mathf.Clamp(value, 0f, float.MaxValue); } }
        public float MinimumDisturbance { get { return _minimumDisturbance; } set { _minimumDisturbance = Mathf.Clamp(value, 0f, float.MaxValue); } }
        #endregion

        #region Methods

        /// <summary>
        /// Generate a ripple at a particular position.
        /// </summary>
        /// <param name="position">Ripple position.</param>
        /// <param name="disturbanceFactor">Range: [0..1]: The disturbance is linearly interpolated between the minimum disturbance and the maximum disturbance by this factor.</param>
        /// <param name="pullWaterDown">Pull water down or up?</param>
        /// <param name="playSoundEffect">Play the sound effect.</param>
        /// <param name="playParticleEffect">Play the particle effect.</param>
        /// <param name="smoothRipple">Disturb neighbor vertices to create a smoother ripple (wave).</param>
        /// <param name="smoothingFactor">Range: [0..1]: The amount of disturbance to apply to neighbor vertices.</param>
        public void GenerateRipple(Vector2 position, float disturbanceFactor, bool pullWaterDown, bool playSoundEffect, bool playParticleEffect, bool smoothRipple, float smoothingFactor = 0.5f)
        {
            float xPosition = _mainModule.TransformPointWorldToLocal(position).x;

            float leftBoundary = _simulationModule.LeftBoundary;
            float rightBoundary = _simulationModule.RightBoundary;
            int surfaceVerticesCount = _meshModule.SurfaceVerticesCount;
            int startIndex = _simulationModule.IsUsingCustomBoundaries ? 1 : 0;
            int endIndex = _simulationModule.IsUsingCustomBoundaries ? surfaceVerticesCount - 2 : surfaceVerticesCount - 1;

            if (xPosition < leftBoundary || xPosition > rightBoundary)
                return;

            float disturbance = Mathf.Lerp(_minimumDisturbance, _maximumDisturbance, Mathf.Clamp01(disturbanceFactor));
            float velocity = (pullWaterDown ? -1f : 1f) * _simulationModule.StiffnessSquareRoot * disturbance;

            float delta = (xPosition - leftBoundary) * _meshModule.SubdivisionsPerUnit;
            int nearestVertexIndex = startIndex + Mathf.RoundToInt(delta);

            var velocities = _simulationModule.Velocities;

            velocities[nearestVertexIndex] += velocity;
            if (smoothRipple)
            {
                smoothingFactor = Mathf.Clamp01(smoothingFactor);
                float smoothedVelocity = velocity * smoothingFactor;

                int previousNearestIndex = nearestVertexIndex - 1;
                if (previousNearestIndex >= startIndex)
                    velocities[previousNearestIndex] += smoothedVelocity;

                int nextNearestIndex = nearestVertexIndex + 1;
                if (nextNearestIndex <= endIndex)
                    velocities[nextNearestIndex] += smoothedVelocity;
            }

            _simulationModule.MarkVelocitiesArrayAsChanged();

            Vector3 spawnPosition = _mainModule.TransformPointLocalToWorld(new Vector3(xPosition, _mainModule.Height * 0.5f));

            if (playParticleEffect)
                _particleEffect.PlayParticleEffect(spawnPosition);

            if (playSoundEffect)
                _soundEffect.PlaySoundEffect(spawnPosition, disturbanceFactor);
        }

        internal void Initialize()
        {
            _meshModule = _waterObject.MeshModule;
            _mainModule = _waterObject.MainModule;
            _simulationModule = _waterObject.SimulationModule;
        }

        internal void Update()
        {

#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif

            _soundEffect.Update();
            _particleEffect.Update();
        }

        private static Transform CreateRipplesEffectsRoot(Transform parent)
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return null;
            #endif
            var root = new GameObject("ScriptGeneratedRipplesEffects").transform;
            root.parent = parent;
            return root;
        }

        #endregion

        #region Editor Only Methods
        #if UNITY_EDITOR
        internal void Validate(WaterScriptGeneratedRipplesModuleParameters parameters)
        {
            MinimumDisturbance = parameters.MinimumDisturbance;
            MaximumDisturbance = parameters.MaximumDisturbance;

            _particleEffect.Validate(parameters.ParticleEffectParameters);
            _soundEffect.Validate(parameters.SoundEffectParameters);
        }
        #endif
        #endregion
    }

    public struct WaterScriptGeneratedRipplesModuleParameters
    {
        public float MaximumDisturbance;
        public float MinimumDisturbance;
        public WaterRipplesParticleEffectParameters ParticleEffectParameters;
        public WaterRipplesSoundEffectParameters SoundEffectParameters;
    }
}
