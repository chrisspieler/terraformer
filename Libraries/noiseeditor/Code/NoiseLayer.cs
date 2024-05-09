namespace Sandbox;

public class NoiseLayer
{
	/// <summary>
	/// Specifies which operation will be used to blend this layer with the one below.
	/// </summary>
	public NoiseBlendMode BlendMode { get; set; }
	public float Strength { get; set; }
}
