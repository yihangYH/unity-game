//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/red"
{
    Properties
    {
        _LightSize("LightSize", Float) = 4 
        _LightPower("LightPower", Range(0.0,5)) = 2 
        _Intensity("Intensity", Range(0.0, 10)) = 1 
        _LightColor ("LightColor", Color) = (1,1,1,1) // set light color as white
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
    Pass //pass for main object(the cylinder)
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
			};
			vertOut vert(vertIn v)
			{
				vertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag(vertOut v) : SV_Target
			{
				return float4(1.0f, 0.0f, 0.0f, 0.5f); // return red color
			}
			ENDCG
		}
        Pass // pass for light around the object 
        {
			Cull Front //remove front so we can still see the main cylinder
            Blend SrcAlpha One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

             uniform float4 _LightColor;
             uniform float  _LightSize;
             uniform float  _LightPower;
             uniform float  _Intensity;  
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;

                float4 tangent : TANGENT;

            };

            struct v2f
            {
                
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD2;
				float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;

            };
            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz += v.normal*_LightSize; 
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half NormalDotVertex =  dot(normalize(i.normal), normalize(i.worldPos.xyz-_WorldSpaceCameraPos.xyz )); // use the postion of vertex world space - the position of vertex camera space
                float fresnel =pow(saturate(NormalDotVertex),_LightPower)*_Intensity;
                return float4(_LightColor.rgb,fresnel);
            }
            ENDCG
        }

    }
}