using Editor;

namespace NoiseEditor;

[EditorApp( "Noise Editor", "terrain", "For editing landscape noise generator parameters." )]
public class NoiseEditorApp : DockWindow
{
	public NoiseEditorApp()
	{
		DeleteOnClose = true;
		WindowTitle = "Noise Editor";
		MinimumSize = new Vector2( 800, 600 );
		Size = new Vector2( 1280, 720 );

		RestoreDefaultDockLayout();
	}

	protected override void RestoreDefaultDockLayout()
	{
		var noiseTextureView = new NoiseTextureView();
		var property = new LayerProperties();
		var layerList = new LayerList();

		DockManager.Clear();

		DockManager.RegisterDockType( "LayerList", "layers", () => new LayerList() );

		DockManager.AddDock( null, noiseTextureView, DockArea.Left, default, 0.5f );
		DockManager.AddDock( null, layerList, DockArea.Right, default, 0.35f );
		DockManager.AddDock( noiseTextureView, property, DockArea.BottomOuter, default, 0.3f );

		DockManager.Update();
	}
}
