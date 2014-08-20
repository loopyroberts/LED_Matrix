using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using Un4seen.Bass.AddOn.Tags;

namespace LED_Matrix
{
    public partial class formMain : Form
    {
        #region Gamma table
        byte[] Gamma = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2,
                2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5,
                6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 10, 10, 11, 11,
                11, 12, 12, 13, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18,
                19, 19, 20, 21, 21, 22, 22, 23, 23, 24, 25, 25, 26, 27, 27, 28,
                29, 29, 30, 31, 31, 32, 33, 34, 34, 35, 36, 37, 37, 38, 39, 40,
                40, 41, 42, 43, 44, 45, 46, 46, 47, 48, 49, 50, 51, 52, 53, 54,
                55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70,
                71, 72, 73, 74, 76, 77, 78, 79, 80, 81, 83, 84, 85, 86, 88, 89,
                90, 91, 93, 94, 95, 96, 98, 99,100,102,103,104,106,107,109,110,
                111,113,114,116,117,119,120,121,123,124,126,128,129,131,132,134,
                135,137,138,140,142,143,145,146,148,150,151,153,155,157,158,160,
                162,163,165,167,169,170,172,174,176,178,179,181,183,185,187,189,
                191,193,194,196,198,200,202,204,206,208,210,212,214,216,218,220,
                222,224,227,229,231,233,235,237,239,241,244,246,248,250,252,255
                       };
#endregion
        #region Clock table
        byte[, ,] clockPoints ={{{4,0},	{4,1},	{4,2},	{4,3},	{4,4}},
                                {{5,0},	{4,1},	{4,2},	{4,3},	{4,4}},
                                {{5,0},	{5,1},	{4,2},	{4,3},	{4,4}},
                                {{6,0},	{6,1},	{5,2},	{5,3},	{4,4}},
                                {{7,0},	{6,1},	{5,2},	{5,3},	{4,4}},
                                {{7,0},	{6,1},	{6,2},	{5,3},	{4,4}},
                                {{7,0},	{7,1},	{6,2},	{5,3},	{4,4}},
                                {{8,0},	{7,1},	{6,2},	{5,3},	{4,4}},
                                {{8,1},	{7,1},	{6,2},	{5,3},	{4,4}},
                                {{8,1},	{7,2},	{6,2},	{5,3},	{4,4}},
                                {{8,1},	{7,2},	{6,3},	{5,3},	{4,4}},
                                {{8,2},	{7,2},	{6,3},	{5,3},	{4,4}},
                                {{8,2},	{7,3},	{6,3},	{5,4},	{4,4}},
                                {{8,3},	{7,3},	{6,4},	{5,4},	{4,4}},
                                {{8,3},	{7,4},	{6,4},	{5,4},	{4,4}},
                                {{8,4},	{7,4},	{6,4},	{5,4},	{4,4}},
                                {{8,5},	{7,4},	{6,4},	{5,4},	{4,4}},
                                {{8,5},	{7,5},	{6,4},	{5,4},	{4,4}},
                                {{8,6},	{7,6},	{6,5},	{5,5},	{4,4}},
                                {{8,7},	{7,6},	{6,5},	{5,5},	{4,4}},
                                {{8,7},	{7,6},	{6,6},	{5,5},	{4,4}},
                                {{8,7},	{7,7},	{6,6},	{5,5},	{4,4}},
                                {{8,8},	{7,7},	{6,6},	{5,5},	{4,4}},
                                {{7,8},	{7,7},	{6,6},	{5,5},	{4,4}},
                                {{7,8},	{6,7},	{6,6},	{5,5},	{4,4}},
                                {{7,8},	{6,7},	{5,6},	{5,5},	{4,4}},
                                {{6,8},	{6,7},	{5,6},	{5,5},	{4,4}},
                                {{6,8},	{5,7},	{5,6},	{5,5},	{4,4}},
                                {{5,8},	{5,7},	{4,6},	{4,5},	{4,4}},
                                {{5,8},	{4,7},	{4,6},	{4,5},	{4,4}},
                                {{4,8},	{4,7},	{4,6},	{4,5},	{4,4}},
                                {{3,8},	{4,7},	{4,6},	{4,5},	{4,4}},
                                {{3,8},	{3,7},	{4,6},	{4,5},	{4,4}},
                                {{2,8},	{3,7},	{3,6},	{3,5},	{4,4}},
                                {{2,8},	{2,7},	{3,6},	{3,5},	{4,4}},
                                {{1,8},	{2,7},	{3,6},	{3,5},	{4,4}},
                                {{1,8},	{2,7},	{2,6},	{3,5},	{4,4}},
                                {{1,8},	{1,7},	{2,6},	{3,5},	{4,4}},
                                {{0,8},	{1,7},	{2,6},	{3,5},	{4,4}},
                                {{0,7},	{1,7},	{2,6},	{3,5},	{4,4}},
                                {{0,7},	{1,6},	{2,6},	{3,5},	{4,4}},
                                {{0,7},	{1,6},	{2,5},	{3,5},	{4,4}},
                                {{0,6},	{1,6},	{2,5},	{3,5},	{4,4}},
                                {{0,5},	{1,5},	{2,4},	{3,4},	{4,4}},
                                {{0,5},	{1,4},	{2,4},	{3,4},	{4,4}},
                                {{0,4},	{1,4},	{2,4},	{3,4},	{4,4}},
                                {{0,3},	{1,4},	{2,4},	{3,4},	{4,4}},
                                {{0,3},	{1,3},	{2,4},	{3,4},	{4,4}},
                                {{0,2},	{1,3},	{2,3},	{3,4},	{4,4}},
                                {{0,2},	{1,2},	{2,3},	{3,3},	{4,4}},
                                {{0,1},	{1,2},	{2,3},	{3,3},	{4,4}},
                                {{0,1},	{1,2},	{2,2},	{3,3},	{4,4}},
                                {{0,1},	{1,1},	{2,2},	{3,3},	{4,4}},
                                {{0,0},	{1,1},	{2,2},	{3,3},	{4,4}},
                                {{1,0},	{1,1},	{2,2},	{3,3},	{4,4}},
                                {{1,0},	{2,1},	{2,2},	{3,3},	{4,4}},
                                {{1,0},	{2,1},	{3,2},	{3,3},	{4,4}},
                                {{2,0},	{2,1},	{3,2},	{3,3},	{4,4}},
                                {{3,0},	{3,1},	{4,2},	{4,3},	{4,4}},
                                {{3,0},	{4,1},	{4,2},	{4,3},	{4,4}}};
        #endregion

        Random rand = new Random();
        System.IO.Ports.SerialPort port1;
        Bitmap drawArea;
        bool mouseDown = false;
        bool connected = false;
        Color currentCol, secCol;
        Color[, ,] mat;
        byte timeSeconds = 0;
        byte timeTick = 0;
        List<String> playlist_items = new List<String>();
        int currentFrame, wt, currentStream, frameMax, visNum, visPos, visColNum;
        private int _tickCounter = 0;
        private int _syncer = 0;
        private int _30mslength = 0;
        private int _deviceLatencyMS = 0; // device latency in milliseconds
        private int _deviceLatencyBytes = 0; // device latency in bytes
        private Un4seen.Bass.Misc.WaveForm WF2 = null;
        private SYNCPROC _sync = null;
        private float[] _rmsData;

        public formMain()
        {
            InitializeComponent();
        }
 
        private void formMain_Load(object sender, EventArgs e)
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, this.Handle))
            {
                MessageBox.Show(this, "Bass_Init error!");
                this.Close();
                return;
            }

            wt = 0;
            port1 = new System.IO.Ports.SerialPort();
            currentFrame = 1;
            frameMax = 1;
            visNum = 0;
            visPos = 0;
            visColNum = 0;
            currentCol = Color.Blue;
            secCol = Color.Red;
            mat = new Color[101, 10, 10];
            for (int c = 0; c < 100; c++)
            {
                clearMat();
            }

            updateGfx();
        }

        private void clearMat()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    mat[currentFrame, x, y] = Color.Black;
                }
            }
        }

        private void pictureMain_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            int x, y;
            x = (e.X / 30);
            y = (e.Y / 30);
            if (x > 9) x = 9;
            if (y > 9) y = 9;
            if (e.Button.Equals(MouseButtons.Left))
            {
                mat[currentFrame, x, y] = currentCol;
            }
            else if (e.Button.Equals(MouseButtons.Right))
            {
                mat[currentFrame, x, y] = secCol;
            }
            updateGfx();
        }

        private void pictureMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                int x, y;
                x = (e.X / 30);
                y = (e.Y / 30);
                if (x > 9) x = 9;
                if (y > 9) y = 9;
                if (x < 0) x = 0;
                if (y < 0) y = 0;
                if (e.Button.Equals(MouseButtons.Left))
                {
                    mat[currentFrame, x, y] = currentCol;
                }
                else if (e.Button.Equals(MouseButtons.Right))
                {
                    mat[currentFrame, x, y] = secCol;
                }
                updateGfx();
            }
        }

        private void pictureMain_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void updateGfx()
        {
            drawArea = new Bitmap(pictureMain.Size.Width, pictureMain.Size.Height);
            Graphics g = Graphics.FromImage(drawArea);
            SolidBrush p = new SolidBrush(Color.Black);
            Pen np = new Pen(Color.Gray);
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    p.Color = Color.Blue;
                    p.Color = mat[currentFrame, x, y];
                    g.FillRectangle(p, x * 30, y * 30, 30, 30);
                    g.DrawRectangle(np, x * 30, y * 30, 30, 30);
                }
            }
            pictureMain.Image = drawArea;
        }

        private void chooseColBut_Click(object sender, EventArgs e)
        {
            ColorDialog s = new ColorDialog();
            s.Color = currentCol;
            s.ShowDialog();
            currentCol = s.Color;
            colourBox.BackColor = currentCol;
        }

        private void secColBut_Click(object sender, EventArgs e)
        {
            ColorDialog s = new ColorDialog();
            s.Color = secCol;
            s.ShowDialog();
            secCol = s.Color;
            secColBox.BackColor = secCol;
        }

        private void fBut_Click(object sender, EventArgs e)
        {
            if (currentFrame <= 100)
            {
                currentFrame++;
                if(frameMax<currentFrame)
                {
                    frameMax = currentFrame;
                }
                currLab.Text = (currentFrame).ToString();
                maxLab.Text = (frameMax).ToString();
            }
            updateGfx();
        }

        private void bBut_Click(object sender, EventArgs e)
        {
            if (currentFrame > 1)
            {
                currentFrame--;
                currLab.Text = (currentFrame).ToString();
            }
            updateGfx();
        }

        private void openImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog().Equals(DialogResult.OK))
            {
                Image i = Image.FromFile(fd.FileName);
                Bitmap bmp = new Bitmap(i, 10, 10);
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        mat[currentFrame, x, y] = bmp.GetPixel(x, y);
                    }
                }
                updateGfx();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void connectionBut_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                connected = true;
                port1.BaudRate = Convert.ToInt32(baudBox.Text);
                port1.PortName = portBox.Text;
                port1.Open();
                connectionBut.Text = "Disconnect";
                refreshTimer.Start();
            }
            else
            {
                connected = false;
                connectionBut.Text = "Connect";
                refreshTimer.Stop();
                port1.Close();

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (connected)
            {
                byte[] data = new byte[300];
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        data[299 - (3 * (i * 10 + j))] = Gamma[mat[currentFrame, i, j].B];
                        data[299 - (3 * (i * 10 + j) + 1)] = Gamma[mat[currentFrame, i, j].G];
                        data[299 - (3 * (i * 10 + j) + 2)] = Gamma[mat[currentFrame, i, j].R];
                    }
                }
                port1.Write(data, 0, 300);
            }
        }

        private void playBut_Click(object sender, EventArgs e)
        {
            frameTimer.Start();
        }

        private void startWaveBut_Click(object sender, EventArgs e)
        {
            waveTimer.Start();
            currentFrame = 0;
        }

        private void waveTimer_Tick(object sender, EventArgs e)
        {
            wt++;
            int cR, cG, cB;
            Color c = new Color();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cR = 0;
                    cG = 0;
                    cB = 0;

                    cR += Convert.ToByte(Convert.ToDouble(m1.Value / 100.0) * (Math.Sin((sz1.Value / 100.0) * (i * Math.Sin(o1.Value * Math.PI / 180.0) + j * Math.Cos(o1.Value * Math.PI / 180.0)) + wt * (s1.Value / 100.0) + (p1.Value * Math.PI / 180.0)) + 1) * col1.BackColor.R / 2.0);
                    cR += Convert.ToByte(Convert.ToDouble(m2.Value / 100.0) * (Math.Sin((sz2.Value / 100.0) * (i * Math.Sin(o2.Value * Math.PI / 180.0) + j * Math.Cos(o2.Value * Math.PI / 180.0)) + wt * (s2.Value / 100.0) + (p2.Value * Math.PI / 180.0)) + 1) * col2.BackColor.R / 2.0);
                    cR += Convert.ToByte(Convert.ToDouble(m3.Value / 100.0) * (Math.Sin((sz3.Value / 100.0) * (i * Math.Sin(o3.Value * Math.PI / 180.0) + j * Math.Cos(o3.Value * Math.PI / 180.0)) + wt * (s3.Value / 100.0) + (p3.Value * Math.PI / 180.0)) + 1) * col3.BackColor.R / 2.0);

                    cG += Convert.ToByte(Convert.ToDouble(m1.Value / 100.0) * (Math.Sin((sz1.Value / 100.0) * (i * Math.Sin(o1.Value * Math.PI / 180.0) + j * Math.Cos(o1.Value * Math.PI / 180.0)) + wt * (s1.Value / 100.0) + (p1.Value * Math.PI / 180.0)) + 1) * col1.BackColor.G / 2.0);
                    cG += Convert.ToByte(Convert.ToDouble(m2.Value / 100.0) * (Math.Sin((sz2.Value / 100.0) * (i * Math.Sin(o2.Value * Math.PI / 180.0) + j * Math.Cos(o2.Value * Math.PI / 180.0)) + wt * (s2.Value / 100.0) + (p2.Value * Math.PI / 180.0)) + 1) * col2.BackColor.G / 2.0);
                    cG += Convert.ToByte(Convert.ToDouble(m3.Value / 100.0) * (Math.Sin((sz3.Value / 100.0) * (i * Math.Sin(o3.Value * Math.PI / 180.0) + j * Math.Cos(o3.Value * Math.PI / 180.0)) + wt * (s3.Value / 100.0) + (p3.Value * Math.PI / 180.0)) + 1) * col3.BackColor.G / 2.0);

                    cB += Convert.ToByte(Convert.ToDouble(m1.Value / 100.0) * (Math.Sin((sz1.Value / 100.0) * (i * Math.Sin(o1.Value * Math.PI / 180.0) + j * Math.Cos(o1.Value * Math.PI / 180.0)) + wt * (s1.Value / 100.0) + (p1.Value * Math.PI / 180.0)) + 1) * col1.BackColor.B / 2.0);
                    cB += Convert.ToByte(Convert.ToDouble(m2.Value / 100.0) * (Math.Sin((sz2.Value / 100.0) * (i * Math.Sin(o2.Value * Math.PI / 180.0) + j * Math.Cos(o2.Value * Math.PI / 180.0)) + wt * (s2.Value / 100.0) + (p2.Value * Math.PI / 180.0)) + 1) * col2.BackColor.B / 2.0);
                    cB += Convert.ToByte(Convert.ToDouble(m3.Value / 100.0) * (Math.Sin((sz3.Value / 100.0) * (i * Math.Sin(o3.Value * Math.PI / 180.0) + j * Math.Cos(o3.Value * Math.PI / 180.0)) + wt * (s3.Value / 100.0) + (p3.Value * Math.PI / 180.0)) + 1) * col3.BackColor.B / 2.0);

                    if (cR > 255) { cR = 255; }
                    if (cG > 255) { cG = 255; }
                    if (cB > 255) { cB = 255; }

                    c = Color.FromArgb(cR, cG, cB);
                    mat[0, i, j] = c;
                }
            }
            updateGfx();
        }

        private void stopWaveBut_Click(object sender, EventArgs e)
        {
            waveTimer.Stop();
        }

        private void s1_Scroll(object sender, EventArgs e)
        {

        }

        private void col1_Click(object sender, EventArgs e)
        {
            ColorDialog s = new ColorDialog();
            s.Color = col1.BackColor;
            s.ShowDialog();
            col1.BackColor = s.Color;
        }

        private void col2_Click(object sender, EventArgs e)
        {
            ColorDialog s = new ColorDialog();
            s.Color = col2.BackColor;
            s.ShowDialog();
            col2.BackColor = s.Color;
        }

        private void col3_Click(object sender, EventArgs e)
        {
            ColorDialog s = new ColorDialog();
            s.Color = col3.BackColor;
            s.ShowDialog();
            col3.BackColor = s.Color;
        }
        private void songEnd(int handle, int channel, int data, IntPtr user)
        {
            if (!InvokeRequired)
            {
                if (playList.SelectedIndex != playList.Items.Count - 1)
                {
                    playList.SelectedIndex += 1;
                    musPlay.PerformClick();
                }
            }
            else
            {
                Invoke(new Action<int, int, int, IntPtr>(songEnd), 0, 0, 0, new IntPtr(0));
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void DrawWavePosition(long pos, long len)
        {
            // Note: we might take the latency of the device into account here!
            // so we show the position as heard, not played.
            // That's why we called Bass.Bass_Init with the BASS_DEVICE_LATENCY flag
            // and then used the BASS_INFO structure to get the latency of the device

            if (len == 0 || pos < 0)
            {
                this.pictureBox1.Image = null;
                return;
            }

            Bitmap bitmap = null;
            Graphics g = null;
            Pen p = null;
            double bpp = 0;

            try
            {
                // not zoomed: width = length of stream
                bpp = len / (double)this.pictureBox1.Width;  // bytes per pixel
                // we take the device latency into account
                // Not really needed, but if you have a real slow device, you might need the next line
                // so the BASS_ChannelGetPosition might return a position ahead of what we hear
                pos -= _deviceLatencyBytes;

                p = new Pen(Color.Red);
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bitmap);
                g.Clear(Color.Black);
                int x = (int)Math.Round(pos / bpp);  // position (x) where to draw the line
                g.DrawLine(p, x, 0, x, pictureBox1.Height - 1);
                bitmap.MakeTransparent(Color.Black);
            }
            catch
            {
                bitmap = null;
            }
            finally
            {
                // clean up graphics resources
                if (p != null)
                    p.Dispose();
                if (g != null)
                    g.Dispose();
            }
            this.pictureBox1.Image = bitmap;
        }
        private void DrawWave()
        {
            if (WF2 != null)
                pictureBox1.BackgroundImage = WF2.CreateBitmap(pictureBox1.Width, pictureBox1.Height, -1, -1, true);
            else
                pictureBox1.BackgroundImage = null;
        }
        // zoom helper varibales

        private void GetWaveForm()
        {
            // unzoom...(display the whole wave form)

            // render a wave form
            WF2 = new WaveForm(playlist_items[playList.SelectedIndex], new WAVEFORMPROC(MyWaveFormCallback), this);
            WF2.FrameResolution = 0.01f; // 10ms are nice
            WF2.CallbackFrequency = 2000; // every 30 seconds rendered (3000*10ms=30sec)
            WF2.ColorBackground = Color.WhiteSmoke;
            WF2.ColorLeft = Color.Gainsboro;
            WF2.ColorLeftEnvelope = Color.Gray;
            WF2.ColorRight = Color.LightGray;
            WF2.ColorRightEnvelope = Color.DimGray;
            WF2.ColorMarker = Color.DarkBlue;
            WF2.DrawWaveForm = WaveForm.WAVEFORMDRAWTYPE.Stereo;
            WF2.DrawMarker = WaveForm.MARKERDRAWTYPE.Line | WaveForm.MARKERDRAWTYPE.Name | WaveForm.MARKERDRAWTYPE.NamePositionAlternate;
            WF2.MarkerLength = 0.75f;
            // our playing stream will be in 32-bit float!
            // but here we render with 16-bit (default) - just to demo the WF2.SyncPlayback method
            WF2.RenderStart(true, BASSFlag.BASS_SAMPLE_FLOAT);
        }
        private void MyWaveFormCallback(int framesDone, int framesTotal, TimeSpan elapsedTime, bool finished)
        {
            if (finished)
            {
                Console.WriteLine("Finished rendering in {0}sec.", elapsedTime);
                Console.WriteLine("FramesRendered={0} of {1}", WF2.FramesRendered, WF2.FramesToRender);
                // eg.g use this to save the rendered wave form...
                //WF.WaveFormSaveToFile( @"C:\test.wf" );

                // auto detect silence at beginning and end
                long cuein = 0;
                long cueout = 0;
                WF2.GetCuePoints(ref cuein, ref cueout, -25.0, -42.0, -1, -1);
                WF2.AddMarker("CUE", cuein);
                WF2.AddMarker("END", cueout);
            }
            // will be called during rendering...
            DrawWave();
        }
        private void RMS(int channel, out int peakL, out int peakR)
        {
            float maxL = 0f;
            float maxR = 0f;
            int length = _30mslength; // 30ms window already set at buttonPlay_Click
            int l4 = length / 4; // the number of 32-bit floats required (since length is in bytes!)

            // increase our data buffer as needed
            if (_rmsData == null || _rmsData.Length < l4)
                _rmsData = new float[l4];

            // Note: this is a special mechanism to deal with variable length c-arrays.
            // In fact we just pass the address (reference) to the first array element to the call.
            // However the .Net marshal operation will copy N array elements (so actually fill our float[]).
            // N is determined by the size of our managed array, in this case N=l4
            length = Bass.BASS_ChannelGetData(channel, _rmsData, length);

            l4 = length / 4; // the number of 32-bit floats received

            for (int a = 0; a < l4; a++)
            {
                float absLevel = Math.Abs(_rmsData[a]);
                // decide on L/R channel
                if (a % 2 == 0)
                {
                    // L channel
                    if (absLevel > maxL)
                        maxL = absLevel;
                }
                else
                {
                    // R channel
                    if (absLevel > maxR)
                        maxR = absLevel;
                }
            }

            // limit the maximum peak levels to +6bB = 0xFFFF = 65535
            // the peak levels will be int values, where 32767 = 0dB!
            // and a float value of 1.0 also represents 0db.
            peakL = (int)Math.Round(32767f * maxL) & 0xFFFF;
            peakR = (int)Math.Round(32767f * maxR) & 0xFFFF;
        }

        private void musAdd_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = true;
            op.ShowDialog();
            foreach (string file in op.FileNames)
            {
                playlist_items.Add(file);
                TAG_INFO tag = BassTags.BASS_TAG_GetFromFile(file);
                playList.Items.Add(tag.artist + " - " + tag.title);
            }
        }

        private void musPlay_Click(object sender, EventArgs e)
        {
            frameTimer.Stop();
            waveTimer.Stop();
            GetWaveForm();
            currentFrame = 0;
            musTimer.Stop();
            Bass.BASS_StreamFree(currentStream);
            if (playList.Text != String.Empty)
            {
                // create the stream
                currentStream = Bass.BASS_StreamCreateFile(playlist_items[playList.SelectedIndex], 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);
                if (currentStream != 0)
                {
                    // used in RMS
                    _30mslength = (int)Bass.BASS_ChannelSeconds2Bytes(currentStream, 0.03); // 30ms window
                    // latency from milliseconds to bytes
                    _deviceLatencyBytes = (int)Bass.BASS_ChannelSeconds2Bytes(currentStream, _deviceLatencyMS / 1000.0);

                    if (WF2 != null && WF2.IsRendered)
                    {
                        // make sure playback and wave form are in sync, since
                        // we rended with 16-bit but play here with 32-bit
                        WF2.SyncPlayback(currentStream);

                        long cuein = WF2.GetMarker("CUE");
                        long cueout = WF2.GetMarker("END");

                        int cueinFrame = WF2.Position2Frames(cuein);
                        int cueoutFrame = WF2.Position2Frames(cueout);
                        Console.WriteLine("CueIn at {0}sec.; CueOut at {1}sec.", WF2.Frame2Seconds(cueinFrame), WF2.Frame2Seconds(cueoutFrame));

                        if (cuein >= 0)
                        {
                            Bass.BASS_ChannelSetPosition(currentStream, cuein);
                        }
                        if (cueout >= 0)
                        {
                            Bass.BASS_ChannelRemoveSync(currentStream, _syncer);
                            _syncer = Bass.BASS_ChannelSetSync(currentStream, BASSSync.BASS_SYNC_POS, cueout, _sync, IntPtr.Zero);
                        }
                    }
                }
                if (currentStream != 0 && Bass.BASS_ChannelPlay(currentStream, false))
                {
                    musTimer.Start();
                    // get some channel info
                    BASS_CHANNELINFO info = new BASS_CHANNELINFO();
                    Bass.BASS_ChannelGetInfo(currentStream, info);
                }
                else
                {
                    Console.WriteLine("Error={0}", Bass.BASS_ErrorGetCode());
                }
                updateGfx();
                musTimer.Start();
            }
        }

        private void musPause_Click(object sender, EventArgs e)
        {
            Bass.BASS_ChannelPause(currentStream);
        }

        private void musStop_Click(object sender, EventArgs e)
        {
            Bass.BASS_ChannelPause(currentStream);
            currentStream = Bass.BASS_StreamCreateFile(playlist_items[playList.SelectedIndex], 0L, 0L, BASSFlag.BASS_DEFAULT);
        }

        private void musDel_Click(object sender, EventArgs e)
        {
            
        }

        private void musTimer_Tick(object sender, EventArgs e)
        {
            clearMat();
            if (Bass.BASS_ChannelIsActive(currentStream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                // the stream is still playing...
            }
            else
            {
                // the stream is NOT playing anymore...
                musTimer.Stop();

                DrawWavePosition(-1, -1);


                return;
            }

            // from here on, the stream is for sure playing...
            _tickCounter++;
            long pos = Bass.BASS_ChannelGetPosition(currentStream); // position in bytes
            long len = Bass.BASS_ChannelGetLength(currentStream); // length in bytes

            if (_tickCounter == 5)
            {
                // display the position every 250ms (since timer is 50ms)
                _tickCounter = 0;
                double totaltime = Bass.BASS_ChannelBytes2Seconds(currentStream, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(currentStream, pos); // the elapsed time length
                double remainingtime = totaltime - elapsedtime;
            }

            // display the level bars
            int peakL = 0;
            int peakR = 0;
            // for testing you might also call RMS_2, RMS_3 or RMS_4
            RMS(currentStream, out peakL, out peakR);
            // level to dB
            double dBlevelL = Utils.LevelToDB(peakL, 65535);
            double dBlevelR = Utils.LevelToDB(peakR, 65535);

            // update the wave position
            DrawWavePosition(pos, len);

            BASSActive status = Bass.BASS_ChannelIsActive(currentStream);
            if (currentStream != 0 && status == BASSActive.BASS_ACTIVE_PLAYING)
            {
                leftVol.Value = ((Bass.BASS_ChannelGetLevel(currentStream)) >> 16) & 0xffff;
                rightVol.Value = (Bass.BASS_ChannelGetLevel(currentStream)) & 0xffff;
                switch (visNum)
                {
                    case 0:
                            fftLR();
                            break;
                    case 1:
                            fftOneSided();
                            break;
                    case 2:
                            volLR();
                            break;
                    case 3:
                            fullVis();
                            break;
                    case 4:
                            volFade();
                            break;
                }

                updateGfx();
            }
        }

        private void volFade()
        {
            visPos++;
            byte vol = (byte)(leftVol.Value>>2&0xff);
            for(byte i=0;i<10;i++)
            {
                for(byte j=0;j<10;j++)
                {
                    
                    mat[0, i, j] = ColorFromHSV(visPos/2550.0*360,1,vol/255.0);
                }
            }
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        private void fftLR()
        {
            float[] buf = new float[256];
            float[] bufBinL = new float[5];
            float[] bufBinR = new float[5];
            Bass.BASS_ChannelGetData(currentStream, buf, (int)(BASSData.BASS_DATA_FFT256 | BASSData.BASS_DATA_FFT_INDIVIDUAL));
            buf[2] = 0;
            buf[1] = 0;
            for (byte i = 0; i < 128; i++)
            {
                bufBinL[(int)(i / 25.6)] += buf[i * 2];
                bufBinR[(int)(i / 25.6)] += buf[i * 2 + 1];
            }
            byte[] aval = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (byte i = 0; i < 5; i++)
            {
                byte hL = (byte)(Math.Sqrt(Math.Sqrt(bufBinL[i])) * 10);
                if (hL > 9) hL = 10;
                if (hL <= 0) hL = 0;
                aval[i] = (byte)hL;
                byte hR = (byte)(Math.Sqrt(Math.Sqrt(bufBinR[i])) * 10);
                if (hR > 10) hR = 10;
                aval[(byte)(9 - i)] = hR;
            }
            updateVis(aval);
        }


        private void fftOneSided()
        {
            float[] buf = new float[128];
            float[] bufBin = new float[10];
            Bass.BASS_ChannelGetData(currentStream, buf, (int)(BASSData.BASS_DATA_FFT256));
            buf[2] = 0;
            buf[1] = 0;
            for (byte i = 0; i < 127; i++)
            {
                bufBin[(int)(i / 12.8)] += buf[i];
            }
            byte[] aval = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (byte i = 0; i < 10; i++)
            {
                byte h = (byte)(Math.Sqrt(Math.Sqrt(bufBin[i])) * 10);
                if (h > 9) h = 10;
                if (h <= 0) h = 0;
                aval[i] = (byte)h;
            }
            updateVis(aval);
        }

        private void updateVis(byte[] data)
        {
            switch (visColNum)
            {
                case 0:
                    setHeightRainCol(data);
                    break;
                case 1:
                    setHeightGradCol(data);
                    break;
                case 2:
                    setHeight(data);
                    break;
                case 3:
                    setHeightRainColShift(data);
                    break;
            }
        }

        private void volLR()
        {
            byte lHeight = (byte)(leftVol.Value / 3276.8);
            byte rHeight = (byte)(rightVol.Value / 3276.8);
            if (lHeight > 9) lHeight = 9;
            if (rHeight > 9) rHeight = 9;

            byte lHeightSide, rHeightSide;

            if (lHeight == 0)
            {
                lHeightSide = 0;
            }
            else
            {
                lHeightSide = lHeight++;
            }
            if (rHeight == 0)
            {
                rHeightSide = 0;
            }
            else
            {
                rHeightSide = rHeight++;
            }
            byte[] aval = { lHeightSide, lHeight, lHeight, lHeight, lHeightSide, rHeightSide, rHeight, rHeight, rHeight, rHeightSide };
            updateVis(aval);
        }

        private void fullVis()
        {
            byte val = (byte)(leftVol.Value / 3276.8);
            if (val > 9) val = 9;
            byte[] aval = { val, val, val, val, val, val, val, val, val, val };
            updateVis(aval);
        }
        private void setHeight(byte[] height)
        {
            for (byte j=0;j<10;j++)
            {
                if (height[j] != 255)
                {
                    for (byte i = 0; i < height[j]; i++)
                    {
                        mat[0, j, 9 - i] = currentCol;
                    }
                }
            }
        }
        private void setHeightGradCol(byte[] height)
        {
            byte cR, cG, cB;
            for (byte j = 0; j < 10; j++)
            {
                if (height[j] != 255)
                {
                    for (byte i = 0; i < height[j]; i++)
                    {
                        cR = 0;
                        cG = 0;
                        cB = 0;
                        if (i < 5) { cG = 255; }
                        else { cG = (byte)(255 - (i - 4) * 255 / 5.0); }
                        if (i < 5) { cR = (byte)((i) * 255 / 5.0); }
                        else { cR = 255; }
                        mat[0, j, 9 - i] = Color.FromArgb(cR, cG, cB);
                    }
                }
            }
        }
        private void setHeightRainCol(byte[] height)
        {
            for (byte j=0;j<10;j++)
            {
                if (height[j] != 255)
                {
                    for (byte i = 0; i < height[j]; i++)
                    {
                        mat[0, j, 9 - i] = ColorFromHSV(i * 36, 1, 1);
                    }
                }
            }
        }
        private void setHeightRainColShift(byte[] height)
        {
            visPos++;
            for (byte j = 0; j < 10; j++)
            {
                if (height[j] != 255)
                {
                    for (byte i = 0; i < height[j]; i++)
                    {
                        mat[0, j, 9 - i] = ColorFromHSV(visPos / 2550.0 * 360, 1, 1);
                    }
                }
            }
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (WF2 == null)
                return;

            long pos = WF2.GetBytePositionFromX(e.X, pictureBox1.Width, -1, -1);
            Bass.BASS_ChannelSetPosition(currentStream, pos);
        }

        private void stopBut_Click(object sender, EventArgs e)
        {
            frameTimer.Stop();
        }

        private void frameTimer_Tick(object sender, EventArgs e)
        {
            if(currentFrame<frameMax)
            {
                currentFrame++;
            }
            else
            {
                currentFrame = 1;
            }
            currLab.Text = (currentFrame).ToString();
            updateGfx();
        }

        private void ffBut_Click(object sender, EventArgs e)
        {
            currentFrame = frameMax;
            currLab.Text = (currentFrame).ToString();
            updateGfx();
        }

        private void bbBut_Click(object sender, EventArgs e)
        {
            currentFrame = 1;
            currLab.Text = (currentFrame).ToString();
            updateGfx();
        }

        private void visUp_Click(object sender, EventArgs e)
        {
            visNum++;
        }

        private void visDown_Click(object sender, EventArgs e)
        {
            if(visNum>0) visNum--;
        }

        private void visColChange()
        {
            if (visColNum == 0)
            {
                visChangingColBut.BackgroundImage = Properties.Resources.rgb;
                visChangingColBut.BackgroundImageLayout = ImageLayout.Stretch;
            }
            if (visColNum == 1)
            {

                visChangingColBut.BackgroundImage = Properties.Resources.ryg;
                visChangingColBut.BackgroundImageLayout = ImageLayout.Stretch;
            }
            if (visColNum == 2)
            {
                visChangingColBut.BackgroundImage = null;
                visChangingColBut.BackColor = currentCol;
            }
            if (visColNum == 3)
            {
                visChangingColBut.BackgroundImage = null;
                visChangingColBut.BackColor = ColorFromHSV(visPos / 2550.0 * 360, 1, 1);
            }
        }


        private void visChangingColBut_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                visColNum++;
                if (visColNum > 3) visColNum = 0;
                visColChange();
            }
            else if (e.Button == MouseButtons.Right)
            {
                visColNum = 2;
                ColorDialog s = new ColorDialog();
                s.Color = currentCol;
                s.ShowDialog();
                currentCol = s.Color;
                visColChange();
            }
        }

        private void delBut_Click(object sender, EventArgs e)
        {
            if (frameMax > 1)
            {
                
                for (int frame = currentFrame; frame <= frameMax; frame++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            mat[frame, i, j] = mat[frame + 1, i, j];
                        }
                    }
                }
                frameMax--;
                if (currentFrame > 1) currentFrame--;
                currLab.Text = (currentFrame).ToString();
                maxLab.Text = (frameMax).ToString();
            }
            updateGfx();
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void changeModeBut_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                for (int i = 0; i < 7; i++)
                {
                    port1.Write("d");
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        private void clockStartBut_Click(object sender, EventArgs e)
        {
            waveTimer.Stop();
            musTimer.Stop();
            currentFrame = 0;
            clearMat();
            clockTimer.Start();
            
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            //clearMat();
            timeTick++;
            if (timeTick > 59) timeTick = 0;
            if (timeTick == timeSeconds)
            {
                currentCol = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                timeSeconds++;
                timeTick++;
            }
            if (timeTick > 59) timeTick = 0;
            if (timeSeconds>= 60) timeSeconds= 0;

            for (int i = 0; i < 5; i++)
            {
                byte x, y;
                    x = clockPoints[timeTick, i, 0];
                    y = clockPoints[timeTick, i, 1];
                mat[0, x, y] = currentCol;
            }
            for (int i = 0; i < 5; i++)
            {
                byte x, y;
                if (timeSeconds <= 30)
                {
                    x = clockPoints[timeSeconds, i, 0];
                    y = clockPoints[timeSeconds, i, 1];
                }
                else
                {
                    x = clockPoints[timeSeconds, i, 0];
                    y = clockPoints[timeSeconds, i, 1];
                }
                mat[0, x, y] = Color.White;
            }

            updateGfx();
        }
    }
}
