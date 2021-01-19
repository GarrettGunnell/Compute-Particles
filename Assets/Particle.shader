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

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
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
