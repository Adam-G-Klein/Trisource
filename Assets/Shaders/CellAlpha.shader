Shader "Beset/CellAlpha"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _cellSolid ("Cell Solidness", float) = 0.5
        _minAlpha ("Min Alpha", float) = 0.1
        _membraneThickness ("Membrane Thickness", float)  = 0.2
        _membraneGradient ("Membrane Gradient", float) = 0.5
        [PerRendererData] _nucleusLocation ("Nucleus Location",Vector ) = (0.5,0.5,0,0)
        _nucleusRadius ("Nucleus Radius", float) = 0.2
        _nucleusGradient ("Nucleus Gradient", float) = 0.5
        [PerRendererData] _publicAlpha ("Publicly Setable Alpha", float) = 1

    

        _membraneColor ("Membrane Color", Color) = (1,0,0,1)
        _nucleusColor ("Nucleus Color", Color) = (1,0,0,1)
        _cytoplasmColor ("Cytoplasm Color", Color) = (1,0,0,0.5)
        _isButton ("Is Menu Button", float) = 0

    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Opaque" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off //don't render to depth buffer
        Blend SrcAlpha OneMinusSrcAlpha //traditional transparency

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag addshadow keepalpha
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };
            
            fixed4 _membraneColor;
            fixed4 _nucleusColor;
            fixed4 _cytoplasmColor;
            float _cellSolid;
            float _minAlpha;
            Vector _nucleusLocation;
            float _membraneThickness;
            float _membraneGradient;
            float _nucleusGradient;
            float _nucleusRadius;
            float _publicAlpha;
            float _isButton;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                UNITY_INITIALIZE_OUTPUT(v2f,OUT);
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, IN.texcoord);
                fixed4 _realCenter = fixed4(0.5,0.5,0.5,0);//fixed values of geometric center of cell
                float _centerDist = distance(IN.texcoord, _realCenter.xyz);
                float _nucleusDist = distance(IN.texcoord, _nucleusLocation.xyz);
                if(_nucleusDist < _nucleusRadius){
                    c *= _nucleusColor;
                    c.a = 1 / (_nucleusGradient * _nucleusDist);
                }else if(_centerDist > 1 - _membraneThickness){
                    if(_isButton == 1){
                        c.a = 0;
                    }else{
                        c *= _membraneColor;
                        c.a = 1 / (_membraneGradient * (1-_centerDist));
                    }
                }
                else {


                    c *= _cytoplasmColor;
                    c.a = _minAlpha;
                }
                clamp(c.a, _minAlpha, 1);
                c.a *= _publicAlpha;
                return c;
            }
        ENDCG
        }
    }
}
