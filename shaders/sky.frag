#version 330

uniform vec3 groundColor;
uniform vec3 topColor;
uniform vec3 horizonColor;

in vec2 v_position;

// Output to the screen
out vec4 p3d_FragColor;

void main() {
    // Calculate the vertical gradient
    float gradient = clamp(v_position.y, 0.0, 1.0);

    // Interpolate between the top color and horizon color based on the gradient
    vec3 skyColor = mix(topColor, horizonColor, gradient);

    // Calculate the final fragment color by blending the ground color and sky color
    p3d_FragColor = vec4(mix(groundColor, skyColor, gradient), 1.0);
}
