// Custom URP-native TextMeshPro SDF shader.
//
// Neither of the two TMP shaders already in this project work for real dynamic lighting under URP:
// - "TextMeshPro/Distance Field (Surface)" uses #pragma surface, a Built-in RP-only feature URP can't run.
// - The bundled "TMP_SDF-URP Lit.shadergraph" doesn't expose TMP's required property names, so TMP's
//   own runtime/inspector can't drive it (font atlas, face color etc. never get wired up).
// This reimplements TMP's actual SDF face/outline/underlay math (ported from TMPro.cginc / TMP_SDF.shader)
// in an explicit URP HLSL pass, then replaces TMP's "Bevel" fake lighting (a manual _LightAngle slider,
// not connected to real Light objects) with real URP main + additional light + ambient SH sampling.
Shader "TextMeshPro/URP Lit (Custom)"
{
    Properties
    {
        _FaceColor          ("Face Color", Color) = (1,1,1,1)
        _FaceDilate         ("Face Dilate", Range(-1,1)) = 0

        _OutlineColor       ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth       ("Outline Thickness", Range(0,1)) = 0
        _OutlineSoftness    ("Outline Softness", Range(0,1)) = 0

        _UnderlayColor      ("Underlay Color", Color) = (0,0,0,0.5)
        _UnderlayOffsetX    ("Underlay OffsetX", Range(-1,1)) = 0
        _UnderlayOffsetY    ("Underlay OffsetY", Range(-1,1)) = 0
        _UnderlayDilate     ("Underlay Dilate", Range(-1,1)) = 0
        _UnderlaySoftness   ("Underlay Softness", Range(0,1)) = 0
        [Toggle(UNDERLAY_ON)] _UseUnderlay ("Enable Underlay", Float) = 0

        _WeightNormal       ("Weight Normal", float) = 0
        _WeightBold         ("Weight Bold", float) = 0.5

        _ShaderFlags        ("Flags", float) = 0
        _ScaleRatioA        ("Scale RatioA", float) = 1
        _ScaleRatioB        ("Scale RatioB", float) = 1
        _ScaleRatioC        ("Scale RatioC", float) = 1

        _MainTex            ("Font Atlas", 2D) = "white" {}
        _TextureWidth       ("Texture Width", float) = 512
        _TextureHeight      ("Texture Height", float) = 512
        _GradientScale      ("Gradient Scale", float) = 5.0
        _ScaleX             ("Scale X", float) = 1.0
        _ScaleY             ("Scale Y", float) = 1.0
        _PerspectiveFilter  ("Perspective Correction", Range(0,1)) = 0.875
        _Sharpness          ("Sharpness", Range(-1,1)) = 0

        _VertexOffsetX      ("Vertex OffsetX", float) = 0
        _VertexOffsetY      ("Vertex OffsetY", float) = 0

        // -- Real scene lighting (replaces TMP's fake Bevel/_LightAngle hack) --
        _AmbientStrength    ("Ambient Strength", Range(0,2)) = 1.0
        _LightStrength      ("Direct Light Strength", Range(0,4)) = 1.0
        _MinBrightness      ("Minimum Brightness", Range(0,1)) = 0.15

        _MaskCoord          ("Mask Coordinates", vector) = (0,0,32767,32767)
        _ClipRect           ("Clip Rect", vector) = (-32767,-32767,32767,32767)
        _MaskSoftnessX      ("Mask SoftnessX", float) = 0
        _MaskSoftnessY      ("Mask SoftnessY", float) = 0

        _StencilComp        ("Stencil Comparison", Float) = 8
        _Stencil            ("Stencil ID", Float) = 0
        _StencilOp          ("Stencil Operation", Float) = 0
        _StencilWriteMask   ("Stencil Write Mask", Float) = 255
        _StencilReadMask    ("Stencil Read Mask", Float) = 255

        _CullMode           ("Cull Mode", Float) = 0
        _ColorMask          ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull [_CullMode]
        ZWrite Off
        ZTest LEqual
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Universal Forward"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature_local UNDERLAY_ON
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _CLUSTER_LIGHT_LOOP
            #pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float4 color      : COLOR;
                float4 texcoord0  : TEXCOORD0;  // xy = atlas UV, w = weight/scale (sign = bold flag)
                float2 texcoord1  : TEXCOORD1;  // underlay UV source
            };

            struct Varyings
            {
                float4 positionCS  : SV_POSITION;
                float4 color       : COLOR;
                float2 atlas       : TEXCOORD0;
                float4 param       : TEXCOORD1; // alphaClip, scale, bias, weight
                float3 positionWS  : TEXCOORD2;
                float3 normalWS    : TEXCOORD3;
                half3  vertexLighting : TEXCOORD4; // additional lights, computed per-vertex when quality tier asks for it
            #if UNDERLAY_ON
                float4 underlayUV  : TEXCOORD5; // uv.xy, scale, bias
            #endif
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                half4 _FaceColor;
                float _FaceDilate;

                half4 _OutlineColor;
                float _OutlineWidth;
                float _OutlineSoftness;

                half4 _UnderlayColor;
                float _UnderlayOffsetX;
                float _UnderlayOffsetY;
                float _UnderlayDilate;
                float _UnderlaySoftness;

                float _WeightNormal;
                float _WeightBold;

                float _ShaderFlags;
                float _ScaleRatioA;
                float _ScaleRatioB;
                float _ScaleRatioC;

                float _TextureWidth;
                float _TextureHeight;
                float _GradientScale;
                float _ScaleX;
                float _ScaleY;
                float _PerspectiveFilter;
                float _Sharpness;

                float _VertexOffsetX;
                float _VertexOffsetY;

                float _AmbientStrength;
                float _LightStrength;
                float _MinBrightness;

                float4 _ClipRect;
                float4 _MaskCoord;
                float _MaskSoftnessX;
                float _MaskSoftnessY;
            CBUFFER_END

            // Ported verbatim from TMPro.cginc — TMP's own face/outline SDF blend.
            half4 GetFaceOutlineColor(half d, half4 faceColor, half4 outlineColor, half outline, half softness)
            {
                half faceAlpha = 1 - saturate((d - outline * 0.5 + softness * 0.5) / (1.0 + softness));
                half outlineAlpha = saturate((d + outline * 0.5)) * sqrt(min(1.0, outline));

                faceColor.rgb *= faceColor.a;
                outlineColor.rgb *= outlineColor.a;

                faceColor = lerp(faceColor, outlineColor, outlineAlpha);
                faceColor *= faceAlpha;

                return faceColor;
            }

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;

                float bold = step(input.texcoord0.w, 0);

                float4 vertexPos = input.positionOS;
                vertexPos.x += _VertexOffsetX;
                vertexPos.y += _VertexOffsetY;

                VertexPositionInputs posInputs = GetVertexPositionInputs(vertexPos.xyz);
                output.positionCS = posInputs.positionCS;
                output.positionWS = posInputs.positionWS;
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);

                float2 pixelSize = output.positionCS.w;
                pixelSize /= float2(_ScaleX, _ScaleY) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));
                float scale = rsqrt(dot(pixelSize, pixelSize));
                scale *= abs(input.texcoord0.w) * _GradientScale * (_Sharpness + 1);
                if (UNITY_MATRIX_P[3][3] == 0)
                {
                    float3 viewDir = GetWorldSpaceNormalizeViewDir(posInputs.positionWS);
                    scale = lerp(abs(scale) * (1 - _PerspectiveFilter), scale, abs(dot(output.normalWS, viewDir)));
                }

                float weight = lerp(_WeightNormal, _WeightBold, bold) / 4.0;
                weight = (weight + _FaceDilate) * _ScaleRatioA * 0.5;

                float bias = (.5 - weight) + (.5 / scale);
                float alphaClip = (1.0 - _OutlineWidth * _ScaleRatioA - _OutlineSoftness * _ScaleRatioA);
                alphaClip = alphaClip / 2.0 - (.5 / scale) - weight;

                output.color = input.color;
                output.atlas = input.texcoord0.xy;
                output.param = float4(alphaClip, scale, bias, weight);

                half3 vertexLighting = half3(0, 0, 0);
            #if defined(_ADDITIONAL_LIGHTS_VERTEX)
                uint vertexLightsCount = GetAdditionalLightsCount();
                for (uint vIdx = 0u; vIdx < vertexLightsCount; vIdx++)
                {
                    Light vLight = GetAdditionalLight(vIdx, output.positionWS);
                    vertexLighting += vLight.color * (saturate(dot(output.normalWS, vLight.direction)) * vLight.distanceAttenuation);
                }
            #endif
                output.vertexLighting = vertexLighting;

            #if UNDERLAY_ON
                float uScale = scale;
                uScale /= 1 + (_UnderlaySoftness * _ScaleRatioC * uScale);
                float uBias = (0.5 - weight) * uScale - 0.5 - (_UnderlayDilate * _ScaleRatioC * 0.5 * uScale);

                float ux = -(_UnderlayOffsetX * _ScaleRatioC) * _GradientScale / _TextureWidth;
                float uy = -(_UnderlayOffsetY * _ScaleRatioC) * _GradientScale / _TextureHeight;
                output.underlayUV = float4(input.texcoord1 + float2(ux, uy), uScale, uBias);
            #endif

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.atlas).a;
                clip(c - input.param.x);

                float scale = input.param.y;
                float bias  = input.param.z;
                float sd = (bias - c) * scale;

                float outline  = (_OutlineWidth * _ScaleRatioA) * scale;
                float softness = (_OutlineSoftness * _ScaleRatioA) * scale;

                half4 faceColor = _FaceColor;
                faceColor.rgb *= input.color.rgb;
                half4 outlineColor = _OutlineColor;

                faceColor = GetFaceOutlineColor(sd, faceColor, outlineColor, outline, softness);

            #if UNDERLAY_ON
                float d = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.underlayUV.xy).a * input.underlayUV.z;
                faceColor += _UnderlayColor * saturate(d - input.underlayUV.w) * (1 - faceColor.a);
            #endif

                // Real scene lighting: main directional light (with shadows) + additional point/spot
                // lights + baked/ambient probe, instead of TMP's manual _LightAngle slider.
                float3 N = normalize(input.normalWS);
                float4 shadowCoord = TransformWorldToShadowCoord(input.positionWS);
                Light mainLight = GetMainLight(shadowCoord);

                float3 lightSum = mainLight.color * (saturate(dot(N, mainLight.direction)) * mainLight.shadowAttenuation);

            #if defined(_ADDITIONAL_LIGHTS)
                uint additionalLightsCount = GetAdditionalLightsCount();
                for (uint lightIndex = 0u; lightIndex < additionalLightsCount; lightIndex++)
                {
                    Light light = GetAdditionalLight(lightIndex, input.positionWS);
                    lightSum += light.color * (saturate(dot(N, light.direction)) * light.distanceAttenuation * light.shadowAttenuation);
                }
            #endif
                lightSum += input.vertexLighting;

                float3 ambient = SampleSH(N);
                float3 shading = max(lightSum * _LightStrength + ambient * _AmbientStrength, _MinBrightness);

                faceColor.rgb *= shading;

                return faceColor * input.color.a;
            }
            ENDHLSL
        }
    }

    Fallback Off
    CustomEditor "TMPro.EditorUtilities.TMP_SDFShaderGUI"
}
