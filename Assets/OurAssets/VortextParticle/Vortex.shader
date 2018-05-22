// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33017,y:32703,varname:node_3138,prsc:2|emission-9186-OUT,alpha-5956-OUT;n:type:ShaderForge.SFN_Tex2d,id:8019,x:32072,y:32808,varname:node_8019,prsc:2,ntxv:0,isnm:False|UVIN-6283-UVOUT,TEX-7592-TEX;n:type:ShaderForge.SFN_Panner,id:6283,x:31858,y:32838,varname:node_6283,prsc:2,spu:0,spv:-1|UVIN-4613-UVOUT;n:type:ShaderForge.SFN_Tex2dAsset,id:7592,x:31570,y:32619,ptovrint:False,ptlb:tex,ptin:_tex,varname:node_7592,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7059,x:32083,y:32999,varname:node_7059,prsc:2,ntxv:0,isnm:False|UVIN-4005-UVOUT,TEX-7592-TEX;n:type:ShaderForge.SFN_TexCoord,id:4613,x:31628,y:32861,varname:node_4613,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:4005,x:31858,y:32999,varname:node_4005,prsc:2,spu:0,spv:-0.5|UVIN-4613-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5956,x:32402,y:32971,varname:node_5956,prsc:2|A-8019-R,B-7059-R,C-6957-OUT;n:type:ShaderForge.SFN_Color,id:3636,x:32454,y:32697,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_3636,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7352941,c2:1,c3:0.9014199,c4:1;n:type:ShaderForge.SFN_Multiply,id:9186,x:32687,y:32774,varname:node_9186,prsc:2|A-3636-RGB,B-2714-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2714,x:32454,y:32866,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_2714,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_TexCoord,id:2195,x:32109,y:33216,varname:node_2195,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:3515,x:32283,y:33216,varname:node_3515,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1.5|IN-2195-V;n:type:ShaderForge.SFN_Clamp01,id:6957,x:32460,y:33216,varname:node_6957,prsc:2|IN-3515-OUT;proporder:7592-3636-2714;pass:END;sub:END;*/

Shader "Shader Forge/Vortex" {
    Properties {
        _tex ("tex", 2D) = "white" {}
        _Colour ("Colour", Color) = (0.7352941,1,0.9014199,1)
        _Brightness ("Brightness", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _tex; uniform float4 _tex_ST;
            uniform float4 _Colour;
            uniform float _Brightness;
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
                float3 emissive = (_Colour.rgb*_Brightness);
                float3 finalColor = emissive;
                float4 node_193 = _Time;
                float2 node_6283 = (i.uv0+node_193.g*float2(0,-1));
                float4 node_8019 = tex2D(_tex,TRANSFORM_TEX(node_6283, _tex));
                float2 node_4005 = (i.uv0+node_193.g*float2(0,-0.5));
                float4 node_7059 = tex2D(_tex,TRANSFORM_TEX(node_4005, _tex));
                return fixed4(finalColor,(node_8019.r*node_7059.r*saturate((i.uv0.g*1.5+0.0))));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
