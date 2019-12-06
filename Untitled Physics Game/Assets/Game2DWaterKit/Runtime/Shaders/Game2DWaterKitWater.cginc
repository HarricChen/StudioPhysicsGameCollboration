#ifndef Game2D_WaterKit_Water_INCLUDED
#define Game2D_WaterKit_Water_INCLUDED

#define Water2D_HasWaterTextureSheet defined(Water2D_WaterTextureSheet) || defined(Water2D_WaterTextureSheetWithLerp)
#define Water2D_HasWaterTexture  defined(Water2D_WaterTexture) || Water2D_HasWaterTextureSheet
#define Water2D_HasSurfaceTextureSheet defined(Water2D_SurfaceTextureSheet) || defined(Water2D_SurfaceTextureSheetWithLerp)
#define Water2D_HasSurfaceTexture defined(Water2D_Surface) && (defined(Water2D_SurfaceTexture) || Water2D_HasSurfaceTextureSheet)
#define Water2D_HasPartiallySubmergedObjectsRefractionTexture defined(Water2D_Refraction) && defined(Water2D_FakePerspective)
#define Water2D_HasPartiallySubmergedObjectsReflectionTexture defined(Water2D_Reflection) && defined(Water2D_FakePerspective)

			CBUFFER_START(UnityPerObject)
				#if defined(Water2D_Refraction) || defined(Water2D_Reflection)
					#if defined(Water2D_Refraction)
						uniform sampler2D _RefractionTexture;
						#if Water2D_HasPartiallySubmergedObjectsRefractionTexture
							uniform sampler2D _RefractionTexturePartiallySubmergedObjects;
						#endif
					#endif

					#if defined(Water2D_Reflection)
						uniform sampler2D _ReflectionTexture;
						#if Water2D_HasPartiallySubmergedObjectsReflectionTexture
							uniform sampler2D _ReflectionTexturePartiallySubmergedObjects;
						#endif
						uniform half _ReflectionLowerLimit;
					#endif

					uniform float4x4 _WaterMVP;
				#endif
				uniform float4 _AspectRatio;
			CBUFFER_END // UnityPerObject

			CBUFFER_START(UnityPerMaterial)
				#if defined(Water2D_Refraction) 
					half _RefractionNoiseSpeed;
					half _RefractionNoiseStrength;
					half4 _RefractionNoiseTiling;
					half _RefractionAmountOfBending;
				#endif

				#if defined(Water2D_Reflection) 
					half _ReflectionNoiseSpeed;
					half _ReflectionNoiseStrength;
					half4 _ReflectionNoiseTiling;
					half _ReflectionVisibility;
				#endif

				#if defined(Water2D_Surface)
					half _SurfaceLevel;
					#if defined(Water2D_SurfaceColorGradient)
					half4 _SurfaceColorGradientStart;
					half4 _SurfaceColorGradientEnd;
					half _SurfaceColorGradientOffset;
					#else
					half4 _SurfaceColor;
					#endif
					#if Water2D_HasSurfaceTexture
						sampler2D _SurfaceTexture;
						float4 _SurfaceTexture_ST;
						half _SurfaceTextureOpacity;

						#if defined(Water2D_SurfaceTextureScroll)
							half _SurfaceTextureScrollingSpeedX;
							half _SurfaceTextureScrollingSpeedY;
						#endif

						#if defined(Water2D_SurfaceNoise)
							half _SurfaceNoiseSpeed;
							half _SurfaceNoiseStrength;
							half4 _SurfaceNoiseTiling;
						#endif

						#if Water2D_HasSurfaceTextureSheet
							half _SurfaceTextureSheetFramesPerSecond;
							half _SurfaceTextureSheetFramesCount;
							half _SurfaceTextureSheetInverseColumns;
							half _SurfaceTextureSheetInverseRows;
						#endif
					#endif

					#if defined(Water2D_FakePerspective)
						half _SubmergeLevel;
					#endif
				#endif

				#if defined(Water2D_ColorGradient)
					half4 _WaterColorGradientStart;
					half4 _WaterColorGradientEnd;
					half _WaterColorGradientOffset;
				#else
					half4 _WaterColor;
				#endif

				#if Water2D_HasWaterTexture
					sampler2D _WaterTexture;
					float4 _WaterTexture_ST;
					half _WaterTextureOpacity;
					
					#if defined(Water2D_WaterTextureScroll)
						half _WaterTextureScrollingSpeedX;
						half _WaterTextureScrollingSpeedY;
					#endif

					#if defined(Water2D_WaterNoise)
						half _WaterNoiseSpeed;
						half _WaterNoiseStrength;
						half4 _WaterNoiseTiling;
					#endif

					#if Water2D_HasWaterTextureSheet
						half _WaterTextureSheetFramesPerSecond;
						half _WaterTextureSheetFramesCount;
						half _WaterTextureSheetInverseColumns;
						half _WaterTextureSheetInverseRows;
					#endif
				#endif

				#if defined(Water2D_ApplyEmissionColor)
					half3 _WaterEmissionColor;
					half _WaterEmissionColorIntensity;
				#endif
					
				#if defined(Water2D_Refraction) || defined(Water2D_Reflection) || (Water2D_HasWaterTexture && defined(Water2D_WaterNoise)) || (defined(Water2D_Surface) && Water2D_HasSurfaceTexture && defined(Water2D_SurfaceNoise))
					sampler2D _NoiseTexture;
					float4 _NoiseTexture_ST;
				#endif

			CBUFFER_END // UnityPerMaterial
				
			struct Attributes
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				#if defined(LIGHTMAP_ON)
					float2 lightmapCoord : TEXCOORD1;
				#endif
			};

			struct Varyings
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;

				#if defined(Water2D_Refraction) || defined(Water2D_Reflection)
					float2 refractionReflectionUV : TEXCOORD1;
				#endif

				#if defined(Water2D_Refraction)
					float2 refractionUV : TEXCOORD2;
				#endif

				#if defined(Water2D_Reflection) 
					float3 reflectionUV : TEXCOORD3;
				#endif

				#if Water2D_HasWaterTexture
					#if defined(Water2D_WaterNoise)
						float4 waterTextureUV : TEXCOORD4;
					#else
						float2 waterTextureUV : TEXCOORD4;
					#endif
				#endif

				#if Water2D_HasSurfaceTexture
					#if defined(Water2D_SurfaceNoise)
						float4 surfaceTextureUV : TEXCOORD5;
					#else
						float2 surfaceTextureUV : TEXCOORD5;
					#endif
				#endif

				#if defined(Game2DWaterKit_SRP_Lit)
					float2 lightingUV : TEXCOORD6;
				#else
					#if defined(LIGHTMAP_ON)
						float2 lightmapCoord : TEXCOORD6;
					#else
						#if defined(UNITY_SHOULD_SAMPLE_SH)
			 				half3 sh : TEXCOORD6;
						#endif
					#endif

					#if defined(UNITY_PASS_FORWARDBASE) || defined(UNITY_PASS_FORWARDADD)
  						float3 worldPos : TEXCOORD7;
 					#endif
				#endif

				#if !defined(Game2DWaterKit_SRP_Lit) && !defined(Game2DWaterKit_SRP_Unlit)
				UNITY_FOG_COORDS(8)
				#endif
				
				#if defined(Game2DWaterKit_VertexLit_Vertex) || defined(Game2DWaterKit_VertexLit_VertexLM) || defined(Game2DWaterKit_VertexLit_OnlyDirectional)
					half3 lightColor : COLOR0;
				#endif
			};
			
			#include "Game2DWaterKit.cginc"

			inline Varyings Water2D_Vert(Attributes v){
				Varyings o;
 				UNITY_INITIALIZE_OUTPUT(Varyings,o);
				
				o.pos = ComputeClipPosition(v.pos);
				o.uv = v.uv;

				#if defined(Water2D_Refraction) || defined(Water2D_Reflection)
					float4 pos = mul(_WaterMVP,v.pos);
					o.refractionReflectionUV = (pos.xy / pos.w) * 0.5 + 0.5;
				#endif

				float2 vertexPositionWorldSpace = (mul(unity_ObjectToWorld, v.pos)).xy;

				#if defined(Water2D_Refraction) 
					o.refractionUV.xy = TRANSFORM_TEX((vertexPositionWorldSpace * _RefractionNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _RefractionNoiseSpeed);
				#endif

				#if defined(Water2D_Reflection) 
					o.reflectionUV.xy = TRANSFORM_TEX((vertexPositionWorldSpace * _ReflectionNoiseTiling.xy), _NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _ReflectionNoiseSpeed);
					o.reflectionUV.z = v.pos.y;
				#endif

				#if defined(Water2D_Surface) && Water2D_HasSurfaceTexture
					#if defined(Water2D_SurfaceTextureStretch) || defined(Water2D_SurfaceTextureStretchAutoX) || defined(Water2D_SurfaceTextureStretchAutoY)
						#if defined(Water2D_SurfaceTextureStretchAutoX)
							_SurfaceTexture_ST.xy /= (1.0 - _SurfaceLevel);
							o.surfaceTextureUV.x = v.uv.x * (_SurfaceTexture_ST.x * _AspectRatio.x) + _SurfaceTexture_ST.z;
							o.surfaceTextureUV.y = (v.uv.y - _SurfaceLevel) * _SurfaceTexture_ST.y + _SurfaceTexture_ST.w;
						#elif defined(Water2D_SurfaceTextureStretchAutoY)
							o.surfaceTextureUV.x = v.uv.x * _SurfaceTexture_ST.x + _SurfaceTexture_ST.z;
							o.surfaceTextureUV.y = (v.uv.y - _SurfaceLevel) * (_SurfaceTexture_ST.y * _AspectRatio.w) + _SurfaceTexture_ST.w;
						#else
							o.surfaceTextureUV.x = v.uv.x * _SurfaceTexture_ST.x + _SurfaceTexture_ST.z;
							o.surfaceTextureUV.y = (v.uv.y - _SurfaceLevel) * (_SurfaceTexture_ST.y / (1.0 - _SurfaceLevel)) + _SurfaceTexture_ST.w;
						#endif
					#else
						o.surfaceTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _SurfaceTexture);
					#endif

					#if defined(Water2D_SurfaceTextureScroll)
						o.surfaceTextureUV.xy += fmod2(float2(_SurfaceTextureScrollingSpeedX, _SurfaceTextureScrollingSpeedY) * _G2DWK_Frame_Time.x);
					#endif

					#if defined(Water2D_SurfaceNoise)
						o.surfaceTextureUV.zw = TRANSFORM_TEX((o.surfaceTextureUV.xy * _SurfaceNoiseTiling.xy),_NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _SurfaceNoiseSpeed);
					#endif
				#endif

				#if Water2D_HasWaterTexture
					#if defined(Water2D_WaterTextureStretch) || defined(Water2D_WaterTextureStretchAutoX) || defined(Water2D_WaterTextureStretchAutoY)
						#if defined(Water2D_WaterTextureStretchAutoX)
							#if defined(Water2D_Surface)
								_WaterTexture_ST.xy /= _SurfaceLevel;
							#endif
							o.waterTextureUV.x = v.uv.x * (_WaterTexture_ST.x * _AspectRatio.x) + _WaterTexture_ST.z;
							o.waterTextureUV.y = v.uv.y * _WaterTexture_ST.y + _WaterTexture_ST.w;
						#elif defined(Water2D_WaterTextureStretchAutoY)
							o.waterTextureUV.x = v.uv.x * _WaterTexture_ST.x + _WaterTexture_ST.z;
							o.waterTextureUV.y = v.uv.y * (_WaterTexture_ST.y * _AspectRatio.w) + _WaterTexture_ST.w;
						#else
							#if defined(Water2D_Surface)
								_WaterTexture_ST.y /= _SurfaceLevel;
							#endif
							o.waterTextureUV.xy = TRANSFORM_TEX(v.uv, _WaterTexture);
						#endif
					#else
						o.waterTextureUV.xy = TRANSFORM_TEX(vertexPositionWorldSpace, _WaterTexture);
					#endif
						
					#if defined(Water2D_WaterTextureScroll)
						o.waterTextureUV.xy += fmod2(float2(_WaterTextureScrollingSpeedX, _WaterTextureScrollingSpeedY) * _G2DWK_Frame_Time.x);
					#endif

					#if defined(Water2D_WaterNoise)
						o.waterTextureUV.zw = TRANSFORM_TEX((o.waterTextureUV.xy * _WaterNoiseTiling.xy),_NoiseTexture) + fmod2(_G2DWK_Frame_Time.w * _WaterNoiseSpeed);
					#endif

				#endif
				return o;
			}

			#if defined(Water2D_Reflection) && (defined(Water2D_ReflectionFadeLinear) || defined(Water2D_ReflectionFadeExponentialTwo) || defined(Water2D_ReflectionFadeExponentialThree) || defined(Water2D_ReflectionFadeExponentialFour))
			inline half4 FadeReflectionColor(half4 color, half fadeFactor)
			{
				#if defined(Water2D_ReflectionFadeLinear)
					color.a *= fadeFactor;
				#elif defined(Water2D_ReflectionFadeExponentialTwo)
					color.a *= fadeFactor * fadeFactor;
				#elif defined(Water2D_ReflectionFadeExponentialThree)
					color.a *= fadeFactor * fadeFactor * fadeFactor;
				#elif defined(Water2D_ReflectionFadeExponentialFour)
					color.a *= fadeFactor * fadeFactor * fadeFactor * fadeFactor;
				#endif

				return color;
			}
			#endif

			#if defined(Water2D_Refraction) && defined(Water2D_FakePerspective) && !defined(Game2DWaterKit_Unlit) && !defined(Game2DWaterKit_SRP_Unlit)
			#if defined(Game2DWaterKit_PixelLit_Add)
			inline half4 Water2D_Frag(Varyings i, out half frontColorOpacity)
			#else
			inline half4 Water2D_Frag(Varyings i, out half4 frontColor)
			#endif
			#else
			inline half4 Water2D_Frag(Varyings i)
			#endif
			{
				half4 finalColor = 0;

				#if defined(Water2D_Surface)
					bool isSurface = i.uv.y > _SurfaceLevel;
					#if defined(Water2D_FakePerspective)
						bool isBelowSubmergeLevel = i.uv.y < _SubmergeLevel;
					#endif
				#endif

				// Sampling Refraction Render-Texture
				#if defined(Water2D_Refraction)
					float refractionDistortion = (tex2D(_NoiseTexture,i.refractionUV.xy).r - 0.5) * _RefractionNoiseStrength + _RefractionAmountOfBending;
					half4 refractionColor = tex2D(_RefractionTexture,float2(i.refractionReflectionUV.xy + refractionDistortion));
					#if Water2D_HasPartiallySubmergedObjectsRefractionTexture
						half4 refractionColorPartiallySubmergedObjects;
						if (isBelowSubmergeLevel) {
							refractionColorPartiallySubmergedObjects = tex2D(_RefractionTexturePartiallySubmergedObjects, float2(i.refractionReflectionUV.xy + refractionDistortion));
							refractionColor.rgb += refractionColorPartiallySubmergedObjects.rgb - refractionColor.rgb * refractionColorPartiallySubmergedObjects.a;
						}
						else {
							refractionColorPartiallySubmergedObjects = tex2D(_RefractionTexturePartiallySubmergedObjects, i.refractionReflectionUV.xy);
						}
					#endif
				#endif

				// Sampling Reflection Render-Texture
				#if defined(Water2D_Reflection)
					half4 reflectionColor = 0;
					float reflectionDistortion = (tex2D(_NoiseTexture, i.reflectionUV.xy).g - 0.5) * _ReflectionNoiseStrength;
					
					#if Water2D_HasPartiallySubmergedObjectsReflectionTexture
						if(isSurface){
							float reflectionCoordY = (i.uv.y - _SurfaceLevel) / (1.0 - _SurfaceLevel);
							float2 reflectionTextureCoord = float2(i.refractionReflectionUV.x + reflectionDistortion, 1.0 - reflectionCoordY + reflectionDistortion);
							reflectionColor = tex2D(_ReflectionTexture, reflectionTextureCoord);
							#if defined(Water2D_ReflectionFadeLinear) || defined(Water2D_ReflectionFadeExponentialTwo) || defined(Water2D_ReflectionFadeExponentialThree) || defined(Water2D_ReflectionFadeExponentialFour)
							reflectionColor = FadeReflectionColor(reflectionColor, reflectionCoordY);
							#endif
							if (isBelowSubmergeLevel) {
								float reflectionPartiallySubmergedObjectsCoordY = (i.uv.y - _SurfaceLevel) / (_SubmergeLevel - _SurfaceLevel);
								float2 reflectionTextureCoordPartiallySubmergedObjects = float2(i.refractionReflectionUV.x + reflectionDistortion, 1.0 - reflectionPartiallySubmergedObjectsCoordY + reflectionDistortion);
								half4 reflectionColorPartiallySubmergedObjects = tex2D(_ReflectionTexturePartiallySubmergedObjects, reflectionTextureCoordPartiallySubmergedObjects);
								#if defined(Water2D_ReflectionFadeLinear) || defined(Water2D_ReflectionFadeExponentialTwo) || defined(Water2D_ReflectionFadeExponentialThree) || defined(Water2D_ReflectionFadeExponentialFour)
								reflectionColorPartiallySubmergedObjects = FadeReflectionColor(reflectionColorPartiallySubmergedObjects, reflectionPartiallySubmergedObjectsCoordY);
								#endif

								reflectionColor.rgb = lerp(reflectionColor.rgb, reflectionColorPartiallySubmergedObjects.rgb, reflectionColorPartiallySubmergedObjects.a);
								reflectionColor.a = reflectionColorPartiallySubmergedObjects.a + (1.0 - reflectionColorPartiallySubmergedObjects.a) * reflectionColor.a;
							}
						}
					#else
						i.refractionReflectionUV.y = 1.0 - i.refractionReflectionUV.y;
						reflectionColor = tex2D(_ReflectionTexture, float2(i.refractionReflectionUV.xy + reflectionDistortion));
						#if defined(Water2D_ReflectionFadeLinear) || defined(Water2D_ReflectionFadeExponentialTwo) || defined(Water2D_ReflectionFadeExponentialThree) || defined(Water2D_ReflectionFadeExponentialFour)
						reflectionColor = FadeReflectionColor(reflectionColor, i.uv.y);
						#endif
					#endif
				#endif

				#if Water2D_HasWaterTexture || Water2D_HasSurfaceTexture
					half4 textureSampledColor = 0;
				#endif

				// Sampling Water Surface Texture
				#if Water2D_HasSurfaceTexture
					if(isSurface){
						#if defined(Water2D_SurfaceNoise)
							i.surfaceTextureUV.xy += (tex2D(_NoiseTexture,i.surfaceTextureUV.zw).b - 0.5) * _SurfaceNoiseStrength;
						#endif

						#if Water2D_HasSurfaceTextureSheet
							#if defined(Water2D_SurfaceTextureSheetWithLerp)
							textureSampledColor = SampleTextureSheetLerp(_SurfaceTexture, i.surfaceTextureUV.xy);
							#else
							textureSampledColor = SampleTextureSheet(_SurfaceTexture, i.surfaceTextureUV.xy);
							#endif
						#else
							textureSampledColor = tex2D(_SurfaceTexture,i.surfaceTextureUV.xy);
						#endif

						textureSampledColor.a *= _SurfaceTextureOpacity;
					}
				#endif

				// Sampling Water Body Texture
				#if Water2D_HasWaterTexture
					#if defined(Water2D_Surface)
						if(!isSurface){
					#endif
							#if defined(Water2D_WaterNoise)
								i.waterTextureUV.xy += (tex2D(_NoiseTexture,i.waterTextureUV.zw).a - 0.5) * _WaterNoiseStrength;
							#endif

							#if Water2D_HasWaterTextureSheet
								#if defined(Water2D_WaterTextureSheetWithLerp)
								textureSampledColor = SampleTextureSheetLerp(_WaterTexture, i.waterTextureUV.xy);
								#else
								textureSampledColor = SampleTextureSheet(_WaterTexture, i.waterTextureUV.xy);
								#endif
							#else
								textureSampledColor = tex2D(_WaterTexture,i.waterTextureUV.xy);
							#endif

							textureSampledColor.a *= _WaterTextureOpacity;
					#if defined(Water2D_Surface)
						}
					#endif
				#endif
				
				// Tint Color
				half4 tintColor = 0;
				#if defined(Water2D_Surface)
					if(isSurface)
					{
						#if defined(Water2D_SurfaceColorGradient)
							tintColor = _SurfaceColorGradientEnd + saturate((i.uv.y - _SurfaceLevel) / (1.0 - _SurfaceLevel) + _SurfaceColorGradientOffset) * (_SurfaceColorGradientStart - _SurfaceColorGradientEnd);
						#else
							tintColor = _SurfaceColor;
						#endif
					}else{
				#endif
						#if defined(Water2D_ColorGradient)
							#if defined (Water2D_Surface)
								tintColor = _WaterColorGradientEnd + saturate((i.uv.y / _SurfaceLevel) + _WaterColorGradientOffset) * (_WaterColorGradientStart - _WaterColorGradientEnd);
							#else
								tintColor = _WaterColorGradientEnd + saturate(i.uv.y + _WaterColorGradientOffset) * (_WaterColorGradientStart - _WaterColorGradientEnd);
							#endif
						#else
							tintColor = _WaterColor;
						#endif
				#if defined(Water2D_Surface)
					}
				#endif

				// Applying Colors

				// Applying Refraction Render-Texture Color
				#if defined(Water2D_Refraction)
					finalColor = refractionColor;
				#endif

				// Applying Reflection Render-Texture Color
				#if defined(Water2D_Reflection)
					#if Water2D_HasPartiallySubmergedObjectsReflectionTexture
							if(isSurface){
					#endif
								reflectionColor.a *= step(i.reflectionUV.z, _ReflectionLowerLimit) * _ReflectionVisibility;
								finalColor = MixColors(finalColor, reflectionColor);
					#if Water2D_HasPartiallySubmergedObjectsReflectionTexture
							}
					#endif
				#endif

				#if defined(Water2D_ApplyTintColorBeforeTexture)
					// Applying Tint Color
					finalColor = MixColors(finalColor, tintColor);

					// Applying Water Body/Surface Texture Color
					#if Water2D_HasSurfaceTexture ||  Water2D_HasWaterTexture
					finalColor = MixColors(finalColor, textureSampledColor);
					#endif
				#else
					// Applying Water Body/Surface Texture Color
					#if Water2D_HasSurfaceTexture ||  Water2D_HasWaterTexture
					finalColor = MixColors(finalColor, textureSampledColor);
					#endif

					// Applying Tint Color
					finalColor = MixColors(finalColor, tintColor);
				#endif

				// Applying Partially Submerged Objects Render-Texture Color
				#if Water2D_HasPartiallySubmergedObjectsRefractionTexture
					#if defined(Game2DWaterKit_Unlit) || defined(Game2DWaterKit_SRP_Unlit)
						if (!isBelowSubmergeLevel) {
							finalColor.rgb += refractionColorPartiallySubmergedObjects.rgb - finalColor.rgb * refractionColorPartiallySubmergedObjects.a;
						}
					#endif

					#if defined(Game2DWaterKit_SRP_Lit)
						frontColor.rgb = refractionColorPartiallySubmergedObjects.rgb; //out parameter
						frontColor.a = refractionColorPartiallySubmergedObjects.a * (isBelowSubmergeLevel ? 0.0 : 1.0); //out parameter
					#elif !defined(Game2DWaterKit_SRP_Unlit)
						#if defined(Game2DWaterKit_PixelLit_Add)
							frontColorOpacity = refractionColorPartiallySubmergedObjects.a * (isBelowSubmergeLevel ? 0.0 : 1.0); //out parameter
						#elif !defined(Game2DWaterKit_Unlit)
							frontColor.rgb = refractionColorPartiallySubmergedObjects.rgb; //out parameter
							frontColor.a = refractionColorPartiallySubmergedObjects.a * (isBelowSubmergeLevel ? 0.0 : 1.0); //out parameter
						#endif
					#endif
				#endif
				
				#if !defined(Water2D_Refraction)
				finalColor.rgb /= finalColor.a;
				#endif

				return finalColor;
			}

#endif // Game2D_WaterKit_Water_INCLUDED
