Shader "Custom/HologramVideo"
{
    Properties
    {
        _MainTex ("Video Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (0, 1, 1, 0.5)
        _ScanSpeed ("Scan Speed", Float) = 2.0
        _ScanDensity ("Scan Density", Float) = 100.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            Lighting Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _ScanSpeed;
            float _ScanDensity;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float scan = sin((_Time.y * _ScanSpeed + i.uv.y * _ScanDensity) * 6.28) * 0.1 + 0.9;
                fixed4 tex = tex2D(_MainTex, i.uv);
                tex.rgb *= _Color.rgb;
                tex.a *= _Color.a * scan;
                return tex;
            }
            ENDCG
        }
    }
}
