public sealed class TerrainGenerator : Component
{
	[Property] public float TerrainChunkSize { get; set; } = 2500f;
	[Property] public GameObject TerrainChunkPrefab { get; set; }
	[Property] public int Size { get; set; } = 3;
	[Property]
	public float RandomSeed
	{
		get => _randomSeed;
		set
		{
			_randomSeed = value;
			_seedWasUsed = false;
		}
	}
	private float _randomSeed;
	private bool _seedWasUsed;

	private List<TerrainChunkGenerator> _generatedTerrain = new();

	protected override void OnStart()
	{
		RegenerateSeed();

		if ( TerrainChunkPrefab is null )
			return;

		GenerateTerrain();
	}

	public void DeleteTerrain()
	{
		foreach ( var chunk in _generatedTerrain.ToList() )
		{
			chunk.GameObject.Destroy();
		}
		for ( int x = 0; x < Size; x++ )
		{
			for ( int y = 0; y < Size; y++ )
			{
				var offset = new Vector3( TerrainChunkSize * x, TerrainChunkSize * y, 0 );
				var position = Transform.Position + offset;
				var go = TerrainChunkPrefab.Clone( position );
				go.BreakFromPrefab();
				var terrainChunkGo = go.Components.GetInDescendantsOrSelf<TerrainChunkGenerator>();
				_generatedTerrain.Add( terrainChunkGo );
			}
		}
		_seedWasUsed = true;
	}

	public void GenerateTerrain()
	{
		DeleteTerrain();

	}

	[Button("Regenerate Seed")]
	public void RegenerateSeed()
	{
		RandomSeed = Game.Random.Float( 0, 1_000_000 );
		TerrainChunkGenerator._noiseSeed = RandomSeed;
	}

	[Button("Regenerate Terrain")]
	public void RegenerateTerrain()
	{
		if ( _seedWasUsed )
		{
			RegenerateSeed();
		}
		foreach( var chunk in _generatedTerrain.ToList() )
		{
			chunk.Generate();
		}
		_seedWasUsed = true;
	}
}
