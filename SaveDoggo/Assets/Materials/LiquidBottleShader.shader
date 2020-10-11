// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/LiquidBottleShader"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        [HDR]_SurfaceColor ("Liquid Surface Color", Color) = (1,1,1,1)
        _GlassColor ("Glass Color", Color) = (1,1,1,1)
        _GlassTransparency ("Glass Transparency", Float) = 0.1
        //_SubColor ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _FillAmount ("Fill Amount", Range(-2,2)) = 0.3
        _RimIntensity ("Rim Intensity", Float) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        //LOD 200
        Zwrite On
        Cull Back // we want the front and back faces
        //AlphaToMask On
        //Blend One OneMinusSrcAlpha

        Pass
        {
            Name "LiquidBackFace"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
 
                struct v2f {
                    float4 pos          : POSITION;
                    float4 screenPos    : TEXCOORD0;
                    float4 worldPos     : TEXCOORD1;
                };
 
                sampler2D _CameraDepthTexture;
                //float4 _DepthColor;
                //float _WaterDepth;
                float _FillAmount;
                float3 _SurfaceColor;

                v2f vert (appdata_full v)
                {
                    v2f o;
                    //float3x3 scaleDown = float3x3(0.8, 0.0, 0.0, 0.0, 0.8, 0.0, 0.0, 0.0, 0.8);
                    //float3 tempvertex = v.vertex;
                    //tempvertex = v.vertex + v. * 0.2; 
                    //float3 tempvertex = v.vertex;//mul(scaleDown, v.vertex);
                    o.pos = UnityObjectToClipPos(v.vertex); 
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    o.screenPos = ComputeScreenPos(o.pos);
                    return o;
                }
 

 
                half4 frag(v2f i) : COLOR
                {
                    float4 worldBase = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0));
                    float height = (i.worldPos - worldBase).y;
                    if(height < _FillAmount){
                        return float4(_SurfaceColor,1.0);
                    }

                    //float depth = 1 - saturate(_WaterDepth - (LinearEyeDepth(tex2D(_CameraDepthTexture, i.screenPos.xy / i.screenPos.w).r) - i.screenPos.z));
                    return float4(1.0,1.0,1.0,0.0);
                }
                ENDCG
        }
        CGPROGRAM
        #pragma surface surf Standard alpha
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 viewDir;
            float4 vertex : POSITION;
            float3 worldPos : POSITION;
            float3 normal : NORMAL;
            fixed facing : VFACE;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _FillAmount;
        float3 _GlassColor;
        float _GlassTransparency;
        float _RimIntensity;
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed facing = IN.facing;
            float viewDotNormal = clamp(dot(normalize(IN.viewDir), o.Normal), 0, 1);
            half rim = 0.0;
            if(facing > 0){
                rim = 1.0 - viewDotNormal;
            }
            float3 objectPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0,0,0,1));
            if(objectPos.y < _FillAmount){
                o.Alpha = 1.0;
                o.Albedo = _Color * _GlassColor;
                o.Emission += pow(rim, _RimIntensity) * _Color;
                if(objectPos.y > _FillAmount - 0.003){
                    o.Albedo = _Color + 0.20;  
                }

            }
            else{
                o.Albedo = _GlassColor;
                o.Alpha = rim + _GlassTransparency;
                o.Emission += pow(rim, _RimIntensity) * 2;
            }
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
