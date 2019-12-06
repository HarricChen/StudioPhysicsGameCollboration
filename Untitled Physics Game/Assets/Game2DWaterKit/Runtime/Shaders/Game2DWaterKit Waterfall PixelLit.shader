﻿Shader "Game2DWaterKit/Built-in Render Pipeline/PixelLit (Supports Lightmaps)/Waterfall"
{
	Properties
	{
		// Body Properties
		_BodyColor("Body Color",color) = (0.11,0.64,0.92,0.25)
		_BodyColorGradientStart("Body Color Gradient Start",color) = (1.0,1.0,1.0,0.25)
		_BodyColorGradientEnd("Body Color Gradient End",color) = (1.0,1.0,1.0,0.25)
		_BodyColorGradientOffset("Body Color Gradient Offset",Range(-1.0,1.0)) = 0.0

		_BodyTexture("Body Texture" , 2D) = "white" {}
		_BodyTextureTilingParameters("Body Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_BodyTextureScrollingSpeed("Body Texture Scrolling Speed", float) = 1.0
		_BodyTextureOpacity("Body Texture Opacity",range(0,1)) = 0.5
		_BodyTextureSheetFramesPerSecond("Body Texture Sheet Frames Per Second",float) = 0.0
		_BodyTextureSheetColumns("Body Texture Sheet Columns",float) = 1.0
		_BodyTextureSheetRows("Body Texture Sheet Rows",float) = 1.0
		_BodyTextureSheetFramesCount("Body Texture Sheet Frames Count",float) = 1.0
		_BodyTextureSheetInverseColumns("Body Texture Sheet Inverse of Columns",float) = 1.0
		_BodyTextureSheetInverseRows("Body Texture Sheet Inverse of Rows",float) = 1.0

		_BodySecondTexture("Body Second Texture" , 2D) = "white" {}
		_BodySecondTextureTilingParameters("Body Second Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_BodySecondTextureScrollingSpeed("Body Second Texture Scrolling Speed", float) = 1.0
		_BodySecondTextureOpacity("Body Second Texture Opacity",range(0,1)) = 0.5
		_BodySecondTextureSheetFramesPerSecond("Body Second Texture Sheet Frames Per Second",float) = 0.0
		_BodySecondTextureSheetColumns("Body Second Texture Sheet Columns",float) = 1.0
		_BodySecondTextureSheetRows("Body Second Texture Sheet Rows",float) = 1.0
		_BodySecondTextureSheetFramesCount("Body Second Texture Sheet Frames Count",float) = 1.0
		_BodySecondTextureSheetInverseColumns("Body Second Texture Sheet Inverse of Columns",float) = 1.0
		_BodySecondTextureSheetInverseRows("Body Second Texture Sheet Inverse of Rows",float) = 1.0

		_BodyNoiseSpeed("Body Texture Distortion Effect - Speed",float) = 0.5
		_BodyNoiseScaleOffset("Body Texture Distortion Effect - Scale Offset",vector) = (1,1,0,0)
		_BodyNoiseStrength("Body Texture Distortion Effect - Strength",Range(0.001,1.0)) = 0.025
		_BodyNoiseTiling("Body Texture Distortion Effect - Tiling", vector) = (1,1,0,0)

		// Top Edge Properties
		_TopEdgeTexture ("Top Edge Texture", 2D) = "white" {}
		_TopEdgeTextureTilingParameters("Top Edge Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_TopEdgeTextureOpacity ("Top Edge Texture Opacity", Range(0.0,1.0)) = 0.75
		_TopEdgeTextureSheetRows("Top Edge Texture Sheet Rows", float) = 1
		_TopEdgeTextureSheetInverseRows ("Top Edge Texture Sheet Inverse Rows", float) = 1
		_TopEdgeTextureSheetColumns("Top Edge Texture Sheet Columns", float) = 1
		_TopEdgeTextureSheetInverseColumns("Top Edge Texture Sheet Inverse Columns", float) = 1
		_TopEdgeTextureSheetFramesCount ("Top Edge Texture Sheet Frames Count", float) = 1
		_TopEdgeTextureSheetFramesPerSecond ("Top Edge Texture Sheet Frames Per Second", float) = 1

		// Bottom Edge Properties
		_BottomEdgeTexture ("Bottom Edge Texture", 2D) = "white" {}
		_BottomEdgeTextureTilingParameters("Bottom Edge Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_BottomEdgeTextureOpacity ("Bottom Edge Texture Opacity", Range(0.0,1.0)) = 0.75
		_BottomEdgeTextureSheetRows("Bottom Edge Texture Sheet Rows", float) = 1
		_BottomEdgeTextureSheetInverseRows ("Bottom Edge Texture Sheet Inverse Rows", float) = 1
		_BottomEdgeTextureSheetColumns("Bottom Edge Texture Sheet Columns", float) = 1
		_BottomEdgeTextureSheetInverseColumns ("Bottom Edge Texture Sheet Inverse Columns", float) = 1
		_BottomEdgeTextureSheetFramesCount("Bottom Edge Texture Sheet Frames Count", float) = 1
		_BottomEdgeTextureSheetFramesPerSecond ("Bottom Edge Texture Sheet Frames Per Second", float) = 1
		
		// Shared Top-Botom Edges Properties
		_TopBottomEdgesThickness ("Top-Bottom Edges Thickness", vector) = (0.2,5,0.2,5)
		_TopBottomEdgesTexture ("Top-Bottom Edges Texture", 2D) = "white" {}
		_TopBottomEdgesTextureTilingParameters("Top-Bottom Edges Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_TopBottomEdgesTextureFlipParameters("Top-Bottom Edges Texture Flip" , vector) = (0,0,0,0)
		_TopBottomEdgesTextureOpacity ("Top-Bottom Edges Texture Opacity", Range(0.0,1.0)) = 0.75
		_TopBottomEdgesTextureSheetRows("Top-Bottom Edges Texture Sheet Rows", float) = 1
		_TopBottomEdgesTextureSheetInverseRows ("Top-Bottom Edges Texture Sheet Rows", float) = 1
		_TopBottomEdgesTextureSheetColumns("Top-Bottom Edges Texture Sheet Columns", float) = 1
		_TopBottomEdgesTextureSheetInverseColumns ("Top-Bottom Edges Texture Sheet Columns", float) = 1
		_TopBottomEdgesTextureSheetFramesCount("Top-Bottom Edge Texture Sheet Frames Count", float) = 1
		_TopBottomEdgesTextureSheetFramesPerSecond ("Top-Bottom Edges Texture Sheet Frames Per Second", float) = 1

		_TopBottomEdgesNoiseStrength ("Top-Bottom Edges Texture Distortion Effect - Strength", Range(0.001,1.0)) = 0.025
		_TopBottomEdgesNoiseSpeed ("Top-Bottom Edges Texture Distortion Effect - Speed", float) = 0.5
		_TopBottomEdgesNoiseTiling ("Top-Bottom Edges Texture Distortion Effect - Tiling", vector) = (1,1,0,0)
		_TopBottomEdgesNoiseScaleOffset ("Top-Bottom Edges Texture Distortion Effect - Scale Offset", vector) = (1.0,1.0,0.0,0.0)

		// Left Edge Properties
		_LeftEdgeTexture ("Left Edge Texture", 2D) = "white" {}
		_LeftEdgeTextureTilingParameters("Left Edge Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_LeftEdgeTextureScrollingSpeed("Left Edge Texture Scrolling Speed", float) = 1.0
		_LeftEdgeTextureOpacity ("Left Edge Texture Opacity", Range(0.0,1.0)) = 0.75
		_LeftEdgeTextureSheetRows("Left Edge Texture Sheet Rows", float) = 1
		_LeftEdgeTextureSheetInverseRows ("Left Edge Texture Sheet Inverse Rows", float) = 1
		_LeftEdgeTextureSheetColumns("Left Edge Texture Sheet Columns", float) = 1
		_LeftEdgeTextureSheetInverseColumns("Left Edge Texture Sheet Inverse Columns", float) = 1
		_LeftEdgeTextureSheetFramesCount ("Left Edge Texture Sheet Frames Count", float) = 1
		_LeftEdgeTextureSheetFramesPerSecond ("Left Edge Texture Sheet Frames Per Second", float) = 1

		// Right Edge Properties
		_RightEdgeTexture ("Right Edge Texture", 2D) = "white" {}
		_RightEdgeTextureTilingParameters("Right Edge Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_RightEdgeTextureScrollingSpeed("Right Edge Texture Scrolling Speed", float) = 1.0
		_RightEdgeTextureOpacity ("Right Edge Texture Opacity", Range(0.0,1.0)) = 0.75
		_RightEdgeTextureSheetRows("Right Edge Texture Sheet Rows", float) = 1
		_RightEdgeTextureSheetInverseRows ("Right Edge Texture Sheet Inverse Rows", float) = 1
		_RightEdgeTextureSheetColumns("Right Edge Texture Sheet Columns", float) = 1
		_RightEdgeTextureSheetInverseColumns ("Right Edge Texture Sheet Inverse Columns", float) = 1
		_RightEdgeTextureSheetFramesCount("Right Edge Texture Sheet Frames Count", float) = 1
		_RightEdgeTextureSheetFramesPerSecond ("Right Edge Texture Sheet Frames Per Second", float) = 1

		// Shared Left-Right Edges Properties
		_LeftRightEdgesThickness ("Left-Right Edges Thickness", vector) = (0.2,5,0.2,5)
		_LeftRightEdgesTexture ("Left-Right Edges Texture", 2D) = "white" {}
		_LeftRightEdgesTextureTilingParameters("Left-Right Edges Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		_LeftRightEdgesTextureScrollingSpeed("Left-Right Edges Texture Scrolling Speed", float) = 1.0
		_LeftRightEdgesTextureFlipParameters("Left-Right Edges Texture Flip" , vector) = (0,0,0,0)
		_LeftRightEdgesTextureOpacity ("Left-Right Edges Texture Opacity", Range(0.0,1.0)) = 0.75
		_LeftRightEdgesTextureSheetRows("Left-Right Edges Texture Sheet Rows",float) = 1.0
		_LeftRightEdgesTextureSheetInverseRows("Left-Right Edges Texture Sheet Inverse of Rows",float) = 1.0
		_LeftRightEdgesTextureSheetColumns("Left-Right Edges Texture Sheet Columns",float) = 1.0
		_LeftRightEdgesTextureSheetInverseColumns("Left-Right Edges Texture Sheet Inverse of Columns",float) = 1.0
		_LeftRightEdgesTextureSheetFramesCount("Left-Right Edges Texture Sheet Frames Count",float) = 1.0
		_LeftRightEdgesTextureSheetFramesPerSecond("Left-Right Edges Texture Sheet Frames Per Second",float) = 0.0
		_LeftRightEdgesTextureAlphaCutoff("Left-Right Edges Texture Alpha Cutoff", Range(0.0,1.0)) = 0.1

		_LeftRightEdgesNoiseStrength("Left-Right Edges Texture Distortion Effect -  Strength", Range(0.001,1.0)) = 0.025
		_LeftRightEdgesNoiseSpeed("Left-Right Edges Texture Distortion Effect -  Speed", float) = 0.5
		_LeftRightEdgesNoiseTiling("Left-Right Edges Texture Distortion Effect - Tiling", vector) = (1,1,0,0)
		_LeftRightEdgesNoiseScaleOffset("Left-Right Edges Texture Distortion Effect -  Scale Offset", vector) = (1.0,1.0,0.0,0.0)

		//Refraction Properties
		_RefractionTexture ("Refraction Texture", 2D) = "white" {}
		_RefractionNoiseStrength ("Refraction Distortion Effect -  Strength", Range(0.001,0.1)) = 0.015
		_RefractionNoiseSpeed ("Refraction Distortion Effect -  Speed", float) = 0.5
		_RefractionNoiseTiling ("Refraction Distortion Effect -  Tiling", vector) = (1, 1, 0, 0)
		_RefractionNoiseScaleOffset ("Refraction Distortion Effect -  Offset", vector) = (1.0,1.0,0.0,0.0)
		
		//Emission Properties
		_EmissionColor("Emission Color",color) = (1.0,1.0,1.0,0.0)
		_EmissionColorIntensity("Emission Color Intensity",float) = 1.0

		//Noise Texture (RGBA): Body(A) , Top-Bottom Edges(B) , Left-Right Edges(G) and Refraction(R)
		_NoiseTexture("Noise Texture", 2D) = "black" {}

		// Keywords States
		// Body
		_Waterfall2D_IsColorGradientEnabled("Body Color - Gradient Color Mode", float) = 0
		_Waterfall2D_IsBodyTextureSheetEnabled("Body Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsBodyTextureSheetWithLerpEnabled("Body Texture Sheet - Lerp Toggle", float) = 0
		_Waterfall2D_IsBodySecondTextureSheetEnabled("Body Second Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsBodySecondTextureSheetWithLerpEnabled("Body Second Texture Sheet - Lerp Toggle", float) = 0
		_Waterfall2D_IsBodyNoiseEnabled("Body Texture - Distortion Effect Toggle", float) = 0

		// Left-Right Edges
		_Waterfall2D_IsLeftEdgeEnabled("Left Edge Toggle", float) = 0
		_Waterfall2D_IsLeftEdgeTextureSheetEnabled("Left Edge Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsLeftEdgeTextureSheetWithLerpEnabled("Left Edge Texture Sheet - Lerp Toggle", float) = 0

		_Waterfall2D_IsRightEdgeEnabled("Right Edge Toggle", float) = 0
		_Waterfall2D_IsRightEdgeTextureSheetEnabled("Right Edge Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsRightEdgeTextureSheetWithLerpEnabled("Right Edge Texture Sheet - Lerp Toggle", float) = 0

		_Waterfall2D_IsLeftRightEdgesEnabled("Left-Right Edges Toggle", float) = 0
		_Waterfall2D_IsLeftRightEdgesNoiseEnabled("Left-Right Edges Texture - Distortion Effect Toggle", float) = 0
		_Waterfall2D_IsLeftRightEdgesUseSameTextureEnabled("Left-Right Edges Use Same Texture Toggle", float) = 0
		_Waterfall2D_IsLeftRightEdgesTextureSheetEnabled("Left-Right Edges Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsLeftRightEdgesTextureSheetWithLerpEnabled("Left-Right Edges Texture Sheet - Lerp Toggle", float) = 0
		_Waterfall2D_IsLeftRightEdgesTextureAlphaCutoffEnabled("Left-Right Edges Texture Alpha Cutoff Toggle", float) = 0

		// Top-Bottom Edges
		_Waterfall2D_IsTopEdgeEnabled ("Top Edge Toggle", float) = 0
		_Waterfall2D_IsTopEdgeTextureSheetEnabled ("Top Edge Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsTopEdgeTextureSheetWithLerpEnabled ("Top Edge Texture Sheet - Lerp Toggle", float) = 0

		_Waterfall2D_IsBottomEdgeEnabled ("Bottom Edge Toggle", float) = 0
		_Waterfall2D_IsBottomEdgeTextureSheetEnabled ("Bottom Edge Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsBottomEdgeTextureSheetWithLerpEnabled ("Bottom Edge Texture Sheet - Lerp Toggle", float) = 0

		_Waterfall2D_IsTopBottomEdgesEnabled("Top-Bottom Edges Toggle", float) = 0
		_Waterfall2D_IsTopBottomEdgesNoiseEnabled ("Top-Botom Edges - Distortion Effect Toggle", float) = 0
		_Waterfall2D_IsTopBottomEdgesUseSameTextureEnabled("Top-Bottom Edges Use Same Texture Toggle", float) = 0
		_Waterfall2D_IsTopBottomEdgesTextureSheetEnabled("Top-Bottom Edges Texture - Is A Texture Sheet Toggle", float) = 0
		_Waterfall2D_IsTopBottomEdgesTextureSheetWithLerpEnabled("Top-Bottom Edges Texture Sheet - Lerp Toggle", float) = 0

		// Refraction
		_Waterfall2D_IsRefractionEnabled ("Refraction Effect Toggle", float) = 0

		// Emission
		_Waterfall2D_IsEmissionColorEnabled("Emission Toggle", float) = 0

		// Sprite Mask (Stencil) Options
		[Enum(None,0,Visible Inside Mask,4,Visible Outside Mask,5)] _SpriteMaskInteraction ("Sprite Mask Interaction", float) = 0
		_SpriteMaskInteractionRef ("Sprite Mask Interaction Ref", float) = 1

		_Waterfall2D_IsApplyTintColorOnTopOfTextureEnabled("Apply Tint Color On Top Of Texture Toggle", float) = 0.0

		[HideInInspector] _SrcBlend("__src", float) = 5
		[HideInInspector] _DstBlend("__dst", float) = 10
		[HideInInspector] _ZWrite("__zw", float) = 0
			
		[HideInInspector] _Game2DWaterKit_MaterialType("__type", float) = 1.0 // 0 -> water material / 1 -> waterfall material
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane"}

		Stencil
		{
			Ref 1
			Comp [_SpriteMaskInteraction]
		}

		Cull Off
		ZWrite [_ZWrite]
		Blend [_SrcBlend] [_DstBlend]

		Pass
		{
			Name "FORWARD"
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_fog
			#pragma multi_compile_fwdbase noshadowmask nodynlightmap nodirlightmap noshadow
			
			#pragma shader_feature _ Waterfall2D_ApplyTintColorBeforeTexture
			// Body shader features
			#pragma shader_feature _ Waterfall2D_BodyColorGradient
			#pragma shader_feature _ Waterfall2D_BodyTexture Waterfall2D_BodyTextureSheet Waterfall2D_BodyTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_BodyTextureStretch Waterfall2D_BodyTextureStretchAutoX Waterfall2D_BodyTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_BodySecondTexture Waterfall2D_BodySecondTextureSheet Waterfall2D_BodySecondTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_BodySecondTextureStretch Waterfall2D_BodySecondTextureStretchAutoX Waterfall2D_BodySecondTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_BodyTextureNoise
			// Top-Bottom Edges shader features
			#pragma shader_feature _ Waterfall2D_TopEdge Waterfall2D_TopEdgeTextureSheet Waterfall2D_TopEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_TopEdgeTextureStretch Waterfall2D_TopEdgeTextureStretchAutoX Waterfall2D_TopEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_BottomEdge Waterfall2D_BottomEdgeTextureSheet Waterfall2D_BottomEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_BottomEdgeTextureStretch Waterfall2D_BottomEdgeTextureStretchAutoX Waterfall2D_BottomEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesSameTexture Waterfall2D_TopBottomEdgesSameTextureSheet Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesFlipTopEdgeY Waterfall2D_TopBottomEdgesFlipBottomEdgeY
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesTextureStretch Waterfall2D_TopBottomEdgesTextureStretchAutoX Waterfall2D_TopBottomEdgesTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesNoise
			// Left-Right Edges shader features
			#pragma shader_feature _ Waterfall2D_LeftEdge Waterfall2D_LeftEdgeTextureSheet Waterfall2D_LeftEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_LeftEdgeTextureStretch Waterfall2D_LeftEdgeTextureStretchAutoX Waterfall2D_LeftEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_RightEdge Waterfall2D_RightEdgeTextureSheet Waterfall2D_RightEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_RightEdgeTextureStretch Waterfall2D_RightEdgeTextureStretchAutoX Waterfall2D_RightEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesSameTexture Waterfall2D_LeftRightEdgesSameTextureSheet Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesFlipLeftEdgeX Waterfall2D_LeftRightEdgesFlipRightEdgeX
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesTextureStretch Waterfall2D_LeftRightEdgesTextureStretchAutoX Waterfall2D_LeftRightEdgesTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesTextureAlphaCutoff
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesNoise
			// Refraction
			#pragma shader_feature _ Waterfall2D_Refraction
			// Emission
			#pragma shader_feature _ Waterfall2D_ApplyEmissionColor
			
			#ifndef UNITY_PASS_FORWARDBASE
			#define UNITY_PASS_FORWARDBASE
			#endif
			#ifdef UNITY_LIGHT_PROBE_PROXY_VOLUME
			#undef UNITY_LIGHT_PROBE_PROXY_VOLUME
			#endif

			#define Game2DWaterKit_PixelLit_Base
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "Game2DWaterKitWaterfall.cginc"
			
			Varyings vert (Attributes v)
			{
				Varyings o = Waterfall2D_Vert(v);
				
				float3 worldPos = mul(unity_ObjectToWorld, v.pos).xyz;
				o.worldPos = worldPos;

				#ifdef LIGHTMAP_ON
				// Lightmap
				o.lightmapCoord.xy = v.lightmapCoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#else
				// SH/ambient and vertex lights
				#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
				o.sh = ComputeSH(worldPos);
				#endif
				#endif

				return o;
			}
			
			half4 frag (Varyings i) : SV_Target
			{
				half4 c = Waterfall2D_Frag(i);

				#ifdef LIGHTMAP_ON
				half3 lightColor = ComputeLightColor(i.worldPos, i.lightmapCoord);
				#else
				#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
				half3 lightColor = ComputeLightColor(i.worldPos, i.sh);
				#else
				half3 lightColor = ComputeLightColor(i.worldPos);
				#endif
				#endif

				c.rgb *= lightColor;

				return c;
			}

			ENDCG
		}

		Pass
		{
			Name "FORWARD"
			Tags { "LightMode" = "ForwardAdd" }
			Blend ONE ONE

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_fog
			#pragma skip_variants INSTANCING_ON
			#pragma multi_compile_fwdadd noshadowmask nodynlightmap nodirlightmap noshadow
			
			#pragma shader_feature _ Waterfall2D_ApplyTintColorBeforeTexture
			// Body shader features
			#pragma shader_feature _ Waterfall2D_BodyColorGradient
			#pragma shader_feature _ Waterfall2D_BodyTexture Waterfall2D_BodyTextureSheet Waterfall2D_BodyTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_BodyTextureStretch Waterfall2D_BodyTextureStretchAutoX Waterfall2D_BodyTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_BodySecondTexture Waterfall2D_BodySecondTextureSheet Waterfall2D_BodySecondTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_BodySecondTextureStretch Waterfall2D_BodySecondTextureStretchAutoX Waterfall2D_BodySecondTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_BodyTextureNoise
			// Top-Bottom Edges shader features
			#pragma shader_feature _ Waterfall2D_TopEdge Waterfall2D_TopEdgeTextureSheet Waterfall2D_TopEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_TopEdgeTextureStretch Waterfall2D_TopEdgeTextureStretchAutoX Waterfall2D_TopEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_BottomEdge Waterfall2D_BottomEdgeTextureSheet Waterfall2D_BottomEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_BottomEdgeTextureStretch Waterfall2D_BottomEdgeTextureStretchAutoX Waterfall2D_BottomEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesSameTexture Waterfall2D_TopBottomEdgesSameTextureSheet Waterfall2D_TopBottomEdgesSameTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesFlipTopEdgeY Waterfall2D_TopBottomEdgesFlipBottomEdgeY
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesTextureStretch Waterfall2D_TopBottomEdgesTextureStretchAutoX Waterfall2D_TopBottomEdgesTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_TopBottomEdgesNoise
			// Left-Right Edges shader features
			#pragma shader_feature _ Waterfall2D_LeftEdge Waterfall2D_LeftEdgeTextureSheet Waterfall2D_LeftEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_LeftEdgeTextureStretch Waterfall2D_LeftEdgeTextureStretchAutoX Waterfall2D_LeftEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_RightEdge Waterfall2D_RightEdgeTextureSheet Waterfall2D_RightEdgeTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_RightEdgeTextureStretch Waterfall2D_RightEdgeTextureStretchAutoX Waterfall2D_RightEdgeTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesSameTexture Waterfall2D_LeftRightEdgesSameTextureSheet Waterfall2D_LeftRightEdgesSameTextureSheetWithLerp
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesFlipLeftEdgeX Waterfall2D_LeftRightEdgesFlipRightEdgeX
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesTextureStretch Waterfall2D_LeftRightEdgesTextureStretchAutoX Waterfall2D_LeftRightEdgesTextureStretchAutoY
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesTextureAlphaCutoff
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesNoise
			// Refraction
			#pragma shader_feature _ Waterfall2D_Refraction
			// Emission
			#pragma shader_feature _ Waterfall2D_ApplyEmissionColor
			
			#ifndef UNITY_PASS_FORWARDADD
			#define UNITY_PASS_FORWARDADD
			#endif
			#ifdef UNITY_LIGHT_PROBE_PROXY_VOLUME
			#undef UNITY_LIGHT_PROBE_PROXY_VOLUME
			#endif

			#define Game2DWaterKit_PixelLit_Add

			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "Game2DWaterKitWaterfall.cginc"
			
			Varyings vert (Attributes v)
			{
				Varyings o = Waterfall2D_Vert(v);
				o.worldPos = mul(unity_ObjectToWorld, v.pos).xyz;
				return o;
			}
			
			half4 frag (Varyings i) : SV_Target
			{
				half4 c = Waterfall2D_Frag(i);
				float3 worldPos = i.worldPos;
				#ifndef USING_DIRECTIONAL_LIGHT
					half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				#else
					half3 lightDir = _WorldSpaceLightPos0.xyz;
				#endif
				UNITY_LIGHT_ATTENUATION(atten, i, worldPos)
				c.rgb *= _LightColor0.rgb * max (0, -lightDir.z) * atten;
				return c;
			}

			ENDCG
		}
	}

	CustomEditor "Game2DWaterKit.Game2DWaterfallShaderGUI"
}
