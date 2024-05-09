namespace Sandbox;

public enum NoiseBlendMode
{
	Add,
	Subtract
}

public static class NoiseBlendModeExtensions
{
	public static float Blend( this NoiseBlendMode blend, float first, float second )
	{
        return blend switch
        {
            NoiseBlendMode.Add => Add(first, second),
			NoiseBlendMode.Subtract => Subtract(first, second),
			_ => throw new System.Exception( $"Unsupported blend mode: {blend}")
        };
    }

	private static float Add( float first, float second )
	{
		return ( first + second ).Clamp( 0f, 1f );
	}

	private static float Subtract( float first, float second )
	{
		return ( first - second ).Clamp( 0f, 1f );
	}
}
