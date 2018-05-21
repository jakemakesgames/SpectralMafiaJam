// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33648,y:32984,varname:node_3138,prsc:2|emission-7005-OUT;n:type:ShaderForge.SFN_Fresnel,id:5400,x:32959,y:33125,varname:node_5400,prsc:2|EXP-1840-OUT;n:type:ShaderForge.SFN_Slider,id:1840,x:32614,y:33121,ptovrint:False,ptlb:fresnel amount,ptin:_fresnelamount,varname:node_1840,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.223281,max:3;n:type:ShaderForge.SFN_Multiply,id:7005,x:33372,y:33134,varname:node_7005,prsc:2|A-2682-OUT,B-296-RGB,C-3117-OUT;n:type:ShaderForge.SFN_Color,id:296,x:32959,y:33275,ptovrint:False,ptlb:colour,ptin:_colour,varname:node_296,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8042279,c2:0.9191176,c3:0.8858392,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:3117,x:32959,y:33440,ptovrint:False,ptlb:brightness,ptin:_brightness,varname:node_3117,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ConstantClamp,id:2682,x:33170,y:32979,varname:node_2682,prsc:2,min:0.2,max:1|IN-5400-OUT;proporder:1840-296-3117;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _fresnelamount ("fresnel amount", Range(0, 3)) = 1.223281
        _colour ("colour", Color) = (0.8042279,0.9191176,0.8858392,1)
        _brightness ("brightness", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _fresnelamount;
            uniform float4 _colour;
            uniform float _brightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = (clamp(pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnelamount),0.2,1)*_colour.rgb*_brightness);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
