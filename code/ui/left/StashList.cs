using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;
using System.Linq;

[Library]
public partial class StashList : Panel
{
	VirtualScrollPanel Canvas;

	public StashList()
	{
		AddClass( "spawnpage" );
		AddChild( out Canvas, "canvas" );

		Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemWidth = 100;
		Canvas.Layout.ItemHeight = 100;

		Canvas.OnCreateCell = ( cell, data ) =>
		{
			var file = (string)data;
			var panel = cell.Add.Panel( "icon" );
			panel.AddEventListener( "onclick", () => ConsoleSystem.Run( "spawn", "stashItems/" + file ) );
			panel.Style.BackgroundImage = Texture.Load( FileSystem.Mounted, $"/stashItems/{file}_c.png", false );
		};

		foreach ( var file in FileSystem.Mounted.FindFile( "stashItems", "*.vmdl_c.png", true ) )
		{
			if ( string.IsNullOrWhiteSpace( file ) ) continue;
			if ( file.Contains( "_lod0" ) ) continue;
			if ( file.Contains( "clothes" ) ) continue;

			Canvas.AddItem( file.Remove( file.Length - 6 ) );
		}
	}
}
