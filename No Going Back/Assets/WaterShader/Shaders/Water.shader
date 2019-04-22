Shader "Custom/Water"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
		_DepthFactor("Depth Factor", float) = 1.0
		_WaveSpeed("Wave Speed", float) = 1.0
		_WaveAmp("Wave Amp", float) = 0.2
		_DepthRampTex("Depth Ramp", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_MainTex("Main Texture", 2D) = "white" {}
	}
	SubShader
	{ 
		Tags
		{
			"Queue" = "Transparent"
		}
	Pass
	{
	CGPROGRAM
	// required to use ComputeScreenPos()
	#include "UnityCG.cginc"

	#pragma vertex vert
	#pragma fragment frag

	// Unity built-in - NOT required in Properties
	sampler2D _CameraDepthTexture;
	sampler2D _DepthRampTex;
	float _WaveSpeed;
	float _WaveAmp;
	sampler2D _NoiseTex;
	float4 _Color;
	float4 _EdgeColor;
	float  _DepthFactor;
	sampler2D _MainTex;

	struct vertexInput
	 {
	   float4 vertex : POSITION;
	   float4 texCoord : TEXCOORD1;
	 };

	struct vertexOutput
	 {
	   float4 pos : SV_POSITION;
	   float4 texCoord : TEXCOORD0;
	   float4 screenPos : TEXCOORD1;
	 };

	vertexOutput vert(vertexInput input)
	{
		vertexOutput output;
		UNITY_INITIALIZE_OUTPUT(vertexOutput, output);

		// convert obj-space position to camera clip space
		output.pos = UnityObjectToClipPos(input.vertex);

		float noiseSample = tex2Dlod(_NoiseTex, float4(input.texCoord.xy, 0, 0));
		output.pos.y += sin(_Time * _WaveSpeed * noiseSample) * _WaveAmp;
		output.pos.x += cos(_Time * _WaveSpeed * noiseSample) * _WaveAmp;

		// compute depth (screenPos is a float4)
		output.screenPos = ComputeScreenPos(output.pos);

		return output;
	 }

	  float4 frag(vertexOutput input) : COLOR
	  {
		  // sample camera depth texture
		  float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.screenPos);
		  float depth = LinearEyeDepth(depthSample).r;

		  float foamLine = 1 - saturate(_DepthFactor * (depth - input.screenPos.w));

		  float4 foamRamp = float4(tex2D(_DepthRampTex, float2(foamLine, 0.5)).rgb, 1.0);
		  
		  float4 albedo = tex2D(_MainTex, input.texCoord.xy);

		  float4 col = _Color * foamRamp * albedo;

		  return col;
	  }

		ENDCG
	 } }}