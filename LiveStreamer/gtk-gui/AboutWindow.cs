
// This file has been generated by the GUI designer. Do not modify.

public partial class AboutWindow
{
	private global::Gtk.Label label1;
	private global::Gtk.Button buttonClose;
	
	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget AboutWindow
		this.Name = "AboutWindow";
		this.TypeHint = ((global::Gdk.WindowTypeHint)(1));
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Internal child AboutWindow.VBox
		global::Gtk.VBox w1 = this.VBox;
		w1.Name = "dialog1_VBox";
		w1.BorderWidth = ((uint)(2));
		// Container child dialog1_VBox.Gtk.Box+BoxChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("gLiveStreamer\nLivestreamer: http://livestreamer.tanuki.se/");
		this.label1.Justify = ((global::Gtk.Justification)(2));
		w1.Add (this.label1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1 [this.label1]));
		w2.Position = 0;
		// Internal child AboutWindow.ActionArea
		global::Gtk.HButtonBox w3 = this.ActionArea;
		w3.Name = "dialog1_ActionArea";
		w3.Spacing = 10;
		w3.BorderWidth = ((uint)(5));
		w3.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
		// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
		this.buttonClose = new global::Gtk.Button ();
		this.buttonClose.CanDefault = true;
		this.buttonClose.CanFocus = true;
		this.buttonClose.Name = "buttonClose";
		this.buttonClose.UseStock = true;
		this.buttonClose.UseUnderline = true;
		this.buttonClose.Label = "gtk-close";
		this.AddActionWidget (this.buttonClose, -7);
		global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w3 [this.buttonClose]));
		w4.Expand = false;
		w4.Fill = false;
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 316;
		this.DefaultHeight = 214;
		this.Show ();
		this.buttonClose.Clicked += new global::System.EventHandler (this.OnClose);
	}
}