Shader "Custom/2DOutlineSmoothImproved"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.05)) = 0.01
        _SmoothFactor ("Smooth Factor", Range(0.01, 1.0)) = 0.05
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200

        Pass
        {
            Name "OUTLINE"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;
            float _SmoothFactor;

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

            static const float2 _dirs[8] = {
                float2(1, 0), float2(-1, 0),
                float2(0, 1), float2(0, -1),
                float2(0.707, 0.707), float2(-0.707, 0.707),
                float2(0.707, -0.707), float2(-0.707, -0.707)
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.uv).a;
                float outline = 0;

                // Loop through 8 directional offsets
                for (int j = 0; j < 8; j++)
                {
                    float2 offsetUV = i.uv + _dirs[j] * _OutlineWidth;
                    float sampledAlpha = tex2D(_MainTex, offsetUV).a;

                    // Blend the outline smoothly based on nearby pixels' alpha values
                    outline += smoothstep(0.0, _SmoothFactor, sampledAlpha);
                }

                outline = outline / 8.0; // Average the surrounding alpha values

                // Draw outline if current pixel is transparent and surrounding pixels contribute
                if (alpha == 0 && outline > 0.01)
                {
                    return float4(_OutlineColor.rgb, outline);
                }

                // Otherwise, return the main texture's color
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
