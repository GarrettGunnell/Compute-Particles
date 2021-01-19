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
            };

            struct Particle {
                float3 position;
            };

            StructuredBuffer<Particle> particleBuffer;

            v2f vert (appdata v, uint instance_id : SV_InstanceID) {
                v2f o;
                o.vertex = UnityObjectToClipPos(float4(particleBuffer[instance_id].position, 1.0f));
                
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f f) : SV_Target {
                return 1;
            }

            ENDCG
        }
    }
}
