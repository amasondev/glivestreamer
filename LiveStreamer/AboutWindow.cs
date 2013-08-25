using System;

public partial class AboutWindow : Gtk.Dialog
{
	public AboutWindow ()
	{
		this.Build ();
	}

	protected void OnClose (object sender, EventArgs e) {
		this.Destroy();
	}
}

