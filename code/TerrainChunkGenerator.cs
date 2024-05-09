using Sandbox.Utility;
using System;

public sealed class TerrainChunkGenerator : Component
{
	[Property, RequireComponent] 
	public Terrain Terrain { get; set; }
	[Property] public float NoiseSeed => _noiseSeed;
	internal static float _noiseSeed = 0f;
	[Property, Range(0.02f, 1.5f)] public float NoiseScale { get; set; } = 1f;

	protected override void OnStart()
	{
		Terrain ??= Components.Get<Terrain>();
		Generate();
	}

	protected override void DrawGizmos()
	{
		if ( Terrain?.HeightMap is null )
			return;

		Gizmo.Draw.Sprite( new Vector3( 500f, 500f, 700f ), 200f, Terrain.HeightMap );
	}

	[Button( "Regenerate Terrain" )]
	public void Generate()
	{
		var size = 513;
		var worldPos = Transform.Position;
		var texelSpacing = Terrain.TerrainSize / size;
		var data = new ushort[ size * size ];
		for ( int x = 0; x < size; x++ )
		{
			for ( int y = 0; y < size; y++ )
			{
				ushort noise = 0;
				var texPos = worldPos + new Vector3( x, y ) * texelSpacing;
				var coarseNoise = Perlin( texPos.x, texPos.y, NoiseSeed, NoiseScale * 0.1f );
				noise = AddNoise( noise, coarseNoise );
				var valleyNoise = Perlin( texPos.x, texPos.y, NoiseSeed, NoiseScale * 0.01f );
				noise = SubtractNoise( noise, valleyNoise );
				var valleyNoise2 = Perlin( texPos.x, texPos.y, NoiseSeed, NoiseScale * 0.007f );
				noise = SubtractNoise( noise, valleyNoise2 * 0.4f );
				var fineNoise = Perlin( texPos.x, texPos.y, NoiseSeed, NoiseScale * 1f );
				var fineNoise2 = Perlin( texPos.x, texPos.y, NoiseSeed, NoiseScale * 0.4f );
				fineNoise = SubtractNoise( fineNoise, fineNoise2 * 1f );
				noise = AddNoise( noise, fineNoise * 0.02f );
				data[y * size + x] = noise;
			}
		}
		Terrain.HeightMap.Update<ushort>( data, 0, 0, size, size );
		Terrain.DirtyHeightmapRegion( 0, 0, size, size );
	}

	private ushort Perlin( float x, float y, float noiseSeed, float noiseScale )
	{
		var perlin = Noise.Perlin( (x + noiseSeed) * noiseScale, (y + noiseSeed) * noiseScale, 0 );
		return (ushort)perlin.Remap( 0, 1, 0, ushort.MaxValue );
	}

	private ushort AddNoise( float first, float second )
	{
		float sum = first + second;
		return (ushort)sum.Clamp( ushort.MinValue, ushort.MaxValue );
	}

	private ushort SubtractNoise( float first, float second )
	{
		var progress = first.LerpInverse( ushort.MinValue, ushort.MaxValue );
		progress = Easing.SineEaseOut( progress );
		float sum = first - second * progress;
		return (ushort)sum.Clamp( ushort.MinValue, ushort.MaxValue );
	}
}
