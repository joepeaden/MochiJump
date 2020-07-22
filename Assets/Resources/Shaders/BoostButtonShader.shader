Shader "Unlit/BoostButtonShader"
{

    // look at me, writing shaders left and right
    // hell yeh mane

    Properties
    {
        _MainColor("Main Color", Color) = (1, 1, 1, 1)
        _BoostMeter("Boost Meter", float) = 0
        _FillColor("Fill Color", Color) = (1, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            float4 _MainTex_ST;
            float4 _MainColor;
            float _BoostMeter;
            float4 _FillColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = _MainColor;
                
                if (i.uv.y <= _BoostMeter)
                {
                    col = _FillColor;
                }

                // getting distance from center of circle
                /*float2 vector_ = i.uv - float2(0.5, 0.5);
                float magnitude = length(vector_);*/

                //col += float4(magnitude, magnitude, magnitude, 1);

                return col;
            }
            ENDCG
        }
    }
}
