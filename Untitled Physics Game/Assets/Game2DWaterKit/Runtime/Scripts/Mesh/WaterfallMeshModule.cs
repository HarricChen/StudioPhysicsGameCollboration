namespace Game2DWaterKit.Mesh
{
    using Game2DWaterKit.Main;
    using UnityEngine;

    public class WaterfallMeshModule : MeshModule
    {
        private Game2DWaterfall _waterfallObject;

        public WaterfallMeshModule(Game2DWaterfall waterfallObject)
        {
            _waterfallObject = waterfallObject;
        }

        internal override void Initialize()
        {
            _mainModule = _waterfallObject.MainModule;

            base.Initialize();
        }

        override protected void RecomputeMesh()
        {
            Vector2 halfSize;
            halfSize.x = _mainModule.Width * 0.5f;
            halfSize.y = _mainModule.Height * 0.5f;

            var vertices = new Vector3[4];
            var triangles = new int[6];
            var uvs = new Vector2[4];

            vertices[0] = new Vector3(-halfSize.x, halfSize.y);
            vertices[1] = new Vector3(halfSize.x, halfSize.y);
            vertices[2] = new Vector3(halfSize.x, -halfSize.y);
            vertices[3] = new Vector3(-halfSize.x, -halfSize.y);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            uvs[0] = new Vector2(0f, 1f);
            uvs[1] = new Vector2(1f, 1f);
            uvs[2] = new Vector2(1f, 0f);
            uvs[3] = new Vector2(0f, 0f);

            Mesh.Clear(false);
            Mesh.vertices = vertices;
            Mesh.uv = uvs;
            Mesh.triangles = triangles;
            Mesh.RecalculateNormals();

            Mesh.bounds = _bounds = new Bounds(Vector3.zero, new Vector3(_mainModule.Width, _mainModule.Height));

            _recomputeMeshData = false;
        }

#if UNITY_EDITOR
        internal void Validate()
        {
            if (_recomputeMeshData)
                RecomputeMesh();
        }
#endif
    }
}
