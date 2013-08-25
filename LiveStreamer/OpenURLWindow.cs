using System;

public partial class OpenURLWindow : Gtk.Dialog
{
	protected MainWindow mainWindow;
	public OpenURLWindow (MainWindow mainWin)
	{
		this.Build ();
		mainWindow = mainWin;
	}

	protected void OnWatch (object sender, EventArgs e)
	{
		string newURL = urlBox.Text;
		if (!newURL.Contains (" ") && newURL != "") {
			mainWindow.LaunchLivestreamer (newURL);
		} else {
			mainWindow.StatusMessage("Enter a valid URL.");
		}
		this.Destroy();
	}

	protected void OnCancel (object sender, EventArgs e)
	{
		this.Destroy();
	}

}

