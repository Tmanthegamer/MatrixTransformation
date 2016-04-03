using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;



namespace asgn5v1
{
    public delegate void UpdateRotateCallback(int direction);
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
	{

        private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer
        const int UP = 0;
        const int DOWN = 1;
        const int LEFT = 2;
        const int RIGHT = 3;
        const int X = 0;
        const int Y = 1;
        const int Z = 2;

        public volatile bool _rotateX = false;
        public volatile bool _rotateY = false;
        public volatile bool _rotateZ = false;

        int numpts = 0;
		int numlines = 0;
        bool gooddata = false;		
		double[,] vertices;
		double[,] scrnpts;
        Thread thread;
        bool alive;
        double[,] ctrans = new double[4,4];  //your main transformation matrix
		private System.Windows.Forms.ImageList tbimages;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton transleftbtn;
		private System.Windows.Forms.ToolBarButton transrightbtn;
		private System.Windows.Forms.ToolBarButton transupbtn;
		private System.Windows.Forms.ToolBarButton transdownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton scaleupbtn;
		private System.Windows.Forms.ToolBarButton scaledownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton rotxby1btn;
		private System.Windows.Forms.ToolBarButton rotyby1btn;
		private System.Windows.Forms.ToolBarButton rotzby1btn;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton rotxbtn;
		private System.Windows.Forms.ToolBarButton rotybtn;
		private System.Windows.Forms.ToolBarButton rotzbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton shearrightbtn;
		private System.Windows.Forms.ToolBarButton shearleftbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton resetbtn;
		private System.Windows.Forms.ToolBarButton exitbtn;
        int[,] lines;

		public Transformer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			Text = "COMP 4560:  Assignment 5 (2016-4O) (Tyler Trepanier-Bracken)";
			ResizeRedraw = true;
			BackColor = Color.Black;
			MenuItem miNewDat = new MenuItem("New &Data...",
				new EventHandler(MenuNewDataOnClick));
			MenuItem miExit = new MenuItem("E&xit", 
				new EventHandler(MenuFileExitOnClick));
			MenuItem miDash = new MenuItem("-");
			MenuItem miFile = new MenuItem("&File",
				new MenuItem[] {miNewDat, miDash, miExit});
			MenuItem miAbout = new MenuItem("&About",
				new EventHandler(MenuAboutOnClick));
			Menu = new MainMenu(new MenuItem[] {miFile, miAbout});
            Console.WriteLine("main thread: Starting worker thread...");
            alive = true;
            thread = new Thread(new ThreadStart(DoWork));
            thread.Start();
        }

        public void DoWork()
        {
            while (alive)
            {

                while (_rotateX)
                {
                    Invoke(new UpdateRotateCallback(this.UpdateRotate), new object[] {X});
                    Thread.Sleep(25);
                }
                while (_rotateY)
                {
                    Invoke(new UpdateRotateCallback(this.UpdateRotate), new object[] { Y });
                    Thread.Sleep(25);
                }
                while (_rotateZ)
                {
                    Invoke(new UpdateRotateCallback(this.UpdateRotate), new object[] { Z });
                    Thread.Sleep(25);
                }
            }

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Transformer());
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;
            float width = grfx.DpiX;
            float height = grfx.DpiY;
           

            Pen pen = new Pen(Color.White, 3);
			double temp1;
			int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp1 = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp1 += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp1;
                    }
                }

                Console.WriteLine("Origin>>[x:" + scrnpts[0 , 0] + "] [y:" + scrnpts[0, 1] + "]\n");

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
		} // end of OnPaint

        void Init()
        {
            Rectangle rect = RectangleToScreen(this.ClientRectangle);
            int clientHeight = rect.Bottom - rect.Top;
            int clientWidth = (rect.Right - rect.Left);

            double scale = (clientHeight  / 2) / 20;

            double minx = vertices[0, 0];
            double miny = vertices[0, 1];

            double[,] translateBeforeScale = TranslateCoords3D((-1 * minx), (-1 * miny), 0);
            double[,] flipVerticalMatrix = FlipCoords3D(false, true, false);
            double[,] scaleToInitialSize = ScaleCoords3D(scale, scale, scale);
            double[,] translateAfterScale = TranslateCoords3D(clientWidth / 2, clientHeight / 2, 0);

            double[][,] combo = new double[4][,];
            combo[0] = translateBeforeScale;
            combo[1] = flipVerticalMatrix;
            combo[2] = scaleToInitialSize;
            combo[3] = translateAfterScale;

            double[,] tnet = TNet(combo);
            ctrans = tnet;
        }

        //Can be negative or positive coordinates
        double[,] TranslateCoords3D(double x, double y, double z)
        {
            double[,] temp = new double[4,4];
            temp[0, 0] = 1;
            temp[1, 1] = 1;
            temp[2, 2] = 1;
            temp[3, 3] = 1;

            //allocate the x,y,z coordinates.
            temp[3, 0] = x;
            temp[3, 1] = y;
            temp[3, 2] = z;

            return temp;
        }

        //Can be negative or positive coordinates
        double[,] FlipCoords3D(bool x = false, bool y = false, bool z = false)
        {
            double[,] temp = new double[4, 4];

            temp[0, 0] = x ? -1 : 1;
            temp[1, 1] = y ? -1 : 1;
            temp[2, 2] = z ? -1 : 1;
            temp[3, 3] = 1;

            return temp;
        }

        double[,] TNet(double[][,] mats)
        {
            int len = mats.Length;
            double[,] calc1, calc2;
            double[,] temp = new double[4, 4];
            double value = 0;

            calc1 = mats[0];

            for (int i = 0; i < len - 1; i++)
            {
                calc2 = mats[i + 1];
                for (int j = 0; j < 4; j++)
                {
                    //Console.Write("row:[" + j + "]");
                    for (int k = 0; k < 4; k++)
                    {
                        double a = (calc1[j, 0] * calc2[0, k]);
                        double b = (calc1[j, 1] * calc2[1, k]);
                        double c = (calc1[j, 2] * calc2[2, k]);
                        double d = (calc1[j, 3] * calc2[3, k]);

                        value = a + b + c + d;
                        temp[j, k] = value;
                        //Console.Write("{" + k + ":" + value + "}");
                    }
                    //Console.WriteLine("");
                }
                calc1 = temp;
                //Console.WriteLine("\n\n-=-=-=-=-=-=-=-=-=");
            }

            /*for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Console.WriteLine("[" + j +"," + k + "]:" + temp[j,k]);
                }
            }*/
            return temp;
        }

        //Can be negative or positive coordinates
        double[,] RotateCoords2D(double angle, bool clockwise = false)
        {
            double[,] temp = new double[4, 4];
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            temp[0, 0] = 1;
            temp[1, 1] = 1;
            temp[2, 2] = 1;
            temp[3, 3] = 1;

            //allocate the x,y,z coordinates.
            temp[0, 0] = cos;
            temp[0, 1] = clockwise ? sin : (sin * -1);
            temp[1, 0] = clockwise ? (sin * -1) : sin;
            temp[1, 1] = cos;

            return temp;
        }

        double[,] ScaleCoords3D(double xFactor, double yFactor, double zFactor)
        {
            double[,] temp = new double[4, 4];
            temp[0, 0] = xFactor;
            temp[1, 1] = yFactor;
            temp[2, 2] = zFactor;
            temp[3, 3] = 1;

            return temp;
        }

        void Scale(int direction)
        {
            double[,] matrix = new double[4, 4];
            double initX = scrnpts[0, 0];
            double initY = scrnpts[0, 1];
            double initZ = 1;
            switch (direction)
            {
                case UP:
                    matrix = ScaleCoords3D(1.1, 1.1, 1.1);
                    break;
                case DOWN:
                    matrix = ScaleCoords3D(0.9, 0.9, 0.9);
                    break;
            }
            double[,] PreTranslate = TranslateCoords3D(-1 * initX, -1 * initY, -1 * initZ);
            double[,] PostTranslate = TranslateCoords3D(initX, initY, initZ);
            double[][,] combo = new double[4][,];
            combo[0] = ctrans;
            combo[1] = PreTranslate;
            combo[2] = matrix;
            combo[3] = PostTranslate;
            ctrans = TNet(combo);
        }

        public void Rotate(int direction)
        {
            double initX = 1;
            double initY = 1;
            double initZ = 1;

            double cos = Math.Round(Math.Cos(0.05), 15);
            double sin = Math.Round(Math.Sin(0.05), 15);
            double[,] PreTranslate, matrix, PostTranslate, temp;
            double[][,] combo, combo2;

            PreTranslate = PostTranslate = matrix = new double[4, 4];
            combo = new double[3][,];
            combo2 = new double[2][,];

            matrix[0, 0] = 1;
            matrix[1, 1] = 1;
            matrix[2, 2] = 1;
            matrix[3, 3] = 1;

            initX = (vertices[0, 0] * ctrans[0, 0]) + (vertices[0, 1] * ctrans[1, 0]) + (vertices[0, 2] * ctrans[2, 0]) + (vertices[0, 3] * ctrans[3, 0]);
            initY = (vertices[0, 0] * ctrans[0, 1]) + (vertices[0, 1] * ctrans[1, 1]) + (vertices[0, 2] * ctrans[2, 1]) + (vertices[0, 3] * ctrans[3, 1]);
            initZ = (vertices[0, 0] * ctrans[0, 2]) + (vertices[0, 1] * ctrans[1, 2]) + (vertices[0, 2] * ctrans[2, 2]) + (vertices[0, 3] * ctrans[3, 2]);

            switch (direction)
            {
                case X:
                    matrix[1, 1] = cos;
                    matrix[1, 2] = -sin;
                    matrix[2, 1] = sin;
                    matrix[2, 2] = cos;

                    double tempx = matrix[1, 1] + matrix[2, 1];
                    double tempy = matrix[1, 2] + matrix[2, 2];
                    break;
                case Y:
                    matrix[0, 0] = cos;
                    matrix[0, 2] = sin;
                    matrix[2, 0] = -sin; 
                    matrix[2, 2] = cos;
                    break;
                case Z:
                    matrix[0, 0] = cos;
                    matrix[0, 1] = sin;
                    matrix[1, 0] = -sin;
                    matrix[1, 1] = cos;
                    break;
            }

            PreTranslate = TranslateCoords3D(-initX, -initY, -initZ);
            PostTranslate = TranslateCoords3D(initX, initY, initZ);

            //combo[0] = ctrans;
            combo[0] = PreTranslate;
            combo[1] = matrix;
            combo[2] = PostTranslate;

            temp = TNet(combo);

            combo2[0] = ctrans;
            combo2[1] = temp;
            ctrans = TNet(combo2);

            /*for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ctrans[i, j] = Math.Round(ctrans[i, j], 14);
                }
            }*/
        }

        void MenuNewDataOnClick(object obj, EventArgs ea)
		{
			//MessageBox.Show("New Data item clicked.");
			gooddata = GetNewData();
			RestoreInitialImage();
		}

		void MenuFileExitOnClick(object obj, EventArgs ea)
		{
            _rotateX = false;
            _rotateY = false;
            _rotateZ = false;
            thread.Abort();
            alive = false;
            Close();
		}

        void MenuAboutOnClick(object obj, EventArgs ea)
		{
			AboutDialogBox dlg = new AboutDialogBox();
			dlg.ShowDialog();
		}

		void RestoreInitialImage()
		{
			Invalidate();
            Init();
		} // end of RestoreInitialImage

		bool GetNewData()
		{
			string strinputfile,text;
			ArrayList coorddata = new ArrayList();
			ArrayList linesdata = new ArrayList();
			OpenFileDialog opendlg = new OpenFileDialog();
			opendlg.Title = "Choose File with Coordinates of Vertices";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;				
				FileInfo coordfile = new FileInfo(strinputfile);
				StreamReader reader = coordfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) coorddata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeCoords(coorddata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Coordinates File***");
				return false;
			}
            
			opendlg.Title = "Choose File with Data Specifying Lines";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;
				FileInfo linesfile = new FileInfo(strinputfile);
				StreamReader reader = linesfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) linesdata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeLines(linesdata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Line Data File***");
				return false;
			}
			scrnpts = new double[numpts,4];
			setIdentity(ctrans,4,4);  //initialize transformation matrix to identity
			return true;
		} // end of GetNewData

		void DecodeCoords(ArrayList coorddata)
		{
			//this may allocate slightly more rows that necessary
			vertices = new double[coorddata.Count,4];
			numpts = 0;
			string [] text = null;
			for (int i = 0; i < coorddata.Count; i++)
			{
				text = coorddata[i].ToString().Split(' ',',');
				vertices[numpts,0]=double.Parse(text[0]);
				if (vertices[numpts,0] < 0.0d) break;
				vertices[numpts,1]=double.Parse(text[1]);
				vertices[numpts,2]=double.Parse(text[2]);
				vertices[numpts,3] = 1.0d;
				numpts++;						
			}
			
		}// end of DecodeCoords

		void DecodeLines(ArrayList linesdata)
		{
			//this may allocate slightly more rows that necessary
			lines = new int[linesdata.Count,2];
			numlines = 0;
			string [] text = null;
			for (int i = 0; i < linesdata.Count; i++)
			{
				text = linesdata[i].ToString().Split(' ',',');
				lines[numlines,0]=int.Parse(text[0]);
				if (lines[numlines,0] < 0) break;
				lines[numlines,1]=int.Parse(text[1]);
				numlines++;						
			}
		} // end of DecodeLines

		void setIdentity(double[,] A,int nrow,int ncol)
		{
			for (int i = 0; i < nrow;i++) 
			{
				for (int j = 0; j < ncol; j++) A[i,j] = 0.0d;
				A[i,i] = 1.0d;
			}
		}// end of setIdentity
      

		private void Transformer_Load(object sender, System.EventArgs e)
		{
			
		}

        private void Shear(int direction)
        {
            double[,] matrix = new double[4, 4];

            double initX = (0 * ctrans[0, 0]) + (0 * ctrans[1, 0]) + (0 * ctrans[2, 0]) + (1 * ctrans[3, 0]);
            double initY = (0 * ctrans[0, 1]) + (0 * ctrans[1, 1]) + (0 * ctrans[2, 1]) + (1 * ctrans[3, 1]);
            double initZ = (0 * ctrans[0, 2]) + (0 * ctrans[1, 2]) + (0 * ctrans[2, 2]) + (1 * ctrans[3, 2]);

            matrix[0, 0] = 1;
            matrix[1, 1] = 1;
            matrix[2, 2] = 1;
            matrix[3, 3] = 1;
            
            switch (direction)
            {
                case LEFT:
                    matrix[1, 0] = 0.1;
                    break;
                case RIGHT:
                    matrix[1, 0] = -0.1;
                    break;
            }
            double[,] PreTranslate = TranslateCoords3D(-initX, -initY, -initZ);
            double[,] PostTranslate = TranslateCoords3D(initX, initY, initZ);
            double[][,] combo = new double[4][,];
            combo[0] = ctrans;
            combo[1] = PreTranslate;
            combo[2] = matrix;
            combo[3] = PostTranslate;
            ctrans = TNet(combo);
        }

        private void translate(int direction)
        {
            double[,] matrix = new double[4, 4];
            switch (direction)
            {
                case UP:
                    matrix = TranslateCoords3D(0, -35, 0);
                    break;
                case DOWN:
                    matrix = TranslateCoords3D(0, 35, 0);
                    break;
                case LEFT:
                    matrix = TranslateCoords3D(-75, 0, 0);
                    break;
                case RIGHT:
                    matrix = TranslateCoords3D(75, 0, 0);
                    break;
            }
            double[][,] combo = new double[2][,];
            combo[0] = ctrans;
            combo[1] = matrix;
            ctrans = TNet(combo);
        }

        public void UpdateRotate(int direction)
        {
            switch(direction)
            {
                case X:
                    if (!_rotateX)
                        return;
                    break;
                case Y:
                    if (!_rotateY)
                        return;
                    break;
                case Z:
                    if (!_rotateZ)
                        return;
                    break;
            }
            Rotate(direction);
            Refresh();
        }

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == transleftbtn)
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                translate(LEFT);
                Refresh();
			}
			if (e.Button == transrightbtn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                translate(RIGHT);
                Refresh();
			}
			if (e.Button == transupbtn)
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                translate(UP);
                Refresh();
			}
			
			if(e.Button == transdownbtn)
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                translate(DOWN);
                Refresh();
			}
			if (e.Button == scaleupbtn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Scale(UP);
				Refresh();
			}
			if (e.Button == scaledownbtn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Scale(DOWN);
				Refresh();
			}
			if (e.Button == rotxby1btn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Rotate(X);
                Refresh();
			}
			if (e.Button == rotyby1btn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Rotate(Y);
                Refresh();
            }
			if (e.Button == rotzby1btn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Rotate(Z);
                Refresh();
            }

			if (e.Button == rotxbtn) 
			{
                _rotateX = !_rotateX;
                _rotateY = false;
                _rotateZ = false;
            }
			if (e.Button == rotybtn) 
			{
                _rotateX = false;
                _rotateY = !_rotateY;
                _rotateZ = false;
            }
			if (e.Button == rotzbtn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = !_rotateZ;
            }

			if(e.Button == shearleftbtn)
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Shear(LEFT);
                Refresh();
			}

			if (e.Button == shearrightbtn) 
			{
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Shear(RIGHT);
                Refresh();
			}

			if (e.Button == resetbtn)
			{
				RestoreInitialImage();
			}

			if(e.Button == exitbtn) 
			{
                thread.Abort();
                alive = false;
                _rotateX = false;
                _rotateY = false;
                _rotateZ = false;
                Close();
			}

		}

		
	}

	
}
