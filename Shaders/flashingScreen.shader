Shader "Unlit/flashingScreen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color",Color) = (1,1,1,1)
        _Offset ("Offset1", Range(0,5)) = 1
        _Ratio ("Ratio", Range(0,5)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" 
                "Queue" = "Overlay"}
        ZWrite On
        //Blend SrcAlpha OneMinusSrcAlpha
        Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _Color;
            float _Offset;
            float _Ratio;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Offset * _Ratio - _Offset;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // float4 shieldColor = float4(1,1,1,1);
                // float4 bgColor = float4(1,1,1,0);
                // float shieldBarMask = _Shield > i.uv.x;
                float dist = length(i.uv)*.45;
                float4 outColor = lerp(float4(0,0,0,0),_Color,dist);
                

                return outColor;
            }
            ENDCG
        }
    }
}