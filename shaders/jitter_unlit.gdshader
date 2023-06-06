shader_type spatial;
render_mode cull_disabled, specular_disabled, skip_vertex_transform, shadows_disabled;

uniform sampler2D texture_albedo;

float snap(float value)
{
    float snapScale = 100.0f;
    return floor(value * snapScale)/snapScale;
}

void vertex() {
	VERTEX = (MODELVIEW_MATRIX * vec4(VERTEX, 1.0)).xyz;
    NORMAL = normalize((MODELVIEW_MATRIX * vec4(NORMAL, 0.0)).xyz);
	// jitter
	VERTEX.x = snap(VERTEX.x);
	VERTEX.y = snap(VERTEX.y);
	VERTEX.z = snap(VERTEX.z);
}

void fragment() {
    // Sample the texture using the UV coordinates
    ALBEDO = texture(texture_albedo, UV).rgb;
}

void light() {
    DIFFUSE_LIGHT += ALBEDO * normalize(LIGHT_COLOR) * ATTENUATION;
}
