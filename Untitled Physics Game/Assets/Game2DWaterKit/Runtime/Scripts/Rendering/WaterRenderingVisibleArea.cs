namespace Game2DWaterKit.Rendering
{
    using Game2DWaterKit.Utils;
    using Game2DWaterKit.Main;
    using UnityEngine;

    internal class WaterRenderingVisibleArea
    {
        private readonly MainModule _mainModule;

        private int _pixelWidth;
        private int _pixelHeight;

        public WaterRenderingVisibleArea(MainModule mainModule)
        {
            _mainModule = mainModule;
            RefractionProperties = new RenderModeProperties();
            ReflectionProperties = new RenderModeProperties();
        }

        #region Properties
        internal bool IsValid { get; private set; }
        internal int PixelWidth
        {
            get { return _pixelWidth; }
            private set
            {
                _pixelWidth = Mathf.Clamp(Mathf.Abs(value), 0, Screen.width);
                IsValid &= _pixelWidth > 1;
            }
        }
        internal int PixelHeight
        {
            get { return _pixelHeight; }
            private set
            {
                _pixelHeight = Mathf.Clamp(Mathf.Abs(value), 0, Screen.height);
                IsValid &= _pixelHeight > 1;
            }
        }
        internal bool IsOrthographicCamera { get; private set; }
        internal float FarClipPlane { get; private set; }
        internal float Aspect { get; private set; }
        internal float FieldOfView { get; private set; }
        internal float OrthographicSize { get; private set; }
        internal RenderModeProperties RefractionProperties { get; private set; }
        internal RenderModeProperties ReflectionProperties { get; private set; }
        #endregion

        #region Methods

        internal void UpdateArea(SimpleFixedSizeList<Vector2> points, WaterRenderingCameraFrustum cameraFrustum, bool isFullyContainedInWaterBox, bool computePixelSize, float zFar, bool renderRefraction = true, bool renderReflection = false, float reflectionZOffset = 0f, float reflectionAxis = 0f, float reflectionFrustumHeightScalingFactor = 1f)
        {
            IsValid = true;

            var currentCamera = cameraFrustum.CurrentCamera;

            if (isFullyContainedInWaterBox && currentCamera.orthographic && !cameraFrustum.IsIsometric)
            {
                MatchToCurrentCameraOrthographicViewingFrustum(cameraFrustum, computePixelSize, zFar, renderRefraction, renderReflection, reflectionAxis, reflectionZOffset, reflectionFrustumHeightScalingFactor);
                return;
            }

            IsValid = points.Count > 0;
            if (!IsValid)
                return;

            // Compute the AABB of provided points (in water-local space)
            Vector2 boundingBoxMin = points[0];
            Vector2 boundingBoxMax = points[0];
            for (int i = 1, imax = points.Count; i < imax; i++)
            {
                Vector2 point = points[i];

                if (point.x < boundingBoxMin.x)
                    boundingBoxMin.x = point.x;

                if (point.x > boundingBoxMax.x)
                    boundingBoxMax.x = point.x;

                if (point.y < boundingBoxMin.y)
                    boundingBoxMin.y = point.y;

                if (point.y > boundingBoxMax.y)
                    boundingBoxMax.y = point.y;
            }

            if (currentCamera.orthographic)
            {
                if(cameraFrustum.IsIsometric)
                    ComputeIsometricViewingFrustum(cameraFrustum, computePixelSize, boundingBoxMin, boundingBoxMax, zFar, renderRefraction, renderReflection, reflectionAxis, reflectionZOffset, reflectionFrustumHeightScalingFactor);
                else
                    ComputeOrthographicViewingFrustum(cameraFrustum, computePixelSize, boundingBoxMin, boundingBoxMax, zFar, renderRefraction, renderReflection, reflectionAxis, reflectionZOffset, reflectionFrustumHeightScalingFactor);

                return;
            }

            ComputePerspectiveViewingFrustum(cameraFrustum, computePixelSize, boundingBoxMin, boundingBoxMax, zFar, renderRefraction, renderReflection, reflectionAxis, reflectionZOffset, reflectionFrustumHeightScalingFactor, isFullyContainedInWaterBox);
        }

        private void ComputeOrthographicViewingFrustum(WaterRenderingCameraFrustum renderingCameraFrustum, bool computePixelSize, Vector2 boundingBoxMin, Vector2 boundingBoxMax, float zFar, bool renderRefraction, bool renderReflection, float reflectionAxis, float reflectionZOffset, float reflectionFrustumHeightScalingFactor)
        {
            var wnrBoundingBoxMin = _mainModule.TransformPointLocalToWorldNoRotation(boundingBoxMin);
            var wnrBoundingBoxMax = _mainModule.TransformPointLocalToWorldNoRotation(boundingBoxMax);

            float frustumWidth = wnrBoundingBoxMax.x - wnrBoundingBoxMin.x;
            float frustumHeight = wnrBoundingBoxMax.y - wnrBoundingBoxMin.y;

            if (computePixelSize)
            {
                var currentCamera = renderingCameraFrustum.CurrentCamera;
                float pixelsPerUnit = currentCamera.pixelHeight * 0.5f / currentCamera.orthographicSize;
                PixelWidth = (int)(frustumWidth * pixelsPerUnit);
                PixelHeight = (int)(frustumHeight * pixelsPerUnit);

                if (!IsValid)
                    return;
            }

            var boundingBoxCenter = (boundingBoxMin + boundingBoxMax) * 0.5f;
            float halfFrustumWidth = frustumWidth * 0.5f;
            float halfFrustumHeight = frustumHeight * 0.5f;

            var rotation = Quaternion.Euler(0f, 0f, _mainModule.ZRotation);

            if (renderRefraction)
            {
                RefractionProperties.Position = _mainModule.TransformPointLocalToWorld(boundingBoxCenter);
                RefractionProperties.Rotation = rotation;
                RefractionProperties.ProjectionMatrix = Matrix4x4.Ortho(-halfFrustumWidth, halfFrustumWidth, -halfFrustumHeight, halfFrustumHeight, -0.001f, zFar);
                RefractionProperties.NearClipPlane = 0f;
            }

            if (renderReflection)
            {
                ReflectionProperties.Position = _mainModule.TransformPointLocalToWorld(new Vector2(boundingBoxCenter.x, 2f * reflectionAxis - boundingBoxCenter.y));
                ReflectionProperties.Rotation = rotation;

                if (!renderRefraction || reflectionFrustumHeightScalingFactor != 1f || reflectionZOffset != 0f)
                    ReflectionProperties.ProjectionMatrix = Matrix4x4.Ortho(-halfFrustumWidth, halfFrustumWidth, -halfFrustumHeight, halfFrustumHeight * reflectionFrustumHeightScalingFactor, reflectionZOffset, zFar);
                else
                    ReflectionProperties.ProjectionMatrix = RefractionProperties.ProjectionMatrix;

                ReflectionProperties.NearClipPlane = reflectionZOffset;
            }

            IsOrthographicCamera = true;
            OrthographicSize = halfFrustumHeight;
            Aspect = frustumWidth / frustumHeight;
            FarClipPlane = zFar;
        }

        private void MatchToCurrentCameraOrthographicViewingFrustum(WaterRenderingCameraFrustum renderingCameraFrustum, bool computePixelSize, float zFar, bool renderRefraction, bool renderReflection, float reflectionAxis, float reflectionZOffset, float reflectionFrustumHeightScalingFactor)
        {
            Camera currentCamera = renderingCameraFrustum.CurrentCamera;

            float orthographicSize = currentCamera.orthographicSize;
            float aspect = currentCamera.aspect;

            float halfFrustumHeight = orthographicSize;
            float halfFrustumWidth = halfFrustumHeight * aspect;

            if (computePixelSize)
            {
                PixelWidth = currentCamera.pixelWidth;
                PixelHeight = currentCamera.pixelHeight;
            }

            Vector3 lCurrentRenderingCameraPosition = renderingCameraFrustum.Position;
            lCurrentRenderingCameraPosition.z = 0f;

            if (renderRefraction)
            {
                RefractionProperties.Position = _mainModule.TransformPointLocalToWorld(lCurrentRenderingCameraPosition);
                RefractionProperties.Rotation = Quaternion.Euler(0f, 0f, renderingCameraFrustum.Rotation.z + _mainModule.ZRotation);
                RefractionProperties.ProjectionMatrix = Matrix4x4.Ortho(-halfFrustumWidth, halfFrustumWidth, -halfFrustumHeight, halfFrustumHeight, -0.001f, zFar);
                RefractionProperties.NearClipPlane = 0f;
            }

            if (renderReflection)
            {
                ReflectionProperties.Position = _mainModule.TransformPointLocalToWorld(new Vector3(lCurrentRenderingCameraPosition.x, 2f * reflectionAxis - lCurrentRenderingCameraPosition.y));
                ReflectionProperties.Rotation = Quaternion.Euler(0f, 0f, -renderingCameraFrustum.Rotation.z + _mainModule.ZRotation);

                if (!renderRefraction || reflectionFrustumHeightScalingFactor != 1f || reflectionZOffset != 0f)
                    ReflectionProperties.ProjectionMatrix = Matrix4x4.Ortho(-halfFrustumWidth, halfFrustumWidth, -halfFrustumHeight, halfFrustumHeight * reflectionFrustumHeightScalingFactor, reflectionZOffset, zFar);
                else
                    ReflectionProperties.ProjectionMatrix = RefractionProperties.ProjectionMatrix;

                ReflectionProperties.NearClipPlane = reflectionZOffset;
            }

            IsOrthographicCamera = true;
            OrthographicSize = orthographicSize;
            Aspect = aspect;
            FarClipPlane = zFar;
        }

        private void ComputeIsometricViewingFrustum(WaterRenderingCameraFrustum renderingCameraFrustum, bool computePixelSize, Vector2 boundingBoxMin, Vector2 boundingBoxMax, float zFar, bool renderRefraction, bool renderReflection, float reflectionAxis, float reflectionZOffset, float reflectionFrustumHeightScalingFactor)
        {
            var currentCamera = renderingCameraFrustum.CurrentCamera;

            var waterLocalSpaceToCameraSpaceMatrix = currentCamera.worldToCameraMatrix * _mainModule.LocalToWorldMatrix;

            Vector3 cBoundingBoxMin = waterLocalSpaceToCameraSpaceMatrix.MultiplyPoint3x4(boundingBoxMin);
            Vector3 cBoundingBoxMax = waterLocalSpaceToCameraSpaceMatrix.MultiplyPoint3x4(boundingBoxMax);
            Vector3 cBoundingBoxMinXMaxY = waterLocalSpaceToCameraSpaceMatrix.MultiplyPoint3x4(new Vector3(boundingBoxMin.x, boundingBoxMax.y));
            Vector3 cBoundingBoxMaxXMinY = waterLocalSpaceToCameraSpaceMatrix.MultiplyPoint3x4(new Vector3(boundingBoxMax.x, boundingBoxMin.y));

            Vector3 frustumBottomLeft, frustumTopRight;

            frustumBottomLeft.x = WaterUtility.Min(cBoundingBoxMin.x, cBoundingBoxMinXMaxY.x, cBoundingBoxMax.x, cBoundingBoxMaxXMinY.x);
            frustumBottomLeft.y = WaterUtility.Min(cBoundingBoxMin.y, cBoundingBoxMinXMaxY.y, cBoundingBoxMax.y, cBoundingBoxMaxXMinY.y);

            frustumTopRight.x = WaterUtility.Max(cBoundingBoxMin.x, cBoundingBoxMinXMaxY.x, cBoundingBoxMax.x, cBoundingBoxMaxXMinY.x);
            frustumTopRight.y = WaterUtility.Max(cBoundingBoxMin.y, cBoundingBoxMinXMaxY.y, cBoundingBoxMax.y, cBoundingBoxMaxXMinY.y);

            float nearClipPlane = -WaterUtility.Max(cBoundingBoxMin.z, cBoundingBoxMinXMaxY.z, cBoundingBoxMax.z, cBoundingBoxMaxXMinY.z);

            float frustumWidth = frustumTopRight.x - frustumBottomLeft.x;
            float frustumHeight = frustumTopRight.y - frustumBottomLeft.y;

            if (computePixelSize)
            {
                float pixelsPerUnit = currentCamera.pixelHeight * 0.5f / currentCamera.orthographicSize;
                PixelWidth = (int)(frustumWidth * pixelsPerUnit);
                PixelHeight = (int)(frustumHeight * pixelsPerUnit);

                if (!IsValid)
                    return;
            }

            float farClipPlane = nearClipPlane + zFar;

            Vector3 position = currentCamera.cameraToWorldMatrix.MultiplyPoint3x4(new Vector3((frustumBottomLeft.x + frustumTopRight.x) * 0.5f, (frustumBottomLeft.y + frustumTopRight.y) * 0.5f));

            if (renderRefraction)
            {
                RefractionProperties.Position = position;
                RefractionProperties.Rotation = currentCamera.transform.rotation;
                RefractionProperties.ProjectionMatrix = ComputeObliqueOrthographicMatrix(frustumWidth, frustumHeight, nearClipPlane, farClipPlane, RefractionProperties.Position, RefractionProperties.Rotation, _mainModule.Position, _mainModule.ForwardDirection, -0.001f, 1f);
                RefractionProperties.NearClipPlane = nearClipPlane;
            }

            if (renderReflection)
            {
                Vector3 lPosition = _mainModule.TransformPointWorldToLocal(position);
                lPosition.y = 2f * reflectionAxis - lPosition.y;

                Vector3 rotationEulerAngles = renderingCameraFrustum.Rotation;
                rotationEulerAngles.x *= -1f;
                rotationEulerAngles.z = -rotationEulerAngles.z + _mainModule.ZRotation;

                ReflectionProperties.Position = _mainModule.TransformPointLocalToWorld(lPosition);
                ReflectionProperties.Rotation = Quaternion.Euler(rotationEulerAngles);
                ReflectionProperties.NearClipPlane = nearClipPlane + reflectionZOffset;
                ReflectionProperties.ProjectionMatrix = ComputeObliqueOrthographicMatrix(frustumWidth, frustumHeight, ReflectionProperties.NearClipPlane, farClipPlane, ReflectionProperties.Position, ReflectionProperties.Rotation, _mainModule.Position, _mainModule.ForwardDirection, reflectionZOffset, reflectionFrustumHeightScalingFactor);
            }

            IsOrthographicCamera = true;
            OrthographicSize = frustumHeight * 0.5f;
            Aspect = frustumWidth / frustumHeight;
            FarClipPlane = farClipPlane;
        }

        private void ComputePerspectiveViewingFrustum(WaterRenderingCameraFrustum renderingCameraFrustum, bool computePixelSize, Vector2 boundingBoxMin, Vector2 boundingBoxMax, float zFar, bool renderRefraction, bool renderReflection, float reflectionAxis, float reflectionZOffset, float reflectionFrustumHeightScalingFactor, bool isFullyContainedInWaterBox)
        {
            var wnrBoundingBoxMin = _mainModule.TransformPointLocalToWorldNoRotation(boundingBoxMin);
            var wnrBoundingBoxMax = _mainModule.TransformPointLocalToWorldNoRotation(boundingBoxMax);

            var currentCamera = renderingCameraFrustum.CurrentCamera;

            if (computePixelSize)
            {
                if (isFullyContainedInWaterBox)
                {
                    PixelWidth = currentCamera.pixelWidth;
                    PixelHeight = currentCamera.pixelHeight;
                }
                else
                {
                    var sBoundingBoxMin = currentCamera.WorldToScreenPoint(wnrBoundingBoxMin);
                    var sBoundingBoxMax = currentCamera.WorldToScreenPoint(wnrBoundingBoxMax);

                    if (renderingCameraFrustum.Rotation.z != 0f && renderingCameraFrustum.Rotation.z != 180f)
                    {
                        var sBoundingBoxMinXMaxY = currentCamera.WorldToScreenPoint(new Vector3(wnrBoundingBoxMin.x, wnrBoundingBoxMax.y, wnrBoundingBoxMin.z));
                        var sBoundingBoxMaxXMinY = currentCamera.WorldToScreenPoint(new Vector3(wnrBoundingBoxMax.x, wnrBoundingBoxMin.y, wnrBoundingBoxMin.z));
                        PixelWidth = (int)(Mathf.Max(Vector2.Distance(sBoundingBoxMinXMaxY, sBoundingBoxMax), Vector2.Distance(sBoundingBoxMin, sBoundingBoxMaxXMinY)));
                        PixelHeight = (int)(Mathf.Max(Vector2.Distance(sBoundingBoxMin, sBoundingBoxMinXMaxY), Vector2.Distance(sBoundingBoxMaxXMinY, sBoundingBoxMax)));
                    }
                    else
                    {
                        PixelWidth = (int)(sBoundingBoxMax.x - sBoundingBoxMin.x);
                        PixelHeight = (int)(sBoundingBoxMax.y - sBoundingBoxMin.y);
                    }
                }

                if (!IsValid)
                    return;
            }

            Vector3 lCurrentRenderingCameraPosition = renderingCameraFrustum.Position;
            Vector3 wnrCurrentRenderingCameraPosition = _mainModule.TransformPointLocalToWorldNoRotation(lCurrentRenderingCameraPosition);

            float nearClipPlane = _mainModule.Position.z - wnrCurrentRenderingCameraPosition.z;
            float farClipPlane = nearClipPlane + zFar;

            float frustumLeft = wnrBoundingBoxMin.x - wnrCurrentRenderingCameraPosition.x;
            float frustumRight = wnrBoundingBoxMax.x - wnrCurrentRenderingCameraPosition.x;
            float frustumTop = wnrBoundingBoxMax.y - wnrCurrentRenderingCameraPosition.y;
            float frustumBottom = wnrBoundingBoxMin.y - wnrCurrentRenderingCameraPosition.y;

            var rotation = Quaternion.Euler(0f, 0f, _mainModule.ZRotation);

            if (renderRefraction)
            {
                RefractionProperties.Position = _mainModule.TransformPointLocalToWorld(renderingCameraFrustum.Position);
                RefractionProperties.Rotation = rotation;
                RefractionProperties.ProjectionMatrix = Matrix4x4.Frustum(frustumLeft, frustumRight, frustumBottom, frustumTop, nearClipPlane - 0.001f, farClipPlane);
                RefractionProperties.NearClipPlane = nearClipPlane;
            }

            if (renderReflection)
            {
                lCurrentRenderingCameraPosition.y = 2f * reflectionAxis - lCurrentRenderingCameraPosition.y;

                ReflectionProperties.Position = _mainModule.TransformPointLocalToWorld(lCurrentRenderingCameraPosition);
                ReflectionProperties.Rotation = rotation;

                float reflectionFrustumTop;

                if (reflectionFrustumHeightScalingFactor != 1f)
                {
                    float wnrReflectionFrustumTopEdge = 0.5f * (wnrBoundingBoxMin.y * (1f + reflectionFrustumHeightScalingFactor) + wnrBoundingBoxMax.y * (1f - reflectionFrustumHeightScalingFactor));
                    reflectionFrustumTop = wnrCurrentRenderingCameraPosition.y - wnrReflectionFrustumTopEdge;
                }
                else reflectionFrustumTop = -frustumBottom;

                if (reflectionZOffset == 0f)
                {
                    ReflectionProperties.NearClipPlane = nearClipPlane;
                    ReflectionProperties.ProjectionMatrix = Matrix4x4.Frustum(frustumLeft, frustumRight, -frustumTop, reflectionFrustumTop, nearClipPlane, farClipPlane);
                }
                else
                {
                    float reflectionNearClipPlane = Mathf.Clamp(nearClipPlane + reflectionZOffset, 0.01f, farClipPlane - 0.01f);
                    float s = reflectionNearClipPlane / nearClipPlane;

                    float reflectionFrustumLeft = frustumLeft * s;
                    float reflectionFrustumRight = frustumRight * s;
                    float reflectionFrustumBottom = -frustumTop * s;
                    reflectionFrustumTop *= s;

                    ReflectionProperties.NearClipPlane = reflectionNearClipPlane;
                    ReflectionProperties.ProjectionMatrix = Matrix4x4.Frustum(reflectionFrustumLeft, reflectionFrustumRight, reflectionFrustumBottom, reflectionFrustumTop, reflectionNearClipPlane, farClipPlane);
                }
            }

            IsOrthographicCamera = false;
            FieldOfView = currentCamera.fieldOfView;
            Aspect = (frustumRight - frustumLeft) / (frustumTop - frustumBottom);
            FarClipPlane = farClipPlane;
        }

        private Matrix4x4 ComputeObliqueOrthographicMatrix(float width, float height, float near, float far, Vector3 cameraPosition, Quaternion cameraRotation, Vector3 clipPlanePosition, Vector3 clipPlaneNormal, float zOffset, float viewingFrustumHeightScalingFactor)
        {
            float halfWidth = width * 0.5f;
            float halfHeight = height * 0.5f;

            var projectionMatrix = Matrix4x4.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight * viewingFrustumHeightScalingFactor, near, far);
            var worldToCameraMatrix = Matrix4x4.TRS(cameraPosition, cameraRotation, new Vector3(1f, 1f, -1f)).inverse;

            Vector3 cClipPlaneNormal = worldToCameraMatrix.MultiplyVector(clipPlaneNormal);
            Vector3 cClipPlanePosition = worldToCameraMatrix.MultiplyPoint3x4(clipPlanePosition + clipPlaneNormal * zOffset);
            Vector4 cClipPlane = new Vector4(cClipPlaneNormal.x, cClipPlaneNormal.y, cClipPlaneNormal.z, -Vector3.Dot(cClipPlanePosition, cClipPlaneNormal));
            
            Vector4 c = cClipPlane * (2.0F / (Vector4.Dot(cClipPlane, projectionMatrix.inverse * Vector4.one)));
            projectionMatrix.m20 = c.x;
            projectionMatrix.m21 = c.y;
            projectionMatrix.m22 = c.z;
            projectionMatrix.m23 = c.w - 1.0F;

            return projectionMatrix;
        }

        #endregion

        internal class RenderModeProperties
        {
            internal Vector3 Position { get; set; }
            internal Quaternion Rotation { get; set; }
            internal Matrix4x4 ProjectionMatrix { get; set; }
            internal float NearClipPlane { get; set; }

            internal RenderModeProperties()
            {
                Rotation = Quaternion.identity;
            }
        }
    }
}
