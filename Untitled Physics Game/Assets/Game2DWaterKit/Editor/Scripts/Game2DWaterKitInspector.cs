namespace Game2DWaterKit
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.AnimatedValues;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    
    [InitializeOnLoad]
    public abstract class Game2DWaterKitInspector : Editor
    {
        private static Dictionary<string, AnimBool> _foldoutsAnimBools = new Dictionary<string, AnimBool>();
        private static System.Action _foldoutsAnimBoolsCallback;

        protected static bool _isInSimulationMode;
        protected static bool _isSimulationModeOwnedByWaterfallEditor;

        protected SerializedObject _meshRendererSerializedObject;
        protected SerializedProperty _materialProperty;

        protected bool _isMultiEditing;

        static Game2DWaterKitInspector()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui -= Game2DWaterKitObjectResizeTool.WatchForObjectSizeChanges;
            SceneView.duringSceneGui += Game2DWaterKitObjectResizeTool.WatchForObjectSizeChanges;
#else
            SceneView.onSceneGUIDelegate -= Game2DWaterKitObjectResizeTool.WatchForObjectSizeChanges;
            SceneView.onSceneGUIDelegate += Game2DWaterKitObjectResizeTool.WatchForObjectSizeChanges;
#endif
        }

        private void OnEnable()
        {
            foreach (Game2DWaterKitObject waterKitObject in targets)
            {
                if (!waterKitObject.IsInitialized)
                    waterKitObject.InitializeModules();
            }

            Game2DWaterKitObjectResizeTool.RepaintInspector = Repaint;
            _foldoutsAnimBoolsCallback += Repaint;
            _isMultiEditing = targets.Length > 1;

            Initiliaze();
        }

        private void OnDisable()
        {
            _foldoutsAnimBoolsCallback -= Repaint;
        }

        public override void OnInspectorGUI()
        {
            if (!Game2DWaterKitStyles.IsInitialized)
                Game2DWaterKitStyles.Initialize();

            if(_meshRendererSerializedObject == null)
            {
                _meshRendererSerializedObject = new SerializedObject(Selection.gameObjects.Select(go => go.GetComponent<Renderer>()).ToArray());
                _materialProperty = _meshRendererSerializedObject.FindProperty("m_Materials.Array.data[0]");
            }

            serializedObject.Update();
            _meshRendererSerializedObject.Update();

            if (Game2DWaterKitManagerWindow.HasActionRequired())
            {
                BeginBoxGroup(true);
                EditorGUILayout.HelpBox("Action is required! " + Game2DWaterKitManagerWindow.ActionRequiredMessage, MessageType.Error);
                if (GUILayout.Button("Take Action!"))
                    Game2DWaterKitManagerWindow.ShowWindow();
                EndBoxGroup();
            }

            DrawProperties();

            serializedObject.ApplyModifiedProperties();
            _meshRendererSerializedObject.ApplyModifiedProperties();
        }

        protected abstract void Initiliaze();
        protected abstract void DrawProperties();
        protected abstract void IterateSimulationPreview();
        protected abstract void RestartSimulationPreview();

        protected void DrawPrefabUtilityProperties()
        {
            if (!_isMultiEditing)
                DrawPropertiesFadeGroup("Prefab Utility", DrawPrefabUtility, true, false, false);
        }

        protected void FrameTargetObject()
        {
            Selection.activeObject = target;
            SceneView.FrameLastActiveSceneView();
        }

        protected void DrawSimulationPreviewProperties(bool isWaterfall, System.Action DoExtraPropertiesLayout = null)
        {
            var isSimulationModeActive = !EditorApplication.isPlayingOrWillChangePlaymode && _isInSimulationMode && (isWaterfall == _isSimulationModeOwnedByWaterfallEditor);

            BeginPropertiesGroup(false, "Simulation Preview");

            if (_isInSimulationMode && isWaterfall != _isSimulationModeOwnedByWaterfallEditor)
                EditorGUILayout.HelpBox(string.Format("Another {0} simulation is running! It will stop if you enter this simulation mode!", _isSimulationModeOwnedByWaterfallEditor ? "waterfall" : "water"), MessageType.Info);

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            isSimulationModeActive = GUI.Toggle(EditorGUILayout.GetControlRect(), isSimulationModeActive, isSimulationModeActive ? Game2DWaterKitStyles.PreviewSimulationOnButtonLabel : Game2DWaterKitStyles.PreviewSimulationOffButtonLabel, Game2DWaterKitStyles.ButtonStyle);
            EditorGUI.EndDisabledGroup();
            if (EditorGUI.EndChangeCheck())
            {
                if (isSimulationModeActive)
                {
                    FrameTargetObject();
                    
                    if (Game2DWaterKitSimulationPreviewMode.IsRunning)
                        Game2DWaterKitSimulationPreviewMode.Stop();

                    Game2DWaterKitSimulationPreviewMode.IterateSimulation = IterateSimulationPreview;
                    Game2DWaterKitSimulationPreviewMode.RestartSimulation = RestartSimulationPreview;

                    Game2DWaterKitSimulationPreviewMode.Start();

                    _isInSimulationMode = true;
                    _isSimulationModeOwnedByWaterfallEditor = isWaterfall;
                    
                }
                else
                {
                    Game2DWaterKitSimulationPreviewMode.Stop();

                    Game2DWaterKitSimulationPreviewMode.IterateSimulation = null;
                    Game2DWaterKitSimulationPreviewMode.RestartSimulation = null;

                    _isInSimulationMode = false;
                }
            }

            if (isSimulationModeActive)
            {
                EditorGUILayout.Space();

                if (DoExtraPropertiesLayout != null)
                    DoExtraPropertiesLayout.Invoke();

                BeginPropertiesGroup();

                var rect = EditorGUILayout.GetControlRect();
                float xMax = rect.xMax;

                rect.width = rect.width * 0.5f - 1f;

                EditorGUI.BeginChangeCheck();

                var targetFPSLabel = Game2DWaterKitStyles.SimulationModeTargetFrameratePropertyLabel;
                SetEditorGUIUtilityLabelWidth(targetFPSLabel.WidthRegular, true);
                EditorGUI.BeginChangeCheck();
                float targetFPS = EditorGUI.FloatField(rect, targetFPSLabel.Content, 1f / Game2DWaterKitSimulationPreviewMode.TimeStep);
                if (EditorGUI.EndChangeCheck())
                    Game2DWaterKitSimulationPreviewMode.TimeStep = 1f / Mathf.Clamp(targetFPS, 25f, float.MaxValue);

                rect.x += rect.width + 2f;

                var simulationTimeStepLabel = Game2DWaterKitStyles.SimulationModeTimeStepPropertyLabel;
                SetEditorGUIUtilityLabelWidth(simulationTimeStepLabel.WidthRegular, true);
                EditorGUI.BeginChangeCheck();
                float timeStep = EditorGUI.FloatField(rect, simulationTimeStepLabel.Content, Game2DWaterKitSimulationPreviewMode.TimeStep);
                if (EditorGUI.EndChangeCheck())
                    Game2DWaterKitSimulationPreviewMode.TimeStep = Mathf.Clamp(timeStep, 0.0001f, 0.04f);

                if (GUILayout.Button("Match to Project Fixed Delta Time"))
                    Game2DWaterKitSimulationPreviewMode.TimeStep = Time.fixedDeltaTime;

                if (EditorGUI.EndChangeCheck())
                    Game2DWaterKitSimulationPreviewMode.RestartSimulation();

                if (Game2DWaterKitSimulationPreviewMode.IsRunning)
                {
                    int speed = Mathf.Clamp(Mathf.RoundToInt(Game2DWaterKitSimulationPreviewMode.RelativeAnimationSpeed * 100f), 0, 100);
                    EditorGUILayout.HelpBox(string.Format("The simualtion is running at {0}% its actual speed (its target framerate)", speed), MessageType.Info);
                }

                EndPropertiesGroup();
            }

            EndPropertiesGroup();
        }

        protected void DrawSimulationPreviewStopRestartButtons()
        {
            EditorGUI.BeginDisabledGroup(!Game2DWaterKitSimulationPreviewMode.IsRunning);
            if (GUI.Button(EditorGUILayout.GetControlRect(), Game2DWaterKitStyles.StopSimulationButtonLabel))
                Game2DWaterKitSimulationPreviewMode.Stop();
            EditorGUI.EndDisabledGroup();

            if (GUI.Button(EditorGUILayout.GetControlRect(), Game2DWaterKitStyles.RestartSimulationButtonLabel))
                Game2DWaterKitSimulationPreviewMode.Restart();
        }

        protected void DrawSizeProperty()
        {
            var sizeProperty = serializedObject.FindProperty("_size");

            EditorGUI.BeginDisabledGroup(_isMultiEditing && sizeProperty.hasMultipleDifferentValues);

            Vector3 scale = (target as MonoBehaviour).transform.localScale;
            Vector2 size = Vector2.Scale(sizeProperty.vector2Value, scale);

            if(!Game2DWaterKitObjectResizeTool.IsResizing && scale != Vector3.one)
            {
                sizeProperty.vector2Value = size;
                (target as MonoBehaviour).transform.localScale = Vector3.one;
            }

            var rect = EditorGUILayout.GetControlRect();
            float xMax = rect.xMax;

            rect.xMax -= 27f;

            SetEditorGUIUtilityLabelWidth(sizeProperty, Game2DWaterKitStyles.SizePropertyLabel, true);
            var sizeLabel = EditorGUI.BeginProperty(rect, Game2DWaterKitStyles.SizePropertyLabel.Content, sizeProperty);
            rect = EditorGUI.PrefixLabel(rect, sizeLabel);
            EditorGUI.BeginChangeCheck();
            size = EditorGUI.Vector2Field(rect, string.Empty, size);
            if (EditorGUI.EndChangeCheck())
                sizeProperty.vector2Value = size;
            EditorGUI.EndProperty();
            
            rect.xMax = xMax;
            rect.xMin = xMax - 25f;

            bool editSize = Tools.current == Tool.Rect;
            EditorGUI.BeginChangeCheck();
            editSize = GUI.Toggle(rect, editSize, editSize ? Game2DWaterKitStyles.EditSizeIconOnButtonLabel : Game2DWaterKitStyles.EditSizeIconOffButtonLabel, Game2DWaterKitStyles.ButtonStyle);
            if (EditorGUI.EndChangeCheck())
            {
                if (editSize)
                {
                    FrameTargetObject();
                    Tools.current = Tool.Rect;
                }
                else Tools.current = Tool.View;
            }

            EditorGUI.EndDisabledGroup();
        }

        protected void DrawRenderTextureProperties(string propertyName)
        {
            BeginPropertiesGroup(false, "Render-Texture Properties");

            var renderTextureUseFixedSize = serializedObject.FindProperty(propertyName + "RenderTextureUseFixedSize");

            DrawProperty(renderTextureUseFixedSize, Game2DWaterKitStyles.RenderTextureUseFixedSizePropertyLabel);

            if (renderTextureUseFixedSize.boolValue)
                DrawProperty(propertyName + "RenderTextureFixedSize", Game2DWaterKitStyles.RenderTextureFixedSizePropertyLabel);
            else
                DrawProperty(propertyName + "RenderTextureResizeFactor", Game2DWaterKitStyles.RenderTextureResizingFactorPropertyLabel);

            DrawProperty(propertyName + "RenderTextureFilterMode", Game2DWaterKitStyles.RenderTextureFilterModePropertyLabel);

            EndPropertiesGroup();
        }

        protected void DrawRenderingModuleProperties(bool hasAnActiveEffect)
        {
            BeginPropertiesGroup();
            DrawProperty(_materialProperty, Game2DWaterKitStyles.MaterialPropertyLabel);
            EndPropertiesGroup();

            BeginPropertiesGroup();

            bool hasDifferentMaterials = _materialProperty.hasMultipleDifferentValues;
            bool disabled = hasDifferentMaterials || !hasAnActiveEffect;
            
            if (hasDifferentMaterials)
                EditorGUILayout.HelpBox(Game2DWaterKitStyles.CantMultiEditBecauseUsingDifferentMaterialMessage, MessageType.Info);

            EditorGUI.BeginDisabledGroup(disabled);
            DrawProperty("_renderingModuleFarClipPlane", Game2DWaterKitStyles.FarClipPlanePropertyLabel);

#if !GAME_2D_WATER_KIT_LWRP && !GAME_2D_WATER_KIT_URP
            DrawProperty("_renderingModuleRenderPixelLights", Game2DWaterKitStyles.RenderPixelLightsPropertyLabel);
#endif

            bool isAntiAliasingEnabledInQualitySettings = QualitySettings.antiAliasing > 1;
            EditorGUI.BeginDisabledGroup(!isAntiAliasingEnabledInQualitySettings);
            var allowMSAAProperty = serializedObject.FindProperty("_renderingModuleAllowMSAA");
            var allowMSAAPropertyRect = EditorGUILayout.GetControlRect();
            var allowMSAAPropertyLabel = EditorGUI.BeginProperty(allowMSAAPropertyRect, Game2DWaterKitStyles.AllowMSAAPropertyLabel.Content, allowMSAAProperty);
            SetEditorGUIUtilityLabelWidth(allowMSAAProperty, Game2DWaterKitStyles.AllowMSAAPropertyLabel);
            EditorGUI.BeginChangeCheck();
            bool allowMSAA = EditorGUI.Toggle(allowMSAAPropertyRect, allowMSAAPropertyLabel, isAntiAliasingEnabledInQualitySettings && allowMSAAProperty.boolValue);
            if (EditorGUI.EndChangeCheck())
                allowMSAAProperty.boolValue = allowMSAA;
            EditorGUI.EndProperty();
            EditorGUI.EndDisabledGroup();

            DrawProperty("_renderingModuleAllowHDR", Game2DWaterKitStyles.AllowHDRPropertyLabel);
            EditorGUI.EndDisabledGroup();

            EndPropertiesGroup();

            BeginPropertiesGroup();
            DrawSortingLayerField(serializedObject.FindProperty("_renderingModuleSortingLayerID"), serializedObject.FindProperty("_renderingModuleSortingOrder"));
            EndPropertiesGroup();
        }

        protected void DrawTimeIntervalProperties(string propertyName)
        {
            BeginPropertiesGroup(false, "Time Interval Properties");

            var randomizeInterval = serializedObject.FindProperty(propertyName + "RandomizeTimeInterval");

            DrawProperty(randomizeInterval, Game2DWaterKitStyles.RandomizeTimeIntervalPropertyLabel);

            if (randomizeInterval.boolValue)
            {
                DrawProperty(propertyName + "MinimumTimeInterval", Game2DWaterKitStyles.MinimumTimeIntervalPropertyLabel, true);
                DrawProperty(propertyName + "MaximumTimeInterval", Game2DWaterKitStyles.MaximumTimeIntervalPropertyLabel, true);
            }
            else DrawProperty(propertyName + "TimeInterval", Game2DWaterKitStyles.TimeIntervalPropertyLabel, true);

            EndPropertiesGroup();
        }

        protected void DrawProperty(string propertyName, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, bool delayedField = false)
        {
            DrawProperty(serializedObject.FindProperty(propertyName), propertyLabel, delayedField);
        }

        protected void DrawProperty(Rect rect, string propertyName, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, bool delayedField = false)
        {
            DrawProperty(rect, serializedObject.FindProperty(propertyName), propertyLabel, delayedField);
        }

        protected static void DrawProperty(Rect rect, SerializedProperty property, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, bool delayedField = false)
        {
            SetEditorGUIUtilityLabelWidth(property, propertyLabel);

            if (delayedField && property.propertyType == SerializedPropertyType.Float)
                EditorGUI.DelayedFloatField(rect, property, propertyLabel.Content);
            else
                EditorGUI.PropertyField(rect, property, propertyLabel.Content, true);
        }

        protected static void DrawProperty(SerializedProperty property, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, bool delayedField = false)
        {
            SetEditorGUIUtilityLabelWidth(property, propertyLabel);

            if (delayedField && property.propertyType == SerializedPropertyType.Float)
                EditorGUILayout.DelayedFloatField(property, propertyLabel.Content);
            else
                EditorGUILayout.PropertyField(property, propertyLabel.Content, true);
        }

        protected static bool DrawPropertyWithPreviewButton(SerializedProperty property, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, bool previewState, bool isPreviewButtonEnabled = true)
        {
            var rect = DrawPropertyPrecedingActionButton(property, propertyLabel);

            EditorGUI.BeginDisabledGroup(!isPreviewButtonEnabled);
            previewState = GUI.Toggle(rect, previewState, previewState ? Game2DWaterKitStyles.PreviewIconOnButtonLabel : Game2DWaterKitStyles.PreviewIconOffButtonLabel, Game2DWaterKitStyles.ButtonStyle);
            EditorGUI.EndDisabledGroup();

            return previewState;
        }

        protected static bool DrawPropertyWithActionButton(SerializedProperty property, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, GUIContent actionButtonLabel)
        {
            var rect = DrawPropertyPrecedingActionButton(property, propertyLabel);
            return GUI.Button(rect, actionButtonLabel);
        }

        protected static bool DrawSliderWithActionButton(ref float value, float min, float max, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel, GUIContent actionButtonLabel)
        {
            var rect = EditorGUILayout.GetControlRect();

            float xMax = rect.xMax;

            rect.xMax -= 27;
            value = EditorGUI.Slider(rect, propertyLabel.Content, value, min, max);

            rect.xMax = xMax;
            rect.xMin = xMax - 25;
            return GUI.Button(rect, Game2DWaterKitStyles.RunSimulationButtonLabel);
        }

        protected static bool DrawPropertiesFadeGroup(string groupName, System.Action DoPropertiesLayout, bool useHelpBoxStyle = true, bool indent = false, bool bold = false, SerializedProperty groupToggleProperty = null)
        {
            bool hasChangedGroupToggleState = false;

            var fadeGroupAnimBool = GetFoldoutAnimBool(groupName);

            if (groupToggleProperty == null)
            {
                EditorGUI.BeginChangeCheck();
                if (indent)
                    EditorGUI.indentLevel++;
                var foldout = EditorGUILayout.Foldout(fadeGroupAnimBool.value, RemovePropertyID(groupName), true, bold ? Game2DWaterKitStyles.BoldFoldoutStyle : EditorStyles.foldout);
                if (indent)
                    EditorGUI.indentLevel--;
                if (EditorGUI.EndChangeCheck())
                    fadeGroupAnimBool.target = foldout;
            }
            else
            {
                var rect = EditorGUILayout.GetControlRect();

                if (indent)
                {
                    EditorGUI.indentLevel++;
                    rect = EditorGUI.IndentedRect(rect);
                    EditorGUI.indentLevel--;
                }

                float xmax = rect.xMax;

                rect.xMin += 3f;
                rect.xMax = rect.xMin + 14f;

                EditorGUI.BeginChangeCheck();
                DrawGroupToggle(rect, groupToggleProperty, GUIContent.none);
                if (EditorGUI.EndChangeCheck())
                    hasChangedGroupToggleState = true;

                rect.xMin -= 3f;
                rect.xMax = xmax;
                EditorGUI.BeginChangeCheck();
                var foldout = EditorGUI.Foldout(rect, fadeGroupAnimBool.value, "     " + RemovePropertyID(groupName), true, bold ? Game2DWaterKitStyles.BoldFoldoutStyle : EditorStyles.foldout);

                if (EditorGUI.EndChangeCheck())
                    fadeGroupAnimBool.target = foldout;
            }

            using (var group = new EditorGUILayout.FadeGroupScope(fadeGroupAnimBool.faded))
            {
                if (group.visible)
                {
                    BeginBoxGroup(useHelpBoxStyle);
                    bool isDisabled = groupToggleProperty == null ? false : !groupToggleProperty.boolValue;
                    EditorGUI.BeginDisabledGroup(isDisabled);
                    DoPropertiesLayout.Invoke();
                    EditorGUI.EndDisabledGroup();
                    EndBoxGroup();
                }
            }

            return hasChangedGroupToggleState;
        }

        protected static void DrawPropertiesGroup(System.Action DoPropertiesLayout, bool useHelpBoxStyle = false, string groupName = null, SerializedProperty groupToggleProperty = null)
        {
            BeginPropertiesGroup(useHelpBoxStyle, groupName, groupToggleProperty);
            DoPropertiesLayout.Invoke();
            EndPropertiesGroup();
        }

        protected static void BeginPropertiesGroup(bool useHelpBoxStyle = false, string groupName = null, SerializedProperty groupToggleProperty = null)
        {
            if (!string.IsNullOrEmpty(groupName))
            {
                Rect labelRect = EditorGUILayout.GetControlRect();

                if (!useHelpBoxStyle)
                {
                    labelRect.x += 3f;
                    labelRect.y += 5f;
                }

                if (groupToggleProperty == null)
                    EditorGUI.LabelField(labelRect, RemovePropertyID(groupName), EditorStyles.boldLabel);
                else
                    DrawGroupToggle(labelRect, groupToggleProperty, Game2DWaterKitStyles.GetTempLabel(RemovePropertyID(groupName)), true);
            }

            BeginBoxGroup(useHelpBoxStyle);
        }

        protected static void EndPropertiesGroup()
        {
            EndBoxGroup();
        }

        protected static void SetEditorGUIUtilityLabelWidth(float labelWidth, bool forceUsingLabelWidth = false)
        {
            if (forceUsingLabelWidth)
                EditorGUIUtility.labelWidth = labelWidth;
            else
                EditorGUIUtility.labelWidth = labelWidth > Game2DWaterKitStyles.MinimumLabelWidth ? labelWidth : Game2DWaterKitStyles.MinimumLabelWidth;
        }

        protected static void SetEditorGUIUtilityLabelWidth(SerializedProperty property, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel label, bool forceUsingLabelWidth = false)
        {
            SetEditorGUIUtilityLabelWidth(property.prefabOverride ? label.WidthBold : label.WidthRegular, forceUsingLabelWidth);
        }

        protected static void DrawSortingLayerField(SerializedProperty layerID, SerializedProperty orderInLayer)
        {
            MethodInfo methodInfo = typeof(EditorGUILayout).GetMethod("SortingLayerField", BindingFlags.Static | BindingFlags.NonPublic, null, new[] {
                typeof( GUIContent ),
                typeof( SerializedProperty ),
                typeof( GUIStyle ),
                typeof( GUIStyle )
            }, null);

            if (methodInfo != null)
            {
                var orderInLayerLabel = Game2DWaterKitStyles.SortingOrderInLayerPropertyLabel;
                var sortingLayerLabel = Game2DWaterKitStyles.SortingLayerPropertyLabel;

                SetEditorGUIUtilityLabelWidth(layerID, sortingLayerLabel);
                object[] parameters = { sortingLayerLabel.Content, layerID, EditorStyles.popup, EditorStyles.label };
                methodInfo.Invoke(null, parameters);

                DrawProperty(orderInLayer, orderInLayerLabel);
            }
        }

        protected static string[] GetAllLayersNamesInMask(int mask)
        {
            List<string> layers = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                if (mask == (mask | (1 << i)) && !string.IsNullOrEmpty(LayerMask.LayerToName(i)))
                {
                    layers.Add(LayerMask.LayerToName(i));
                }
            }
            return layers.ToArray();
        }

        protected static bool MaskContainsLayer(int mask, int layerIndex)
        {
            return mask == (mask | (1 << layerIndex));
        }

        protected static int LayerMaskToConcatenatedLayersMask(int mask, string[] displayedOptions)
        {
            int concatenatedMask = 0;

            for (int i = 0; i < displayedOptions.Length; i++)
            {
                int layer = LayerMask.NameToLayer(displayedOptions[i]);
                if (MaskContainsLayer(mask, layer))
                {
                    concatenatedMask |= (1 << i);
                }
            }

            return concatenatedMask;
        }

        protected static int ConcatenatedLayersMaskToLayerMask(int concatMask, string[] displayedOptions)
        {
            int mask = 0;
            for (int i = 0; i < displayedOptions.Length; i++)
            {
                if (MaskContainsLayer(concatMask, i))
                {
                    mask |= (1 << LayerMask.NameToLayer(displayedOptions[i]));
                }
            }
            return mask;
        }

        protected static void DrawRectInSceneView(Vector2 position, Vector2 size, Color color)
        {
            Vector2 topLeft = position + new Vector2(0f, size.y);
            Vector2 topRight = position + size;
            Vector3 bottomRight = position + new Vector2(size.x, 0f);

            Handles.color = color;
            Handles.DrawPolyLine(position, topLeft, topRight, bottomRight, position);
        }

        private static Rect DrawPropertyPrecedingActionButton(SerializedProperty property, Game2DWaterKitStyles.Game2DWaterKitPropertyLabel propertyLabel)
        {
            var rect = EditorGUILayout.GetControlRect();

            float xMax = rect.xMax;

            rect.xMax -= 27;
            DrawProperty(rect, property, propertyLabel);

            rect.xMax = xMax;
            rect.xMin = xMax - 25;

            return rect;
        }

        private static void BeginBoxGroup(bool useHelpBoxStyle = true)
        {
            EditorGUILayout.BeginVertical(useHelpBoxStyle ? Game2DWaterKitStyles.HelpBoxStyle : Game2DWaterKitStyles.GroupBoxStyle);
        }

        private static void EndBoxGroup()
        {
            EditorGUILayout.EndVertical();
        }

        private static AnimBool GetFoldoutAnimBool(string name)
        {
            AnimBool foldoutAnimBool;

            if (!_foldoutsAnimBools.TryGetValue(name, out foldoutAnimBool))
            {
                foldoutAnimBool = new AnimBool(() => _foldoutsAnimBoolsCallback.Invoke());
                _foldoutsAnimBools.Add(name, foldoutAnimBool);
            }

            return foldoutAnimBool;
        }

        private static string RemovePropertyID(string groupName)
        {
            int hashIndex = groupName.IndexOf('#');
            return hashIndex == -1 ? groupName : groupName.Substring(0, hashIndex);
        }

        private static void DrawGroupToggle(Rect rect, SerializedProperty groupToggleProperty, GUIContent label, bool boldLabel = false)
        {
            EditorGUI.showMixedValue = groupToggleProperty.hasMultipleDifferentValues;
            
            EditorGUI.BeginChangeCheck();
            bool isEnabled = EditorGUI.ToggleLeft(rect, label, groupToggleProperty.boolValue, boldLabel ? EditorStyles.boldLabel : EditorStyles.label);
            if (EditorGUI.EndChangeCheck())
                groupToggleProperty.boolValue = isEnabled;

            EditorGUI.showMixedValue = false;
        }

        private static string GetValidAssetFileName(string prefabsPath, string assetName, string assetExtension, System.Type assetType)
        {
            string fileName = assetName;

            string path = prefabsPath + fileName + assetExtension;
            bool prefabWithSameNameExist = AssetDatabase.LoadAssetAtPath(path, assetType) != null;
            if (prefabWithSameNameExist)
            {
                int i = 1;
                while (prefabWithSameNameExist)
                {
                    fileName = assetName + " " + i;
                    path = prefabsPath + fileName + assetExtension;
                    prefabWithSameNameExist = AssetDatabase.LoadAssetAtPath(path, assetType) != null;
                    i++;
                }
            }

            return fileName;
        }

        private void DrawPrefabUtility()
        {
            string prefabsPath = EditorPrefs.GetString("Water2D_Paths_PrefabUtility_Path", "Assets/");

            GameObject go = (target as MonoBehaviour).gameObject;

            var material = go.GetComponent<MeshRenderer>().sharedMaterial;

            Texture waterNoiseTexture = material != null && material.HasProperty("_NoiseTexture") ? material.GetTexture("_NoiseTexture") : null;

#if UNITY_2018_3_OR_NEWER
            bool isPrefabInstance = PrefabUtility.GetPrefabInstanceStatus(go) == PrefabInstanceStatus.Connected;
#else
            PrefabType prefabType = PrefabUtility.GetPrefabType(go);
            bool isPrefabInstance = prefabType == PrefabType.PrefabInstance;
            bool isPrefabInstanceDisconnected = prefabType == PrefabType.DisconnectedPrefabInstance;
#endif

            bool materialAssetAlreadyExist = material != null && AssetDatabase.Contains(material);
            bool textureAssetAlreadyExist = waterNoiseTexture != null && AssetDatabase.Contains(waterNoiseTexture);

            EditorGUI.BeginDisabledGroup(true);
#if UNITY_2018_2_OR_NEWER
            Object prefabObjct = isPrefabInstance ? PrefabUtility.GetCorrespondingObjectFromSource(go) : null;
#else
            Object prefabObjct = isPrefabInstance ? PrefabUtility.GetPrefabParent(go) : null;
#endif
            EditorGUILayout.ObjectField(prefabObjct, typeof(Object), false);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(prefabsPath, string.Format("Prefab Path: {0}", prefabsPath)), EditorStyles.textField);
            if (GUILayout.Button(".", EditorStyles.miniButton, GUILayout.MaxWidth(14f)))
            {
                string newPrefabsPath = EditorUtility.OpenFolderPanel("Select prefabs path", "Assets", "");
                if (!string.IsNullOrEmpty(newPrefabsPath))
                {
                    newPrefabsPath = newPrefabsPath.Substring(Application.dataPath.Length);
                    prefabsPath = "Assets" + newPrefabsPath + "/";
                    EditorPrefs.SetString("Water2D_Paths_PrefabUtility_Path", prefabsPath);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (!isPrefabInstance)
            {
                if (GUILayout.Button("Create Prefab"))
                {
                    string fileName = GetValidAssetFileName(prefabsPath, go.name, ".prefab", typeof(GameObject));

                    if (!textureAssetAlreadyExist && waterNoiseTexture != null)
                    {
                        string noiseTexturePath = prefabsPath + fileName + "_noiseTexture.asset";
                        AssetDatabase.CreateAsset(waterNoiseTexture, noiseTexturePath);
                    }

                    if (!materialAssetAlreadyExist && material != null)
                    {
                        string materialPath = prefabsPath + fileName + ".mat";
                        AssetDatabase.CreateAsset(material, materialPath);
                    }

                    string prefabPath = prefabsPath + fileName + ".prefab";
#if UNITY_2018_3_OR_NEWER
                    PrefabUtility.SaveAsPrefabAssetAndConnect(go, prefabPath, InteractionMode.AutomatedAction);
#else
                    PrefabUtility.CreatePrefab(prefabPath, go, ReplacePrefabOptions.ConnectToPrefab);
#endif
                }
            }
#if UNITY_2018_3_OR_NEWER
                    /*
                    As of Unity 2018.3, disconnecting (unlinking) and relinking a Prefab instance are no longer supported.
                    Alternatively, we can now unpack a Prefab instance if we want to entirely remove its link to its Prefab asset 
                    and thus be able to restructure the resulting plain GameObject as we please.
                    */
                    if (isPrefabInstance)
                    {
                        EditorGUILayout.HelpBox(Game2DWaterKitStyles.NewPrefabWorkflowMessage, MessageType.Info);
                    }
#else
            if (isPrefabInstance)
            {
                if (GUILayout.Button("Unlink Prefab"))
                {
#if UNITY_2018_2_OR_NEWER
                    GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(go) as GameObject;
#else
                    GameObject prefab = PrefabUtility.GetPrefabParent(go) as GameObject;
#endif
                    PrefabUtility.DisconnectPrefabInstance(go);
                    UnityEngine.Material prefabMaterial = prefab.GetComponent<MeshRenderer>().sharedMaterial;
                    if (material != null && material == prefabMaterial)
                    {
                        bool usePrefabMaterial = EditorUtility.DisplayDialog("Use same prefab's material?",
                    "Do you still want to use the prefab's material?",
                    "Yes",
                    "No, create water's own material");

                        if (!usePrefabMaterial)
                        {
                            UnityEngine.Material duplicateMaterial = new UnityEngine.Material(material);
                            if (waterNoiseTexture != null)
                            {
                                Texture duplicateWaterNoiseTexture = Instantiate<Texture>(waterNoiseTexture);
                                duplicateMaterial.SetTexture("_NoiseTexture", duplicateWaterNoiseTexture);
                            }
                            go.GetComponent<MeshRenderer>().sharedMaterial = duplicateMaterial;
                        }
                    }
                }
            }

            if (isPrefabInstanceDisconnected)
            {
                if (GUILayout.Button("Relink Prefab"))
                {
                    PrefabUtility.ReconnectToLastPrefab(go);
#if UNITY_2018_2_OR_NEWER
                    GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(go) as GameObject;
#else
                    GameObject prefab = PrefabUtility.GetPrefabParent(go) as GameObject;
#endif
                    UnityEngine.Material prefabMaterial = prefab.GetComponent<MeshRenderer>().sharedMaterial;

                    if (prefabMaterial != null && material != prefabMaterial)
                    {
                        bool usePrefabMaterial = EditorUtility.DisplayDialog("Use prefab's material?",
                        "Do you want to use the prefab's material?",
                        "Yes",
                        "No, continue to use the current water material");

                        if (usePrefabMaterial)
                        {
                            go.GetComponent<MeshRenderer>().sharedMaterial = prefabMaterial;
                        }
                        else
                        {
                            if (!materialAssetAlreadyExist)
                            {
                                string fileName = GetValidAssetFileName(prefabsPath, go.name, ".mat", typeof(UnityEngine.Material));

                                if (!textureAssetAlreadyExist)
                                {
                                    string noiseTexturePath = prefabsPath + fileName + "_noiseTexture.asset";
                                    AssetDatabase.CreateAsset(waterNoiseTexture, noiseTexturePath);
                                }

                                string materialPath = prefabsPath + fileName + ".mat";
                                AssetDatabase.CreateAsset(material, materialPath);
                            }
                        }
                    }
                }
            }
#endif
        }

        protected System.Action<Arg> CreateDelegate<T, Arg>(T instance, string methodName)
        {
            return (System.Action<Arg>)System.Delegate.CreateDelegate(typeof(System.Action<Arg>), instance, methodName);
        }

        protected System.Action CreateDelegate<T>(T instance, string methodName)
        {
            return (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), instance, methodName);
        }

        protected System.Func<Arg> CreatePropertyGetterDelegate<T, Arg>(T instance, string propertyName)
        {
            return (System.Func<Arg>)System.Delegate.CreateDelegate(typeof(System.Func<Arg>), instance, "get_" + propertyName);
        }

        private static class Game2DWaterKitObjectResizeTool
        {
            public static System.Action RepaintInspector;

            private static bool _hasPendingChanges;
            private static bool _isCurrentActiveTransformValid;
            private static Transform _activeTransform;
            private static SerializedObject _currentActiveSerializedObject;

            public static bool IsResizing { get; private set; }

            public static void WatchForObjectSizeChanges(SceneView sceneView)
            {
                var currentActiveTransform = Selection.activeTransform;

                if (currentActiveTransform == null)
                    return;

                if (currentActiveTransform != _activeTransform)
                {
                    _activeTransform = currentActiveTransform;
                    _isCurrentActiveTransformValid = currentActiveTransform.GetComponent<Game2DWaterKitObject>() != null;

                    if (_isCurrentActiveTransformValid)
                        _currentActiveSerializedObject = new SerializedObject(currentActiveTransform.GetComponent<Game2DWaterKitObject>());
                }

                if (_isCurrentActiveTransformValid)
                {
                    var currentEvent = Event.current;

                    if (currentEvent == null || !currentEvent.isMouse || currentEvent.type == EventType.MouseMove)
                        return;

                    bool isScalingTool = Tools.current != Tool.Move && Tools.current != Tool.Rotate && Tools.current != Tool.View;

                    if (isScalingTool && currentActiveTransform.localScale != Vector3.one)
                    {
                        bool isMouseDragEvent = currentEvent.type == EventType.MouseDrag;

                        _hasPendingChanges |= isMouseDragEvent;

                        if (_hasPendingChanges && !isMouseDragEvent)
                        {
                            _currentActiveSerializedObject.Update();
                            var sizeProperty = _currentActiveSerializedObject.FindProperty("_size");
                            sizeProperty.vector2Value = Vector2.Scale(sizeProperty.vector2Value, currentActiveTransform.localScale);
                            _currentActiveSerializedObject.ApplyModifiedProperties();
                            Undo.RecordObject(_currentActiveSerializedObject.targetObject, "setting new size");
                            currentActiveTransform.localScale = Vector3.one;

                            _hasPendingChanges = false;
                        }

                        if (isMouseDragEvent && RepaintInspector != null)
                            RepaintInspector.Invoke();

                        IsResizing = isMouseDragEvent;
                    }
                }
            }
        }

        protected static class Game2DWaterKitSimulationPreviewMode
        {
            public static System.Action IterateSimulation;
            public static System.Action RestartSimulation;

            public static float TimeStep { get; set; }
            public static bool IsRunning { get; private set; }
            public static float RelativeAnimationSpeed { get; private set; }

            private static float _previousTime;
            private static float _elapsedTimeSinceLastIteration;
            private static float _elapsedTime;
            private static int _simulatedFrames;

            static Game2DWaterKitSimulationPreviewMode()
            {
                TimeStep  = 1 / 30f;
            }

            public static void Start()
            {
                if(IterateSimulation != null && RestartSimulation != null)
                {
                    EditorApplication.update -= Update;
                    EditorApplication.update += Update;

                    _previousTime = Time.realtimeSinceStartup;
                    _elapsedTimeSinceLastIteration = 0f;

                    IsRunning = true;
                }
            }

            public static void Stop()
            {
                EditorApplication.update -= Update;
                if(RestartSimulation != null)
                    RestartSimulation();
                IsRunning = false;
            }

            public static void Restart()
            {
                if (IterateSimulation != null && RestartSimulation != null)
                {
                    if (!IsRunning)
                        Start();

                    RestartSimulation();
                }
            }

            private static void Update()
            {
                if(EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    EditorApplication.update -= Update;
                    IsRunning = false;
                }

                float currentTime = Time.realtimeSinceStartup;
                float deltaTime = currentTime - _previousTime;

                _elapsedTimeSinceLastIteration += deltaTime;
                _elapsedTime += deltaTime;

                if (_elapsedTimeSinceLastIteration >= TimeStep)
                {
                    _elapsedTimeSinceLastIteration = 0f;
                    IterateSimulation();
                    _simulatedFrames++;
                }

                if (_elapsedTime >= 0.25f) // Report approximate simulation framerate every 0.25 second
                {
                    float simulatedFramesPerSecond = _simulatedFrames / _elapsedTime;
                    RelativeAnimationSpeed = simulatedFramesPerSecond * TimeStep;

                    _elapsedTime = 0f;
                    _simulatedFrames = 0;
                }

                _previousTime = currentTime;
            }
        }

        public static class Game2DWaterKitStyles
        {
            public static bool IsInitialized;

#region GUI Styles
            public static GUIStyle HelpBoxStyle;
            public static GUIStyle GroupBoxStyle;
            public static GUIStyle BoldFoldoutStyle;
            public static GUIStyle ButtonStyle;
#endregion

#region Icon Buttons Labels
            public static GUIContent PreviewIconOffButtonLabel;
            public static GUIContent PreviewIconOnButtonLabel;
            public static GUIContent EditSizeIconOffButtonLabel;
            public static GUIContent EditSizeIconOnButtonLabel;
            public static GUIContent RunSimulationButtonLabel;
            public static GUIContent StopSimulationButtonLabel;
            public static GUIContent ContinueSimulationButtonLabel;
            public static GUIContent PreviewSimulationOnButtonLabel;
            public static GUIContent PreviewSimulationOffButtonLabel;
            public static GUIContent RestartSimulationButtonLabel;
#endregion

#region Properties Labels
            public static Game2DWaterKitPropertyLabel SizePropertyLabel;
            public static Game2DWaterKitPropertyLabel SubdivisionsPerUnitPropertyLabel;
            public static Game2DWaterKitPropertyLabel UseEdgeColliderPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaveStiffnessPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaveDampingPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaveSpreadPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaveUseCustomBoundariesPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaveFirstCustomBoundaryPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaveSecondCustomBoundaryPropertyLabel;
            public static Game2DWaterKitPropertyLabel BuoyancyEffectorSurfaceLevelPropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesRaycastMaskPropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesRaycastMinimumDepthPropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesRaycastMaximumDepthPropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesRaycastMaximumDistancePropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesMinimumVelocityPropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesOnWaterEnterEventPropertyLabel;
            public static Game2DWaterKitPropertyLabel OnCollisionRipplesOnWaterExitEventPropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesUpdateWhenOffscreenPropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesRandomizeDisturbancePropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesSourcesPositionsPropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesRandomizeSourcesPositionsPropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesRandomSourceCountPropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesAllowDuplicateSourcesPositionsPropertyLabel;
            public static Game2DWaterKitPropertyLabel ConstantRipplesEditSourcesPositionsButtonLabel;
            public static Game2DWaterKitPropertyLabel ScriptGeneratedRipplesDisturbanceFactorPropertyLabel;
            public static Game2DWaterKitPropertyLabel TimeIntervalPropertyLabel;
            public static Game2DWaterKitPropertyLabel RandomizeTimeIntervalPropertyLabel;
            public static Game2DWaterKitPropertyLabel MinimumTimeIntervalPropertyLabel;
            public static Game2DWaterKitPropertyLabel MaximumTimeIntervalPropertyLabel;
            public static Game2DWaterKitPropertyLabel SmoothRipplesPropertyLabel;
            public static Game2DWaterKitPropertyLabel SmoothingFactorPropertyLabel;
            public static Game2DWaterKitPropertyLabel DisturbancePropertyLabel;
            public static Game2DWaterKitPropertyLabel MinimumDisturbancePropertyLabel;
            public static Game2DWaterKitPropertyLabel MaximumDisturbancePropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectAudioClipPropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectPoolSizePropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectPoolCanExpandPropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectUseConstantPitchPropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectPitchPropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectMinimumPitchPropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectMaximumPitchPropertyLabel;
            public static Game2DWaterKitPropertyLabel SoundEffectVolumePropertyLabel;
            public static Game2DWaterKitPropertyLabel ParticleEffectParticleSystemPropertyLabel;
            public static Game2DWaterKitPropertyLabel ParticleEffectPoolSizePropertyLabel;
            public static Game2DWaterKitPropertyLabel ParticleEffectPoolCanExpandPropertyLabel;
            public static Game2DWaterKitPropertyLabel ParticleEffectSpawnOffsetPropertyLabel;
            public static Game2DWaterKitPropertyLabel ParticleEffectStopActionPropertyLabel;
            public static Game2DWaterKitPropertyLabel RefractionReflectionMaskPropertyLabel;
            public static Game2DWaterKitPropertyLabel RefractionReflectionPartiallySubmergedObjectsMaskPropertyLabel;
            public static Game2DWaterKitPropertyLabel ReflectionPartiallySubmergedObjectsViewingFrustumHeightScalingFactorPropertyLabel;
            public static Game2DWaterKitPropertyLabel ReflectionOtherObjectsViewingFrustumHeightScalingFactorPropertyLabel;
            public static Game2DWaterKitPropertyLabel ReflectionViewingFrustumHeightScalingFactorPropertyLabel;
            public static Game2DWaterKitPropertyLabel ReflectionZOffsetPropertyLabel;
            public static Game2DWaterKitPropertyLabel RenderTextureFixedSizePropertyLabel;
            public static Game2DWaterKitPropertyLabel RenderTextureResizingFactorPropertyLabel;
            public static Game2DWaterKitPropertyLabel RenderTextureUseFixedSizePropertyLabel;
            public static Game2DWaterKitPropertyLabel RenderTextureFilterModePropertyLabel;
            public static Game2DWaterKitPropertyLabel MaterialPropertyLabel;
            public static Game2DWaterKitPropertyLabel FarClipPlanePropertyLabel;
            public static Game2DWaterKitPropertyLabel AllowMSAAPropertyLabel;
            public static Game2DWaterKitPropertyLabel AllowHDRPropertyLabel;
            public static Game2DWaterKitPropertyLabel RenderPixelLightsPropertyLabel;
            public static Game2DWaterKitPropertyLabel SortingLayerPropertyLabel;
            public static Game2DWaterKitPropertyLabel SortingOrderInLayerPropertyLabel;

            public static Game2DWaterKitPropertyLabel WaterfallAffectedWaterObjectPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaterfallAffectedWaterObjectRippleSpreadPropertyLabel;
            public static Game2DWaterKitPropertyLabel WaterfallAffectedWaterObjectRippleUpdateWhenOffscreenPropertyLabel;

            public static Game2DWaterKitPropertyLabel SimulationModeTargetFrameratePropertyLabel;
            public static Game2DWaterKitPropertyLabel SimulationModeTimeStepPropertyLabel;
            public static Game2DWaterKitPropertyLabel SimulationModeOnCollisionRipplesRegionPropertyLabel;
            public static Game2DWaterKitPropertyLabel SimulationModeScriptGeneratedRipplesSourcePositionropertyLabel;
            public static Game2DWaterKitPropertyLabel SimulationModeTypeOfRippleToPreviewPropertyLabel;
#endregion

#region Messages
            public static readonly string CantMultiEditBecauseUsingDifferentMaterialMessage = "Can't multi-edit these properties when the objects share different materials!";
            public static readonly string DisabledReflectionMessage = "Reflection properties are disabled. \"Reflection\" can be activated in the material inspector.";
            public static readonly string DisabledRefractionMessage = "Refraction properties are disabled. \"Refraction\" can be activated in the material inspector.";
            #endregion

            #region Sceneview Handles Colors
            public static Color BuoyancySurfaceLevelPreviewColor;
            public static Color WaterSubdivisionsPreviewColor;
            public static Color ConstantRipplesSourcesColorAdd;
            public static Color ConstantRipplesSourcesColorRemove;
            public static Color CustomBoundariesPreviewColor;
            public static Color OnCollisionRipplesSimulationRegionBoundariesColor;
            public static Color ScriptGeneratedRipplesSimulationRegionBoundariesColor;
#endregion

#region Misc
            public static float MinimumLabelWidth = 150f;
            public static readonly string[] RipplesTypes = new[] { "On Collision Ripples", "Constant Ripples", "Script-Genrated Ripples" };
            public static readonly string NewPrefabWorkflowMessage = "As of Unity 2018.3, disconnecting (unlinking) and relinking a Prefab instance are no longer supported. Alternatively, you can now unpack a Prefab instance if you want to entirely remove its link to its Prefab asset and thus be able to restructure the resulting plain GameObject as you please.";
            public static readonly string SimulationModuleWavePropertiesMessage = "These properties are shared between all ripples types";
            private static readonly GUIContent _tempLabel = new GUIContent();
#endregion

            public static void Initialize()
            {
                UpdateHandlesColors();

#region GUI Styles
                HelpBoxStyle = new GUIStyle(EditorStyles.helpBox);
                GroupBoxStyle = new GUIStyle("GroupBox");
                ButtonStyle = new GUIStyle("button");
#endregion

#region Icon Buttons Labels
                BoldFoldoutStyle = new GUIStyle(EditorStyles.foldout);
                BoldFoldoutStyle.fontStyle = FontStyle.Bold;

                string prefix = EditorGUIUtility.isProSkin ? "d_" : string.Empty;

                PreviewIconOffButtonLabel = GetButtonIconContent(prefix + "btn_preview_off", "Preview On");
                PreviewIconOnButtonLabel = GetButtonIconContent(prefix + "btn_preview_on", "Preview Off");

                EditSizeIconOffButtonLabel = GetButtonIconContent(prefix + "btn_resize_off", "Edit Size On");
                EditSizeIconOnButtonLabel = GetButtonIconContent(prefix + "btn_resize_on", "Edit Size Off");

                PreviewSimulationOffButtonLabel = GetButtonIconContent(prefix + "btn_start_off", "Enter Simulation Mode", "Enter Simulation Mode");
                PreviewSimulationOnButtonLabel = GetButtonIconContent(prefix + "btn_stop_on", "Quit Simulation Mode", "Quit Simulation Mode");
                RestartSimulationButtonLabel = GetButtonIconContent(prefix + "btn_restart_off", "Restart Simulation", "Restart Simulation");
                StopSimulationButtonLabel = GetButtonIconContent(prefix + "btn_stop_off", "Stop Simulation", "Stop Simulation");
                RunSimulationButtonLabel = GetButtonIconContent(prefix + "btn_start_off", "Run Simulation");
#endregion

#region Properties Labels
                SizePropertyLabel = CreatePropertyLabel("Size", "Sets the object width/height");
                SubdivisionsPerUnitPropertyLabel = CreatePropertyLabel("Subdivisions Per Unit", "Sets the number of water’s surface vertices within one unit.");
                UseEdgeColliderPropertyLabel = CreatePropertyLabel("Use Edge Collider", "Adds/Removes an EdgeCollider2D component that limits the water boundaries (left, right and bottom edges). The water script takes care of updating the edge collider points.");

                WaveStiffnessPropertyLabel = CreatePropertyLabel("Stiffness", "Controls the frequency of wave vibration. A low value will make waves oscillate slowly, while a high value will make waves oscillate quickly.");
                WaveDampingPropertyLabel = CreatePropertyLabel("Damping", "Controls how fast the waves decay. A low value will make waves oscillate for a long time, while a high value will make waves oscillate for a short time.");
                WaveSpreadPropertyLabel = CreatePropertyLabel("Spread", "Controls how fast the waves spread.");
                WaveUseCustomBoundariesPropertyLabel = CreatePropertyLabel("Use Custom Boundaries", "Enable/Disable using custom wave boundaries. When waves reach a boundary, they bounce back.");
                WaveFirstCustomBoundaryPropertyLabel = CreatePropertyLabel("First Boundary", "The location of the first boundary.");
                WaveSecondCustomBoundaryPropertyLabel = CreatePropertyLabel("Second Boundary", "The location of the second boundary.");
                BuoyancyEffectorSurfaceLevelPropertyLabel = CreatePropertyLabel("Surface Level", "Sets the surface location of the buoyancy fluid. When an object is above this line, no buoyancy forces are applied. When an object is intersecting or completely below this line, buoyancy forces are applied.");

                OnCollisionRipplesRaycastMaskPropertyLabel = CreatePropertyLabel("Collision Mask", "Only objects on these layers will disturb the water’s surface and will trigger the OnWaterEnter and the OnWaterExit events when they get into or out of the water.");
                OnCollisionRipplesRaycastMinimumDepthPropertyLabel = CreatePropertyLabel("Minimum Depth", "Only objects with Z coordinate (depth) greater than or equal to this value will disturb the water’s surface.");
                OnCollisionRipplesRaycastMaximumDepthPropertyLabel = CreatePropertyLabel("Maximum Depth", "Only objects with Z coordinate (depth) less than or equal to this value will disturb the water’s surface.");
                OnCollisionRipplesRaycastMaximumDistancePropertyLabel = CreatePropertyLabel("Maximum Distance", "The maximum distance from the water's surface over which to check for collisions (Default: 0.5)");
                OnCollisionRipplesMinimumVelocityPropertyLabel = CreatePropertyLabel("Minimum Velocity", "Sets the minimum velocity a rigidbody falling into water should have to cause the maximum disturbance to the water's surface");
                OnCollisionRipplesOnWaterEnterEventPropertyLabel = CreatePropertyLabel("On Water Enter", "Event that is triggered when a rigidbody falls into water.");
                OnCollisionRipplesOnWaterExitEventPropertyLabel = CreatePropertyLabel("On Water Exit", "Event that is triggered when a rigidbody gets out of the water.");

                ScriptGeneratedRipplesDisturbanceFactorPropertyLabel = CreatePropertyLabel("Disturbance Factor", "Range: [0..1]: The disturbance is linearly interpolated between the minimum disturbance and the maximum disturbance by this factor.");

                TimeIntervalPropertyLabel = CreatePropertyLabel("Time Interval", "Generate ripples at regular time interval (expressed in seconds).");
                RandomizeTimeIntervalPropertyLabel = CreatePropertyLabel("Randomize Time Inteval", "Randomize the time interval.");
                MinimumTimeIntervalPropertyLabel = CreatePropertyLabel("Minimum Time Interval", "The minimum time interval.");
                MaximumTimeIntervalPropertyLabel = CreatePropertyLabel("Maximum Time Interval", "The maximum time interval");
                DisturbancePropertyLabel = CreatePropertyLabel("Disturbance", "Sets the displacement of the water’s surface.");
                MinimumDisturbancePropertyLabel = CreatePropertyLabel("Minimum Disturbance", "Sets the minimum displacement of the water’s surface.");
                MaximumDisturbancePropertyLabel = CreatePropertyLabel("Maximum Disturbance", "Sets the maximum displacement of the water’s surface.");
                SmoothRipplesPropertyLabel = CreatePropertyLabel("Smooth Ripples", "Disturb neighbor surface vertices to create a smoother ripple.");
                SmoothingFactorPropertyLabel = CreatePropertyLabel("Smoothing Factor", "The relative amount of disturbance to apply to neighbor surface vertices.");

                SoundEffectAudioClipPropertyLabel = CreatePropertyLabel("Audio Clip", "The AudioClip asset to play.");
                SoundEffectPoolSizePropertyLabel = CreatePropertyLabel("Pool Size", "Sets the number of audio source objects that will be created and pooled when the game starts.");
                SoundEffectPoolCanExpandPropertyLabel = CreatePropertyLabel("Can Expand", "Enables/Disables increasing the number of pooled audio source objects at runtime if needed.");
                SoundEffectUseConstantPitchPropertyLabel = CreatePropertyLabel("Constant Pitch", "Apply constant audio clip playback speed.");
                SoundEffectPitchPropertyLabel = CreatePropertyLabel("Pitch", "Apply constant audio clip playback speed.");
                SoundEffectMinimumPitchPropertyLabel = CreatePropertyLabel("Minimum Pitch", "Sets the audio clip’s minimum playback speed. (when ‘Constant Pitch’ is toggled off)");
                SoundEffectMaximumPitchPropertyLabel = CreatePropertyLabel("Maximum Pitch", "Sets the audio clip’s maximum playback speed. (when constant pitch is toggled off)");
                SoundEffectVolumePropertyLabel = CreatePropertyLabel("Volume", "Sets the audio clip’s volume.");

                ParticleEffectParticleSystemPropertyLabel = CreatePropertyLabel("Particle System", "Sets the particle effect system to play.");
                ParticleEffectPoolSizePropertyLabel = CreatePropertyLabel("Pool Size", "Sets the number of particle system objects that will be created and pooled when the game starts.");
                ParticleEffectPoolCanExpandPropertyLabel = CreatePropertyLabel("Can Expand", "Enables/Disables increasing the number of pooled particle system objects at runtime if needed.");
                ParticleEffectSpawnOffsetPropertyLabel = CreatePropertyLabel("Spawn Offset", "Shifts the particle system spawn position.");
                ParticleEffectStopActionPropertyLabel = CreatePropertyLabel("Stop Action", "This UnityEvent is triggered when the particle system finishes playing.");

                ConstantRipplesUpdateWhenOffscreenPropertyLabel = CreatePropertyLabel("Continue creating ripples when off-screen", "Continue creating ripples even when the water object is not visible to any camera in the scene.");
                ConstantRipplesRandomizeDisturbancePropertyLabel = CreatePropertyLabel("Randomize Disturbance", "Randomize the disturbance (displacement) of the water's surface.");
                ConstantRipplesSourcesPositionsPropertyLabel = CreatePropertyLabel("Ripples Sources Positions (X-Axis)", "Ripples sources positions list.");
                ConstantRipplesRandomizeSourcesPositionsPropertyLabel = CreatePropertyLabel("Randomize Positions", "Randomize constant ripples sources positions. When checked, random surface vertices are disturbed each time the constant ripples are generated.");
                ConstantRipplesRandomSourceCountPropertyLabel = CreatePropertyLabel("Ripples Source Count", "When Randomize Positions is checked, this sets the number of random surface vertices to disturb when generating constant ripples.");
                ConstantRipplesAllowDuplicateSourcesPositionsPropertyLabel = CreatePropertyLabel("Allow Duplicate Positions", "Allow generating multiple ripples in the same position and at the same time.");
                ConstantRipplesEditSourcesPositionsButtonLabel = CreatePropertyLabel("Edit Positions", "Add/Remove ripples sources positions in the sceneview.");

                RefractionReflectionMaskPropertyLabel = CreatePropertyLabel("Objects To Render", "Only objects on these layers will be rendered by the water camera.");
                RefractionReflectionPartiallySubmergedObjectsMaskPropertyLabel = CreatePropertyLabel("Partially Submerged Objects", "Objects on these layers will be rendered as partially submerged into water when they intersect the submerge level.");
                ReflectionPartiallySubmergedObjectsViewingFrustumHeightScalingFactorPropertyLabel = CreatePropertyLabel("Partially Submerged Objects", "Sets how much to scale the partially submerged objects reflection camera viewing frustum height. The default viewing frustum height is equal to the distance between the surface level and the submerge level.");
                ReflectionOtherObjectsViewingFrustumHeightScalingFactorPropertyLabel = CreatePropertyLabel("Other Objects", "Sets how much to scale the reflection camera viewing frustum height when rendering other objects (all objects specified in ‘Objects to render’ layers except those specified in ‘Partially Submerged Objects’ layers). The default viewing frustum height for the reflection camera is equal to the surface thickness.");
                ReflectionViewingFrustumHeightScalingFactorPropertyLabel = CreatePropertyLabel("Height Scaling Factor", "Sets how much to scale the reflection camera viewing frustum height.");
                ReflectionZOffsetPropertyLabel = CreatePropertyLabel("Z-Offset", "Controls where to start rendering the reflection relative to the water object z-position.");

                RenderTextureFixedSizePropertyLabel = CreatePropertyLabel("Size", "Sets the render texture size.");
                RenderTextureResizingFactorPropertyLabel = CreatePropertyLabel("Resizing Factor", "Specifies how much the RenderTexture is resized. The \"normal\" (before resizing) RenderTexture size is equal to the water visible area size");
                RenderTextureUseFixedSizePropertyLabel = CreatePropertyLabel("Use Fixed Size", "Use a fixed render texture size.");
                RenderTextureFilterModePropertyLabel = CreatePropertyLabel("Filter Mode", "Sets the render texture filter mode.");
                MaterialPropertyLabel = CreatePropertyLabel("Material", null);
                FarClipPlanePropertyLabel = CreatePropertyLabel("Far Clip Plane", "Sets the furthest point relative to the water that will be included in the refraction/reflection rendering.");
                AllowMSAAPropertyLabel = CreatePropertyLabel("Allow MSAA", "Allow multi-sample anti-aliasing rendering.");
                AllowHDRPropertyLabel = CreatePropertyLabel("Allow HDR", "Allow high dynamic range rendering.");
                RenderPixelLightsPropertyLabel = CreatePropertyLabel("Render Pixel Lights", "Controls whether or not the rendered objects will be affected by pixel lights. Disabling this property could increase performance at the expense of visual fidelity.");
                SortingLayerPropertyLabel = CreatePropertyLabel("Sorting Layer", "The name of the mesh renderer sorting layer.");
                SortingOrderInLayerPropertyLabel = CreatePropertyLabel("Order In Layer", "The order within the sorting layer.");

                WaterfallAffectedWaterObjectPropertyLabel = CreatePropertyLabel("Water Object", null);
                WaterfallAffectedWaterObjectRippleSpreadPropertyLabel = CreatePropertyLabel("Spread",null);
                WaterfallAffectedWaterObjectRippleUpdateWhenOffscreenPropertyLabel = CreatePropertyLabel("Continue creating ripples when off-screen", "Continue creating ripples even when the waterfall object is not visible to any camera in the scene.");

                SimulationModeTargetFrameratePropertyLabel = CreatePropertyLabel("Target Framerate","Sets the target number of simulation iterations per second.");
                SimulationModeTimeStepPropertyLabel = CreatePropertyLabel("Timestep", "The interval in seconds at which the simulation updates.");
                SimulationModeOnCollisionRipplesRegionPropertyLabel = CreatePropertyLabel("Region", "Specifies the region where to simulate collision forces. You would change the region size to approximetely match the size of the rigidbody you want to simulate.");
                SimulationModeScriptGeneratedRipplesSourcePositionropertyLabel = CreatePropertyLabel("Position", "Sets the position where to create the ripple.");
                SimulationModeTypeOfRippleToPreviewPropertyLabel = CreatePropertyLabel("Type of ripple to preview.", null);
#endregion

                IsInitialized = true;
            }

            private static GUIContent GetButtonIconContent(string iconName, string tooltip, string text = null)
            {
                return new GUIContent
                {
                    image = (Texture) Resources.Load("Images/" + iconName),
                    text = text,
                    tooltip = tooltip
                };
            }

            public static void UpdateHandlesColors()
            {
                BuoyancySurfaceLevelPreviewColor = GetColorFromEditorPrefs("BuoyancySurfaceLevelPreviewColor", Color.cyan);
                WaterSubdivisionsPreviewColor = GetColorFromEditorPrefs("WaterSubdivisionsPreviewColor", new Color(0.89f, 0.259f, 0.204f));
                ConstantRipplesSourcesColorAdd = GetColorFromEditorPrefs("ConstantRipplesSourcesColorAdd", Color.green);
                ConstantRipplesSourcesColorRemove = GetColorFromEditorPrefs("ConstantRipplesSourcesColorRemove", Color.red);
                CustomBoundariesPreviewColor = GetColorFromEditorPrefs("CustomBoundariesPreviewColor", Color.yellow);
                OnCollisionRipplesSimulationRegionBoundariesColor = GetColorFromEditorPrefs("OnCollisionRipplesSimulationRegionBoundariesColor", Color.cyan);
                ScriptGeneratedRipplesSimulationRegionBoundariesColor = GetColorFromEditorPrefs("ScriptGeneratedRipplesSimulationRegionBoundariesColor", Color.green);
            }

            public static Color GetColorFromEditorPrefs(string key, Color defaultColor)
            {
                return JsonUtility.FromJson<Color>(EditorPrefs.GetString(key, JsonUtility.ToJson(defaultColor)));
            }

            public static void SetColorInEditorPrefs(string key, Color color)
            {
                EditorPrefs.SetString(key, JsonUtility.ToJson(color));
            }

            public static Game2DWaterKitPropertyLabel CreatePropertyLabel(string text, string tooltip)
            {
                const float labelRightMargin = 4f;

                var labelContent = new Game2DWaterKitPropertyLabel();

                labelContent.Content = new GUIContent(text, tooltip);
                labelContent.WidthRegular = EditorStyles.label.CalcSize(labelContent.Content).x + labelRightMargin;
                labelContent.WidthBold = EditorStyles.boldLabel.CalcSize(labelContent.Content).x + labelRightMargin;

                return labelContent;
            }

            public static GUIContent GetTempLabel(string text)
            {
                _tempLabel.text = text;
                return _tempLabel;
            }

            public class Game2DWaterKitPropertyLabel
            {
                public GUIContent Content;
                public float WidthRegular;
                public float WidthBold;
            }
        }
    }
}
