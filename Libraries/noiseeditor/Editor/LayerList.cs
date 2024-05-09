using Editor;

namespace NoiseEditor;

public class LayerList : Widget
{
	public LayerList() : base()
	{

		Name = "LayerList";
		WindowTitle = "Layer List";
		MinimumSize = new Vector2( 100, 100 );

		SetWindowIcon( "layers" );
	}
}
