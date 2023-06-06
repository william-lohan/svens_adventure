shader_type canvas_item;

render_mode blend_mul;

uniform float strength : hint_range(0.0, 1.0, 0.1) = 0.5;
uniform float height : hint_range(1, 10, 1) = 3;

void fragment() {
	
	// / SCREEN_PIXEL_SIZE.y needed?
	float y_pixel = FRAGCOORD.y; // / SCREEN_PIXEL_SIZE.y;
	
	float scan_strength = mod(y_pixel, height);
	
	COLOR = mix(vec4(1.0), vec4(scan_strength, scan_strength, scan_strength, 1.0), strength);

}
