namespace Game2DWaterKit
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class Game2DWater : Game2DWaterKitObject
    {
        #region Deprecated Properties

        #region Water Properties
        //MeshProperties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector2 WaterSize { get { return MainModule.WaterSize; } set { MainModule.SetWaterSize(value); } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int SubdivisionsCountPerUnit { get { return MeshModule.SubdivisionsPerUnit; } set { MeshModule.SubdivisionsPerUnit = value; } }
        //Wave Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float Damping { get { return SimulationModule.Damping; } set { SimulationModule.Damping = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float Spread { get { return SimulationModule.Spread; } set { SimulationModule.Spread = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float Stiffness { get { return SimulationModule.Stiffness; } set { SimulationModule.Stiffness = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool UseCustomBoundaries { get { return SimulationModule.IsUsingCustomBoundaries; } set { SimulationModule.IsUsingCustomBoundaries = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float FirstCustomBoundary { get { return SimulationModule.FirstCustomBoundary; } set { SimulationModule.FirstCustomBoundary = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float SecondCustomBoundary { get { return SimulationModule.SecondCustomBoundary; } set { SimulationModule.SecondCustomBoundary = value; } }
        //Misc Properties
        public float BuoyancyEffectorSurfaceLevel { get { return AttachedComponentsModule.BuoyancyEffectorSurfaceLevel; } set { AttachedComponentsModule.BuoyancyEffectorSurfaceLevel = value; } }
        //Water Events
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public UnityEvent OnWaterEnter { get { return OnCollisonRipplesModule.OnWaterEnter; } set { OnCollisonRipplesModule.OnWaterEnter = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public UnityEvent OnWaterExit { get { return OnCollisonRipplesModule.OnWaterExit; } set { OnCollisonRipplesModule.OnWaterExit = value; } }
        #endregion

        #region On-Collision Ripples Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ActivateOnCollisionOnWaterEnterRipples { get { return OnCollisonRipplesModule.IsOnWaterEnterRipplesActive; } set { OnCollisonRipplesModule.IsOnWaterEnterRipplesActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ActivateOnCollisionOnWaterExitRipples { get { return OnCollisonRipplesModule.IsOnWaterExitRipplesActive; } set { OnCollisonRipplesModule.IsOnWaterExitRipplesActive = value; } }
        //Disturbance Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesMinimumDisturbance { get { return OnCollisonRipplesModule.MinimumDisturbance; } set { OnCollisonRipplesModule.MinimumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float MinimumDisturbance { get { return OnCollisonRipplesModule.MinimumDisturbance; } set { OnCollisonRipplesModule.MinimumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesMaximumDisturbance { get { return OnCollisonRipplesModule.MaximumDisturbance; } set { OnCollisonRipplesModule.MaximumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float MaximumDisturbance { get { return OnCollisonRipplesModule.MaximumDisturbance; } set { OnCollisonRipplesModule.MaximumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesVelocityMultiplier { get { return OnCollisonRipplesModule.VelocityMultiplier; } set { OnCollisonRipplesModule.VelocityMultiplier = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float VelocityMultiplier { get { return OnCollisonRipplesModule.VelocityMultiplier; } set { OnCollisonRipplesModule.VelocityMultiplier = value; } }
        //Collision Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public LayerMask OnCollisionRipplesCollisionMask { get { return OnCollisonRipplesModule.CollisionMask; } set { OnCollisonRipplesModule.CollisionMask = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public LayerMask CollisionMask { get { return OnCollisonRipplesModule.CollisionMask; } set { OnCollisonRipplesModule.CollisionMask = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesCollisionRaycastMaxDistance { get { return OnCollisonRipplesModule.CollisionRaycastMaximumDistance; } set { OnCollisonRipplesModule.CollisionRaycastMaximumDistance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float CollisionRaycastMaxDistance { get { return OnCollisonRipplesModule.CollisionRaycastMaximumDistance; } set { OnCollisonRipplesModule.CollisionRaycastMaximumDistance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesCollisionMinimumDepth { get { return OnCollisonRipplesModule.CollisionMinimumDepth; } set { OnCollisonRipplesModule.CollisionMinimumDepth = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float MinimumCollisionDepth { get { return OnCollisonRipplesModule.CollisionMinimumDepth; } set { OnCollisonRipplesModule.CollisionMinimumDepth = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesCollisionMaximumDepth { get { return OnCollisonRipplesModule.CollisionMaximumDepth; } set { OnCollisonRipplesModule.CollisionMaximumDepth = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float MaximumCollisionDepth { get { return OnCollisonRipplesModule.CollisionMaximumDepth; } set { OnCollisonRipplesModule.CollisionMaximumDepth = value; } }
        //Sound Effect Properties (On Water Enter)
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesActivateOnWaterEnterSoundEffect { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.IsActive; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public AudioClip OnCollisionRipplesOnWaterEnterAudioClip { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioClip; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioClip = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public AudioClip SplashAudioClip { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioClip; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioClip = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterEnterMinimumAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float MinimumAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.MinimumAudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.MinimumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterEnterMaximumAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.MaximumAudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.MaximumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float MaximumAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.MaximumAudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.MaximumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterEnterAudioVolume { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioVolume; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioVolume = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesUseConstantOnWaterEnterAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.IsUsingConstantAudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.IsUsingConstantAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool UseConstantAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.IsUsingConstantAudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.IsUsingConstantAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterEnterAudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float AudioPitch { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioPitch; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int OnCollisionRipplesOnWaterEnterSoundEffectPoolSize { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.PoolSize; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesOnWaterEnterSoundEffectPoolExpandIfNecessary { get { return OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.CanExpandPool; } set { OnCollisonRipplesModule.OnWaterEnterRipplesSoundEffect.CanExpandPool = value; } }
        //Particle Effect Proeprties (On Water Enter)
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesActivateOnWaterEnterParticleEffect { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.IsActive; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ActivateOnCollisionSplashParticleEffect { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.IsActive; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public ParticleSystem OnCollisionRipplesOnWaterEnterParticleEffect { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.ParticleSystem; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.ParticleSystem = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public ParticleSystem OnCollisionSplashParticleEffect { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.ParticleSystem; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.ParticleSystem = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int OnCollisionRipplesOnWaterEnterParticleEffectPoolSize { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.PoolSize; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int OnCollisionSplashParticleEffectPoolSize { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.PoolSize; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector3 OnCollisionRipplesOnWaterEnterParticleEffectSpawnOffset { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.SpawnOffset; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.SpawnOffset = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector3 OnCollisionSplashParticleEffectSpawnOffset { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.SpawnOffset; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.SpawnOffset = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public UnityEvent OnCollisionRipplesOnWaterEnterParticleEffectStopAction { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.StopAction; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.StopAction = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesOnWaterEnterParticleEffectPoolExpandIfNecessary { get { return OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.CanExpandPool; } set { OnCollisonRipplesModule.OnWaterEnterRipplesParticleEffect.CanExpandPool = value; } }
        //Sound Effect Properties (On Water Exit)
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesActivateOnWaterExitSoundEffect { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.IsActive; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public AudioClip OnCollisionRipplesOnWaterExitAudioClip { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioClip; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioClip = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterExitMinimumAudioPitch { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioPitch; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterExitMaximumAudioPitch { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.MaximumAudioPitch; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.MaximumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterExitAudioVolume { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioVolume; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioVolume = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesUseConstantOnWaterExitAudioPitch { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.IsUsingConstantAudioPitch; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.IsUsingConstantAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float OnCollisionRipplesOnWaterExitAudioPitch { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioPitch; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int OnCollisionRipplesOnWaterExitSoundEffectPoolSize { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.PoolSize; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesOnWaterExitSoundEffectPoolExpandIfNecessary { get { return OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.CanExpandPool; } set { OnCollisonRipplesModule.OnWaterExitRipplesSoundEffect.CanExpandPool = value; } }
        //Particle Effect Proeprties (On Water Exit)
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesActivateOnWaterExitParticleEffect { get { return OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.IsActive; } set { OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public ParticleSystem OnCollisionRipplesOnWaterExitParticleEffect { get { return OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.ParticleSystem; } set { OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.ParticleSystem = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int OnCollisionRipplesOnWaterExitParticleEffectPoolSize { get { return OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.PoolSize; } set { OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector3 OnCollisionRipplesOnWaterExitParticleEffectSpawnOffset { get { return OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.SpawnOffset; } set { OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.SpawnOffset = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public UnityEvent OnCollisionRipplesOnWaterExitParticleEffectStopAction { get { return OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.StopAction; } set { OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.StopAction = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool OnCollisionRipplesOnWaterExitParticleEffectPoolExpandIfNecessary { get { return OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.CanExpandPool; } set { OnCollisonRipplesModule.OnWaterExitRipplesParticleEffect.CanExpandPool = value; } }
        #endregion

        #region Constant Ripples Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ActivateConstantRipples { get { return ConstantRipplesModule.IsActive; } set { ConstantRipplesModule.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesUpdateWhenOffscreen { get { return ConstantRipplesModule.UpdateWhenOffscreen; } set { ConstantRipplesModule.UpdateWhenOffscreen = value; } }
        //Disturbance Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesRandomizeDisturbance { get { return ConstantRipplesModule.RandomizeDisturbance; } set { ConstantRipplesModule.RandomizeDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesDisturbance { get { return ConstantRipplesModule.Disturbance; } set { ConstantRipplesModule.Disturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesMinimumDisturbance { get { return ConstantRipplesModule.MinimumDisturbance; } set { ConstantRipplesModule.MinimumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesMaximumDisturbance { get { return ConstantRipplesModule.MaximumDisturbance; } set { ConstantRipplesModule.MaximumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesSmoothDisturbance { get { return ConstantRipplesModule.SmoothRipples; } set { ConstantRipplesModule.SmoothRipples = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesSmoothFactor { get { return ConstantRipplesModule.SmoothingFactor; } set { ConstantRipplesModule.SmoothingFactor = value; } }
        //Interval Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesRandomizeInterval { get { return ConstantRipplesModule.RandomizeTimeInterval; } set { ConstantRipplesModule.RandomizeTimeInterval = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesInterval { get { return ConstantRipplesModule.TimeInterval; } set { ConstantRipplesModule.TimeInterval = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesMinimumInterval { get { return ConstantRipplesModule.MinimumTimeInterval; } set { ConstantRipplesModule.MinimumTimeInterval = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesMaximumInterval { get { return ConstantRipplesModule.MaximumTimeInterval; } set { ConstantRipplesModule.MaximumTimeInterval = value; } }
        //Ripple Source Positions Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesRandomizeRipplesSourcesPositions { get { return ConstantRipplesModule.RandomizeRipplesSourcePositions; } set { ConstantRipplesModule.RandomizeRipplesSourcePositions = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public List<float> ConstantRipplesSourcePositions { get { return ConstantRipplesModule.SourcePositions; } set { ConstantRipplesModule.SourcePositions = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int ConstantRipplesRandomizeRipplesSourcesCount { get { return ConstantRipplesModule.RandomRipplesSourceCount; } set { ConstantRipplesModule.RandomRipplesSourceCount = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesAllowDuplicateRipplesSourcesPositions { get { return ConstantRipplesModule.AllowDuplicateRipplesSourcePositions; } set { ConstantRipplesModule.AllowDuplicateRipplesSourcePositions = value; } }
        //Sound Effect Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesActivateSoundEffect { get { return ConstantRipplesModule.SoundEffect.IsActive; } set { ConstantRipplesModule.SoundEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public AudioClip ConstantRipplesAudioClip { get { return ConstantRipplesModule.SoundEffect.AudioClip; } set { ConstantRipplesModule.SoundEffect.AudioClip = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesMinimumAudioPitch { get { return ConstantRipplesModule.SoundEffect.AudioPitch; } set { ConstantRipplesModule.SoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesMaximumAudioPitch { get { return ConstantRipplesModule.SoundEffect.MaximumAudioPitch; } set { ConstantRipplesModule.SoundEffect.MaximumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesAudioVolume { get { return ConstantRipplesModule.SoundEffect.AudioVolume; } set { ConstantRipplesModule.SoundEffect.AudioVolume = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesUseConstantAudioPitch { get { return ConstantRipplesModule.SoundEffect.IsUsingConstantAudioPitch; } set { ConstantRipplesModule.SoundEffect.IsUsingConstantAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ConstantRipplesAudioPitch { get { return ConstantRipplesModule.SoundEffect.AudioPitch; } set { ConstantRipplesModule.SoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int ConstantRipplesSoundEffectPoolSize { get { return ConstantRipplesModule.SoundEffect.PoolSize; } set { ConstantRipplesModule.SoundEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesSoundEffectPoolExpandIfNecessary { get { return ConstantRipplesModule.SoundEffect.CanExpandPool; } set { ConstantRipplesModule.SoundEffect.CanExpandPool = value; } }
        //Particle Effect Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesActivateParticleEffect { get { return ConstantRipplesModule.ParticleEffect.IsActive; } set { ConstantRipplesModule.ParticleEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ActivateConstantSplashParticleEffect { get { return ConstantRipplesModule.ParticleEffect.IsActive; } set { ConstantRipplesModule.ParticleEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public ParticleSystem ConstantRipplesParticleEffect { get { return ConstantRipplesModule.ParticleEffect.ParticleSystem; } set { ConstantRipplesModule.ParticleEffect.ParticleSystem = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public ParticleSystem ConstantSplashParticleEffect { get { return ConstantRipplesModule.ParticleEffect.ParticleSystem; } set { ConstantRipplesModule.ParticleEffect.ParticleSystem = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int ConstantRipplesParticleEffectPoolSize { get { return ConstantRipplesModule.ParticleEffect.PoolSize; } set { ConstantRipplesModule.ParticleEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int ConstantSplashParticleEffectPoolSize { get { return ConstantRipplesModule.ParticleEffect.PoolSize; } set { ConstantRipplesModule.ParticleEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public UnityEvent ConstantRipplesParticleEffectStopAction { get { return ConstantRipplesModule.ParticleEffect.StopAction; } set { ConstantRipplesModule.ParticleEffect.StopAction = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector3 ConstantRipplesParticleEffectSpawnOffset { get { return ConstantRipplesModule.ParticleEffect.SpawnOffset; } set { ConstantRipplesModule.ParticleEffect.SpawnOffset = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector3 ConstantSplashParticleEffectSpawnOffset { get { return ConstantRipplesModule.ParticleEffect.SpawnOffset; } set { ConstantRipplesModule.ParticleEffect.SpawnOffset = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ConstantRipplesParticleEffectPoolExpandIfNecessary { get { return ConstantRipplesModule.ParticleEffect.CanExpandPool; } set { ConstantRipplesModule.ParticleEffect.CanExpandPool = value; } }
        #endregion

        #region Script-Generated Ripples Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ScriptGeneratedRipplesMinimumDisturbance { get { return ScriptGeneratedRipplesModule.MinimumDisturbance; } set { ScriptGeneratedRipplesModule.MinimumDisturbance = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ScriptGeneratedRipplesMaximumDisturbance { get { return ScriptGeneratedRipplesModule.MaximumDisturbance; } set { ScriptGeneratedRipplesModule.MaximumDisturbance = value; } }
        //Sound Effect Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ScriptGeneratedRipplesActivateSoundEffect { get { return ScriptGeneratedRipplesModule.SoundEffect.IsActive; } set { ScriptGeneratedRipplesModule.SoundEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public AudioClip ScriptGeneratedRipplesAudioClip { get { return ScriptGeneratedRipplesModule.SoundEffect.AudioClip; } set { ScriptGeneratedRipplesModule.SoundEffect.AudioClip = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ScriptGeneratedRipplesMinimumAudioPitch { get { return ScriptGeneratedRipplesModule.SoundEffect.MinimumAudioPitch; } set { ScriptGeneratedRipplesModule.SoundEffect.MinimumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ScriptGeneratedRipplesMaximumAudioPitch { get { return ScriptGeneratedRipplesModule.SoundEffect.MaximumAudioPitch; } set { ScriptGeneratedRipplesModule.SoundEffect.MaximumAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ScriptGeneratedRipplesUseConstantAudioPitch { get { return ScriptGeneratedRipplesModule.SoundEffect.IsUsingConstantAudioPitch; } set { ScriptGeneratedRipplesModule.SoundEffect.IsUsingConstantAudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ScriptGeneratedRipplesAudioPitch { get { return ScriptGeneratedRipplesModule.SoundEffect.AudioPitch; } set { ScriptGeneratedRipplesModule.SoundEffect.AudioPitch = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ScriptGeneratedRipplesAudioVolume { get { return ScriptGeneratedRipplesModule.SoundEffect.AudioVolume; } set { ScriptGeneratedRipplesModule.SoundEffect.AudioVolume = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int ScriptGeneratedRipplesSoundEffectPoolSize { get { return ScriptGeneratedRipplesModule.SoundEffect.PoolSize; } set { ScriptGeneratedRipplesModule.SoundEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ScriptGeneratedRipplesSoundEffectPoolExpandIfNecessary { get { return ScriptGeneratedRipplesModule.SoundEffect.CanExpandPool; } set { ScriptGeneratedRipplesModule.SoundEffect.CanExpandPool = value; } }
        //Particle Effect Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ScriptGeneratedRipplesActivateParticleEffect { get { return ScriptGeneratedRipplesModule.ParticleEffect.IsActive; } set { ScriptGeneratedRipplesModule.ParticleEffect.IsActive = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public ParticleSystem ScriptGeneratedRipplesParticleEffect { get { return ScriptGeneratedRipplesModule.ParticleEffect.ParticleSystem; } set { ScriptGeneratedRipplesModule.ParticleEffect.ParticleSystem = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int ScriptGeneratedRipplesParticleEffectPoolSize { get { return ScriptGeneratedRipplesModule.ParticleEffect.PoolSize; } set { ScriptGeneratedRipplesModule.ParticleEffect.PoolSize = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public Vector3 ScriptGeneratedRipplesParticleEffectSpawnOffset { get { return ScriptGeneratedRipplesModule.ParticleEffect.SpawnOffset; } set { ScriptGeneratedRipplesModule.ParticleEffect.SpawnOffset = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public UnityEvent ScriptGeneratedRipplesParticleEffectStopAction { get { return ScriptGeneratedRipplesModule.ParticleEffect.StopAction; } set { ScriptGeneratedRipplesModule.ParticleEffect.StopAction = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool ScriptGeneratedRipplesParticleEffectPoolExpandIfNecessary { get { return ScriptGeneratedRipplesModule.ParticleEffect.CanExpandPool; } set { ScriptGeneratedRipplesModule.ParticleEffect.CanExpandPool = value; } }
        #endregion

        #region Refraction & Reflection Rendering Properties
        //Refraction Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float RefractionRenderTextureResizeFactor { get {return RenderingModule.Refraction.RenderTextureResizingFactor; } set { RenderingModule.Refraction.RenderTextureResizingFactor = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public LayerMask RefractionCullingMask { get { return RenderingModule.Refraction.CullingMask; } set { RenderingModule.Refraction.CullingMask = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public FilterMode RefractionRenderTextureFilterMode { get { return RenderingModule.Refraction.RenderTextureFilterMode; } set { RenderingModule.Refraction.RenderTextureFilterMode = value; } }
        //Reflection Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ReflectionRenderTextureResizeFactor { get { return RenderingModule.Reflection.RenderTextureResizingFactor; } set { RenderingModule.Reflection.RenderTextureResizingFactor = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public LayerMask ReflectionCullingMask { get { return RenderingModule.Reflection.CullingMask; } set { RenderingModule.Reflection.CullingMask = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public FilterMode ReflectionRenderTextureFilterMode { get { return RenderingModule.Reflection.RenderTextureFilterMode; } set { RenderingModule.Reflection.RenderTextureFilterMode = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float ReflectionZOffset { get { return RenderingModule.ReflectionZOffset; } set { RenderingModule.ReflectionZOffset = value; } }
        //Other Properties
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int SortingLayerID { get { return RenderingModule.SortingLayerID; } set { RenderingModule.SortingLayerID = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public int SortingOrder { get { return RenderingModule.SortingOrder; } set { RenderingModule.SortingOrder = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool RenderPixelLights { get { return RenderingModule.RenderPixelLights; } set { RenderingModule.RenderPixelLights = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public float FarClipPlane { get { return RenderingModule.FarClipPlane; } set { RenderingModule.FarClipPlane = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool AllowMSAA { get { return RenderingModule.AllowMSAA; } set { RenderingModule.AllowMSAA = value; } }
        [System.Obsolete("This property is now deprecated. You could safely remove the [Deprecated] folder in the scripts folder to get rid of all of deprecated properties")]
        public bool AllowHDR { get { return RenderingModule.AllowHDR; } set { RenderingModule.AllowHDR = value; } }
        #endregion

        #endregion

        #region Deprecated Methods
        [System.Obsolete("Please use ScriptGeneratedRipplesManager.GenerateRipple instead")]
        public void GenerateRipple(Vector2 position, float disturbanceFactor, bool playSoundEffect, bool playParticleEffect, bool smooth, float smoothingFactor = 0.5f)
        {
            ScriptGeneratedRipplesModule.GenerateRipple(position, disturbanceFactor,(disturbanceFactor < 0f), playSoundEffect, playParticleEffect, smooth, smoothingFactor);
        }
        [System.Obsolete("Please use ScriptGeneratedRipplesManager.GenerateRipple instead")]
        public void CreateSplash(float xPosition, float disturbanceFactor, bool playSoundEffect, bool playParticleEffect, bool smooth, float smoothingFactor = 0.5f)
        {
            ScriptGeneratedRipplesModule.GenerateRipple(new Vector2(xPosition, 0f), disturbanceFactor, (disturbanceFactor < 0f), playSoundEffect, playParticleEffect, smooth, smoothingFactor);
        }
        #endregion
    }
}
