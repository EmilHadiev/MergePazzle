Shader "Custom/Mobile Lit"
{
    Properties
    {
        [Header(Base)]
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Tint", Color) = (1, 1, 1, 1)

        [Header(Specular)]
        _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
        _Gloss ("Specular Strength", Range(0, 1)) = 0.25
        _Shininess ("Shininess", Range(0.03, 1)) = 0.3

        [Header(Emission)]
        [HDR] _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1)
        _EmissionMap ("Emission Map", 2D) = "white" {}

        [Header(Rendering)]
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" "PerformanceChecks" = "False" }
        LOD 200
        Cull [_Cull]

        CGPROGRAM
        // BlinnPhong: diffuse + cheap specular, works with directional/point/spot lights,
        // ambient, light probes, lightmaps, fog and shadows. Has a deferred variant too.
        // For even cheaper shading replace "BlinnPhong" with "Lambert" (drops specular).
        #pragma surface surf BlinnPhong addshadow
        // 2.5 keeps the shader compatible with WebGL 1.0 (GLES 2.0) and WebGL 2.0 (GLES 3.0).
        // It is also Unity's default requirement, so this line is mostly documentation.
        #pragma target 2.5

        sampler2D _MainTex;
        sampler2D _EmissionMap;
        fixed4 _Color;
        half4 _EmissionColor;
        half _Gloss;
        half _Shininess;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_EmissionMap;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            o.Albedo = albedo.rgb;
            o.Alpha = 1.0;

            // o.Gloss - specular intensity, o.Specular - sharpness (exponent = Specular * 128)
            o.Gloss = _Gloss;
            o.Specular = _Shininess;

            o.Emission = tex2D(_EmissionMap, IN.uv_EmissionMap).rgb * _EmissionColor.rgb;
        }
        ENDCG
    }

    FallBack "Mobile/Diffuse"
}
