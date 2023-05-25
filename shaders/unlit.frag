#version 330

// Uniform inputs
uniform sampler2D p3d_Texture0;
uniform vec4 p3d_ColorScale;

// Input from vertex shader
in vec2 texcoord;

// Output to the screen
out vec4 p3d_FragColor;

void main()
{
    vec4 color = texture(p3d_Texture0, texcoord) * p3d_ColorScale;
    p3d_FragColor = color.rgba;
}
