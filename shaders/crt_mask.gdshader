shader_type canvas_item;

render_mode blend_mul;

uniform float strength : hint_range(0.0, 1.0, 0.1) = 0.5;
uniform sampler2D mask_texture: source_color, hint_default_white, filter_linear, repeat_enable;

void fragment() {
	
	// * vec2(0.1) so the texture doesn't appear smaller than a single pixel
	vec2 mask_uv = FRAGCOORD.xy * vec2(0.1);
	
	vec4 sample = texture(mask_texture, mask_uv);
	COLOR = mix(vec4(1.0), sample, strength);

}
