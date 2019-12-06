namespace Game2DWaterKit.Main
{
    using UnityEngine;

    public class WaterMainModule : MainModule
    {
        private Game2DWater _waterObject;

        public WaterMainModule(Game2DWater waterObject, Vector2 waterSize)
        {
            _waterObject = waterObject;
            _transform = waterObject.transform;
            _size = waterSize;
        }

        #region Properties

        public Vector2 WaterSize { get { return _size; } }
        internal Game2DWater WaterObject { get { return _waterObject; } }
        internal LargeWaterAreaManager LargeWaterAreaManager { get; set; }
        #endregion

        #region Methods

        [System.Obsolete("Please use SetSize(Vector2, bool) instead.")]
        public void SetWaterSize(Vector2 newWaterSize, bool recomputeMesh = false)
        {
            SetSize(newWaterSize, recomputeMesh);
        }

        internal void Initialize()
        {
            _materialModule = _waterObject.MaterialModule;
            _meshModule = _waterObject.MeshModule;

#if UNITY_EDITOR
            //IsWaterVisible property is set in OnBecameVisible and OnBecameInvisible unity callbacks
            //which are not called in edit mode. So we'll assume that the water is always visible in edit mode
            if (!Application.isPlaying)
            {
                IsVisible = true;
            }
#endif
            UpdateCachedTransformInformation();
        }

        #endregion
    }
}
