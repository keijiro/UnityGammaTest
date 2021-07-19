Shader "GammaTest/Visualizer"
{
    CGINCLUDE

    #include "UnityCG.cginc"

    StructuredBuffer<float> _Buffer;

    void Vertex(float4 vertex : POSITION,
                float2 uv : TEXCOORD,
                out float4 outVertex : SV_Position,
                out float2 outUV : TEXCOORD)
    {
        outVertex = UnityObjectToClipPos(vertex);
        outUV = uv;
    }

    float4 Fragment(float4 vertex : SV_Position,
                    float2 uv : TEXCOORD) : SV_Target
    {
        uint idx = uv.x * 255 + (uint)(uv.y * 2) * 256;
        return _Buffer[idx];
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDCG
        }
    }
}
