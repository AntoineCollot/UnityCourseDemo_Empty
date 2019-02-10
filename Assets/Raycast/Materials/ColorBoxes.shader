Shader "Custom/ColorBoxes" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Coloring ("Coloring", Range(0,1)) = 0.0
		_ColorPeriod ("_ColorPeriod",Range(0,30)) = 1
		_YOffset ("_YOffset",Range(0,2)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows finalcolor:color

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};
		
		half _ColorPeriod;
		half _Coloring;
		half _YOffset;
		
		float triWave(half x,half p)
		{
			return 1-abs(fmod(abs(x),p*2) - p)/(p*0.5);
		}
		
		void color (Input IN, SurfaceOutputStandard o, inout fixed4 color)
		{
			color.x = color.x + triWave(round(IN.worldPos.x+0.0001)+sin(round(IN.worldPos.z+0.0001)),_ColorPeriod) *_Coloring;
			color.z = color.z + triWave(cos(round(IN.worldPos.x+0.0001))+sin(round(IN.worldPos.z+0.0001)),_ColorPeriod) *_Coloring;
			color.z = color.z + triWave(round(IN.worldPos.z+0.0001)+sin(round(IN.worldPos.x+0.0001)),_ColorPeriod) *_Coloring;
		}

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
