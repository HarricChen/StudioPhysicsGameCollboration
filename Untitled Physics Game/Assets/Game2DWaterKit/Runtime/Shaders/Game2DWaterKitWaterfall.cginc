#ifndef Game2D_WaterKit_Waterfall_INCLUDED
#define Game2D_WaterKit_Waterfall_INCLUDED

	#define Waterfall2D_HasBodyTexture defined(Waterfall2D_BodyTexture) || defined(Waterfall2D_BodyTextureSheet) || defined(Waterfall2D_BodyTextureSheetWithLerp)
	#define Waterfall2D_HasBodySecondTexture defined(Waterfall2D_BodySecondTexture) || defined(Waterfall2D_BodySecondTextureSheet) || defined(Waterfall2D_BodySecondTextureSheetWithLerp)
	#define Waterfall2D_HasTopEdge defined(Waterfall2D_TopEdge) || defined(Waterfall2D_TopEdgeTextureSheet) || defined(Waterfall2D_TopEdgeTextureSheetWithLerp) || defined(Waterfall2D_TopBottomEdgesSameTexture) || defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
	#define Waterfall2D_HasBottomEdge defined(Waterfall2D_BottomEdge) || defined(Waterfall2D_BottomEdgeTextureSheet) || defined(Waterfall2D_BottomEdgeTextureSheetWithLerp) || defined(Waterfall2D_TopBottomEdgesSameTexture) || defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
	#define Waterfall2D_HasLeftEdge defined(Waterfall2D_LeftEdge) || defined(Waterfall2D_LeftEdgeTextureSheet) || defined(Waterfall2D_LeftEdgeTextureSheetWithLerp) || defined(Waterfall2D_LeftRightEdgesSameTexture) || defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
	#define Waterfall2D_HasRightEdge defined(Waterfall2D_RightEdge) || defined(Waterfall2D_RightEdgeTextureSheet) || defined(Waterfall2D_RightEdgeTextureSheetWithLerp) || defined(Waterfall2D_LeftRightEdgesSameTexture) || defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)

	CBUFFER_START(UnityPerObject)
		#if defined(Waterfall2D_Refraction)
		uniform float4x4 _WaterfallMVP;
		uniform sampler2D _RefractionTexture;
		#endif
		uniform float4 _AspectRatio;
	CBUFFER_END  // UnityPerObject

	CBUFFER_START(UnityPerMaterial)

	// Refraction Variables
	#if defined(Waterfall2D_Refraction)
		half _RefractionNoiseStrength;
		half _RefractionNoiseSpeed;
		half4 _RefractionNoiseTiling;
	#endif

	// Body Color Variables
	#if defined(Waterfall2D_BodyColorGradient)
		half4 _BodyColorGradientStart;
		half4 _BodyColorGradientEnd;
		half _BodyColorGradientOffset;
	#else
		half4 _BodyColor;
	#endif

	// Body Texture Variables
	#if defined(Waterfall2D_BodyTexture) || defined(Waterfall2D_BodyTextureSheet) || defined(Waterfall2D_BodyTextureSheetWithLerp)
		sampler2D _BodyTexture;
		float4 _BodyTexture_ST;
		half _BodyTextureOpacity;
		half _BodyTextureScrollingSpeed;

		#if defined(Waterfall2D_BodyTextureSheet) || defined(Waterfall2D_BodyTextureSheetWithLerp)
			half _BodyTextureSheetInverseRows;
			half _BodyTextureSheetInverseColumns;
			half _BodyTextureSheetFramesPerSecond;
			half _BodyTextureSheetFramesCount;
		#endif
	#endif
		
	// Body Second Texture Variables
	#if defined(Waterfall2D_BodySecondTexture) || defined(Waterfall2D_BodySecondTextureSheet) || defined(Waterfall2D_BodySecondTextureSheetWithLerp)
		sampler2D _BodySecondTexture;
		float4 _BodySecondTexture_ST;
		half _BodySecondTextureOpacity;
		half _BodySecondTextureScrollingSpeed;

		#if defined(Waterfall2D_BodySecondTextureSheet) || defined(Waterfall2D_BodySecondTextureSheetWithLerp)
			half _BodySecondTextureSheetInverseRows;
			half _BodySecondTextureSheetInverseColumns;
			half _BodySecondTextureSheetFramesPerSecond;
			half _BodySecondTextureSheetFramesCount;
		#endif
	#endif

	#if defined(Waterfall2D_BodyTextureNoise) && (Waterfall2D_HasBodyTexture || Waterfall2D_HasBodySecondTexture)
		half _BodyNoiseSpeed;
		half _BodyNoiseStrength;
		half4 _BodyNoiseTiling;
	#endif

	// Top-Bottom Edges Variables
	#if Waterfall2D_HasTopEdge || Waterfall2D_HasBottomEdge
		half4 _TopBottomEdgesThickness;
	#endif

	#if defined(Waterfall2D_TopBottomEdgesSameTexture) || defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
		sampler2D _TopBottomEdgesTexture;
		float4 _TopBottomEdgesTexture_ST;
		half _TopBottomEdgesTextureOpacity;
		
		#if defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
			half _TopBottomEdgesTextureSheetInverseRows;
			half _TopBottomEdgesTextureSheetInverseColumns;
			half _TopBottomEdgesTextureSheetFramesPerSecond;
			half _TopBottomEdgesTextureSheetFramesCount;
		#endif
	#else
		#if defined(Waterfall2D_TopEdge) || defined(Waterfall2D_TopEdgeTextureSheet) || defined(Waterfall2D_TopEdgeTextureSheetWithLerp)
			sampler2D _TopEdgeTexture;
			float4 _TopEdgeTexture_ST;
			half _TopEdgeTextureOpacity;

			#if defined(Waterfall2D_TopEdgeTextureSheet) || defined(Waterfall2D_TopEdgeTextureSheetWithLerp)
				half _TopEdgeTextureSheetInverseRows;
				half _TopEdgeTextureSheetInverseColumns;
				half _TopEdgeTextureSheetFramesPerSecond;
				half _TopEdgeTextureSheetFramesCount;
			#endif
		#endif

		#if defined(Waterfall2D_BottomEdge) || defined(Waterfall2D_BottomEdgeTextureSheet) || defined(Waterfall2D_BottomEdgeTextureSheetWithLerp)
			sampler2D _BottomEdgeTexture;
			float4 _BottomEdgeTexture_ST;
			half _BottomEdgeTextureOpacity;

			#if defined(Waterfall2D_BottomEdgeTextureSheet) || defined(Waterfall2D_BottomEdgeTextureSheetWithLerp)
				half _BottomEdgeTextureSheetInverseRows;
				half _BottomEdgeTextureSheetInverseColumns;
				half _BottomEdgeTextureSheetFramesPerSecond;
				half _BottomEdgeTextureSheetFramesCount;
			#endif
		#endif
	#endif

	// Top-Bottom Edges Distortion Effect Variables
	#if defined(Waterfall2D_TopBottomEdgesNoise)
		half _TopBottomEdgesNoiseSpeed;
		half _TopBottomEdgesNoiseStrength;
		half4 _TopBottomEdgesNoiseTiling;
	#endif

	// Left-Right Edges Variables
	#if Waterfall2D_HasLeftEdge || Waterfall2D_HasRightEdge
		half4 _LeftRightEdgesThickness;
		#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff)
		half _LeftRightEdgesTextureAlphaCutoff;
		#endif
	#endif

	#if defined(Waterfall2D_LeftRightEdgesSameTexture) || defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
		sampler2D _LeftRightEdgesTexture;
		float4 _LeftRightEdgesTexture_ST;
		half _LeftRightEdgesTextureOpacity;
		half _LeftRightEdgesTextureScrollingSpeed;
		
		#if defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
			half _LeftRightEdgesTextureSheetInverseRows;
			half _LeftRightEdgesTextureSheetInverseColumns;
			half _LeftRightEdgesTextureSheetFramesPerSecond;
			half _LeftRightEdgesTextureSheetFramesCount;
		#endif
	#else
		#if defined(Waterfall2D_LeftEdge) || defined(Waterfall2D_LeftEdgeTextureSheet) || defined(Waterfall2D_LeftEdgeTextureSheetWithLerp)
			sampler2D _LeftEdgeTexture;
			float4 _LeftEdgeTexture_ST;
			half _LeftEdgeTextureOpacity;
			half _LeftEdgeTextureScrollingSpeed;

			#if defined(Waterfall2D_LeftEdgeTextureSheet) || defined(Waterfall2D_LeftEdgeTextureSheetWithLerp)
				half _LeftEdgeTextureSheetInverseRows;
				half _LeftEdgeTextureSheetInverseColumns;
				half _LeftEdgeTextureSheetFramesPerSecond;
				half _LeftEdgeTextureSheetFramesCount;
			#endif
		#endif

		#if defined(Waterfall2D_RightEdge) || defined(Waterfall2D_RightEdgeTextureSheet) || defined(Waterfall2D_RightEdgeTextureSheetWithLerp)
			sampler2D _RightEdgeTexture;
			float4 _RightEdgeTexture_ST;
			half _RightEdgeTextureOpacity;
			half _RightEdgeTextureScrollingSpeed;

			#if defined(Waterfall2D_RightEdgeTextureSheet) || defined(Waterfall2D_RightEdgeTextureSheetWithLerp)
				half _RightEdgeTextureSheetInverseRows;
				half _RightEdgeTextureSheetInverseColumns;
				half _RightEdgeTextureSheetFramesPerSecond;
				half _RightEdgeTextureSheetFramesCount;
			#endif
		#endif
	#endif

	// Left-Right Edges Distortion Effect Variables
	#if defined(Waterfall2D_LeftRightEdgesNoise)
		half _LeftRightEdgesNoiseSpeed;
		half _LeftRightEdgesNoiseStrength;
		half4 _LeftRightEdgesNoiseTiling;
	#endif

	// Emission Variables
	#if defined(Waterfall2D_ApplyEmissionColor)
		half4 _EmissionColor;
		half _EmissionColorIntensity;
	#endif
		
	// Noise Texture
	#if defined(Waterfall2D_Refraction) || defined(Waterfall2D_BodyTextureNoise) || defined(Waterfall2D_TopBottomEdgesNoise) || defined(Waterfall2D_LeftRightEdgesNoise)
		sampler2D _NoiseTexture;
		float4 _NoiseTexture_ST;
	#endif

	CBUFFER_END // UnityPerMaterial
		
	struct Attributes
	{
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;

		#if  defined(LIGHTMAP_ON)
			float2 lightmapCoord : TEXCOORD1;
		#endif
	};

	struct Varyings
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;

		#if Waterfall2D_HasBodyTexture || Waterfall2D_HasBodySecondTexture
			#if Waterfall2D_HasBodySecondTexture
				float4 bodyTextureUV : TEXCOORD1;
			#else
				float2 bodyTextureUV : TEXCOORD1;
			#endif

			#if defined(Waterfall2D_BodyTextureNoise)
				#if Waterfall2D_HasBodySecondTexture
				float4 bodyNoiseTextureUV : TEXCOORD2;
				#else
				float2 bodyNoiseTextureUV : TEXCOORD2;
				#endif
			#endif
		#endif

		#if defined(Waterfall2D_Refraction)
			float4 refractionUV : TEXCOORD3;
		#endif

		#if Waterfall2D_HasLeftEdge || Waterfall2D_HasRightEdge
			#if Waterfall2D_HasRightEdge
				float4 leftRightEdgesTextureUV : TEXCOORD4;
			#else
				float2 leftRightEdgesTextureUV : TEXCOORD4;
			#endif
		#endif
		
		#if Waterfall2D_HasTopEdge || Waterfall2D_HasBottomEdge
			#if Waterfall2D_HasBottomEdge
				float4 topBottomEdgesTextureUV : TEXCOORD5;
			#else
				float2 topBottomEdgesTextureUV : TEXCOORD5;
			#endif
		#endif

		#if defined(Waterfall2D_LeftRightEdgesNoise) || defined(Waterfall2D_TopBottomEdgesNoise)
			#if defined(Waterfall2D_TopBottomEdgesNoise)
				float4 edgesNoiseTextureUV : TEXCOORD6;
			#else
				float2 edgesNoiseTextureUV : TEXCOORD6;
			#endif
		#endif
				
			#if defined(Game2DWaterKit_SRP_Lit)
				float2 lightingUV : TEXCOORD7;
			#else
				#if defined(LIGHTMAP_ON)
					float2 lightmapCoord : TEXCOORD7;
				#else
					#if defined(UNITY_SHOULD_SAMPLE_SH)
			 			half3 sh : TEXCOORD7;
					#endif
				#endif

				#if defined(UNITY_PASS_FORWARDBASE) || defined(UNITY_PASS_FORWARDADD)
  					float3 worldPos : TEXCOORD8;
 				#endif
			#endif

		#if defined(Game2DWaterKit_VertexLit_Vertex) || defined(Game2DWaterKit_VertexLit_VertexLM) || defined(Game2DWaterKit_VertexLit_OnlyDirectional)
			half3 lightColor : COLOR0;
		#endif
	};

	#include "Game2DWaterKit.cginc"

	inline Varyings Waterfall2D_Vert (Attributes v)
	{
		Varyings o;
		UNITY_INITIALIZE_OUTPUT(Varyings, o);

		o.pos = ComputeClipPosition(v.pos);
		o.uv = v.uv;

		float2 vertexPositionWorldSpace = (mul(unity_ObjectToWorld, v.pos)).xy;

		// Body Texture UV

		#if Waterfall2D_HasBodyTexture || Waterfall2D_HasBodySecondTexture
			#if Waterfall2D_HasBodyTexture
				#if defined(Waterfall2D_BodyTextureStretch)
					o.bodyTextureUV.xy = v.uv * _BodyTexture_ST.xy + _BodyTexture_ST.zw;
				#elif defined(Waterfall2D_BodyTextureStretchAutoX)
					o.bodyTextureUV.xy = v.uv * (_BodyTexture_ST.xy * _AspectRatio.xy) + _BodyTexture_ST.zw;
				#elif defined(Waterfall2D_BodyTextureStretchAutoY)
					o.bodyTextureUV.xy = v.uv * (_BodyTexture_ST.xy * _AspectRatio.zw) + _BodyTexture_ST.zw;
				#else
					o.bodyTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _BodyTexture);
				#endif

				o.bodyTextureUV.y += fmod2(_G2DWK_Frame_Time.x * _BodyTextureScrollingSpeed);
			#endif

			#if Waterfall2D_HasBodySecondTexture
				#if defined(Waterfall2D_BodySecondTextureStretch)
					o.bodyTextureUV.zw = v.uv * _BodySecondTexture_ST.xy + _BodySecondTexture_ST.zw;
				#elif defined(Waterfall2D_BodySecondTextureStretchAutoX)
					o.bodyTextureUV.zw = v.uv * (_BodySecondTexture_ST.xy * _AspectRatio.xy) + _BodySecondTexture_ST.zw;
				#elif defined(Waterfall2D_BodySecondTextureStretchAutoY)
					o.bodyTextureUV.zw = v.uv * (_BodySecondTexture_ST.xy * _AspectRatio.zw) + _BodySecondTexture_ST.zw;
				#else
					o.bodyTextureUV.zw = TRANSFORM_TEX(vertexPositionWorldSpace, _BodySecondTexture);
				#endif

				o.bodyTextureUV.w += fmod2(_G2DWK_Frame_Time.x * _BodySecondTextureScrollingSpeed);
			#endif

			#if defined(Waterfall2D_BodyTextureNoise)
				#if Waterfall2D_HasBodyTexture
					o.bodyNoiseTextureUV.xy = TRANSFORM_TEX((o.bodyTextureUV.xy * _BodyNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _BodyNoiseSpeed);
				#endif
				#if Waterfall2D_HasBodySecondTexture
					o.bodyNoiseTextureUV.zw = TRANSFORM_TEX((o.bodyTextureUV.zw * _BodyNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _BodyNoiseSpeed);
				#endif
			#endif
		#endif

		// Left-Right Edges Texture UV

		#if defined(Waterfall2D_LeftRightEdgesSameTexture) || defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
			#if defined(Waterfall2D_LeftRightEdgesTextureStretch) || defined(Waterfall2D_LeftRightEdgesTextureStretchAutoX) || defined(Waterfall2D_LeftRightEdgesTextureStretchAutoY)
				#if defined(Waterfall2D_LeftRightEdgesTextureStretchAutoX)
					float lrx = _LeftRightEdgesTexture_ST.x * _AspectRatio.x;
					o.leftRightEdgesTextureUV.x = v.uv.x * lrx + _LeftRightEdgesTexture_ST.z;
					o.leftRightEdgesTextureUV.y = (v.uv.y * _LeftRightEdgesTexture_ST.y + _LeftRightEdgesTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftRightEdgesTextureScrollingSpeed);
					o.leftRightEdgesTextureUV.z = (v.uv.x - 1.0 + _LeftRightEdgesThickness.z) * lrx + _LeftRightEdgesTexture_ST.z;
					o.leftRightEdgesTextureUV.w = o.leftRightEdgesTextureUV.y;
				#elif defined(Waterfall2D_LeftRightEdgesTextureStretchAutoY)
					float lry = v.uv.y * _LeftRightEdgesTexture_ST.y * _AspectRatio.w;
					o.leftRightEdgesTextureUV.x = v.uv.x * (_LeftRightEdgesTexture_ST.x * _LeftRightEdgesThickness.y) + _LeftRightEdgesTexture_ST.z;
					o.leftRightEdgesTextureUV.y = (lry * _LeftRightEdgesThickness.y + _LeftRightEdgesTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftRightEdgesTextureScrollingSpeed);
					o.leftRightEdgesTextureUV.z = ((v.uv.x - 1.0 + _LeftRightEdgesThickness.z) * _LeftRightEdgesThickness.w) * _LeftRightEdgesTexture_ST.x + _LeftRightEdgesTexture_ST.z;
					o.leftRightEdgesTextureUV.w = (lry * _LeftRightEdgesThickness.w + _LeftRightEdgesTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftRightEdgesTextureScrollingSpeed);
				#else
					o.leftRightEdgesTextureUV.x = v.uv.x * (_LeftRightEdgesThickness.y * _LeftRightEdgesTexture_ST.x) + _LeftRightEdgesTexture_ST.z;
					o.leftRightEdgesTextureUV.y = (v.uv.y * _LeftRightEdgesTexture_ST.y + _LeftRightEdgesTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftRightEdgesTextureScrollingSpeed);
					o.leftRightEdgesTextureUV.z = ((v.uv.x - 1.0 + _LeftRightEdgesThickness.z) * _LeftRightEdgesThickness.w) * _LeftRightEdgesTexture_ST.x + _LeftRightEdgesTexture_ST.z;
					o.leftRightEdgesTextureUV.w = o.leftRightEdgesTextureUV.y;
				#endif
			#else
				o.leftRightEdgesTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _LeftRightEdgesTexture);
				o.leftRightEdgesTextureUV.zw = o.leftRightEdgesTextureUV.xy;
				o.leftRightEdgesTextureUV.yw += fmod2(_G2DWK_Frame_Time.x * _LeftRightEdgesTextureScrollingSpeed);
			#endif

			#if defined(Waterfall2D_LeftRightEdgesFlipLeftEdgeX)
				o.leftRightEdgesTextureUV.x = 1.0 - o.leftRightEdgesTextureUV.x;
			#elif defined(Waterfall2D_LeftRightEdgesFlipRightEdgeX)
				o.leftRightEdgesTextureUV.z = 1.0 - o.leftRightEdgesTextureUV.z;
			#endif
		#else
			//Left Texture UV
			#if defined(Waterfall2D_LeftEdge) || defined(Waterfall2D_LeftEdgeTextureSheet) || defined(Waterfall2D_LeftEdgeTextureSheetWithLerp)
				#if defined(Waterfall2D_LeftEdgeTextureStretch) || defined(Waterfall2D_LeftEdgeTextureStretchAutoX) || defined(Waterfall2D_LeftEdgeTextureStretchAutoY)
					#if defined(Waterfall2D_LeftEdgeTextureStretchAutoX)
						o.leftRightEdgesTextureUV.x = v.uv.x * (_LeftEdgeTexture_ST.x * _AspectRatio.x) + _LeftEdgeTexture_ST.z;
						o.leftRightEdgesTextureUV.y = (v.uv.y * _LeftEdgeTexture_ST.y + _LeftEdgeTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftEdgeTextureScrollingSpeed);
					#elif defined(Waterfall2D_LeftEdgeTextureStretchAutoY)
						o.leftRightEdgesTextureUV.x = (v.uv.x * _LeftRightEdgesThickness.y) * _LeftEdgeTexture_ST.x + _LeftEdgeTexture_ST.z;
						o.leftRightEdgesTextureUV.y = (v.uv.y * _LeftEdgeTexture_ST.y * _AspectRatio.w * _LeftRightEdgesThickness.y + _LeftEdgeTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftEdgeTextureScrollingSpeed);
					#else
						o.leftRightEdgesTextureUV.x = (v.uv.x * _LeftRightEdgesThickness.y) * _LeftEdgeTexture_ST.x + _LeftEdgeTexture_ST.z;
						o.leftRightEdgesTextureUV.y = (v.uv.y * _LeftEdgeTexture_ST.y + _LeftEdgeTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _LeftEdgeTextureScrollingSpeed);
					#endif
				#else
					o.leftRightEdgesTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _LeftEdgeTexture);
					o.leftRightEdgesTextureUV.y += fmod2(_G2DWK_Frame_Time.x * _LeftEdgeTextureScrollingSpeed);
				#endif
			#endif

			//Right Texture UV
			#if defined(Waterfall2D_RightEdge) || defined(Waterfall2D_RightEdgeTextureSheet) || defined(Waterfall2D_RightEdgeTextureSheetWithLerp)
				#if defined(Waterfall2D_RightEdgeTextureStretch) || defined(Waterfall2D_RightEdgeTextureStretchAutoX) || defined(Waterfall2D_RightEdgeTextureStretchAutoY)
					#if defined(Waterfall2D_RightEdgeTextureStretchAutoX)
						o.leftRightEdgesTextureUV.z = ((v.uv.x - 1.0 + _LeftRightEdgesThickness.z)) * (_RightEdgeTexture_ST.x * _AspectRatio.x) + _RightEdgeTexture_ST.z;
						o.leftRightEdgesTextureUV.w = (v.uv.y * _RightEdgeTexture_ST.y + _RightEdgeTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _RightEdgeTextureScrollingSpeed);
					#elif defined(Waterfall2D_RightEdgeTextureStretchAutoY)
						o.leftRightEdgesTextureUV.z = ((v.uv.x - 1.0 + _LeftRightEdgesThickness.z) * _LeftRightEdgesThickness.w) * _RightEdgeTexture_ST.x + _RightEdgeTexture_ST.z;
						o.leftRightEdgesTextureUV.w = (v.uv.y * _RightEdgeTexture_ST.y * _AspectRatio.w * _LeftRightEdgesThickness.w + _RightEdgeTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _RightEdgeTextureScrollingSpeed);
					#else
						o.leftRightEdgesTextureUV.z = ((v.uv.x - 1.0 + _LeftRightEdgesThickness.z) * _LeftRightEdgesThickness.w) * _RightEdgeTexture_ST.x + _RightEdgeTexture_ST.z;
						o.leftRightEdgesTextureUV.w = (v.uv.y * _RightEdgeTexture_ST.y + _RightEdgeTexture_ST.w) + fmod2(_G2DWK_Frame_Time.x * _RightEdgeTextureScrollingSpeed);
					#endif
				#else
					o.leftRightEdgesTextureUV.zw = TRANSFORM_TEX(vertexPositionWorldSpace, _RightEdgeTexture);
					o.leftRightEdgesTextureUV.w += fmod2(_G2DWK_Frame_Time.x * _RightEdgeTextureScrollingSpeed);
				#endif
			#endif
		#endif

		#if defined(Waterfall2D_LeftRightEdgesNoise)
			#if Waterfall2D_HasLeftEdge
				o.edgesNoiseTextureUV.xy = TRANSFORM_TEX((o.leftRightEdgesTextureUV.xy * _LeftRightEdgesNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _LeftRightEdgesNoiseSpeed);
			#elif Waterfall2D_HasRightEdge
				o.edgesNoiseTextureUV.xy = TRANSFORM_TEX((o.leftRightEdgesTextureUV.zw * _LeftRightEdgesNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _LeftRightEdgesNoiseSpeed);
			#endif
		#endif

		// Top-Bottom Edges Texture UV

		#if defined(Waterfall2D_TopBottomEdgesSameTexture) || defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
			#if defined(Waterfall2D_TopBottomEdgesTextureStretch) || defined(Waterfall2D_TopBottomEdgesTextureStretchAutoX) || defined(Waterfall2D_TopBottomEdgesTextureStretchAutoY)
				#if defined(Waterfall2D_TopBottomEdgesTextureStretchAutoX)
				float tbx = v.uv.x * _TopBottomEdgesTexture_ST.x * _AspectRatio.x;
				o.topBottomEdgesTextureUV.x = tbx * _TopBottomEdgesThickness.y + _TopBottomEdgesTexture_ST.z;
				o.topBottomEdgesTextureUV.y = (v.uv.y - (1.0 - _TopBottomEdgesThickness.x)) * (_TopBottomEdgesTexture_ST.y * _TopBottomEdgesThickness.y) + _TopBottomEdgesTexture_ST.w;
				o.topBottomEdgesTextureUV.z = tbx * _TopBottomEdgesThickness.w + _TopBottomEdgesTexture_ST.z;
				o.topBottomEdgesTextureUV.w = v.uv.y * (_TopBottomEdgesTexture_ST.y * _TopBottomEdgesThickness.w) + _TopBottomEdgesTexture_ST.w;
				#elif defined(Waterfall2D_TopBottomEdgesTextureStretchAutoY)
				float tby = _TopBottomEdgesTexture_ST.y * _AspectRatio.w;
				o.topBottomEdgesTextureUV.x = v.uv.x * _TopBottomEdgesTexture_ST.x + _TopBottomEdgesTexture_ST.z;
				o.topBottomEdgesTextureUV.y = (v.uv.y - (1.0 - _TopBottomEdgesThickness.x)) * tby + _TopBottomEdgesTexture_ST.w;
				o.topBottomEdgesTextureUV.z = o.topBottomEdgesTextureUV.x;
				o.topBottomEdgesTextureUV.w = v.uv.y * tby + _TopBottomEdgesTexture_ST.w;
				#else
				o.topBottomEdgesTextureUV.x = v.uv.x * _TopBottomEdgesTexture_ST.x + _TopBottomEdgesTexture_ST.z;
				o.topBottomEdgesTextureUV.y = (v.uv.y - (1.0 - _TopBottomEdgesThickness.x)) * (_TopBottomEdgesTexture_ST.y * _TopBottomEdgesThickness.y) + _TopBottomEdgesTexture_ST.w;
				o.topBottomEdgesTextureUV.z = o.topBottomEdgesTextureUV.x;
				o.topBottomEdgesTextureUV.w = v.uv.y * (_TopBottomEdgesTexture_ST.y * _TopBottomEdgesThickness.w) + _TopBottomEdgesTexture_ST.w;
				#endif
			#else
				o.topBottomEdgesTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _TopBottomEdgesTexture);
				o.topBottomEdgesTextureUV.zw = o.topBottomEdgesTextureUV.xy;
			#endif

			#if defined(Waterfall2D_TopBottomEdgesFlipTopEdgeY)
				o.topBottomEdgesTextureUV.y = 1.0 - o.topBottomEdgesTextureUV.y;
			#elif defined(Waterfall2D_TopBottomEdgesFlipBottomEdgeY)
				o.topBottomEdgesTextureUV.w = 1.0 - o.topBottomEdgesTextureUV.w;
			#endif
		#else
			//Top Texture UV
			#if defined(Waterfall2D_TopEdge) || defined(Waterfall2D_TopEdgeTextureSheet) || defined(Waterfall2D_TopEdgeTextureSheetWithLerp)
				#if defined(Waterfall2D_TopEdgeTextureStretch) || defined(Waterfall2D_TopEdgeTextureStretchAutoX) || defined(Waterfall2D_TopEdgeTextureStretchAutoY)
					#if defined(Waterfall2D_TopEdgeTextureStretchAutoX)
					o.topBottomEdgesTextureUV.x = v.uv.x * (_TopEdgeTexture_ST.x * _AspectRatio.x * _TopBottomEdgesThickness.y) + _TopEdgeTexture_ST.z;
					o.topBottomEdgesTextureUV.y = (v.uv.y - (1.0 - _TopBottomEdgesThickness.x)) * (_TopEdgeTexture_ST.y * _TopBottomEdgesThickness.y) + _TopEdgeTexture_ST.w;
					#elif defined(Waterfall2D_TopEdgeTextureStretchAutoY)
					o.topBottomEdgesTextureUV.x = v.uv.x * _TopEdgeTexture_ST.x + _TopEdgeTexture_ST.z;
					o.topBottomEdgesTextureUV.y = (v.uv.y - (1.0 - _TopBottomEdgesThickness.x)) * (_TopEdgeTexture_ST.y * _AspectRatio.w) + _TopEdgeTexture_ST.w;
					#else
					o.topBottomEdgesTextureUV.x = v.uv.x * _TopEdgeTexture_ST.x + _TopEdgeTexture_ST.z;
					o.topBottomEdgesTextureUV.y = (v.uv.y - (1.0 - _TopBottomEdgesThickness.x)) * (_TopEdgeTexture_ST.y * _TopBottomEdgesThickness.y) + _TopEdgeTexture_ST.w;
					#endif
				#else
					o.topBottomEdgesTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _TopEdgeTexture);
				#endif
			#endif

			//Bottom Texture UV
			#if defined(Waterfall2D_BottomEdge) || defined(Waterfall2D_BottomEdgeTextureSheet) || defined(Waterfall2D_BottomEdgeTextureSheetWithLerp)
				#if defined(Waterfall2D_BottomEdgeTextureStretch) || defined(Waterfall2D_BottomEdgeTextureStretchAutoX) || defined(Waterfall2D_BottomEdgeTextureStretchAutoY)
					#if defined(Waterfall2D_BottomEdgeTextureStretchAutoX)
						float bEdgeThicknessInv = 1.0 * _TopBottomEdgesThickness.w;
						o.topBottomEdgesTextureUV.z = v.uv.x * (_BottomEdgeTexture_ST.x * _AspectRatio.x * bEdgeThicknessInv);
						o.topBottomEdgesTextureUV.w = v.uv.y * (_BottomEdgeTexture_ST.y * bEdgeThicknessInv);
					#elif defined(Waterfall2D_BottomEdgeTextureStretchAutoY)
						o.topBottomEdgesTextureUV.z = v.uv.x * _BottomEdgeTexture_ST.x;
						o.topBottomEdgesTextureUV.w = v.uv.y * _BottomEdgeTexture_ST.y * _AspectRatio.w;
					#else
						o.topBottomEdgesTextureUV.z = v.uv.x * _BottomEdgeTexture_ST.x;
						o.topBottomEdgesTextureUV.w = v.uv.y * _BottomEdgeTexture_ST.y * _TopBottomEdgesThickness.w;
					#endif
					o.topBottomEdgesTextureUV.zw += _BottomEdgeTexture_ST.zw;
				#else
					o.topBottomEdgesTextureUV.zw = TRANSFORM_TEX(vertexPositionWorldSpace, _BottomEdgeTexture);
				#endif
			#endif
		#endif

		#if defined(Waterfall2D_TopBottomEdgesNoise)
			#if Waterfall2D_HasTopEdge
				o.edgesNoiseTextureUV.zw = TRANSFORM_TEX((o.topBottomEdgesTextureUV.xy * _TopBottomEdgesNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _TopBottomEdgesNoiseSpeed);
			#elif Waterfall2D_HasBottomEdge
				o.edgesNoiseTextureUV.zw = TRANSFORM_TEX((o.topBottomEdgesTextureUV.zw * _TopBottomEdgesNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _TopBottomEdgesNoiseSpeed);
			#endif
		#endif
			
		// Refraction UV

		#if defined(Waterfall2D_Refraction)
			float4 pos = mul(_WaterfallMVP, v.pos);
			o.refractionUV.xy = (pos.xy / pos.w) * 0.5 + 0.5;
			o.refractionUV.zw = TRANSFORM_TEX((vertexPositionWorldSpace * _RefractionNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _RefractionNoiseSpeed);
		#endif

		return o;
	}
			
	inline half4 Waterfall2D_Frag (Varyings i)
	{
		half4 finalColor = 0.0;

		// Applying Refraction
		#if defined(Waterfall2D_Refraction)
			i.refractionUV.xy += (tex2D(_NoiseTexture, i.refractionUV.zw).r - 0.5) * _RefractionNoiseStrength;
			finalColor = tex2D(_RefractionTexture, i.refractionUV.xy);
		#endif

		// Applying Body color
		half4 bodyColor;
		#if defined(Waterfall2D_BodyColorGradient)
			bodyColor = lerp(_BodyColorGradientEnd, _BodyColorGradientStart, i.uv.y + _BodyColorGradientOffset);
		#else
			bodyColor = _BodyColor;
		#endif

		#if defined(Waterfall2D_ApplyTintColorBeforeTexture)
			finalColor = MixColors(finalColor, bodyColor);
		#endif

		// Applying Body Textures

		// Applying Body First Texture
		#if Waterfall2D_HasBodyTexture
			#if defined(Waterfall2D_BodyTextureNoise)
				i.bodyTextureUV.xy += (tex2D(_NoiseTexture, i.bodyNoiseTextureUV.xy).a - 0.5) * _BodyNoiseStrength;
			#endif

			#if defined(Waterfall2D_BodyTextureSheet) || defined(Waterfall2D_BodyTextureSheetWithLerp)
			#if defined(Waterfall2D_BodyTextureSheetWithLerp)
			half4 bodyTextureColor = SampleTextureSheetLerp(_BodyTexture, i.bodyTextureUV.xy);
			#else
			half4 bodyTextureColor = SampleTextureSheet(_BodyTexture, i.bodyTextureUV.xy);
			#endif
			#else
			half4 bodyTextureColor = tex2D(_BodyTexture, i.bodyTextureUV.xy);
			#endif

			bodyTextureColor.a *= _BodyTextureOpacity;
			finalColor = MixColors(finalColor, bodyTextureColor);
		#endif

		// Applying Body Second Texture
		#if Waterfall2D_HasBodySecondTexture
			#if defined(Waterfall2D_BodyTextureNoise)
				i.bodyTextureUV.zw += (tex2D(_NoiseTexture, i.bodyNoiseTextureUV.zw).a - 0.5) * _BodyNoiseStrength;
			#endif

			#if defined(Waterfall2D_BodySecondTextureSheet) || defined(Waterfall2D_BodySecondTextureSheetWithLerp)
			#if defined(Waterfall2D_BodySecondTextureSheetWithLerp)
			half4 bodySecondTextureColor = SampleTextureSheetLerp(_BodySecondTexture, i.bodyTextureUV.zw);
			#else
			half4 bodySecondTextureColor = SampleTextureSheet(_BodySecondTexture, i.bodyTextureUV.zw);
			#endif
			#else
			half4 bodySecondTextureColor = tex2D(_BodySecondTexture, i.bodyTextureUV.zw);
			#endif

			bodySecondTextureColor.a *= _BodySecondTextureOpacity;
			finalColor = MixColors(finalColor, bodySecondTextureColor);
		#endif
		
		#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff) && (Waterfall2D_HasLeftEdge || Waterfall2D_HasRightEdge)
			half fragOpacity = 1.0;
		#endif

		// Applying Left Edge Texture
		#if Waterfall2D_HasLeftEdge
			if(i.uv.x < _LeftRightEdgesThickness.x)
			{
				#if defined(Waterfall2D_LeftRightEdgesNoise)
					i.leftRightEdgesTextureUV.xy += (tex2D(_NoiseTexture, i.edgesNoiseTextureUV.xy).g - 0.5) * _LeftRightEdgesNoiseStrength;
				#endif

				#if defined(Waterfall2D_LeftRightEdgesSameTexture) || defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
					
					#if defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
						#if defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
						half4 leftEdgeTextureColor = SampleTextureSheetLerp(_LeftRightEdgesTexture, i.leftRightEdgesTextureUV.xy);
						#else
						half4 leftEdgeTextureColor = SampleTextureSheet(_LeftRightEdgesTexture, i.leftRightEdgesTextureUV.xy);
						#endif
					#else 
						half4 leftEdgeTextureColor = tex2D(_LeftRightEdgesTexture, i.leftRightEdgesTextureUV.xy);
					#endif

					#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff)
					fragOpacity = leftEdgeTextureColor.a;
					#endif
					leftEdgeTextureColor.a *= _LeftRightEdgesTextureOpacity;
				#else

					#if defined(Waterfall2D_LeftEdgeTextureSheet) || defined(Waterfall2D_LeftEdgeTextureSheetWithLerp)
						#if defined(Waterfall2D_LeftEdgeTextureSheetWithLerp)
							half4 leftEdgeTextureColor = SampleTextureSheetLerp(_LeftEdgeTexture, i.leftRightEdgesTextureUV.xy);
						#else
							half4 leftEdgeTextureColor = SampleTextureSheet(_LeftEdgeTexture, i.leftRightEdgesTextureUV.xy);
						#endif
					#else
						half4 leftEdgeTextureColor = tex2D(_LeftEdgeTexture, i.leftRightEdgesTextureUV.xy);
					#endif
						
					#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff)
					fragOpacity = leftEdgeTextureColor.a;
					#endif
					leftEdgeTextureColor.a *= _LeftEdgeTextureOpacity;
				#endif

				finalColor = MixColors(finalColor, leftEdgeTextureColor);
			}
		#endif

		// Applying Right Edge Texture
		#if Waterfall2D_HasRightEdge
			if(i.uv.x > (1.0 - _LeftRightEdgesThickness.z))
			{
				#if defined(Waterfall2D_LeftRightEdgesNoise)
					#if Waterfall2D_HasLeftEdge
						i.edgesNoiseTextureUV.xy = TRANSFORM_TEX((i.leftRightEdgesTextureUV.zw * _LeftRightEdgesNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _LeftRightEdgesNoiseSpeed);
					#endif
					i.leftRightEdgesTextureUV.zw += (tex2D(_NoiseTexture, i.edgesNoiseTextureUV.xy).g - 0.5) * _LeftRightEdgesNoiseStrength;
				#endif

				#if defined(Waterfall2D_LeftRightEdgesSameTexture) || defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
					
					#if defined(Waterfall2D_LeftRightEdgesSameTextureSheet) || defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
						#if defined(Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp)
							half4 rightEdgeTextureColor = SampleTextureSheetLerp(_LeftRightEdgesTexture, i.leftRightEdgesTextureUV.zw);
						#else
							half4 rightEdgeTextureColor = SampleTextureSheet(_LeftRightEdgesTexture, i.leftRightEdgesTextureUV.zw);
						#endif
					#else
						half4 rightEdgeTextureColor = tex2D(_LeftRightEdgesTexture, i.leftRightEdgesTextureUV.zw);
					#endif
						
					#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff)
					fragOpacity = rightEdgeTextureColor.a;
					#endif
					rightEdgeTextureColor.a *= _LeftRightEdgesTextureOpacity;
				#else
					
					#if defined(Waterfall2D_RightEdgeTextureSheet) || defined(Waterfall2D_RightEdgeTextureSheetWithLerp)
						#if defined(Waterfall2D_RightEdgeTextureSheetWithLerp)
							half4 rightEdgeTextureColor = SampleTextureSheetLerp(_RightEdgeTexture, i.leftRightEdgesTextureUV.zw);
						#else
							half4 rightEdgeTextureColor = SampleTextureSheet(_RightEdgeTexture, i.leftRightEdgesTextureUV.zw);
						#endif
					#else
						half4 rightEdgeTextureColor = tex2D(_RightEdgeTexture, i.leftRightEdgesTextureUV.zw);
					#endif
						
					#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff)
					fragOpacity = rightEdgeTextureColor.a;
					#endif
					rightEdgeTextureColor.a *= _RightEdgeTextureOpacity;	
				#endif

				finalColor = MixColors(finalColor, rightEdgeTextureColor);
			}
		#endif
		
		// Applying Top Edge Texture
		#if Waterfall2D_HasTopEdge
			if(i.uv.y > (1.0 - _TopBottomEdgesThickness.x))
			{
				#if defined(Waterfall2D_TopBottomEdgesNoise)
					i.topBottomEdgesTextureUV.xy += (tex2D(_NoiseTexture, i.edgesNoiseTextureUV.zw).b - 0.5) * _TopBottomEdgesNoiseStrength;
				#endif

				#if defined(Waterfall2D_TopBottomEdgesSameTexture) || defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
					
					#if defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
						#if defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
						half4 topEdgeTextureColor = SampleTextureSheetLerp(_TopBottomEdgesTexture, i.topBottomEdgesTextureUV.xy);
						#else
						half4 topEdgeTextureColor = SampleTextureSheet(_TopBottomEdgesTexture, i.topBottomEdgesTextureUV.xy);
						#endif
					#else 
						half4 topEdgeTextureColor = tex2D(_TopBottomEdgesTexture, i.topBottomEdgesTextureUV.xy);
					#endif
						
					topEdgeTextureColor.a *= _TopBottomEdgesTextureOpacity;
				#else

					#if defined(Waterfall2D_TopEdgeTextureSheet) || defined(Waterfall2D_TopEdgeTextureSheetWithLerp)
						#if defined(Waterfall2D_TopEdgeTextureSheetWithLerp)
							half4 topEdgeTextureColor = SampleTextureSheetLerp(_TopEdgeTexture, i.topBottomEdgesTextureUV.xy);
						#else
							half4 topEdgeTextureColor = SampleTextureSheet(_TopEdgeTexture, i.topBottomEdgesTextureUV.xy);
						#endif
					#else
						half4 topEdgeTextureColor = tex2D(_TopEdgeTexture, i.topBottomEdgesTextureUV.xy);
					#endif
						
					topEdgeTextureColor.a *= _TopEdgeTextureOpacity;
				#endif
						
				finalColor = MixColors(finalColor, topEdgeTextureColor);
			}
		#endif

		// Applying Bottom Edge Texture
		#if Waterfall2D_HasBottomEdge
			if(i.uv.y < _TopBottomEdgesThickness.z)
			{
				#if defined(Waterfall2D_TopBottomEdgesNoise)
					#if Waterfall2D_HasTopEdge
						i.edgesNoiseTextureUV.zw = TRANSFORM_TEX((i.topBottomEdgesTextureUV.zw * _TopBottomEdgesNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _TopBottomEdgesNoiseSpeed);
					#endif
					
					i.topBottomEdgesTextureUV.zw += (tex2D(_NoiseTexture, i.edgesNoiseTextureUV.zw).b - 0.5) * _TopBottomEdgesNoiseStrength;
				#endif

				#if defined(Waterfall2D_TopBottomEdgesSameTexture) || defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
					
					#if defined(Waterfall2D_TopBottomEdgesSameTextureSheet) || defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
						#if defined(Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp)
							half4 bottomEdgeTextureColor = SampleTextureSheetLerp(_TopBottomEdgesTexture, i.topBottomEdgesTextureUV.zw);
						#else
							half4 bottomEdgeTextureColor = SampleTextureSheet(_TopBottomEdgesTexture, i.topBottomEdgesTextureUV.zw);
						#endif
					#else
						half4 bottomEdgeTextureColor = tex2D(_TopBottomEdgesTexture, i.topBottomEdgesTextureUV.zw);
					#endif
						
					bottomEdgeTextureColor.a *= _TopBottomEdgesTextureOpacity;
				#else
					
					#if defined(Waterfall2D_BottomEdgeTextureSheet) || defined(Waterfall2D_BottomEdgeTextureSheetWithLerp)
						#if defined(Waterfall2D_BottomEdgeTextureSheetWithLerp)
							half4 bottomEdgeTextureColor = SampleTextureSheetLerp(_BottomEdgeTexture, i.topBottomEdgesTextureUV.zw);
						#else
							half4 bottomEdgeTextureColor = SampleTextureSheet(_BottomEdgeTexture, i.topBottomEdgesTextureUV.zw);
						#endif
					#else
						half4 bottomEdgeTextureColor = tex2D(_BottomEdgeTexture, i.topBottomEdgesTextureUV.zw);
					#endif
						
					bottomEdgeTextureColor.a *= _BottomEdgeTextureOpacity;
				#endif

				finalColor = MixColors(finalColor, bottomEdgeTextureColor);
			}
		#endif

		#if !defined(Waterfall2D_ApplyTintColorBeforeTexture)
			finalColor = MixColors(finalColor, bodyColor);
		#endif
			
		#if !defined(Waterfall2D_Refraction)
			finalColor.rgb /= finalColor.a;
		#endif
			
		#if defined(Waterfall2D_LeftRightEdgesTextureAlphaCutoff) && (Waterfall2D_HasLeftEdge || Waterfall2D_HasRightEdge)
		finalColor.a *= (fragOpacity < _LeftRightEdgesTextureAlphaCutoff) ? 0.0 : fragOpacity;
		#endif

		return finalColor;
	}

#endif // Game2D_WaterKit_Waterfall_INCLUDED
