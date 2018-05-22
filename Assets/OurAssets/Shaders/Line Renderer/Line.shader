// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34363,y:33143,varname:node_3138,prsc:2|emission-532-OUT;n:type:ShaderForge.SFN_TexCoord,id:294,x:32127,y:32939,varname:node_294,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:3694,x:33002,y:33035,varname:node_3694,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-1279-OUT;n:type:ShaderForge.SFN_Length,id:5193,x:33159,y:33035,varname:node_5193,prsc:2|IN-3694-OUT;n:type:ShaderForge.SFN_Floor,id:6567,x:33489,y:33035,varname:node_6567,prsc:2|IN-2297-OUT;n:type:ShaderForge.SFN_OneMinus,id:942,x:33649,y:33035,varname:node_942,prsc:2|IN-6567-OUT;n:type:ShaderForge.SFN_Add,id:2297,x:33327,y:33035,varname:node_2297,prsc:2|A-4494-OUT,B-5193-OUT;n:type:ShaderForge.SFN_Slider,id:4494,x:33002,y:32957,ptovrint:False,ptlb:Circle Size,ptin:_CircleSize,varname:node_4494,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4076549,max:1;n:type:ShaderForge.SFN_Time,id:4193,x:32307,y:33303,varname:node_4193,prsc:2;n:type:ShaderForge.SFN_Add,id:5354,x:32668,y:33035,varname:node_5354,prsc:2|A-1065-OUT,B-864-OUT;n:type:ShaderForge.SFN_Multiply,id:864,x:32478,y:33197,varname:node_864,prsc:2|A-3822-OUT,B-4193-T,C-7830-OUT;n:type:ShaderForge.SFN_Vector2,id:3822,x:32307,y:33197,varname:node_3822,prsc:2,v1:1,v2:0;n:type:ShaderForge.SFN_Frac,id:1279,x:32835,y:33035,varname:node_1279,prsc:2|IN-5354-OUT;n:type:ShaderForge.SFN_Multiply,id:532,x:34043,y:33240,varname:node_532,prsc:2|A-1016-OUT,B-7696-RGB,C-1354-OUT;n:type:ShaderForge.SFN_Clamp01,id:1016,x:33808,y:33035,varname:node_1016,prsc:2|IN-942-OUT;n:type:ShaderForge.SFN_Color,id:7696,x:33808,y:33179,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_7696,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:1354,x:33808,y:33347,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_1354,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:1065,x:32499,y:33027,varname:node_1065,prsc:2|A-9151-OUT,B-294-V;n:type:ShaderForge.SFN_Multiply,id:9151,x:32307,y:33051,varname:node_9151,prsc:2|A-294-U,B-7772-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7772,x:32108,y:33139,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_7772,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_ValueProperty,id:7830,x:32290,y:33526,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_7830,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:4494-7696-1354-7772-7830;pass:END;sub:END;*/

Shader "Shader Forge/Line" {
    Properties {
        _CircleSize ("Circle Size", Range(0, 1)) = 0.4076549
        _Colour ("Colour", Color) = (0.5,0.5,0.5,1)
        _Brightness ("Brightness", Float ) = 0
        _Offset ("Offset", Float ) = 5
        _Speed ("Speed", Float ) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _CircleSize;
            uniform float4 _Colour;
            uniform float _Brightness;
            uniform float _Offset;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_4193 = _Time;
                float node_5193 = length((frac((float2((i.uv0.r*_Offset),i.uv0.g)+(float2(1,0)*node_4193.g*_Speed)))*2.0+-1.0));
                float3 emissive = (saturate((1.0 - floor((_CircleSize+node_5193))))*_Colour.rgb*_Brightness);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
