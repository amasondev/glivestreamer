using System;
using System.IO;
using Gtk;

public partial class EditPlayerWindow : Gtk.Dialog
{
	protected ComboBox mediaPlayerCombo;
	protected ListStore store;
	public EditPlayerWindow (ComboBox cmbo)
	{
		this.Build ();
		mediaPlayerCombo = cmbo;
		CellRenderer[] cells = mediaPlayerCombo.Cells;
		TreeViewColumn col1 = new TreeViewColumn("Player", new Gtk.CellRendererText(), "text", 0);
		playerTreeView.AppendColumn(col1);
		col1.AddAttribute(cells[0], "text", 0);
		store = new ListStore(typeof(string));
		for (int i = 0; i < mediaPlayerCombo.Model.IterNChildren(); i++){
			TreeIter iter;
			mediaPlayerCombo.Model.GetIterFromString(out iter, i.ToString());
			store.AppendValues(mediaPlayerCombo.Model.GetValue(iter, 0));
		}
		playerTreeView.Model = store;
	}

	protected void SelectedPlayer (object sender, EventArgs e)
	{
		store.AppendValues (browseButton.Filename);
	}

	protected void OnRemove (object sender, EventArgs e)
	{
		TreeIter iter;
		TreeModel model;
		if (playerTreeView.Selection.GetSelected (out model, out iter)) {
			store.Remove (ref iter);
		}
	}	

	protected void EnterCommand (object sender, EventArgs e)
	{
		CommandEntryWindow diag = new CommandEntryWindow(store);
		diag.Show ();
	}

	protected void OnOK (object sender, EventArgs e)
	{
		// Clear media player list
		for (int j = 0; j != mediaPlayerCombo.Model.IterNChildren(); ) {
			Console.WriteLine("Removing a line");
			mediaPlayerCombo.RemoveText(0);
		}

		// Add media players to list
		for (int i = 0; i < store.IterNChildren(); i++) {
			TreeIter iter;
			string playerValue;
			// Get strings from list
			store.GetIterFromString(out iter, i.ToString());
			playerValue = store.GetValue(iter, 0).ToString();

			// Append strings to combo box
			Console.WriteLine("Adding " + playerValue);
			mediaPlayerCombo.AppendText(playerValue);

			// Select first player in combo box
			TreeIter newSelect;
			mediaPlayerCombo.Model.GetIterFirst(out newSelect);
			mediaPlayerCombo.SetActiveIter(newSelect);
		}
		this.Destroy();
	}	

	protected void OnCancel (object sender, EventArgs e)
	{
		this.Destroy();
	}


}

