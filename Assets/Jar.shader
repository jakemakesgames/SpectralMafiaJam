// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:35549,y:32991,varname:node_3138,prsc:2|diff-8369-OUT,spec-8366-OUT,gloss-8927-OUT,alpha-1166-OUT;n:type:ShaderForge.SFN_TexCoord,id:1408,x:34394,y:33117,varname:node_1408,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Round,id:8468,x:34595,y:33127,varname:node_8468,prsc:2|IN-1408-V;n:type:ShaderForge.SFN_ConstantClamp,id:7893,x:34896,y:33236,varname:node_7893,prsc:2,min:0.1,max:1|IN-8468-OUT;n:type:ShaderForge.SFN_Slider,id:9562,x:34640,y:32976,ptovrint:False,ptlb:gloss,ptin:_gloss,varname:node_9562,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5982916,max:1;n:type:ShaderForge.SFN_Multiply,id:8927,x:35268,y:33098,varname:node_8927,prsc:2|A-9562-OUT,B-8934-OUT;n:type:ShaderForge.SFN_OneMinus,id:8934,x:34896,y:33077,varname:node_8934,prsc:2|IN-8468-OUT;n:type:ShaderForge.SFN_Color,id:6422,x:34989,y:32748,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_6422,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:8369,x:35244,y:32895,varname:node_8369,prsc:2|A-6422-RGB,B-4496-OUT,T-8934-OUT;n:type:ShaderForge.SFN_Vector1,id:4496,x:34989,y:32914,varname:node_4496,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:8366,x:35305,y:33026,varname:node_8366,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Fresnel,id:5366,x:34896,y:33390,varname:node_5366,prsc:2|EXP-8361-OUT;n:type:ShaderForge.SFN_Add,id:8600,x:35143,y:33281,varname:node_8600,prsc:2|A-7893-OUT,B-5366-OUT;n:type:ShaderForge.SFN_Slider,id:8361,x:34448,y:33356,ptovrint:False,ptlb:fresnel,ptin:_fresnel,varname:node_8361,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Clamp01,id:1166,x:35316,y:33281,varname:node_1166,prsc:2|IN-8600-OUT;proporder:9562-6422-8361;pass:END;sub:END;*/

Shader "Shader Forge/Jar" {
    Properties {
        _gloss ("gloss", Range(0, 1)) = 0.5982916
        _Colour ("Colour", Color) = (0.5,0.5,0.5,1)
        _fresnel ("fresnel", Range(0, 5)) = 0
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
            Blend One OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _gloss;
            uniform float4 _Colour;
            uniform float _fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_8468 = round(i.uv0.g);
                float node_8934 = (1.0 - node_8468);
                float gloss = (_gloss*node_8934);
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float node_8366 = 0.5;
                float3 specularColor = float3(node_8366,node_8366,node_8366);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_4496 = 1.0;
                float3 diffuseColor = lerp(_Colour.rgb,float3(node_4496,node_4496,node_4496),node_8934);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse * saturate((clamp(node_8468,0.1,1)+pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel))) + specular;
                return fixed4(finalColor,saturate((clamp(node_8468,0.1,1)+pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel))));
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _gloss;
            uniform float4 _Colour;
            uniform float _fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_8468 = round(i.uv0.g);
                float node_8934 = (1.0 - node_8468);
                float gloss = (_gloss*node_8934);
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float node_8366 = 0.5;
                float3 specularColor = float3(node_8366,node_8366,node_8366);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_4496 = 1.0;
                float3 diffuseColor = lerp(_Colour.rgb,float3(node_4496,node_4496,node_4496),node_8934);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse * saturate((clamp(node_8468,0.1,1)+pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel))) + specular;
                return fixed4(finalColor,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
