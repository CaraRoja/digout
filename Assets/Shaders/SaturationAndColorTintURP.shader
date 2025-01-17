Shader "Custom/SaturationAndColorTintURP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("Mask", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}

        _Saturation ("Saturation", Range(0, 1)) = 1
        _ColorTint ("Color Tint", Color) = (1,1,1,1)

        // Propriedades legadas
        [HideInInspector] _Color("Tint", Color) = (1,1,1,1)
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [HideInInspector] _AlphaTex("External Alpha", 2D) = "white" {}
        [HideInInspector] _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Tags { "LightMode" = "Universal2D" }

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __
            #pragma multi_compile _ DEBUG_DISPLAY

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color        : COLOR;
                float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4  positionCS  : SV_POSITION;
                half4   color       : COLOR;
                float2  uv          : TEXCOORD0;
                half2   lightingUV  : TEXCOORD1;
                #if defined(DEBUG_DISPLAY)
                float3  positionWS  : TEXCOORD2;
                #endif
                UNITY_VERTEX_OUTPUT_STEREO
            };

            // Texturas e amostradores
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_MaskTex);
            SAMPLER(sampler_MaskTex);
            half4 _MainTex_ST;
            float4 _Color;
            half4 _RendererColor;
            float _Saturation;
            float4 _ColorTint;

            #if USE_SHAPE_LIGHT_TYPE_0
            SHAPE_LIGHT(0)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_1
            SHAPE_LIGHT(1)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_2
            SHAPE_LIGHT(2)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_3
            SHAPE_LIGHT(3)
            #endif

            Varyings Vert(Attributes v)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(v.positionOS);
                #if defined(DEBUG_DISPLAY)
                o.positionWS = TransformObjectToWorld(v.positionOS);
                #endif
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.lightingUV = half2(ComputeScreenPos(o.positionCS).xy);

                o.color = v.color * _Color * _RendererColor;
                return o;
            }

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

            half4 Frag(Varyings i) : SV_Target
            {
                const half4 mainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                const half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);

                // Aplica a cor de tintura
                half4 col = i.color * mainTex * _ColorTint;

                // Calcula a satura��o
                float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
                col.rgb = lerp(float3(grey, grey, grey), col.rgb, _Saturation);

                SurfaceData2D surfaceData;
                InputData2D inputData;

                InitializeSurfaceData(col.rgb, col.a, mask, surfaceData);
                InitializeInputData(i.uv, i.lightingUV, inputData);

                return CombinedShapeLightShared(surfaceData, inputData);
            }
            ENDHLSL
        }

        Pass
        {
            Tags { "LightMode" = "NormalsRendering"}

            HLSLPROGRAM
            #pragma vertex NormalsRenderingVertex
            #pragma fragment NormalsRenderingFragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color        : COLOR;
                float2 uv           : TEXCOORD0;
                float4 tangent      : TANGENT;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4  positionCS      : SV_POSITION;
                half4   color           : COLOR;
                float2  uv              : TEXCOORD0;
                half3   normalWS        : TEXCOORD1;
                half3   tangentWS       : TEXCOORD2;
                half3   bitangentWS     : TEXCOORD3;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            half4 _NormalMap_ST;

            Varyings NormalsRenderingVertex(Attributes attributes)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(attributes);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(attributes.positionOS);
                o.uv = TRANSFORM_TEX(attributes.uv, _NormalMap);
                o.color = attributes.color;
                o.normalWS = -GetViewForwardDir();
                o.tangentWS = TransformObjectToWorldDir(attributes.tangent.xyz);
                o.bitangentWS = cross(o.normalWS, o.tangentWS) * attributes.tangent.w;
                return o;
            }

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"

            half4 NormalsRenderingFragment(Varyings i) : SV_Target
            {
                const half4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                const half3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv));

                return NormalsRenderingShared(mainTex, normalTS, i.tangentWS, i.bitangentWS, i.normalWS);
            }
            ENDHLSL
        }

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex UnlitVertex
            #pragma fragment UnlitFragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color        : COLOR;
                float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4  positionCS      : SV_POSITION;
                float4  color           : COLOR;
                float2  uv              : TEXCOORD0;
                #if defined(DEBUG_DISPLAY)
                float3  positionWS      : TEXCOORD2;
                #endif
                UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            float4 _Color;
            half4 _RendererColor;
            float _Saturation;
            float4 _ColorTint;

            Varyings UnlitVertex(Attributes attributes)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(attributes);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(attributes.positionOS);
                #if defined(DEBUG_DISPLAY)
                o.positionWS = TransformObjectToWorld(attributes.positionOS);
                #endif
                o.uv = TRANSFORM_TEX(attributes.uv, _MainTex);
                o.color = attributes.color * _Color * _RendererColor;
                return o;
            }

            float4 UnlitFragment(Varyings i) : SV_Target
            {
                float4 mainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                float4 col = i.color * mainTex * _ColorTint;

                // Calcula a satura��o
                float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
                col.rgb = lerp(float3(grey, grey, grey), col.rgb, _Saturation);

                #if defined(DEBUG_DISPLAY)
                SurfaceData2D surfaceData;
                InputData2D inputData;
                half4 debugColor = 0;

                InitializeSurfaceData(col.rgb, col.a, surfaceData);
                InitializeInputData(i.uv, inputData);
                SETUP_DEBUG_DATA_2D(inputData, i.positionWS);

                if(CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
                {
                    return debugColor;
                }
                #endif

                return col;
            }
            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
}
