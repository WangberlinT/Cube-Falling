Shader "Transparent/Diffuse With Shadows"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Alpha("Alpha", float) = 1
    }
        SubShader
        {
            Pass
            {
                Tags {"LightMode" = "ForwardBase" "RenderType" = "Transparent"}
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha
                ColorMask RGB

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                #include "Lighting.cginc"

                #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
                #include "AutoLight.cginc"

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    SHADOW_COORDS(1)
                    fixed3 diff : COLOR0;
                    fixed3 ambient : COLOR1;
                    float4 pos : SV_POSITION;
                };
                v2f vert(appdata_base v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                    half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                    o.diff = nl * _LightColor0.rgb;
                    o.ambient = ShadeSH9(half4(worldNormal,1));
                    TRANSFER_SHADOW(o)
                    return o;
                }

                sampler2D _MainTex;
                float _Alpha;

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed shadow = SHADOW_ATTENUATION(i);
                    fixed3 lighting = i.diff * shadow;
                    col.rgb *= lighting;
                    col.a *= _Alpha;
                    return col;
                }
                ENDCG
            }
        }
}