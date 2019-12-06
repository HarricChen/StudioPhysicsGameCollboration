namespace Game2DWaterKit.Material
{
    using Game2DWaterKit.Mesh;
    using UnityEngine;

    public class WaterMaterialModule : MaterialModule
    {
        #region Variables

        private static readonly string _refractionKeyword = "Water2D_Refraction";
        private static readonly string _reflectionKeyword = "Water2D_Reflection";
        private static readonly string _fakePerspectiveKeyword = "Water2D_FakePerspective";
        private static readonly string _gradientColorKeyword = "Water2D_ColorGradient";
        
        private readonly int _refractionRenderTextureID;
        private readonly int _reflectionRenderTextureID;
        private readonly int _refractionPartiallySubmergedObjectsRenderTextureID;
        private readonly int _reflectionPartiallySubmergedObjectsRenderTextureID;
        private readonly int _waterMatrixID;
        private readonly int _surfaceLevelID;
        private readonly int _surfaceSubmergeLevelID;
        private readonly int _waterReflectionLowerLimitID;
        private readonly int _waterSolidColorID;
        private readonly int _waterGradientStartColorID;
        private readonly int _waterGradientEndColorID;
        private readonly int _waterSurfaceColorID;

        private bool _isRefractionEnabled;
        private bool _isReflectionEnabled;
        private bool _isFakePerspectiveEnabled;
        private bool _isUsingGradientColor;

        private Game2DWater _waterObject;
        #endregion

        public WaterMaterialModule(Game2DWater waterObject)
        {
            _waterObject = waterObject;

            _refractionRenderTextureID = Shader.PropertyToID("_RefractionTexture");
            _refractionPartiallySubmergedObjectsRenderTextureID = Shader.PropertyToID("_RefractionTexturePartiallySubmergedObjects");
            _reflectionRenderTextureID = Shader.PropertyToID("_ReflectionTexture");
            _reflectionPartiallySubmergedObjectsRenderTextureID = Shader.PropertyToID("_ReflectionTexturePartiallySubmergedObjects");
            _waterMatrixID = Shader.PropertyToID("_WaterMVP");
            _surfaceLevelID = Shader.PropertyToID("_SurfaceLevel");
            _surfaceSubmergeLevelID = Shader.PropertyToID("_SubmergeLevel");
            _waterReflectionLowerLimitID = Shader.PropertyToID("_ReflectionLowerLimit");
            _waterSolidColorID = Shader.PropertyToID("_WaterColor");
            _waterGradientStartColorID = Shader.PropertyToID("_WaterColorGradientStart");
            _waterGradientEndColorID = Shader.PropertyToID("_WaterColorGradientEnd");
            _waterSurfaceColorID = Shader.PropertyToID("_SurfaceColor");

#if GAME_2D_WATER_KIT_LWRP
            _defaultMaterialShaderName = "Game2DWaterKit/Lightweight Render Pipeline/Unlit/Water";
#elif GAME_2D_WATER_KIT_URP
            _defaultMaterialShaderName = "Game2DWaterKit/Universal Render Pipeline/Unlit/Water";
#else
            _defaultMaterialShaderName = "Game2DWaterKit/Built-in Render Pipeline/Unlit/Water";
#endif
        }

        #region Properties

        public bool IsUsingGradientColor
        {
            get
            {
                #if UNITY_EDITOR
                _isUsingGradientColor = Material.IsKeywordEnabled(_gradientColorKeyword);
                #endif
                return _isUsingGradientColor;
            }
        }

        public Color SolidColor { get { return Material.GetColor(_waterSolidColorID); } set { Material.SetColor(_waterSolidColorID, value); } }

        public Color GradientStartColor { get { return Material.GetColor(_waterGradientStartColorID); } set { Material.SetColor(_waterGradientStartColorID, value); } }

        public Color GradientEndColor { get { return Material.GetColor(_waterGradientEndColorID); } set { Material.SetColor(_waterGradientEndColorID, value); } }

        public Color SurfaceColor { get { return Material.GetColor(_waterSurfaceColorID); } set { Material.SetColor(_waterSurfaceColorID, value); } }

        internal bool IsFakePerspectiveEnabled
        {
            get
            {
                #if UNITY_EDITOR
                _isFakePerspectiveEnabled = Material.IsKeywordEnabled(_fakePerspectiveKeyword);
                #endif
                return _isFakePerspectiveEnabled;
            }
        }

        internal bool IsReflectionEnabled
        {
            get
            {
                #if UNITY_EDITOR
                _isReflectionEnabled = Material.IsKeywordEnabled(_reflectionKeyword);
                _isRendererModuleResponsibleForUpdatingMaterialPropertyBlock = _isRefractionEnabled || _isReflectionEnabled;
                #endif
                return _isReflectionEnabled;
            }
        }

        internal bool IsRefractionEnabled
        {
            get
            {
                #if UNITY_EDITOR
                _isRefractionEnabled = Material.IsKeywordEnabled(_refractionKeyword);
                _isRendererModuleResponsibleForUpdatingMaterialPropertyBlock = _isRefractionEnabled || _isReflectionEnabled;
#endif
                return _isRefractionEnabled;
            }
        }

        internal float SurfaceSubmergeLevel
        {
            get
            {
#if UNITY_EDITOR
                return Material.HasProperty(_surfaceSubmergeLevelID) ? Material.GetFloat(_surfaceSubmergeLevelID) : 0f;
#else
                return Material.GetFloat(_surfaceSubmergeLevelID);
#endif
            }
        }

        internal float SurfaceLevel
        {
            get
            {
#if UNITY_EDITOR
                return Material.HasProperty(_surfaceLevelID) ? Material.GetFloat(_surfaceLevelID) : 0f;
#else
                return Material.GetFloat(_surfaceLevelID);
#endif
            }
        }

#endregion

#region Methods
        override internal void Initialize()
        {
            _mainModule = _waterObject.MainModule;
            _meshModule = _waterObject.MeshModule;

            base.Initialize();

            _isRefractionEnabled = Material.IsKeywordEnabled(_refractionKeyword);
            _isReflectionEnabled = Material.IsKeywordEnabled(_reflectionKeyword);
            _isFakePerspectiveEnabled = Material.IsKeywordEnabled(_fakePerspectiveKeyword);
            _isUsingGradientColor = Material.IsKeywordEnabled(_gradientColorKeyword);

            _isRendererModuleResponsibleForUpdatingMaterialPropertyBlock = _isRefractionEnabled || _isReflectionEnabled;

            UpdateAspectRatio();
        }

        internal void SetRefractionRenderTexture(RenderTexture renderTexture)
        {
            if (renderTexture != null)
                _materialPropertyBlock.SetTexture(_refractionRenderTextureID, renderTexture);
        }

        internal void SetRefractionPartiallySubmergedObjectsRenderTexture(RenderTexture renderTexture)
        {
            if (renderTexture != null)
                _materialPropertyBlock.SetTexture(_refractionPartiallySubmergedObjectsRenderTextureID, renderTexture);
        }

        internal void SetReflectionRenderTexture(RenderTexture renderTexture)
        {
            if (renderTexture != null)
                _materialPropertyBlock.SetTexture(_reflectionRenderTextureID, renderTexture);
        }

        internal void SetReflectionPartiallySubmergedObjectsRenderTexture(RenderTexture renderTexture)
        {
            if (renderTexture != null)
                _materialPropertyBlock.SetTexture(_reflectionPartiallySubmergedObjectsRenderTextureID, renderTexture);
        }

        internal void SetReflectionLowerLimit(float lowerLimit)
        {
            _materialPropertyBlock.SetFloat(_waterReflectionLowerLimitID, lowerLimit);
        }

        internal void SetWaterMatrix(Matrix4x4 matrix)
        {
            _materialPropertyBlock.SetMatrix(_waterMatrixID, matrix);
        }

#endregion
    }
}
