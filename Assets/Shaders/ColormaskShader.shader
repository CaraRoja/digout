Shader "Custom/SpriteMaskedColorShader"
{
    Properties
    {
        _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
        _MaskTex ("Mask (A)", 2D) = "black" {}
        _Color ("Color", Color) = (1,1,1,1)
        _CurrentColor ("Current Color", Color) = (0.886, 0.0, 0.192, 1) // Cor vermelha como padrão
    }
    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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
            float4 _Color;
            float4 _CurrentColor; // Referência para a nova propriedade

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                fixed mask = tex2D(_MaskTex, i.uv).r;
                color.rgb = lerp(color.rgb, color.rgb * _CurrentColor.rgb, mask);
                color.a *= _CurrentColor.a;
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
