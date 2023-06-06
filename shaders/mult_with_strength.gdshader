shader_type canvas_item;

render_mode blend_mul;

uniform float strength : hint_range(0.0, 1.0, 0.1) = 0.5;

void fragment() {
	vec4 sample = texture(TEXTURE, UV);
	COLOR = mix(vec4(1.0), sample, strength);
}
