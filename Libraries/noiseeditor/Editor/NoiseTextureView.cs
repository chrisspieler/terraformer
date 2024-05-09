using Editor;

namespace NoiseEditor;

public class NoiseTextureView : GraphicsView
{
	public NoiseTextureView()
	{
		Name = "NoiseTextureView";
		WindowTitle = "Noise Texture";
		MinimumSize = new Vector2( 100, 100 );

		SetWindowIcon( "texture" );
	}
}
