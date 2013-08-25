using System;
using Gtk;

public partial class AddStreamWindow : Gtk.Dialog
{
	protected ListStore mainList;
	public AddStreamWindow (ListStore streamListStore)
	{
		this.Build ();
		mainList = streamListStore;
	}
	
	protected void OkAdd (object sender, EventArgs e)
	{
		string streamUser = streamUserEntry.Text;
		string streamType = streamTypeCombo.ActiveText;
		if (streamType == "URL" || streamUser.StartsWith("http") || streamUser.StartsWith("www.")) {
			Console.WriteLine ("Adding " + streamUser);
			mainList.AppendValues(streamUser, "URL");
		} else {
			Console.WriteLine ("Adding " + streamUser + " " + streamTypeCombo.ActiveText);
			mainList.AppendValues(streamUser, streamType);
		}
		MainWindow.SaveList(mainList, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "/gLiveStreamer/");
		this.Destroy();
	}

	protected void Cancel (object sender, EventArgs e)
	{
		this.Destroy();
	}
}