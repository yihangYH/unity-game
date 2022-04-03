Shader "Custom/Water"
{
    Properties
    {
        // the corresponding information of water's foam parts
        [Header(Foam)]
        _FoamTex("FoamTex", 2D) = "white" {}            // foam noise picture
        _FoamColor("FoamColor",Color) = (1,1,1,1)       // default water color
        _FoamRange("FoamRange",Range(0,1)) = 1          // foam range
        _FoamSpeed("FoamSpeed",Float) = 0.1             // foam flow speed
        _FoamNoise("FoamNoise",Float) = 1               // noise range

        [Header(WaterColor)]
        _ShallowColor("ShallowColor",Color) = (1,1,1,1) // water shallow color
        _DeepColor("DeepColor",Color) = (1,1,1,1)       // water deep color
        _DepthRange("DepthRange",Range(0,1)) = 1        // water depth range


        [Header(Wave)]
        //fluctuations in x- and z-direction
        _WaveFrequencySpeed("X Frequancy(x),X Speed(y),Z Frequancy(z),Z Speed(w),",Vector) = (0.2,1,0.2,1)

        [Header(Caustics)]
        _CausticTex("CausticTex", 2D) = "white" {}          //caustic texture
        _CausticColor1("CausticsColor1",Color) = (1,1,1,1)  // caustics color 1 and 2
        _CausticColor2("_CausticColor2",Color) = (1,1,1,1)

    }

    SubShader
    {
        Tags { 
            "Queue" = "Transparent" //change queue to transparent
        }

        Pass{
  

            HLSLPROGRAM                             
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"                
            #include "Lighting.cginc"

            // decelear shader variable
            float _FoamRange;
            float4 _ShallowColor;
            float4 _DeepColor;
            float4 _FoamColor;
            float _FoamSpeed;
            float _DepthRange;
            float4 _WaveFrequencySpeed;
            float _FoamNoise;
            float4 _CausticColor1;
            float4 _CausticColor2;
 

            sampler2D _CameraDepthTexture;
            sampler2D _FoamTex;
            float4 _FoamTex_ST;
            sampler2D _CausticTex;
            float4 _CausticTex_ST;

            // get CPU date to the vertex function
            struct Attributes
            {
                float4 vertex           :POSITION;  // vertex data
                float2 uv               :TEXCOORD0; // uv data
            };
             
            struct Varyings
            {
                float4 vertex           :SV_POSITION;   
                float2 uv               :TEXCOORD0;     
                float3 pos_world        :TEXCOORD1;     
                float4 pos_screen       :TEXCOORD2;     
            };

 


            Varyings vert( Attributes v)
            {
                Varyings o;

                //use cos to caculate x direction movement
                v.vertex.y += cos(_Time.y * _WaveFrequencySpeed.y +  v.vertex.x) * _WaveFrequencySpeed.x;
                //use sin to caculate y direction movement
                v.vertex.y += sin(_Time.y * _WaveFrequencySpeed.w +  v.vertex.z ) * _WaveFrequencySpeed.z;

                //Convert model vertices in model space to model vertices in clipping space
                o.vertex = UnityObjectToClipPos(v.vertex);

                //Convert model vertices in model space to model vertices in world space
                o.pos_world = mul(unity_ObjectToWorld,v.vertex.xyz);

                //Convert model vertices in model space to model vertices in screen space
                o.pos_screen = ComputeScreenPos(o.vertex);

                //pass the uv value
                o.uv = v.uv;

                return o;
            }
 
            float4 frag(Varyings i):SV_TARGET
            {
  
                //get the deepth and then divided by i.pos_screen.w to get each pixel location
                float2 screenUV = (i.pos_screen.xy)/i.pos_screen.w;
                //get pixel
                half depthScene = LinearEyeDepth(tex2D(_CameraDepthTexture,screenUV));
                //get cooresponding z value
                float surfaceDepth= UNITY_Z_0_FAR_FROM_CLIPSPACE(i.pos_screen.z);
                //get the intersection between scenes and water
                half depth = saturate(depthScene - surfaceDepth);

                //water color
                //the range between shallow color and deep color
                half water_depth = depth * _DepthRange; 
                //distinguish the shallow water and deep water
                float4 waterColor = lerp(_ShallowColor,_DeepColor,water_depth);


                half foamRange = depth * _FoamRange;
                //get foam texture and then adjust foam testure
                half foam_tex = tex2D(_FoamTex,i.pos_world.xz * _FoamTex_ST.xy + _Time.y * _FoamSpeed).r;
                // enhance the contrast of the foam texture
                foam_tex = pow(foam_tex,_FoamNoise);
                //step(a,b):a < b return 1
                half4 foam_color = step(foamRange,foam_tex) * _FoamColor;
                

                //sampled twice for focal dispersion and misaligned by _CausticTex_ST
                half caustic1 = tex2D(_CausticTex,i.uv + _CausticTex_ST.xy).r;
                half caustic2 = tex2D(_CausticTex,i.uv + _CausticTex_ST.zw).r;

                //two times scorched color
                half3 caustic_color1 = caustic1 * _CausticColor1.rgb;
                half3 caustic_color2 = caustic2 * _CausticColor2.rgb;
                

                //combine all
                float3 finalColor = foam_color.rgb + waterColor.rgb + caustic_color1+ caustic_color2;

    
                return float4(finalColor,1);
 
            }
            ENDHLSL 
        }
    }
    FallBack "Packages/com.unity.render-pipelines.universal/FallbackError"
}
