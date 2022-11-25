Shader "Unlit/fog"
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
                "Queue" = "Transparent"}
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha
        //Blend 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define TAU 6.283185307

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Offset;
            float _Ratio;

            float random(float2 st)
            {
                return frac(sin(dot(st.xy,float2(12.9898,78.233)))*43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul( UNITY_MATRIX_M, float4(v.vertex.xyz,1 ));
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 st = (i.worldPos.xy/i.uv) *10;
                
                
                float2 ipos = floor(st);
                float2 fpos = frac(st);
                
                float3 color = float3(random(ipos),random(fpos),1);
                return float4(1,1,1,1);
                // float condition = saturate(sin((i.uv.x - _Time.y*.05)*TAU*3)*.25+.75) * (saturate(cos((i.uv.y - _Time.x*2)*TAU*3)*0.5+1))-2;
                //return mul(float4(1,1,1,0.25f),float4(random(ipos),condition,random(fpos),0.25f));
            }
            ENDCG
        }
    }
}