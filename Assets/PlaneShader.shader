Shader "Custom/TransparentShadowCollector"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,0.5)
        _ShadowIntensity ("Shadow Intensity", Range(0,1)) = 0.6
    }

    SubShader
    {
        Tags { "Queue"="Geometry" }

        // Основной Pass (прозрачность + освещение)
        Pass
        {
            Tags { "LightMode" = "ForwardBase" }
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            uniform fixed4 _Color;
            uniform float _ShadowIntensity;

            struct v2f
            {
                float4 pos : SV_POSITION;
                LIGHTING_COORDS(0,1)
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                float attenuation = LIGHT_ATTENUATION(i);
                return fixed4(0,0,0,(1 - attenuation) * _ShadowIntensity) + _Color;
            }
            ENDCG
        }

        // Дополнительный Pass для качественной тени
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return float4(0,0,0,1); // Чёрная непрозрачная тень
            }
            ENDCG
        }
    }

    Fallback "VertexLit"
}
