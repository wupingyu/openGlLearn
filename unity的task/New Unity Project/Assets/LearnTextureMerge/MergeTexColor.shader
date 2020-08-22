Shader "Unlit/MergeTexColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DestRect("DestRect",Vector) = (0,0,1,1)
        _DestSrcRect("DestSrcRect", Vector) = (0, 0, 1, 1)
        _Color("Color", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        ZTest off
        Cull off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
            float4 _MainTex_ST;

            float4 _DestRect;
            float4 _DestSrcSize;

            float4 _Color;

            //把顶点/uv的数据传递进来
            v2f vert (appdata v)
            {
                v2f o;

                float2 start = _DestRect.xy / _DestSrcSize.xy;
                float2 size = _DestRect.zw / _DestSrcSize.xy;
                float2 uvSize = _DestRect.zw / _DestSrcSize.zw;

                //o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex = float4((start + size * v.uv) * 2 - 1, 0.5, 1);
                o.vertex.y *= -1;
                //没有管真正的顶点坐标，把（0，0），0，1； 1，0；1.1；当作顶点坐标
                //裁剪坐标，把0-1的值转为-1，1，否则画在了右下角四分之一的地方

                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = TRANSFORM_TEX((v.uv.xy * uvSize - (uvSize - 1) * 0.5), _MainTex);

                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
