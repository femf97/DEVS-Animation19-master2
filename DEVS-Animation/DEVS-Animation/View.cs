using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CPaint
{
    /**
    * @brief 인스턴스가 그려지는 View 클래스
    * @author 불명(김효상 확인)
    * @date 불명(2017-01-22 확인)
    */
    public class View : System.Windows.Forms.Form
    {
        private IContainer components;

        /**
        * @param selectedFigure 누른 figure
        */
        private Figure pushedFigure;

        /**
        * @param editor의 bitmap
        */
        public Bitmap editorBitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width,
                               pictureBox.ClientSize.Height);
                pictureBox.DrawToBitmap(bmp, pictureBox.ClientRectangle);
                return bmp;
            }
        }
        /**
        * @param doc View가 속하는 Document
        */
        private Document doc;
        /**
        * @param drag_off_x drag 되는 동안 x좌표 임시 저장
        */
        private int drag_off_x; 
        /**
        * @param drag_off_y drag 되는 동안 y좌표 임시 저장
        */
        private int drag_off_y;
        /**
        * @param isChanged 이동했거나, 크기가 변경되었는지 확인하는데 사용
        */
        private bool isChanged;
        /**
        * @param prevSizeX View 크기를 조절할 때, 이전 view의 너비를 저장해 차이를 구할 때 사용
        */
        private int prevSizeX;
        /**
        * @param prevSizeY View 크기를 조절할 때, 이전 view의 높이를 저장해 차이를 구할 때 사용
        */
        private int prevSizeY;
        private ContextMenuStrip zIndexMenu;
        private ToolStripMenuItem goToTop;
        private ToolStripMenuItem goToAbove;
        private ToolStripMenuItem goToBelow;
        private ToolStripMenuItem goToBottom;

        /**
        * @param lblPosition 객체의 위치를 나타내는 레이블
        */
        private Label lblPosition;
        private PictureBox pictureBox;

        /**
        * @brief 화면의 Zoom IN/Out과 이동을 위한 전역 변수 선언
        * @author 장한결(8524hg@gamil.com)
        * @date 2019-04-30
        */
        private ZoomInOut zoomControl;

        
        /**
        * @brief View 클래스 생성자
        * @author 방준혁(sonsidal@gamil.com)
        * @date 2016-11-08
        * @remarks 부모폼의 size변경 이벤트 추가
        * @param parent View의 부모 Form
        * @param doc View가 속하는 Document
        */
        public View(Form parent, Document doc)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.MdiParent = parent;
            this.doc = doc;
            this.isChanged = false;
            
            //<ADD> [방준혁] 161108 sonsidal@gmail.com
            //부모폼의 size변경 이벤트 추가
            this.MdiParent.SizeChanged += MdiParent_SizeChanged;
            prevSizeX = this.MdiParent.Width;
            prevSizeY = this.MdiParent.Height;
            
            /**
            * @brief zoomControl 초기화
            * @author 장한결(8524hg@gamil.com)
            * @date 2019-04-30
            */
            zoomControl = new ZoomInOut();
        }

        /**
        * @brief 크기를 변경하는 method
        * @details 이전 부모폼의 크기를 기억해서 부모폼의 크기가 변경될 때 그만큼 자식폼도 크기를 변경
        * @author 김효상
        * @date 2017-07-08
        * @remark 최소화후 복구할 때 폼의 크기가 비정상적으로 변하는 현상 수정
        * @param sender 크기가 변경된 Form
        * @param e EventArgs 인스턴스
        */
        private void MdiParent_SizeChanged(object sender, EventArgs e)
        {
            int sizeX, sizeY;
            if (((Form)sender).WindowState == System.Windows.Forms.FormWindowState.Minimized) return;
            sizeX = ((Form)sender).Width;
            sizeY = ((Form)sender).Height;

            int diffX, diffY;
            diffX = sizeX - prevSizeX;
            diffY = sizeY - prevSizeY;

            this.Width += diffX;
            this.Height += diffY;

            prevSizeX = sizeX;
            prevSizeY = sizeY;
        }

        /**
        * @brief 사용된 리소스들을 모두 정리하는 method
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        * @param disposing 정리가 되었는지를 나타냄
        */
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
            this.lblPosition = new System.Windows.Forms.Label();
            this.zIndexMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.goToAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.goToBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.goToBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.zIndexMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.BackColor = System.Drawing.Color.Transparent;
            this.lblPosition.Location = new System.Drawing.Point(12, 9);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(0, 12);
            this.lblPosition.TabIndex = 1;
            // 
            // zIndexMenu
            // 
            this.zIndexMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.zIndexMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToTop,
            this.goToAbove,
            this.goToBelow,
            this.goToBottom});
            this.zIndexMenu.Name = "zIndexMenu";
            this.zIndexMenu.Size = new System.Drawing.Size(167, 92);
            // 
            // goToTop
            // 
            this.goToTop.Name = "goToTop";
            this.goToTop.Size = new System.Drawing.Size(166, 22);
            this.goToTop.Text = "맨 앞으로 보내기";
            this.goToTop.Click += new System.EventHandler(this.goToTop_Click);
            // 
            // goToAbove
            // 
            this.goToAbove.Name = "goToAbove";
            this.goToAbove.Size = new System.Drawing.Size(166, 22);
            this.goToAbove.Text = "앞으로 보내기";
            this.goToAbove.Click += new System.EventHandler(this.goToAbove_Click);
            // 
            // goToBelow
            // 
            this.goToBelow.Name = "goToBelow";
            this.goToBelow.Size = new System.Drawing.Size(166, 22);
            this.goToBelow.Text = "뒤로 보내기";
            this.goToBelow.Click += new System.EventHandler(this.goToBelow_Click);
            // 
            // goToBottom
            // 
            this.goToBottom.Name = "goToBottom";
            this.goToBottom.Size = new System.Drawing.Size(166, 22);
            this.goToBottom.Text = "맨 뒤로 보내기";
            this.goToBottom.Click += new System.EventHandler(this.goToBottom_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Gray;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(709, 412);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintHandler);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveHandler);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpHandler);
            this.pictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MouseWheelHandler);
            // 
            // View
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(709, 412);
            this.ControlBox = false;
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.pictureBox);
            this.Name = "View";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.View_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintHandler);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveHandler);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpHandler);
            this.zIndexMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /**
        * @brief View가 로드될 때 이벤트
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        */
        private void View_Load(object sender, System.EventArgs e)
		{

		}

        /**
        * @brief 백그라운드를 지우지 않는다
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        */
        protected override void OnPaintBackground(PaintEventArgs e) { }

        /**
        * @brief View에 그리는 이벤트의 Handler
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        * @param sender 사용되지 않음
        * @param e paint 이벤트에 대한 정보
        */
        protected void PaintHandler (object sender, System.Windows.Forms.PaintEventArgs e)
		{
            /**
             * @brief: Canvas 크기를 고정. Canvas에 배경 이미지를 bitmap 형식으로 받을 수 있게 수정. 기존 코드는 주석 처리.
             * @Author: 장한결(8524hg@gmail.com)
             * @data: 2019-05-12
             */
            int Width = 1028;
            int Height = 1024;
            Bitmap Canvas = GetDocument().getBackground();
            float screen_ratio = (float)Height / (float)Width;
            float bitmap_ratio = (float)Canvas.Height / (float)Canvas.Width;

            //Bitmap Canvas = new Bitmap(Width, Height, e.Graphics);
            Graphics g = Graphics.FromImage(Canvas);
            //Graphics g = e.Graphics;
            /*
            // draw all objects
            //Graphics g = this.CreateGraphics ();
            g.FillRectangle(
				new SolidBrush(Color.White),
				0,0,Width, Height);
                */


            int size = GetDocument().getObjectSize();
			for(int i=0; i<size; i++) 
			{
				Figure f= (Figure)doc.getObjectElementAt(i);
				f.paint(g);
			}
			if(doc.getCurrentFigure() != null)
			{
				doc.getCurrentFigure().paint(g);
                foreach (Figure f in doc.getHigherZFigures(doc.getCurrentFigure()))
                    f.paint(g);
			}
            g.Dispose();

            /**
            * @brief Zoom In/Out과 화면 이동을 반영하여 출력
            * @author 장한결(8524hg@gamil.com)
            * @date 2019-04-30
            */
            Graphics g2 = e.Graphics;
            g2.Transform = zoomControl.mat;

            /**
            * @brief: 배경화면의 옵션을 적용해서 출력하는 코드. 출력방식을 e.Graphics.DrawImageUnscaled에서 DrawImage로 변경
            * @author: 장한결(8524hg@gamil.com)
            * @date: 2019-05-12
            */
            Rectangle srcRect;
            Rectangle destRect;
            switch (doc.bgOption)
            {
                case 1:
                default:
                    srcRect = new Rectangle(0, 0, Canvas.Width, Canvas.Height);
                    destRect = new Rectangle(0, 0, Width, Height);
                    break;
                case 2:
                    if (screen_ratio > bitmap_ratio)
                    {
                        srcRect = new Rectangle(0, 0, Canvas.Width, Canvas.Height);
                        destRect = new Rectangle(0, 0, Width, (int)(Width*bitmap_ratio));
                    }
                    else
                    {
                        srcRect = new Rectangle(0, 0, Canvas.Width, Canvas.Height);
                        destRect = new Rectangle(0, 0, (int)(Height/bitmap_ratio), Height);
                    }
                    break;
                case 3:
                    if (screen_ratio > bitmap_ratio)
                    {
                        int temp_width = (int)(Canvas.Height / screen_ratio);
                        srcRect = new Rectangle((Canvas.Width - temp_width)/2, 0, temp_width, Canvas.Height);
                        destRect = new Rectangle(0, 0, Width, Height);
                    }
                    else
                    {
                        int temp_height = (int)(Canvas.Width * screen_ratio);
                        srcRect = new Rectangle(0, (Canvas.Height - temp_height)/2, Canvas.Width, temp_height);
                        destRect = new Rectangle(0, 0, Width, Height);
                    }
                    break;
            }
            e.Graphics.DrawImage(Canvas, destRect, srcRect, GraphicsUnit.Pixel);

            Canvas.Dispose();
		}

        /**
        * @brief 화면에서 마우스 클릭을 떼는 이벤트의 Handler
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        * @param sender 사용되지 않음
        * @param e Mouse 이벤트에 대한 정보
        * @code
        * this.Capture = false;
			
			switch(doc.getCurrentTool()) 
			{
                case FIGURE.ARROW:
                    if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control) doc.setIsMultSel(true);
                    else doc.setIsMultSel(false);
                    isChanged =false;
					break;
                    //Select상태일 때 마우스를 떼면 Select객체를 임시 저장하고 객체를 doc에서
                    //null로 바꾼 뒤 Figlist에 포함되는 Figure들을 저장한다.
                    //그리고 Tool상태는 MULTISEL로 바꾼다. (**수정필요**)
                case FIGURE.SELECT:
                    if (!doc.getIsMultSel())
                    {
                        Selection temp = (Selection)doc.getCurrentFigure();
                        doc.setCurrentFigure(null);
                        doc.setIsMultSel(true);
                        doc.selectFigures(temp);
                        doc.UpdateAllViews(); 
                    }
                    break;
				case FIGURE.LINE:
				case FIGURE.RECT:
				case FIGURE.OVAL:
				case FIGURE.FREELINE:
				case FIGURE.CURVE:
                    doc.setIsMultSel(false);
                    if (doc.getCurrentFigure()==null)
					{
						return;
					}
					// 객체가 비어있는지 검사.
					if(doc.getCurrentFigure().isEmpty()!=true) 
					{
						// 스택에 현재까지의 상태를 저장
						doc.pushStack();
					
						// Document 에 추가
						doc.addObject(doc.getCurrentFigure());
						doc.setCurrentFigure(null);
					}
					doc.UpdateAllViews();
                    //((Form1)MdiParent).debugText.Text = doc.getObjectSize() + "";
					break;
				case FIGURE.POLYLINE:
					break;
				case FIGURE.TEXT:
					break;
				default:
					break;
			}
        * @endcode
        */
        protected void MouseUpHandler (object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.Capture = false;
            pushedFigure = null;

            // 마우스 왼쪽 버튼 이벤트
            if (e.Button == MouseButtons.Left)
            {
                switch (doc.getCurrentTool())
                {
                    case FIGURE.ARROW:
                        if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control) doc.setIsMultSel(true);
                        else doc.setIsMultSel(false);
                        isChanged = false;
                        break;
                    //Select상태일 때 마우스를 떼면 Select객체를 임시 저장하고 객체를 doc에서
                    //null로 바꾼 뒤 Figlist에 포함되는 Figure들을 저장한다.
                    //그리고 Tool상태는 MULTISEL로 바꾼다. (**수정필요**)
                    case FIGURE.BOX:
                        if (!doc.getIsMultSel())
                        {
                            Selection temp = (Selection)doc.getCurrentFigure();
                            doc.refreshDrawingFigure(null);
                            doc.setIsMultSel(true);
                            doc.selectFigures(temp);
                            doc.UpdateAllViews();
                        }
                        break;
                    case FIGURE.LINE:
                    case FIGURE.RECT:
                    case FIGURE.OVAL:
                    case FIGURE.FREELINE:
                    case FIGURE.CURVE:
                        doc.setIsMultSel(false);
                        if (doc.getCurrentFigure() == null)
                        {
                            return;
                        }
                        // 객체가 비어있는지 검사.
                        if (doc.getCurrentFigure().isEmpty() != true)
                        {
                            // 스택에 현재까지의 상태를 저장
                            doc.makeSnapshot();

                            // Document 에 추가
                            doc.addFigure(doc.getCurrentFigure());
                            doc.refreshDrawingFigure(null);
                        }
                        doc.UpdateAllViews();
                        //((Form1)MdiParent).debugText.Text = doc.getObjectSize() + "";
                        break;
                    case FIGURE.POLYLINE:
                        break;
                    case FIGURE.TEXT:
                        break;
                    default:
                        break;
                }
            }
            // 마우스 오른쪽 버튼 이벤트
            else
            {
                /**
                * @brief 화면 이동 종료
                * @author 장한결(8524hg@gamil.com)
                * @date 2019-04-30
                */
                if (e.Button == MouseButtons.Right)
                {
                    zoomControl.Releasing_screen();
                }
            }
        }

        /**
        * @brief 화면에서 마우스를 이동하는 이벤트의 Handler
        * @author 박성식
        * @date 2016-11-12
        * @remark 선택 도형 좌표 표시 가능하도록 수정
        * @param sender 사용되지 않음
        * @param e Mouse 이벤트에 대한 정보
        * @code
        *  int x = e.X;
            int y = e.Y;
            //박성식 161112 
            //선택 도형 좌표 표시 가능하도록 수정
            Figure f = doc.findFigureByPoint(x, y);
            if (f != null && f.getVectorElementAt(0) is Rect)
            {
                Rect r = f.getVectorElementAt(0) as Rect;
                lblPosition.Text = (string.Format("X:{0},Y:{1}", r.getLeft(), r.getTop()));
            }
            else
                lblPosition.Text = "";

            if (this.Capture == false)
                return;

            switch (doc.getCurrentTool()) 
			{
                case FIGURE.ARROW:
                    if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control) doc.setIsMultSel(true);
                    else doc.setIsMultSel(false);
                    if (doc.getIsMultSel() == false)
                    {
                        if (doc.getCurrentFigure() != null)
                        {
                            // 한번만 수행되도록한다.
                            if (isChanged == false)
                            {
                                doc.pushStack();
                                isChanged = true;
                            }

                            // 이전의 드래그 되었던 좌표에서 얼마만큼 이동했는지를 구하여
                            // 그만 큼 이동시킨다.
                            doc.getCurrentFigure().move(x - drag_off_x, y - drag_off_y);

                            // 현재의 좌표를 저장시켜 둔다.
                            drag_off_x = x;
                            drag_off_y = y;

                            // 스택에 넣기위해 true로 바꾼다.

                        }
                    }
                    else
                    {
                        if (doc.getFigListSize() > 0)
                        {
                            if (isChanged == false)
                            {
                                doc.pushStack();
                                isChanged = true;
                            }
                            //멀티케이스 인 경우 리스트에 저장된 애들을 모두 움직임
                            for (int i = 0; i < doc.getFigListSize(); i++)
                            {
                                doc.getCurrentFigure(i).move(x - drag_off_x, y - drag_off_y);
                            }
                            drag_off_x = x;
                            drag_off_y = y;
                        }
                    }
					break;
                case FIGURE.SELECT:
                    if (doc.getIsMultSel())
                    {
                        if (isChanged == false)
                        {
                            isChanged = true;
                        }
                        //멀티케이스 인 경우 리스트에 저장된 애들을 모두 움직임
                        for (int i = 0; i < doc.getFigListSize(); i++)
                        {
                            doc.getCurrentFigure(i).move(x - drag_off_x, y - drag_off_y);
                        }
                        drag_off_x = x;
                        drag_off_y = y;
                    }
                    else
                    {
                        Rect rt = (Rect)doc.getCurrentFigure().getVectorElementAt(0);
                        rt.setRight(x);
                        rt.setBottom(y);
                    }
                    break;
                // line, rectangle, oval 등은 드래그 동안 끝점만 바꿔주면 된다.
                case FIGURE.LINE:
				case FIGURE.RECT:
				case FIGURE.OVAL:
                    doc.setIsMultSel(false);
                    Rect pt = (Rect)doc.getCurrentFigure().getVectorElementAt(0);
					pt.setRight(x);
					pt.setBottom(y);
					break;
					// free line은 드래그 동안에 좌표를 계속 추가시켜준다.
				case FIGURE.FREELINE:
                    doc.setIsMultSel(false);
                    doc.getCurrentFigure().add(x, y);
					break;
				case FIGURE.POLYLINE:
					break;
					// curve도 line, rectangle, oval과 비슷하나 normal을 호출한다. 즉 one, two값이
					// 1/3, 2/3 에 위치하도록 바꿔준다.
					//
					//case FIGURE.CURVE:
					//	CurveFigure c = (CurveFigure)figure;
					//	c.setEnd(x,y);
					//	c.setNormal();
					//	break;
					//
				case FIGURE.TEXT:
					break;
                case FIGURE.ERASE:
                    doc.setIsMultSel(false);
                    doc.allFiguresNotSelected();
                    // 모든 객체에서 눌려진 좌표에 해당되는 객체를 찾는다.
                    doc.setCurrentFigure(doc.findFigureByPoint(x, y));
                    if (doc.getCurrentFigure() != null)
                    {
                        doc.removeObject(doc.getCurrentFigure());
                    }
                    break;
                default:
					break;
			}
			doc.UpdateAllViews();
        * @endcode
        */
        protected void MouseMoveHandler (object sender, System.Windows.Forms.MouseEventArgs e)
		{
            int x = e.X;
            int y = e.Y;
            
            /**
            * @brief 화면 이동 시작
            * @author 장한결(8524hg@gamil.com)
            * @date 2019-04-30
            */
            if (zoomControl.mouse_R_down)
            {
                zoomControl.Moving_screen(x, y);
                pictureBox.Invalidate();

            }

            /**
            * @brief Zoom In/Out 상태에서 그림을 그릴 때, 화면의 좌표를 실제 좌표로 연산
            * @author 장한결
            * @date 2019-04-30
            */
            //**************************//
            x = zoomControl.Real_X(x);  //
            y = zoomControl.Real_Y(y);  //
            //**************************//

            //박성식 161112 
            //선택 도형 좌표 표시 가능하도록 수정
            if (pushedFigure != null && pushedFigure.getVectorElementAt(0) is Rect)
            {
                Rect r = pushedFigure.getVectorElementAt(0) as Rect;
                lblPosition.Text = (string.Format("X:{0},Y:{1}", r.getLeft(), r.getTop()));
            }
            else
                lblPosition.Text = "";

            if (this.Capture == false)
                return;

            Pos movedInAbsoluteSystem = new Pos(x - drag_off_x, y - drag_off_y).InverseRelativeClone(doc.magnificationRatio, doc.screenPos);
            switch (doc.getCurrentTool()) 
			{
                case FIGURE.ARROW:
                    if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control) doc.setIsMultSel(true);
                    else doc.setIsMultSel(false);
                    if (doc.getIsMultSel() == false)
                    {
                        if (doc.getCurrentFigure() != null)
                        {
                            // 한번만 수행되도록한다.
                            if (isChanged == false)
                            {
                                doc.makeSnapshot();
                                isChanged = true;
                            }

                            // 이전의 드래그 되었던 좌표에서 얼마만큼 이동했는지를 구하여
                            // 그만 큼 이동시킨다.
                            doc.getCurrentFigure().moveAndResize(movedInAbsoluteSystem.x, movedInAbsoluteSystem.y);

                            // 현재의 좌표를 저장시켜 둔다.
                            drag_off_x = x;
                            drag_off_y = y;
                        }
                    }
                    else
                    {
                        if (doc.getDrawingFiguresCount() > 0)
                        {
                            if (isChanged == false)
                            {
                                doc.makeSnapshot();
                                isChanged = true;
                            }
                            //멀티케이스 인 경우 리스트에 저장된 애들을 모두 움직임
                            for (int i = 0; i < doc.getDrawingFiguresCount(); i++)
                            {
                                doc.getCurrentFigure(i).moveAndResize(movedInAbsoluteSystem.x, movedInAbsoluteSystem.y);
                            }
                            drag_off_x = x;
                            drag_off_y = y;
                        }
                    }
					break;
                case FIGURE.BOX:
                    if (doc.getIsMultSel())
                    {
                        if (isChanged == false)
                        {
                            isChanged = true;
                        }
                        //멀티케이스 인 경우 리스트에 저장된 애들을 모두 움직임
                        for (int i = 0; i < doc.getDrawingFiguresCount(); i++)
                        {
                            doc.getCurrentFigure(i).moveAndResize(movedInAbsoluteSystem.x, movedInAbsoluteSystem.y);
                        }
                        drag_off_x = x;
                        drag_off_y = y;
                    }
                    else
                    {
                        Rect rt = (Rect)doc.getCurrentFigure().getVectorElementAt(0);
                        rt.setRight(x);
                        rt.setBottom(y);
                    }
                    break;
                // line, rectangle, oval 등은 드래그 동안 끝점만 바꿔주면 된다.
                case FIGURE.LINE:
				case FIGURE.RECT:
				case FIGURE.OVAL:
                    doc.setIsMultSel(false);
                    Rect pt = (Rect)doc.getCurrentFigure().getVectorElementAt(0);
					pt.setRight(x);
					pt.setBottom(y);
					break;
					// free line은 드래그 동안에 좌표를 계속 추가시켜준다.
				case FIGURE.FREELINE:
                    doc.setIsMultSel(false);
                    doc.getCurrentFigure().add(x, y);
					break;
				case FIGURE.POLYLINE:
					break;
					// curve도 line, rectangle, oval과 비슷하나 normal을 호출한다. 즉 one, two값이
					// 1/3, 2/3 에 위치하도록 바꿔준다.
					//
					//case FIGURE.CURVE:
					//	CurveFigure c = (CurveFigure)figure;
					//	c.setEnd(x,y);
					//	c.setNormal();
					//	break;
					//
				case FIGURE.TEXT:
					break;
                case FIGURE.ERASE:
                    doc.setIsMultSel(false);
                    doc.deselectAll();
                    // 모든 객체에서 눌려진 좌표에 해당되는 객체를 찾는다.
                    doc.refreshDrawingFigure(doc.findFigureAt(x, y));
                    if (doc.getCurrentFigure() != null)
                    {
                        doc.removeOriginalFigure(doc.getCurrentFigure());
                    }
                    break;
                default:
					break;
			}
			doc.UpdateAllViews();
            

        }

        /// <summary>
        /// 화면에 마우스를 클릭할 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /**
        * @brief 화면에 마우스를 클릭하는 이벤트의 Handler
        * @author 김효상
        * @date 2016-01-28
        * @remark 객체가 선택된 상태에서 마우스 오른쪽 클릭시, 메뉴 표시. 메뉴를 선택시 해당하는 기능의 함수로 연결.
        */
		protected void MouseDownHandler (object sender, System.Windows.Forms.MouseEventArgs e)
		{
            int x = drag_off_x = e.X;
            int y = drag_off_y = e.Y;

            // 마우스 왼쪽 클릭
            if(e.Button == MouseButtons.Left)
            {
                /**
                * @brief Zoom In/Out 상태에서 그림을 그릴 때, 화면의 좌표를 실제 좌표로 연산
                * @author 장한결
                * @date 2019-04-30
                */
                //**************************//
                x = zoomControl.Real_X(x);  //
                y = zoomControl.Real_Y(y);  //
                //**************************//

                //박성식 배경이 두번 눌린거면 선택도구 취소하도록한다.
                if ((doc.getCurrentTool() == FIGURE.ARROW) || (doc.getCurrentTool() == FIGURE.BOX))
                    pushedFigure = doc.findFigureAt(x, y);
                if (e.Clicks == 2 && doc.findFigureAt(x, y) == null)
                {
                    // if double clicked,
                    switch (doc.getCurrentTool())
                    {
                        case FIGURE.ARROW:
                        case FIGURE.BOX:
                        case FIGURE.LINE:
                        case FIGURE.RECT:
                        case FIGURE.OVAL:
                        case FIGURE.FREELINE:
                        case FIGURE.CURVE:
                        case FIGURE.TEXT:
                        case FIGURE.FILL:
                            doc.setCurrentTool(FIGURE.ARROW);
                            break;
                        // polyline은 안쓰는 코드로 보인다.
                        //case FIGURE.POLYLINE:
                        //    doc.setIsMultSel(false);
                        //    // Poly라인의 경우 만 처리
                        //    if (doc.getCurrentFigure() != null)
                        //    {
                        //        // 스택에 현재까지의 상태를 저장
                        //        doc.pushStack();
                        //        doc.addObject(doc.getCurrentFigure());
                        //        doc.setCurrentFigure(null);
                        //    }
                        //  break;
                        default:
                            break;
                    }
                    GetDocument().UpdateAllViews();
                }
                else if (e.Button == MouseButtons.Right && doc.getDrawingFiguresCount() > 0)
                {
                    //오른쪽 클릭을 했을 때, 선택된 객체가 존재한다면 메뉴를 띄운다.
                    zIndexMenu.Show(this, e.Location);
                }
                else
                {
                    Color currentColor = doc.getCurrentColor();

                    switch (doc.getCurrentTool())
                    {
                        case FIGURE.ARROW:
                            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control) doc.setIsMultSel(true);
                            else doc.setIsMultSel(false);
                            if (doc.getIsMultSel() == false)
                            {
                                // 모든 객체의 선택을 해재한다. (오직 한개만 선택되어야 하기 때문)
                                doc.deselectAll();
                                // 모든 객체에서 눌려진 좌표에 해당되는 객체를 찾는다.
                                doc.refreshDrawingFigure(doc.findFigureAt(x, y));
                                if (doc.getCurrentFigure() != null)
                                {
                                    // 선택한다.
                                    doc.getCurrentFigure().select();
                                }
                            }
                            else
                            {
                                // 모든 객체에서 눌려진 좌표에 해당되는 객체를 찾는다.
                                doc.refreshDrawingFigure(doc.findFigureAt(x, y));
                                if (doc.getDrawingFiguresCount() > 0)
                                {
                                    // 선택한다.
                                    doc.getCurrentFigure(doc.getDrawingFiguresCount() - 1).select();
                                }
                            }
                            break;
                        case FIGURE.BOX:
                            if (doc.findFigureAt(x, y) != null)
                            {
                                doc.setIsMultSel(true);
                            }
                            else
                            {
                                doc.setIsMultSel(false);
                                doc.refreshDrawingFigure(new Selection(x, y, x, y));
                            }
                            break;
                        case FIGURE.LINE:
                            doc.setIsMultSel(false);
                            // line 객체를 만든다.
                            doc.refreshDrawingFigure(new LineFigure(x, y, x, y, currentColor));
                            break;
                        case FIGURE.RECT:
                            doc.setIsMultSel(false);
                            // rect 객체를 만든다.
                            doc.refreshDrawingFigure(new RectFigure(x, y, x, y, currentColor));
                            break;
                        case FIGURE.OVAL:
                            doc.setIsMultSel(false);
                            // 타원객체를 만든다.
                            doc.refreshDrawingFigure(new OvalFigure(x, y, x, y, currentColor));
                            break;
                        case FIGURE.FREELINE:
                            doc.setIsMultSel(false);
                            // 자유선 객체를 만든다.
                            doc.refreshDrawingFigure(new FreeLineFigure(x, y, currentColor));
                            break;
                        case FIGURE.POLYLINE:
                            doc.setIsMultSel(false);
                            // poly 객체를 만든다.
                            if (doc.getCurrentFigure() == null)
                                doc.refreshDrawingFigure(new PolyLineFigure(x, y, currentColor));
                            else
                                doc.getCurrentFigure().add(new Pos(x, y));
                            break;
                        /*
                        case FIGURE.CURVE:
                            // 곡선객체를 만든다.
                            figure = new CurveFigure(x, y,currentColor);
                            break;
                        */
                        case FIGURE.TEXT:
                            break;
                        case FIGURE.ERASE:
                            doc.setIsMultSel(false);
                            doc.deselectAll();
                            // 모든 객체에서 눌려진 좌표에 해당되는 객체를 찾는다.
                            doc.refreshDrawingFigure(doc.findFigureAt(x, y));
                            if (doc.getCurrentFigure() != null)
                            {
                                doc.removeOriginalFigure(doc.getCurrentFigure());
                            }
                            break;
                        //**********************************************************
                        //이름 : 박성식 [16-07-17] 
                        //내용 : 피규어를 찾고 해당 피규어의 채우기 여부,색을 바꾼다.
                        //이유 : 그래야 paint함수에서 채우기 됨
                        //document의 현재 색으로 채운다.
                        //setcurrentcolor는 선그릴때도 사용되는 거라 수정이 필요할듯 보인다.
                        case FIGURE.FILL:
                            Figure f = doc.findFigureAt(x, y);
                            if (f != null)
                            {
                                f.fill(doc.getCurrentColor());
                            }
                            break;
                        default:
                            break;
                            //********************************************************
                    }
                    this.Capture = true;
                    doc.UpdateAllViews();
                }
            }
            // 마우스 오른쪽 클릭
            else
            {
                /**
                * @brief 화면 이동 시작
                * @author 장한결(8524hg@gamil.com)
                * @date 2019-04-30
                */
                zoomControl.Anchoring_screen(x, y);
            }
        }

        /**
        * @brief 마우스 휠 이벤트
        * @author 장한결
        * @date 2019-04-30(생성 일자)
        * @return void
        */
        protected void MouseWheelHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //휠을 위로 돌렸을 때
            if(e.Delta > 0)
            {
                // Zoom IN
                zoomControl.Zoom_IN(e.X, e.Y);
                // 화면 다시 그리기
                pictureBox.Invalidate();
            }
            // 휠을 아래로 돌렸을 때
            else
            {
                // Zoom OUT
                zoomControl.Zoom_OUT(e.X, e.Y);
                // 화면 다시 그리기
                pictureBox.Invalidate();
            }
        }



        /**
        * @brief Document의 getter
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        * @return doc 리턴되는 document
        */
        public Document GetDocument()
		{
			return doc;
		}

        /**
        * @brief PictureBox의 getter
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        * @return doc 리턴되는 PictureBox
        */
        public PictureBox getPictureBox()
        {
            return pictureBox;
        }

        /**
        * @brief 키다운 이벤트의 Handler
        * @author 불명(김효상 확인)
        * @date 불명(2017-01-22 확인)
        * @param sender 사용되지 않음
        * @param e Key 이벤트에 대한 정보
        * @code
        * if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.X:
                        doc.cut();
                        break;
                    case Keys.C:
                        doc.copy();
                        break;
                    case Keys.V:
                        doc.paste();
                        break;
                    case Keys.Z:
                        doc.undo();
                        break;
                    case Keys.A:
                        doc.setCurrentTool(FIGURE.ARROW);
                        break;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                for (int i = doc.getObjectSize()-1; i >=0 ; i--)
                {
                    if (doc.getObjectElementAt(i).isSelected() == true)
                    {
                        doc.removeFigList(doc.getObjectElementAt(i));
                        doc.removeObject(doc.getObjectElementAt(i));
                    }
                }
                doc.UpdateAllViews();
            }
        * @endcode
        */
        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            /**
            * @brief space키를 누를 때 zoomControl의 작업 내역을 리셋(현재 작동 안함)
            * @author 장한결(8524hg@gamil.com)
            * @date 2019-04-30
            */
            if (e.KeyCode == Keys.Space)
            {
                this.zoomControl.Reset();
                pictureBox.Invalidate();
            }

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.X:
                        doc.cut();
                        break;
                    case Keys.C:
                        doc.copy();
                        break;
                    case Keys.V:
                        doc.paste();
                        break;
                    case Keys.Z:
                        doc.undo();
                        break;
                    case Keys.A:
                        doc.setCurrentTool(FIGURE.ARROW);
                        break;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                for (int i = doc.getObjectSize()-1; i >=0 ; i--)
                {
                    if (doc.getObjectElementAt(i).isSelected() == true)
                    {
                        doc.removeFromDrawingFigures(doc.getObjectElementAt(i));
                        doc.removeOriginalFigure(doc.getObjectElementAt(i));
                    }
                }
                doc.UpdateAllViews();
            }
        }
        /**
        * @brief 맨 앞으로 메뉴 클릭시의 Handler
        * @author 김효상
        * @date 2017-01-28
        */
        private void goToTop_Click(object sender, EventArgs e)
        {
            doc.bringSelectedToVeryFront();
            doc.UpdateAllViews();
        }
        /**
        * @brief 앞으로 메뉴 클릭시의 Handler
        * @author 김효상
        * @date 2017-01-28
        */
        private void goToAbove_Click(object sender, EventArgs e)
        {
            doc.bringSelectedToFront();
            doc.UpdateAllViews();
        }
        /**
        * @brief 뒤로 메뉴 클릭시의 Handler
        * @author 김효상
        * @date 2017-01-28
        */
        private void goToBelow_Click(object sender, EventArgs e)
        {
            doc.pushSelectedToBack();
            doc.UpdateAllViews();
        }
        /**
        * @brief 맨 뒤로 메뉴 클릭시의 Handler
        * @author 김효상
        * @date 2017-01-28
        */
        private void goToBottom_Click(object sender, EventArgs e)
        {
            doc.pushSelectedToVeryBack();
            doc.UpdateAllViews();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }


        
    }
}
