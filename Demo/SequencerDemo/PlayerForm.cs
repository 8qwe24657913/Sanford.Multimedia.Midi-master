using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace SequencerDemo {
    public partial class PlayerForm : Form {

        private bool _playing = false;
        private bool _loaded = false;
        private bool AutoStart = false;
        private bool AutoLoop = true;
        private bool Loaded {
            get {
                return _loaded;
            }
            set {
                _loaded = value;
                trackBar.Enabled = value;
            }
        }

        private bool playing {
            get {
                return _playing;
            }
            set {
                _playing = value;
                playButton.Image = value ? Properties.Resources.pause : Properties.Resources.play;
                if (value) { // fix the bug that key(s) don't release when pressed key(s) then play
                    foreach (var key in pressedKeys) {
                        pianoControl1.ReleasePianoKey(key);
                    }
                    pressedKeys.Clear();
                }
            }
        }
        private BindingList<string> playList = new BindingList<string>();
        private int _playIndex = -1;
        private int playIndex {
            get {
                return _playIndex;
            }
            set {
                _playIndex = value;
                if (value == -1) return;
                Action<int> open = index => {
                    if (playListBox.SelectedIndex != index) playListBox.SelectedIndex = index;
                    Open(playList[index]);
                };
                this.BeginInvoke(open, value);
            }
        }
        private string currentFileName;

        private bool closing = false;

        private OutputDevice outDevice;

        private int outDeviceID = 0;

        private const int defaultHeight = 190;
        public PlayerForm() {
            InitializeComponent();
            ResizeParts();
            playListBox.DataSource = playList;
            clock = BlackMagic.GetClock(sequencer1);
            Height = defaultHeight;
        }
        private string lastTip = String.Empty;
        private MidiInternalClock clock;
        private long musicLength = 0;
        private TimeSpan musicTimeSpan;

        private void trackBar_MouseMove(object sender, MouseEventArgs e) {
            if (!Loaded) return;
            var X = Math.Min(Math.Max(e.X, 0), trackBar.Size.Width);
            var now = makeTimeSpan(musicLength * X / trackBar.Size.Width);
            var nextTip = $"{now}/{musicTimeSpan}";
            if (lastTip != nextTip) {
                lastTip = nextTip;
                timeToolTip.SetToolTip(trackBar, nextTip);
            }
        }
        private const long MIN_TIME_SPAN = (long)1e7; // at least 1s
        private static TimeSpan makeTimeSpan(long time) { // time in 100 nanosecond
            var remainder = time % MIN_TIME_SPAN;
            if (remainder > 0) time += MIN_TIME_SPAN - remainder;
            return new TimeSpan(time);
        }
        SkinForm df;
        protected override void OnLoad(EventArgs e) {
            if (OutputDevice.DeviceCount == 0) {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Close();
            } else {
                try {
                    outDevice = new OutputDevice(outDeviceID);
                    sequence1.LoadProgressChanged += HandleLoadProgressChanged;
                    sequence1.LoadCompleted += HandleLoadCompleted;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Close();
                }
            }
            //DoubleBuffered = true;
            TransparencyKey = menuStrip1.BackColor = playListBox.BackColor = BackColor = Color.FromArgb(unchecked((int)0xFFFEFEFF));
            df = new SkinForm(this);
            df.Opacity = 0.5;
            df.Show();
            df.DragEnter += PlayerForm_DragEnter;
            df.DragDrop += PlayerForm_DragDrop;

            base.OnLoad(e);
        }
        List<Keys> pressedKeys = new List<Keys>();
        protected override void OnKeyDown(KeyEventArgs e) {
            if (!playing) {
                pressedKeys.Add(e.KeyCode);
                pianoControl1.PressPianoKey(e.KeyCode);
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            if (!playing) {
                pianoControl1.ReleasePianoKey(e.KeyCode);
            }

            base.OnKeyUp(e);
        }

        protected override void OnClosing(CancelEventArgs e) {
            closing = true;

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            sequence1.Dispose();

            if (outDevice != null) {
                outDevice.Dispose();

                base.OnClosed(e);
            }
        }

        private void ResizeParts() {
            trackBar.Width = Width - 50;
            pianoControl1.Width = Width - 35;
            playListBox.Width = Width - 35;
            toolStripProgressBar1.Width = Width - 110;
        }

        protected override void OnResize(EventArgs e) {
            ResizeParts();
            base.OnResize(e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            LoadFile();
        }
        private bool supressSwitch = false;
        private void SetFileList(string[] list) {
            supressSwitch = true;
            playList.Clear();
            foreach (var file in list) {
                playList.Add(file);
            }
            playListBox.Visible = playList.Count > 1;
            playListBox.Height = playListBox.PreferredHeight;
            var height = defaultHeight + (playListBox.Visible ? playListBox.Height : 0);
            MinimumSize = new Size(MinimumSize.Width, height);
            MaximumSize = new Size(MaximumSize.Width, height);
            Height = height;
            supressSwitch = false;
            playIndex = 0;
        }
        private static Predicate<string> fileFilter = path => Path.GetExtension(path) == ".mid";
        private static string[] FilterFile(string[] files) {
            return new List<string>(files).FindAll(fileFilter).ToArray();
        }
        private void LoadFile() {
            if (openMidiFileDialog.ShowDialog() == DialogResult.OK) {
                var files = FilterFile(openMidiFileDialog.FileNames);
                if (files.Length > 0) {
                    SetFileList(files);
                }
            }
        }

        private bool Opening = false;
        public void Open(string fileName) {
            if (Opening) return;
            try {
                Opening = true;
                sequencer1.Stop();
                sequence1.Clear();
                playing = false;
                Loaded = false;
                toolStripProgressBar1.Visible = true;
                currentFileName = Path.GetFileName(fileName);
                sequence1.LoadAsync(fileName);
                this.Cursor = Cursors.WaitCursor;
                openToolStripMenuItem.Enabled = false;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void outputDeviceToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new OutputDeviceDialog()) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    outDeviceID = dlg.OutputDeviceID;
                }
            }
        }

        private static void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new AboutDialog()) {
                dlg.ShowDialog();
            }
        }

        private void playButton_Click(object sender, EventArgs e) {
            PlayStateChange();
        }

        private void PlayStateChange() {
            try {
                if (toolStripProgressBar1.Visible) return; // loading
                if (!Loaded) { // have no file selected
                    AutoStart = true;
                    LoadFile();
                    return;
                }
                if (playing) {
                    playing = false;
                    sequencer1.Stop();
                    timer1.Stop();
                } else {
                    playing = true;
                    sequencer1.Continue();
                    timer1.Start();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e) {
            sequencer1.Position = trackBar.Value;
        }

        private void HandleLoadProgressChanged(object sender, ProgressChangedEventArgs e) {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }
        private List<TempoLogger> tempos = new List<TempoLogger>();
        private TempoChangeBuilder tempoChangeBuilder = new TempoChangeBuilder();
        private const long timeRatio = 10L;
        private const int DefaultTempo = 400000;
        private void CalculateTime() {
            tempos.Clear();
            tempos.Add(new TempoLogger(DefaultTempo, 0));
            foreach (var track in sequence1) {
                foreach (var evt in track.Iterator()) {
                    if (evt.MidiMessage is MetaMessage message) {
                        if (message.MetaType == MetaType.Tempo) {
                            tempoChangeBuilder.Initialize(message);
                            var tempo = tempoChangeBuilder.Tempo;
                            tempos.Add(new TempoLogger(tempo, evt.AbsoluteTicks));
                        }
                    }
                }
            }
            tempos.Sort();
            long time = 0;
            var last = tempos[0];
            foreach (var log in tempos) {
                time += timeRatio * (log.Tick - last.Tick) * last.Tempo;
                log.TimeSum = time;
                last = log;
            }
            time += timeRatio * (sequence1.GetLength() - last.Tick) * last.Tempo;
            musicLength = time / sequence1.Division;
            musicTimeSpan = makeTimeSpan(musicLength);
        }
        private long Ticks2Time(int ticks) {
            var index = tempos.BinarySearch(new TempoLogger(DefaultTempo, ticks));
            if (index < 0) {
                index = (~index) - 1;
            }
            var tempo = tempos[index];
            return (tempo.TimeSum + timeRatio * (ticks - tempo.Tick) * tempo.Tempo) / sequence1.Division;
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e) {
            Opening = false;
            this.Cursor = Cursors.Arrow;
            openToolStripMenuItem.Enabled = true;
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Value = 0;
            if (e.Error == null) {
                Loaded = true;
                trackBar.Value = 0;
                trackBar.Maximum = sequence1.GetLength();
                clock.Tempo = DefaultTempo; // important to set DefaultTempo back before play
                CalculateTime();
                if (AutoStart) {
                    PlayStateChange();
                }
            } else {
                MessageBox.Show(e.Error.Message);
            }
            AutoStart = false;
        }

        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e) {
            if (closing) {
                return;
            }

            outDevice.Send(e.Message);
            pianoControl1.Send(e.Message);
        }

        private void HandleChased(object sender, ChasedEventArgs e) {
            foreach (ChannelMessage message in e.Messages) {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e) {
            //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e) {
            foreach (ChannelMessage message in e.Messages) {
                outDevice.Send(message);
                pianoControl1.Send(message);
            }
        }

        private void HandlePlayingCompleted(object sender, EventArgs e) {
            if (AutoLoop && playList.Count == 1) {
                //BlackMagic.ResetSequencer(sequencer1);
                Action<int> action = (i) => sequencer1.Start();
                this.BeginInvoke(action, 0);
                return;
            }
            timer1.Stop();
            playing = false;
            if (AutoLoop || (playIndex + 1) < playList.Count) {
                AutoStart = true;
                playIndex = (playIndex + 1) % playList.Count;
            }
        }

        private void pianoControl1_PianoKeyDown(object sender, PianoKeyEventArgs e) {
            #region Guard

            if (playing) {
                return;
            }

            #endregion

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }

        private void pianoControl1_PianoKeyUp(object sender, PianoKeyEventArgs e) {
            #region Guard

            if (playing) {
                return;
            }

            #endregion

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }
        private void timer1_Tick(object sender, EventArgs e) {
            trackBar.Value = Math.Min(sequencer1.Position, trackBar.Maximum);
            var now = makeTimeSpan(Ticks2Time(sequencer1.Position));
            this.Text = $"{now}/{musicTimeSpan} - {currentFileName} - MIDI Player";
        }

        private void loopToolStripMenuItem_Click(object sender, EventArgs e) {
            loopToolStripMenuItem.Checked = !loopToolStripMenuItem.Checked;
            AutoLoop = loopToolStripMenuItem.Checked;
        }

        private void playListBox_SelectedValueChanged(object sender, EventArgs e) {
            if (!supressSwitch && _loaded) {
                AutoStart = true;
                playIndex = playListBox.SelectedIndex;
            } else {
                playListBox.SelectedIndex = playIndex;
            }
        }
        private void PlayerForm_DragEnter(object sender, DragEventArgs e) {
            var allow = false;
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                var filePath = e.Data.GetData(DataFormats.FileDrop) as string[];
                allow = new List<string>(e.Data.GetData(DataFormats.FileDrop) as string[]).Find(fileFilter) != null;
            }
            e.Effect = allow ? DragDropEffects.Copy : DragDropEffects.None;
        }
        private void PlayerForm_DragDrop(object sender, DragEventArgs e) {
            var filePath = e.Data.GetData(DataFormats.FileDrop) as string[];
            AutoStart = true;
            SetFileList(FilterFile(filePath));
        }
    }
}
