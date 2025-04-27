Shader "Custom/Simple2DDissolve"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _DissolveThreshold("Dissolve Threshold", Range(0, 1)) = 0.5
        _Color("Tint Color", Color) = (1,1,1,1)
        _NoiseScale("Noise Scale", Float) = 50
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha
                Cull Off
                ZWrite Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _DissolveThreshold;
                float4 _Color;
                float _NoiseScale;

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

                // Prosty szum
                float random(float2 st) {
                    return frac(sin(dot(st.xy, float2(12.9898,78.233))) * 43758.5453123);
                }

                float noise(float2 st) {
                    float2 i = floor(st);
                    float2 f = frac(st);

                    // 4 rogi
                    float a = random(i);
                    float b = random(i + float2(1.0, 0.0));
                    float c = random(i + float2(0.0, 1.0));
                    float d = random(i + float2(1.0, 1.0));

                    // £agodzenie
                    float2 u = f * f * (3.0 - 2.0 * f);

                    return lerp(a, b, u.x) +
                           (c - a) * u.y * (1.0 - u.x) +
                           (d - b) * u.x * u.y;
                }

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Pobierz kolor sprite'a
                    fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                // Generuj szum
                float n = noise(i.uv * _NoiseScale);

                // Dissolve na podstawie threshold
                float dissolve = step(_DissolveThreshold, n);

                col.a *= dissolve;

                return col;
            }
            ENDCG
        }
        }
}
