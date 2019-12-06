namespace Game2DWaterKit
{
    using UnityEngine;
    using UnityEngine.Serialization;
    using System.Collections.Generic;

    public abstract class Game2DWaterKitObject : MonoBehaviour
    {
        [FormerlySerializedAs("waterSize"), SerializeField] protected Vector2 _size = Vector2.one;
        [System.NonSerialized] protected bool _areModulesInitialized = false;

        private static int _renderedFrameCount;
#if UNITY_EDITOR
        private static int _activeWaterKitObjectCount;
#endif
        private static List<Game2DWaterKitObject> _aliveWaterKitObjects = new List<Game2DWaterKitObject>();

        internal static event System.Action AllWaterKitObjectsDestroyed;
        internal static event System.Action FrameUpdate;

        public bool IsInitialized { get { return _areModulesInitialized; } }

        public abstract void InitializeModules();
        protected abstract void ActivateObjectRendering();
        protected abstract void DeactivateObjectRendering();
        protected abstract void SetObjectVisibilityState(bool isVisible);
        protected abstract void Cleanup();
        protected abstract void RegularUpdate();
        protected abstract void PhysicsUpdate();

        private void OnEnable()
        {
            InitializeModules();
            ActivateObjectRendering();

#if UNITY_EDITOR
            _activeWaterKitObjectCount++;

            if (!Application.isPlaying && _activeWaterKitObjectCount == 1)
                UnityEditor.EditorApplication.update += OnFrameUpdate;
#endif

            if (!_aliveWaterKitObjects.Contains(this))
                _aliveWaterKitObjects.Add(this);
        }

        private void OnDisable()
        {
            DeactivateObjectRendering();

#if UNITY_EDITOR
            _activeWaterKitObjectCount--;

            if (!Application.isPlaying && _activeWaterKitObjectCount == 0)
                UnityEditor.EditorApplication.update -= OnFrameUpdate;
#endif
        }

        private void OnBecameVisible()
        {
            SetObjectVisibilityState(true);
        }

        private void OnBecameInvisible()
        {
            SetObjectVisibilityState(false);
        }

        private void LateUpdate()
        {
            RegularUpdate();

#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif

            if(_renderedFrameCount != Time.renderedFrameCount)
            {
                OnFrameUpdate();

                _renderedFrameCount = Time.renderedFrameCount;
            }
        }

        private void FixedUpdate()
        {
            PhysicsUpdate();
        }

        private void OnDestroy()
        {
            _aliveWaterKitObjects.Remove(this);

            if (_aliveWaterKitObjects.Count == 0 && AllWaterKitObjectsDestroyed != null)
                AllWaterKitObjectsDestroyed.Invoke();

            Cleanup();
        }

        private static void OnFrameUpdate()
        {
            if (FrameUpdate != null)
                FrameUpdate.Invoke();
        }
    }
}