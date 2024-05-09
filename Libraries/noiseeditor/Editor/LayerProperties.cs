using Editor;

namespace NoiseEditor;

public class LayerProperties : Widget
{
	public LayerProperties()
	{
		Name = "Properties";
		WindowTitle = "Layer 1 Properties";
		MinimumSize = new Vector2( 100, 100 );

		SetWindowIcon( "manage_search" );
	}
}
