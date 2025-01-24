Shader "UI/SubtractMask"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            // Write stencil value for the mask (child image)
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
            ColorMask 0 // Disable color output
        }

        Pass
        {
            // Render the main image with stencil subtraction
            Stencil
            {
                Ref 1
                Comp NotEqual
                Pass Keep
            }
            Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex] { combine texture * primary }
        }
    }
}
