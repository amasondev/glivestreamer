using System;
using Gtk;

public partial class CommandEntryWindow : Gtk.Dialog
{
	protected ListStore store;
	public CommandEntryWindow (ListStore store)
	{
		this.Build ();
		this.store = store;
	}
	protected void OnOk (object sender, EventArgs e)
	{
		store.AppendValues(cmdEntry.Text);
		this.Destroy();
	}	

	protected void OnCancel (object sender, EventArgs e)
	{
		this.Destroy();
	}


}