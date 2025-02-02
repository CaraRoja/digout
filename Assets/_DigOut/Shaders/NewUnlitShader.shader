Shader "Custom/SaturationAndColorTintShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}    // Correção: garantir que a sintaxe de definição de default value está correta
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _Saturation ("Saturation", Range(0, 1)) = 1.0
        _ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _MaskTex;
            float _Saturation;
            float4 _ColorTint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 mask = tex2D(_MaskTex, i.uv);
                col *= _ColorTint; // Aplicar a cor de tintura
                float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
                col.rgb = lerp(float3(grey, grey, grey), col.rgb, _Saturation);
                col.a *= mask.a;
                return col;
            }
            ENDCG
        }
    }
}
