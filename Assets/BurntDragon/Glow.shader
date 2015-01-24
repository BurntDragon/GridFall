Shader "GGJ/Glow" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_MainTexZoom ("Base Zoom", float) = 1.0
	_GlowMask ("GlowMask (A)", 2D) = "white" {}
	_GlowMaskZoom ("GlowMask Zoom", float) = 1.0
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_DetailTex ("Detail (RGB) Trans (A)", 2D) = "grey" {}
	_DetailColor ("Main Color", Color) = (.1,.1,.1,.1)
	_ScrollSpeed ("Scroll Speed", Vector) = (1,1,0,0)
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 300
	
CGPROGRAM
#pragma surface surf Lambert alpha
#include "UnityCG.cginc"

sampler2D _MainTex;
sampler2D _GlowMask;
sampler2D _BumpMap;
sampler2D _DetailTex;
fixed4 _Color;
fixed4 _DetailColor;
float4 _ScrollSpeed;
float _MainTexZoom;
float _GlowMaskZoom;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_DetailTex;
};
/*
void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	fixed4 d = lerp(fixed4(.5) - _DetailColor, fixed4(.5) + _DetailColor, tex2D(_DetailTex, IN.uv_DetailTex + _ScrollSpeed * _Time));
	o.Albedo = c.rgb * (d.rgb) * 2;
	o.Alpha = c.a * d.a * 2;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
*/

void surf (Input IN, inout SurfaceOutput o) {	
	fixed4 c = tex2D(_MainTex, (IN.uv_MainTex - float2(.5, .5)) * float2(_MainTexZoom, _MainTexZoom) + float2(.5, .5)) * _Color;
	fixed4 g = tex2D(_GlowMask, (IN.uv_MainTex - float2(.5, .5)) * float2(_GlowMaskZoom, _GlowMaskZoom) + float2(.5, .5)) * _Color;
	fixed4 d = tex2D(_DetailTex, IN.uv_DetailTex + _ScrollSpeed * _Time) * _DetailColor;
	o.Albedo = c.rgb + d.rgb;
	o.Alpha = c.a + g.a * _DetailColor.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
ENDCG
}
}