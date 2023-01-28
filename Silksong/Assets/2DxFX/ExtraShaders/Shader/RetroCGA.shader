//////////////////////////////////////////////
/// 2DxFX v3 - by VETASOFT 2018 //
//////////////////////////////////////////////


//////////////////////////////////////////////

Shader "2DxFX_Extra_Shaders/RetroCGA"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_TurnCGA_Fade_1("_TurnCGA_Fade_1", Range(0, 1)) = 1
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
float _TurnCGA_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float4 TurnCGA(float4 txt, float value)
{
float3 a = float3(0, 0, 0);
#define Turn(n) a = lerp(n, a, step (length (a-txt.rgb), length (n-txt.rgb)));
Turn(float3(0, 0, 0));
Turn(float3(0.33, 1, 1));
Turn(float3(1, 0.33, 1));
Turn(float3(1, 1, 1));
a = lerp(txt.rgb,a,value);
return float4(a, txt.a);
}
float4 frag (v2f i) : COLOR
{
float4 _MainTex_1 = tex2D(_MainTex, i.texcoord);
float4 TurnCGA_1 = TurnCGA(_MainTex_1,_TurnCGA_Fade_1);
float4 FinalResult = TurnCGA_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
