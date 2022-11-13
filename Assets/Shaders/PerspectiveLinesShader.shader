Shader "4D/PerspectiveShader for Lines"

{
    Properties
    {
        _Color("Color", Vector) = (0, 0, 0, 0)

        // How far objects are seen through fog, please don't set it to 0
        _FogAttenuationDistance("Fog Attenuation Distance", float) = 1

        // Global position of the mesh (of the local zero coordinate)
        _Position("Position", Vector) = (0, 0, 0, 0)

        // Rotation
        _RotationXy("Rotation XY", float) = 0
        _RotationXz("Rotation XZ", float) = 0
        _RotationXw("Rotation XW", float) = 0
        _RotationYz("Rotation YZ", float) = 0
        _RotationYw("Rotation YW", float) = 0
        _RotationZw("Rotation ZW", float) = 0

        // Camera
        _CameraPosition("Camera Position", Vector) = (0,0,0,0)
        _CameraRotationXy("Camera Rotation XY", float) = 0
        _CameraRotationXz("Camera Rotation XZ", float) = 0
        _CameraRotationXw("Camera Rotation XW", float) = 0
        _CameraRotationYz("Camera Rotation YZ", float) = 0
        _CameraRotationYw("Camera Rotation YW", float) = 0
        _CameraRotationZw("Camera Rotation ZW", float) = 0
        _ViewportDistance("Viewport Distance", float) = 0
        _CameraForward("Camera Forward", Vector) = (0,0,0,0)

        // Debug
        _Offset("Offset", Vector) = (0,0,0,0)
        _EnableGeometryShader("Enable Geometry Shader", float) = 0
    }
        SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull front
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // definition in PerspectiveShaderBase.hlsl
            #pragma geometry geom
            #pragma fragment frag // definition in PerspectiveShaderBase.hlsl

            #include "UnityCG.cginc"
            #include "PerspectiveShaderBase.hlsl"

            [maxvertexcount(8)]
            void geom(line v2g i[2], inout LineStream<g2f> outPositions)
            {
                bool shouldRender1 = i[0].shouldRender;
                bool shouldRender2 = i[1].shouldRender;

                if (!shouldRender1 ||
                    !shouldRender2)
                { // some vertex/vertices should not be rendered
                    return;
                }

                // Pass a line
                g2f out1;
                g2f out2;

                out1.position = i[0].vertex;
                out2.position = i[1].vertex;
                out1.opacity = i[0].opacity;
                out2.opacity = i[1].opacity;

                outPositions.Append(out1);
                outPositions.Append(out2);
            }

            ENDCG
        }
    }
}
