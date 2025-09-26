Shader "Custom/Contamination"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Progress ("Progress", Range(0,1)) = 0
        _BubbleSize ("Bubble Size", Float) = 0.05
        _Density ("Density", Float) = 50 // nombre de points dans la grille
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Progress;
            float _BubbleSize;
            float _Density;

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

            // Fonction hash pour position aléatoire
            float2 hash22(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 34.45);
                return frac(float2(p.x * p.y, p.x + p.y));
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
                fixed4 col = tex2D(_MainTex, i.uv);

                // Boucle sur un réseau de points pour générer les bulles
                float density = _Density;
                float step = 1.0 / density;
                for (int x = 0; x < int(density); x++)
                {
                    for (int y = 0; y < int(density); y++)
                    {
                        float2 cell = float2(x,y);
                        float2 bubblePos = (cell + hash22(cell)) * step;

                        // La bulle apparaît progressivement avec _Progress
                        float appearThreshold = hash22(cell).x; // random threshold [0,1]
                        if (_Progress >= appearThreshold)
                        {
                            float dist = distance(i.uv, bubblePos);
                            if (dist < _BubbleSize)
                            {
                                col.rgb = lerp(col.rgb, float3(1,0,0), 0.8); // rouge
                            }
                        }
                    }
                }

                return col;
            }
            ENDCG
        }
    }
}
