namespace Game2DWaterKit.Mesh
{
    using Game2DWaterKit.Main;
    using UnityEngine;

    public abstract class MeshModule
    {
        protected MainModule _mainModule;

        protected MeshRenderer _meshRenderer;
        protected MeshFilter _meshFilter;
        protected Mesh _mesh;
        protected Bounds _bounds;

        protected bool _recomputeMeshData;

        internal Mesh Mesh
        {
            get
            {
#if UNITY_EDITOR
                _mesh = _meshFilter.sharedMesh;
#endif
                if (_mesh == null)
                {
                    _mesh = new Mesh();
                    _mesh.MarkDynamic();
                    _meshFilter.sharedMesh = _mesh;
                }
                return _mesh;
            }
        }

        internal MeshRenderer MeshRenderer { get { return _meshRenderer; } }
        internal MeshFilter MeshFilter { get { return _meshFilter; } }

        internal Bounds Bounds { get { return _bounds; } }

        internal void SetRendererActive(bool active)
        {
            _meshRenderer.enabled = active;
        }

        protected abstract void RecomputeMesh();

        internal void RecomputeMeshData()
        {
            _recomputeMeshData = true;
        }

        virtual internal void Initialize()
        {
            _meshRenderer = _mainModule.Transform.GetComponent<MeshRenderer>();
            _meshFilter = _mainModule.Transform.GetComponent<MeshFilter>();
            //We set the meshFilter sharedMesh to null to make sure that this water object 
            //will get its own unique mesh in the next call to Mesh property, as it's undesirable that two water objects
            //refer to and operate on the same mesh (as this might happen when cloning water objects)
            _meshFilter.sharedMesh = null;

            RecomputeMesh();
        }
    }
}