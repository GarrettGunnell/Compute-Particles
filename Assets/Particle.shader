Shader "Hidden/Particle" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float4 particlePos : TEXCOORD0;
            };

            struct Particle {
                float3 position;
            };

            StructuredBuffer<Particle> particleBuffer;

            v2f vert (appdata v, uint instance_id : SV_InstanceID) {
                v2f o;
                o.vertex = UnityObjectToClipPos(float4(particleBuffer[instance_id].position, 1.0f));
                o.particlePos = mul(unity_ObjectToWorld, float4(particleBuffer[instance_id].position.xyz, 1));
                
                return o;
            }

            sampler2D _MainTex;
            float _Amplitude;
            float _Dimension;

            float map(float value, float min1, float max1, float min2, float max2) {
                return (value - min1) * (max2 - min2) / (max1 - min1) + min2; 
            }

            fixed4 frag (v2f f) : SV_Target {
                float4 color = 0;
                float4 position = mul(unity_WorldToObject, f.particlePos);
                color.r = map(position.y, -_Amplitude, _Amplitude, 0.1, 1);
                color.g = map(position.x, 0, _Dimension, 0.1, 1);
                color.b = map(position.z, 0, _Dimension, 0.1, 1);

                return color;
            }

            ENDCG
        }
    }
}
