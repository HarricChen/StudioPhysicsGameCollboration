﻿Shader "Game2DWaterKit/Built-in Render Pipeline/PixelLit (Supports Lightmaps)/Water" 
{
	Properties {
    	//Water Body Properties
		[HideInInspector] _WaterColor ("Body Color",color) = (0.11,0.64,0.92,0.25)
		[HideInInspector] _WaterColorGradientStart ("Body Color Gradient Start",color) = (1.0,1.0,1.0,0.25)
		[HideInInspector] _WaterColorGradientEnd("Body Color Gradient End",color) = (1.0,1.0,1.0,0.25)
		[HideInInspector] _WaterColorGradientOffset ("Body Color Gradient Offset",Range(-1.0,1.0)) = 0.0
		[HideInInspector] _WaterTexture("Body Texture" , 2D) = "white" {}
		[HideInInspector] _WaterTextureScrollingSpeedX ("Body Texture Scroll Speed - X" , float) = 0.0
		[HideInInspector] _WaterTextureScrollingSpeedY ("Body Texture Scroll Speed - Y" , float) = 0.0
		[HideInInspector] _WaterTextureTilingParameters("Water Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		[HideInInspector] _WaterTextureOpacity ("Body Texture Opacity",range(0,1)) = 0.5
		[HideInInspector] _WaterNoiseSpeed ("Body Distortion Effect Speed",float) = 0.025
		[HideInInspector] _WaterNoiseScaleOffset ("Body Distortion Effect Scale Offset",vector) = (1,1,0,0)
		[HideInInspector] _WaterNoiseStrength ("Body Distortion Effect Strength",Range(0.001,1.0)) = 0.025
		[HideInInspector] _WaterNoiseTiling("Body Distortion Effect Tiling", vector) = (1, 1, 0, 0)
		[HideInInspector] _WaterTextureSheetFramesPerSecond("Body Texture Sheet Frames Per Second",float) = 0.0
		[HideInInspector] _WaterTextureSheetColumns("Body Texture Sheet Columns",float) = 1.0
		[HideInInspector] _WaterTextureSheetRows ("Body Texture Sheet Rows",float) = 1.0
		[HideInInspector] _WaterTextureSheetFramesCount("Body Texture Sheet Frames Count",float) = 1.0
		[HideInInspector] _WaterTextureSheetInverseColumns("Body Texture Sheet Inverse of Columns",float) = 1.0
		[HideInInspector] _WaterTextureSheetInverseRows("Body Texture Sheet Inverse of Rows",float) = 1.0

		//Water Surface Properties
		[HideInInspector] _SurfaceLevel("Surface Level",range(0.0,1.0)) = 0.9
		[HideInInspector] _SubmergeLevel("Submerge Level",range(0.0,1.0)) = 0.95
		[HideInInspector] _SurfaceColor ("Surface Color",color) = (0.14,0.54,0.85,0.25)
		[HideInInspector] _SurfaceColorGradientStart("Surface Color Gradient Start",color) = (1.0,1.0,1.0,0.25)
		[HideInInspector] _SurfaceColorGradientEnd("Surface Color Gradient End",color) = (1.0,1.0,1.0,0.25)
		[HideInInspector] _SurfaceColorGradientOffset("Surface Color Gradient Offset",Range(-1.0,1.0)) = 0.0
      	[HideInInspector] _SurfaceTexture ("Surface Texture",2D) = "white" {}
		[HideInInspector] _SurfaceTextureScrollingSpeedX("Surface Texture Scroll Speed - X" , float) = 0.0
		[HideInInspector] _SurfaceTextureScrollingSpeedY("Surface Texture Scroll Speed - Y" , float) = 0.0
		[HideInInspector] _SurfaceTextureTilingParameters("Surface Texture Tiling Mode Properties" , vector) = (0,0,0,0)
		[HideInInspector] _SurfaceTextureOpacity ("Surface Texture Opacity",range(0,1)) = 0.5
		[HideInInspector] _SurfaceNoiseSpeed ("Surface Distortion Effect Speed",float) = 0.025
		[HideInInspector] _SurfaceNoiseScaleOffset ("Surface Distortion Effect Offset",vector) = (1,1,0,0)
		[HideInInspector] _SurfaceNoiseStrength ("Surface Distortion Effect Strength",Range(0.001,1.0)) = 0.025
		[HideInInspector] _SurfaceNoiseTiling("Surface Distortion Effect Tiling", vector) = (1, 1, 0, 0)
		[HideInInspector] _SurfaceTextureSheetFramesPerSecond("Surface Texture Sheet Frames Per Second",float) = 0.0
		[HideInInspector] _SurfaceTextureSheetColumns("Surface Texture Sheet Columns",float) = 1.0
		[HideInInspector] _SurfaceTextureSheetRows ("Surface Texture Sheet Rows",float) = 1.0
		[HideInInspector] _SurfaceTextureSheetFramesCount("Surface Texture Sheet Frames Count",float) = 1.0
		[HideInInspector] _SurfaceTextureSheetInverseColumns("Surface Texture Sheet Inverse of Columns",float) = 1.0
		[HideInInspector] _SurfaceTextureSheetInverseRows("Surface Texture Sheet Inverse of Rows",float) = 1.0

		//Lighting Properties
		[HideInInspector] _WaterEmissionColor("Water Emission Color",color) = (1.0,1.0,1.0,0.0)
		[HideInInspector] _WaterEmissionColorIntensity("Water Emission Color Intensity",float) = 1.0

		//Refraction Properties
		[HideInInspector] _RefractionAmountOfBending ("Refraction Bending",Range(0.0,0.025)) = 0.0
		[HideInInspector] _RefractionNoiseSpeed ("Refraction Distortion Effect Speed",float) = 0.075
		[HideInInspector] _RefractionNoiseScaleOffset ("Refraction Distortion Effect Scale Offset",vector) = (8,5,0,0)
		[HideInInspector] _RefractionNoiseStrength ("Refraction Distortion Effect Strength",Range(0.001,0.1)) = 0.015
		[HideInInspector] _RefractionNoiseTiling("Refraction Distortion Effect Tiling", vector) = (1, 1, 0, 0)

		//Reflection Properties
		[HideInInspector] _ReflectionVisibility ("Reflection Visibility",range(0,1)) = 0.3
		[HideInInspector] _ReflectionFadingParameters ("Reflection Fading Parameters", vector) = (0,0,0,0)
		[HideInInspector] _ReflectionNoiseSpeed ("Reflection Distortion Effect Speed",float) = 0.075
		[HideInInspector] _ReflectionNoiseScaleOffset ("Reflection Distortion Effect Scale Offset",vector) = (5,14,0,0)
		[HideInInspector] _ReflectionNoiseStrength ("Reflection Distortion Effect Strength",Range(0.001,0.1)) = 0.02
		[HideInInspector] _ReflectionNoiseTiling("Reflection Distortion Effect Tiling", vector) = (1, 1, 0, 0)

		//Noise Texture (RGBA): body(A) , surface(B) , reflection(G) and refraction(R)
		[HideInInspector] _NoiseTexture ("Noise Texture",2D) = "black"{}

		//Camera Render Rendertextures
		[HideInInspector] _RefractionTexture ("Refraction Texture", 2D) = "black" {}
		[HideInInspector] _RefractionTexturePartiallySubmergedObjects("Refraction Texture For Partially Submerged Objects", 2D) = "black" {}
		[HideInInspector] _ReflectionTexture ("Reflection Texture",2D) = "black" {}
		[HideInInspector] _ReflectionTexturePartiallySubmergedObjects("Reflection Texture For Partially Submerged Objects",2D) = "black" {}

		// Sprite Mask (Stencil) Options
		[HideInInspector] [Enum(None,0,Visible Inside Mask,4,Visible Outside Mask,5)] _SpriteMaskInteraction("Sprite Mask Interaction", Int) = 0
		[HideInInspector] _SpriteMaskInteractionRef("Sprite Mask Interaction Ref", Int) = 1

		// Other properties
		[HideInInspector] _Mode ("Rendering Mode", float) = 2000
		[HideInInspector] _SrcBlend ("__src", float) = 5
		[HideInInspector] _DstBlend ("__dst", float) = 10
		[HideInInspector] _ZWrite ("__zw", float) = 0
			
		[HideInInspector] _Water2D_IsFakePerspectiveEnabled ("Fake Perspective Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsRefractionEnabled ("Refraction Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsReflectionEnabled ("Reflection Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsWaterNoiseEnabled ("Body Distortion Effect Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsWaterTextureSheetEnabled ("Body Is A Texture Sheet Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsWaterTextureSheetWithLerpEnabled("Body Texture Sheet Lerp Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsSurfaceEnabled ("Surface Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsWaterSurfaceTextureSheetEnabled ("Surface Is A Texture Sheet Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsWaterSurfaceTextureSheetWithLerpEnabled("Surface Texture Sheet Lerp Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsSurfaceNoiseEnabled ("Surface Texture Distortion Effect Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsColorGradientEnabled("Body Color Mode",float) = 0.0
		[HideInInspector] _Water2D_IsSurfaceColorGradientEnabled ("Surface Color Mode",float) = 0.0
		[HideInInspector] _Water2D_IsEmissionColorEnabled("Emission Toggle",float) = 0.0
		[HideInInspector] _Water2D_IsApplyTintColorOnTopOfTextureEnabled("Apply Tint Color On Top Of Texture Toggle", float) = 1.0

		[HideInInspector] _Game2DWaterKit_MaterialType("__type", float) = 0.0 // 0 -> water material / 1 -> waterfall material
	}

SubShader {
      Tags {
		"RenderType"="Opaque"
		"Queue"="Transparent"
		"IgnoreProjector"="True"
		"PreviewType"="Plane"
		}

		Stencil
		{
			Ref[_SpriteMaskInteractionRef]
			Comp[_SpriteMaskInteraction]
		}

		Blend [_SrcBlend] [_DstBlend]
		ZWrite [_ZWrite]
		Cull off

	Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile_fwdbase noshadowmask nodynlightmap nodirlightmap noshadow

			#pragma shader_feature Water2D_FakePerspective
			#pragma shader_feature Water2D_Refraction
			#pragma shader_feature Water2D_Reflection
			#pragma shader_feature _ Water2D_ReflectionFadeLinear Water2D_ReflectionFadeExponentialTwo Water2D_ReflectionFadeExponentialThree Water2D_ReflectionFadeExponentialFour
			#pragma shader_feature _ Water2D_WaterTexture Water2D_WaterTextureSheet Water2D_WaterTextureSheetWithLerp
			#pragma shader_feature _ Water2D_WaterTextureScroll
			#pragma shader_feature _ Water2D_WaterTextureStretch Water2D_WaterTextureStretchAutoX Water2D_WaterTextureStretchAutoY
			#pragma shader_feature Water2D_WaterNoise
			#pragma shader_feature Water2D_Surface
			#pragma shader_feature Water2D_SurfaceColorGradient
			#pragma shader_feature _ Water2D_SurfaceTexture Water2D_SurfaceTextureSheet Water2D_SurfaceTextureSheetWithLerp
			#pragma shader_feature _ Water2D_SurfaceTextureScroll
			#pragma shader_feature _ Water2D_SurfaceTextureStretch Water2D_SurfaceTextureStretchAutoX Water2D_SurfaceTextureStretchAutoY
			#pragma shader_feature Water2D_SurfaceNoise
			#pragma shader_feature Water2D_ColorGradient
			#pragma shader_feature Water2D_ApplyEmissionColor
			#pragma shader_feature _ Water2D_ApplyTintColorBeforeTexture

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
			#include "Game2DWaterKitWater.cginc"

			// vertex shader
			Varyings vert (Attributes v) {
			  Varyings o = Water2D_Vert(v);
			  
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

			  UNITY_TRANSFER_FOG(o,o.pos);
			  return o;
			}

			// fragment shader
			half4 frag (Varyings i) : SV_Target {

				#if defined(Water2D_Refraction) && defined(Water2D_FakePerspective)
				half4 partiallySubmergedObjectsColor;
				half4 c = Water2D_Frag(i,partiallySubmergedObjectsColor);
				#else
				half4 c = Water2D_Frag(i);
				#endif

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

				#if defined(Water2D_Refraction) && defined(Water2D_FakePerspective)
					c.rgb += partiallySubmergedObjectsColor.a * (partiallySubmergedObjectsColor.rgb - c.rgb);
				#endif

				UNITY_APPLY_FOG(i.fogCoord, c);
				return c;
			}

			ENDCG

			}


		// ---- forward rendering additive lights pass:
		Pass {
			Name "FORWARD"
			Tags { "LightMode" = "ForwardAdd" }
			ZWrite Off 
			Blend ONE ONE

			CGPROGRAM
			// compile directives
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma skip_variants INSTANCING_ON
			#pragma multi_compile_fwdadd noshadowmask nodynlightmap nodirlightmap noshadow

			#pragma shader_feature Water2D_FakePerspective
			#pragma shader_feature Water2D_Refraction
			#pragma shader_feature Water2D_Reflection
			#pragma shader_feature _ Water2D_ReflectionFadeLinear Water2D_ReflectionFadeExponentialTwo Water2D_ReflectionFadeExponentialThree Water2D_ReflectionFadeExponentialFour
			#pragma shader_feature _ Water2D_WaterTexture Water2D_WaterTextureSheet Water2D_WaterTextureSheetWithLerp
			#pragma shader_feature _ Water2D_WaterTextureScroll
			#pragma shader_feature _ Water2D_WaterTextureStretch Water2D_WaterTextureStretchAutoX Water2D_WaterTextureStretchAutoY
			#pragma shader_feature Water2D_WaterNoise
			#pragma shader_feature Water2D_Surface
			#pragma shader_feature Water2D_SurfaceColorGradient
			#pragma shader_feature _ Water2D_SurfaceTexture Water2D_SurfaceTextureSheet Water2D_SurfaceTextureSheetWithLerp
			#pragma shader_feature _ Water2D_SurfaceTextureScroll
			#pragma shader_feature _ Water2D_SurfaceTextureStretch Water2D_SurfaceTextureStretchAutoX Water2D_SurfaceTextureStretchAutoY
			#pragma shader_feature Water2D_SurfaceNoise
			#pragma shader_feature Water2D_ColorGradient
			#pragma shader_feature Water2D_ApplyEmissionColor
			#pragma shader_feature _ Water2D_ApplyTintColorBeforeTexture

			#ifndef UNITY_PASS_FORWARDADD
			#define UNITY_PASS_FORWARDADD
			#endif
			#ifdef UNITY_LIGHT_PROBE_PROXY_VOLUME
			#undef UNITY_LIGHT_PROBE_PROXY_VOLUME
			#endif

			#define Game2DWaterKit_PixelLit_Add

			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "Game2DWaterKitWater.cginc"

			// vertex shader
			Varyings vert (Attributes v) {
			  Varyings o = Water2D_Vert(v);

			  o.worldPos = mul(unity_ObjectToWorld, v.pos).xyz;

			  return o;
			}

			// fragment shader
			half4 frag (Varyings i):SV_TARGET  {
			 
				#if defined(Water2D_Refraction) && defined(Water2D_FakePerspective)
					half partiallaySubmergedObjectsColorOpacity;
					half4 c = Water2D_Frag(i, partiallaySubmergedObjectsColorOpacity);
				#else
					half4 c = Water2D_Frag(i);
				#endif

			  float3 worldPos = i.worldPos;

			  #ifndef USING_DIRECTIONAL_LIGHT
				half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
			  #else
				half3 lightDir = _WorldSpaceLightPos0.xyz;
			  #endif

			  UNITY_LIGHT_ATTENUATION(atten, i, worldPos)
				c.rgb *= _LightColor0.rgb * max (0, -lightDir.z) * atten;
				
				#if defined(Water2D_Refraction) && defined(Water2D_FakePerspective)
			    c.rgb *= 1.0 - partiallaySubmergedObjectsColorOpacity;
				#endif

  				return c;
			}
			ENDCG
			}
    } 
	CustomEditor "Game2DWaterKit.Game2DWaterShaderGUI"
  }