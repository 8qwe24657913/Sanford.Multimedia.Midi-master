using Sanford.Multimedia.Midi;

namespace SequencerDemo
{
    partial class PlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mIDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMidiFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pianoControl1 = new Sanford.Multimedia.Midi.UI.PianoControl();
            this.sequence1 = new Sanford.Multimedia.Midi.Sequence();
            this.sequencer1 = new Sanford.Multimedia.Midi.Sequencer();
            this.midiInternalClock1 = new Sanford.Multimedia.Midi.MidiInternalClock(this.components);
            this.timeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.toolStripProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.playListBox = new System.Windows.Forms.ListBox();
            this.playButton = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mIDIToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(632, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.loopToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // loopToolStripMenuItem
            // 
            this.loopToolStripMenuItem.Checked = true;
            this.loopToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loopToolStripMenuItem.Name = "loopToolStripMenuItem";
            this.loopToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.loopToolStripMenuItem.Text = "Loop";
            this.loopToolStripMenuItem.Click += new System.EventHandler(this.loopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mIDIToolStripMenuItem
            // 
            this.mIDIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputDeviceToolStripMenuItem});
            this.mIDIToolStripMenuItem.Name = "mIDIToolStripMenuItem";
            this.mIDIToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.mIDIToolStripMenuItem.Text = "&MIDI";
            // 
            // outputDeviceToolStripMenuItem
            // 
            this.outputDeviceToolStripMenuItem.Name = "outputDeviceToolStripMenuItem";
            this.outputDeviceToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.outputDeviceToolStripMenuItem.Text = "Output Device...";
            this.outputDeviceToolStripMenuItem.Click += new System.EventHandler(this.outputDeviceToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // openMidiFileDialog
            // 
            this.openMidiFileDialog.DefaultExt = "mid";
            this.openMidiFileDialog.Filter = "MIDI files|*.mid|All files|*.*";
            this.openMidiFileDialog.Multiselect = true;
            this.openMidiFileDialog.Title = "Open MIDI file";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pianoControl1
            // 
            this.pianoControl1.BackColor = System.Drawing.SystemColors.Control;
            this.pianoControl1.HighNoteID = 109;
            this.pianoControl1.Location = new System.Drawing.Point(13, 94);
            this.pianoControl1.LowNoteID = 21;
            this.pianoControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pianoControl1.Name = "pianoControl1";
            this.pianoControl1.NoteOnColor = System.Drawing.Color.SkyBlue;
            this.pianoControl1.Size = new System.Drawing.Size(620, 79);
            this.pianoControl1.TabIndex = 5;
            this.pianoControl1.Text = "pianoControl1";
            this.pianoControl1.PianoKeyDown += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.pianoControl1_PianoKeyDown);
            this.pianoControl1.PianoKeyUp += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.pianoControl1_PianoKeyUp);
            // 
            // sequence1
            // 
            this.sequence1.Format = 1;
            // 
            // sequencer1
            // 
            this.sequencer1.Position = 0;
            this.sequencer1.Sequence = this.sequence1;
            this.sequencer1.PlayingCompleted += new System.EventHandler(this.HandlePlayingCompleted);
            this.sequencer1.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.HandleChannelMessagePlayed);
            this.sequencer1.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.HandleSysExMessagePlayed);
            this.sequencer1.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.HandleChased);
            this.sequencer1.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.HandleStopped);
            // 
            // midiInternalClock1
            // 
            this.midiInternalClock1.Ppqn = 24;
            this.midiInternalClock1.Tempo = 500000;
            // 
            // trackBar
            // 
            this.trackBar.Enabled = false;
            this.trackBar.Location = new System.Drawing.Point(42, 40);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(600, 56);
            this.trackBar.TabIndex = 9;
            this.trackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            this.trackBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseMove);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripProgressBar1.Location = new System.Drawing.Point(192, 0);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(440, 28);
            this.toolStripProgressBar1.TabIndex = 10;
            this.toolStripProgressBar1.Visible = false;
            // 
            // playListBox
            // 
            this.playListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playListBox.Font = new System.Drawing.Font("ËÎÌו", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.playListBox.FormattingEnabled = true;
            this.playListBox.ItemHeight = 20;
            this.playListBox.Location = new System.Drawing.Point(13, 180);
            this.playListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playListBox.Name = "playListBox";
            this.playListBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.playListBox.Size = new System.Drawing.Size(620, 362);
            this.playListBox.TabIndex = 11;
            this.playListBox.Visible = false;
            this.playListBox.SelectedValueChanged += new System.EventHandler(this.playListBox_SelectedValueChanged);
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.Transparent;
            this.playButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playButton.Image = global::SequencerDemo.Properties.Resources.play;
            this.playButton.Location = new System.Drawing.Point(13, 40);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(23, 23);
            this.playButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playButton.TabIndex = 8;
            this.playButton.TabStop = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // PlayerForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(632, 183);
            this.Controls.Add(this.playListBox);
            this.Controls.Add(this.toolStripProgressBar1);
            this.Controls.Add(this.pianoControl1);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(3000, 230);
            this.MinimumSize = new System.Drawing.Size(650, 230);
            this.Name = "PlayerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MIDI Player";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PlayerForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlayerForm_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openMidiFileDialog;
        private Sanford.Multimedia.Midi.UI.PianoControl pianoControl1;
        private System.Windows.Forms.ToolStripMenuItem mIDIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputDeviceToolStripMenuItem;
        private Sequence sequence1;
        private Sequencer sequencer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox playButton;
        private MidiInternalClock midiInternalClock1;
        private System.Windows.Forms.ToolTip timeToolTip;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.ProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem loopToolStripMenuItem;
        private System.Windows.Forms.ListBox playListBox;
    }
}

