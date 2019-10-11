using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//현재 테스트용 메시지 양식
//원운동 회전각,중심x,중심y,시간 중심기준 반시계 회전만 가능
//직선운동 좌표,시간
namespace CPaint
{
    enum MoveType { LINEAR, CIRCULAR };
    //작업 순서 시작하면 이미지 설정창 뜸(미리 해둔 경우 안뜸)
    //파이프 스레드(백그라운드 워커) 열려서 메시지 생길때마다 큐에 저장
    //타이머는 주기적으로 메시지큐를 확인하면서 데이터가 있으면 애니메이션 진행

    /**
    * @brief Animator 폼을 구현하기 위한 폼 클래스.
    * @author 불명(김효상 확인)
    * @date 불명(2017-1-23 확인)
    */
    public partial class Animator : Form
    {
        /**
        * param g 그릴 때 사용되는 Graphics 객체
        */ 
        Graphics g; //그릴 때 사용되는 Graphics 객체
        /**
        * param bg 이미지를 담는 Bitmap 객체 (내부 변수이므로 수정 금지)
        */
        Bitmap innerAnimatorBitmap;  //이미지를 담는 Bitmap 객체
        /**
        * param bg 이미지를 담는 Bitmap 객체
        */
        public Bitmap animatorBitmap
        {
            set
            {
                innerAnimatorBitmap = value;
                pictureBox1.Refresh();
            }
        }
        /**
        * param worker 백그라운드에서 작동하는 스레드
        */
        BackgroundWorker worker;    //백그라운드에서 작동하는 스레드
        /**
        * param rsv 시뮬레이터에서 생성된 데이터를 전송 받기 위한 MessageReceiver 객체
        */
        MessageReceiver rsv;    //Message를 수신하는 MessageReceiver객체
        /**
        * param rsv 애니메이션을 진행하는 AnimatorHandler 객체
        */
        AnimatorHandler animHandler;    //애니메이션을 진행하는 AnimatorHandler객체
        /**
        * param receivedMsg 수신한 Message
        */
        string receivedMsg; //수신한 Message
        /**
        * param key mutex를 거는 키
        */
        object key = new object();  //mutex를 거는 키
        /**
        * param innerStartAnim 애니메이션 시작 여부 (내부용 변수, 수정 금지)
        */
        bool innerStartAnim = false; //애니메이션 시작 여부 (내부용 변수, 수정 금지)
        /**
        * param startedSimulation 애니메이션 시작 여부
        */
        public bool startedSimulation {
            get
            {
                return innerStartAnim;
            }
        }
        /**
        * @param prevSizeX View 크기를 조절할 때, 이전 view의 너비를 저장해 차이를 구할 때 사용
        */
        private int prevSizeX;
        /**
        * @param prevSizeY View 크기를 조절할 때, 이전 view의 높이를 저장해 차이를 구할 때 사용
        */
        private int prevSizeY;

        ObjectList obj;

        /**
         * @param loadedObjectList 불러온 Object의 리스트.
         */
        List<Bitmap> loadedObjectList = new List<Bitmap>();

    /**
    * @brief Animator 폼 클래스의 생성자
    * @details MessageReceiver 객체를 생성하고, 깜빡임 방지를 위한 더블버퍼링 설정을 한다.
    * @author 불명(김효상 확인)
    * @date 불명(2017-1-23 확인)
    */
    public Animator(Form parent,ObjectList obj)
        {
            InitializeComponent();

            g = this.pictureBox1.CreateGraphics();
            pictureBox1.BackColor = Color.White;
            this.obj = obj;
            this.MdiParent = parent;
            this.MdiParent.SizeChanged += MdiParent_SizeChanged;
            prevSizeX = this.MdiParent.Width;
            prevSizeY = this.MdiParent.Height;

        }

        /**
        * @brief pictureBox에 그리는 이벤트
        * @author 불명(김효상 확인)
        * @date 불명(2017-1-23 확인)
        */
        public void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (innerAnimatorBitmap != null)
            {
                e.Graphics.DrawImage(innerAnimatorBitmap, pictureBox1.Location.X, pictureBox1.Location.Y, pictureBox1.Width, pictureBox1.Height);
            }
            if (animHandler != null)
                animHandler.updatePos(e.Graphics);
            else
            {
                if (loadedObjectList.Count() > 0)
                {
                    foreach (Bitmap b in loadedObjectList)
                        e.Graphics.DrawImage(b,
                            pictureBox1.Location.X, pictureBox1.Location.Y,
                            pictureBox1.Width, pictureBox1.Height);
                }
            }
        }

        public void refreshPictureBox()
        {
            loadedObjectList.Clear();
            foreach (List<String> currentList in ObjectList.imgList)
            {
                foreach (String s in currentList)
                {
                    String fileToLoad = s;
                    if (fileToLoad.ToLower().EndsWith(".sof"))
                    {
                        fileToLoad = 
                            fileToLoad.Replace(
                                fileToLoad.Substring(
                                    fileToLoad.Length - 4), ".png");
                    }
                    loadedObjectList.Add(graphicHelper.getUnlockedBitmapHandle(fileToLoad));
                }
            }
            pictureBox1.Invalidate();
        }

        /**
        * @brief button1 클릭 이벤트
        * @details 버튼 클릭시 워커를 생성해 오는 데이터 메세지 큐에 저장하고, 타이머를 생성해서 메세지 큐를 주기적으로 확인하며, 메세지가 있으면 애니메이션을 시작한다.
        * @author 불명(김효상 확인)
        * @date 불명(2017-1-23 확인)
        */
        public void button1_Click(object sender, EventArgs e)
        {
            if (!innerStartAnim && ObjectList.imgList.Count > 0)
            {
                List<int>[] models = new List<int>[1];
                models[0] = new List<int>();
                for (int i = 0; i < loadedObjectList.Count(); i++)
                {
                    models[0].Add(i+1);
                }
                animHandler = new AnimatorHandler(g, pictureBox1);
                rsv = new MessageReceiver();
                worker = new BackgroundWorker();
                rsv.RunWorkerAsync();
                worker.DoWork += Worker_DoWork;
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
                innerStartAnim = true;
                //********************************************************************
            }
            else if (ObjectList.imgList.Count == 0)
            {
                MessageBox.Show("Load model and picture in 'Load Model' tab, before entering Animator.", "DEVS-Animation");
            }
        }

        /**
        * @brief BackgroundWorker가 진행될 때 생기는 이벤트
        * @author 불명(김효상 확인)
        * @date 불명(2017-1-23 확인)
        */
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(receivedMsg)) { 
                lock (key)
                {
                    obj.getRTB().Text = receivedMsg + " (Remaining messages: " + rsv.checkMsgCount().ToString() + ")";
                }
            }
        }
        /**
        * @brief BackgroundWorker가 Work할 때 사용하는 메서드
        * @author 불명(김효상 확인)
        * @date 불명(2017-1-23 확인)
        */
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            /*string name = (string)obj.getGridView()[1, 0].Value;*/
            obj.getGridView()[1, 0].Value = "";
            while (innerStartAnim && !worker.CancellationPending)
            {
                string msg;
                msg currentMessage;
                string[] res = rsv.checkMsg(out msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    lock (key)
                    {
                        receivedMsg = msg;
                    }
                    try
                    { 
                        currentMessage = new msg() { msgOrig = res };
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            obj.getGridView()[1, 0].Value = obj.ModelName(currentMessage.modelN);
                            obj.getGridView()[1, 1].Value = currentMessage.seqNo;
                            obj.getGridView()[1, 2].Value = currentMessage.X;
                            obj.getGridView()[1, 3].Value = currentMessage.Y;
                            obj.getGridView()[1, 4].Value = currentMessage.R;
                            obj.getGridView()[1, 5].Value = currentMessage.T;
                        });
                    }
                    catch (FormatException)
                    {
                        //작동과 무관함
                    }

                    worker.ReportProgress(1);
                    //큐 데이터가 움직임이다 
                    if (!string.IsNullOrEmpty(msg) && msg.Contains(','))
                    {
                        if (animHandler != null)
                            animHandler.startAnim(new msg() { msgOrig = res });
                    }
                }
            }
        }
        /**
        * @brief 배경화면 버튼을 클릭했을 때의 이벤트
        * @author 불명(김효상 확인)
        * @date 불명(2017-1-23 확인)
        */
        public void Background_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String name = dialog.FileName;
                innerAnimatorBitmap = new Bitmap(name);
                pictureBox1.Invalidate();
            }
        }

        /**
        * @brief Reset 클릭 이벤트
        * @details 진행 상황을 모두 종료하고 초기화한다.
        * @author 김민규
        * @date 2017-7-13
        */
        public void Reset_Click(object sender, EventArgs e)
        {
            innerStartAnim = false;
            if (worker != null)
                worker.CancelAsync();
            if (rsv != null)
                rsv.StopWorkerAsync();
            animHandler = null;
            innerAnimatorBitmap = null;
            g.Clear(Color.FromArgb(255, 0, 0, 0));
            loadedObjectList.Clear();
            pictureBox1.Invalidate();
            this.Invoke((MethodInvoker)delegate ()
            {
                obj.Clear();
            });
        }

        internal AnimatorHandler AnimatorHandler
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal ModelAnimator ModelAnimator
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal MessageReceiver MessageReceiver
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        private void Animator_Load(object sender, EventArgs e)
        {

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
