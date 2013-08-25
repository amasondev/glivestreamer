using System;
using System.Diagnostics;
using System.IO;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	protected Gtk.ListStore streamListStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		SetupColumns();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void AddStream (object sender, EventArgs e)
	{
		AddStreamWindow newDiag = new AddStreamWindow (streamListStore);
		newDiag.Show();
	}

	protected void RemoveStream (object sender, EventArgs e)
	{
		TreeIter iter;
		TreeModel model;
		if (streamList.Selection.GetSelected (out model, out iter)) {
			streamListStore.Remove (ref iter);
		}

		// Save to list file
		SaveList(streamListStore, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "/gLiveStreamer/");
	}

	protected void SetupColumns ()
	{
		TreeViewColumn col1 = new TreeViewColumn("Stream", new Gtk.CellRendererText(), "text", 0);
		col1.Resizable = true;
		streamList.AppendColumn(col1);
		streamList.AppendColumn("Service", new Gtk.CellRendererText(), "text", 1);

		// Read from list file
		streamListStore = LoadList(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "/gLiveStreamer/");
		streamList.Model = streamListStore;
	}

	protected void WatchSelectedStream (object sender, EventArgs e)
	{
		TreeIter iter;
		TreeModel model;
		if (streamList.Selection.CountSelectedRows() == 1) {
			streamList.Selection.GetSelected (out model, out iter);
			string username = model.GetValue (iter, 0).ToString ();
			string type = model.GetValue (iter, 1).ToString ();
			string url;
			string player = mediaPlayerCombo.ActiveText;
			string quality = qualityCombo.ActiveText;

			if (type == "Justin.tv") {
				url = "http://justin.tv/" + username;
				LaunchLivestreamer (url, quality, player);
			} else if (type == "Twitch.tv") {
				url = "http://twitch.tv/" + username;
				LaunchLivestreamer (url, quality, player);
			} else if (type == "Ustream") {
				url = "http://ustream.tv/" + username;
				LaunchLivestreamer (url, quality, player);
			} else if (type == "Livestream.com") {
				url = "http://www.livestream.com/" + username;
				LaunchLivestreamer (url, quality, player);
			} else if (type == "URL") {
				url = username;
				LaunchLivestreamer (url, quality, player);
			}
		}
	}

	protected void LaunchLivestreamer (string url, string quality, string player)
	{
		Process livestreamer = new Process();
		livestreamer.StartInfo.UseShellExecute = false;
		livestreamer.StartInfo.RedirectStandardOutput = true;
		livestreamer.StartInfo.RedirectStandardError = true;
		livestreamer.StartInfo.FileName = "livestreamer";
		livestreamer.StartInfo.Arguments = url + " " + quality + " -p" + player;
		livestreamer.Start ();
		livestreamer.OutputDataReceived += new DataReceivedEventHandler((s, e) => { 

			string cliMessage = e.Data;



			// Send standard output to status bar
			if (!String.IsNullOrEmpty(cliMessage)) {
				while (cliMessage.Contains("]") ) {
					cliMessage = cliMessage.Remove(0,1);
				}
				cliMessage.Trim();
				try {
					stdOutStatus.Pop(1);
					stdOutStatus.Push (1, cliMessage);
					Console.WriteLine(e.Data);
				} catch (Exception ex) {
					Console.WriteLine(ex.ToString());
				}
			}
		
		});

		livestreamer.BeginOutputReadLine();
	}

	public void LaunchLivestreamer (string url)
	{
		string player = mediaPlayerCombo.ActiveText;
		string quality = qualityCombo.ActiveText;

		LaunchLivestreamer(url, quality, player);
	}

	public void StatusMessage (string message)
	{
		stdOutStatus.Pop(1);
		stdOutStatus.Push(1, message);
		Console.WriteLine(message);
	}

	protected void OnQuit (object sender, EventArgs e)
	{
		Application.Quit();
	}

	public static void SaveList (ListStore list, string path)
	{
		path = path + "streams.list";
		File.Delete(path);
		for (int i = 0; i < list.IterNChildren(); i++) {
			TreeIter iter;
			list.GetIterFromString (out iter, i.ToString ());
			File.AppendAllText(path, list.GetValue(iter, 0).ToString());
			File.AppendAllText(path, " ");
			File.AppendAllText(path, list.GetValue(iter, 1).ToString());
			File.AppendAllText(path, "\n");
		}
	}

	public ListStore LoadList (string path)
	{
		ListStore newList = new Gtk.ListStore (typeof(string), typeof(string));

		// List file exists
		if (File.Exists(path + "streams.list")) {
			StreamReader listFile = File.OpenText (path + "streams.list");
			while (!listFile.EndOfStream) {
				string line = listFile.ReadLine ();
				string[] newLine = line.Split(' ');
				newList.AppendValues(newLine[0], newLine[1]);
			}
			return newList;
		} 
		// List file or directory doesn't exist
		else {
			Directory.CreateDirectory(path);
			File.Create(path + "streams.list");
			return newList;
		}
	}

	protected void OpenURL (object sender, EventArgs e)
	{
		OpenURLWindow diag = new OpenURLWindow (this);
		diag.Show ();
	}

	protected void EditPlayers (object sender, EventArgs e)
	{
		EditPlayerWindow diag = new EditPlayerWindow(mediaPlayerCombo);
		diag.Show();
	}
	protected void OnAbout (object sender, EventArgs e)
	{
		AboutWindow diag = new AboutWindow();
		diag.Show();
	}

}
