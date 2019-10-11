using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CPaint
{
    /// <summary>
    /// 선택된 툴
    /// </summary>
    public enum FIGURE
    {
        ARROW, CURVE, FREELINE, LINE, OVAL, POLYLINE, RECT, TEXT, ERASE, MERGE, BOX,FILL
    }

    /// <summary>
    /// 그려진 객체를 이동시킬때 사용한다. 
    /// </summary>
    public enum SELECTTYPE
    {
        // move형
        MOVE,

        // rect형 포인트일 경우
        LEFT_TOP,
        LEFT_MIDDLE,
        LEFT_BOTTOM,
        MIDDLE_TOP,
        MIDDLE_BOTTOM,
        RIGHT_TOP,
        RIGHT_MIDDLE,
        RIGHT_BOTTOM,

        // curve형
        START,
        END,
        ONE,
        TWO
    }


    /**
     * @brief 메인 폼
     * @details 리본 메뉴를 사용한 폼이다. http://www.codeproject.com/Articles/364272/Easily-Add-a-Ribbon-into-a-WinForms-Application-Cs 참고.
     * @author 박성식
     * @date 2016-07-20
     */
    public partial class Form1 : RibbonForm
    {
        //테스트용 코드입니다.
        private Navigation nav;


        /**
         * @param ObjectList 오브젝트 리스트
         */
        private ObjectList Obj;
        /**
         * @param View 뷰 
         */
        private View view;
        /**
         * @param Animator 애니메이터
         */
        private Animator ani;
        /**
         * @param objflag 오브젝트 리스트 보이기/숨기기용 플래그
         */
        private int objflag;
        /**
         * @param lastSavedPath 마지막으로 저장한 경로
         */
        private String lastSavedPath;

        /**
         * @brief   : View와 공유하는 Document를 수정 할 수 있도록 전역변수로 변경 (기존에는 public Form1()에 존재)
         * @author  : 장한결(8524hg@gmail.com)
         * @date    : 2019-05-11
         */
        private Document doc;


        /**
         * @brief 기본 생성자
         * @author 김효상(kimhys2@naver.com)
         * @date 2017-5-22
         */

        private Point LastPoint;

        private double ratio = 1.0F;
        private Point clickPoint;

        public Form1()
        {
            InitializeComponent();
            
            nav = new Navigation(this);
            nav.Height = this.panel1.Height;
            this.panel1.Controls.Add(nav);
            this.panel1.Width = nav.Width;
            
            //CreateDocument();
            //오브젝트 리스트 초기화
            Obj = new ObjectList(this);
            objflag = 1;
            Obj.Height = this.panel1.Height;
            this.panel1.Controls.Add(Obj);
            this.panel1.Width = Obj.Width;
            Obj.Show();


            //Document 생성
            doc = new Document(this);
            view = new View(this, doc);
            doc.viewList.Add(view);
            doc.setIsMultSel(false);
            doc.setCurrentTool(FIGURE.ARROW);
            doc.setCurrentColor(Color.Black);

            view.Height = mainpanel.Size.Height;
            view.Width = mainpanel.Size.Width;
            mainpanel.Controls.Add(view);
            view.Show();
            mainpanel.MouseWheel += new MouseEventHandler(mainpanel_MouseWheel);

            ratio = 1.0;

            mainpanel.Invalidate();

            //애니메이터
            ani = new Animator(this,Obj);
            ani.TopLevel = false;
            ani.Height = mainpanel.Size.Height;
            ani.Width = mainpanel.Size.Width;
            mainpanel.Controls.Add(ani);
            
            
            //더블 버퍼링
            DoubleBuffered = true;
            Stop.Visible = false;
            FFW.Visible = false;
            RWD.Visible = false;
        }
        //************************************************************

        /**
         * @brief Document 객체를 생성한다.
         * @author 박성식
         * @date 2016-07-20
         * @return Document 본 폼을 부모로 삼는 Document를 생성한다.
         */
        private Document CreateDocument()
        {
            return new Document(this);
        }

        /**
         * @brief 현재 활성화된 Document 객체를 구한다.
         * @author 박성식
         * @date 2016-07-20
         * @return Document 현재 활성화된 Document 객체
         */
        private Document GetActiveDocument()
        {
            Document activeDoc = GetActiveView().GetDocument();
            return activeDoc;
        }

        /**
         * @brief 현재 활성화된 뷰의 레퍼런스를 구한다.
         * @author 박성식
         * @date 2016-07-20
         * @return View 현재 활성화된 뷰의 레퍼런스
         */
        private View GetActiveView()
        {
            return (View)this.ActiveMdiChild;
        }

        /**
         * @brief FILE 버튼 눌렀을시 핸들러
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        /// <summary>
        /// FILE 버튼 눌렀을시 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrbMenuHandler(object sender, MouseEventArgs e)
        {
            if (sender == ribbonOrbMenuItemNew)
            {
                SaveChangesAndReturnCanceled();
                doc.bgReset();
                doc.UpdateAllViews();
            }
            else if (sender == ribbonOrbMenuItemLoad)
            {
                Open();
            }
            /**
            * @brief: File탭의 Background 메뉴가 선택되었을 때 발생하는 이벤트
            * @author 장한결(8524hg@gmail.com)
            * @date 2019-05-12
            */
            else if (sender == ribbonOrbMenuItemBackgound)
            {
                bgPopUp(doc);
            }
            else if (sender == ribbonOrbMenuItemSave)
            {
                SaveAsBoth();
            }
            //**************************************************
            //이름 : 조은성(chot1198@gmail.com) [2016-10-17]
            //내용 : Info 주석처리
            //이유 : 유효하지 않은 내용
            /*
            else if (sender == ribbonOrbMenuItemInfo)
            {
                MessageBox.Show("CPaint 원작자 (Email:sofplus@yahoo.co.kr)\nRibbon Menu (http://officeribbon.codeplex.com/)");
            }
            */
            //**************************************************
            else if (sender == ribbonOrbMenuItemExit)
            {
                Exit();
            }
        }

        /**
         * @brief 작은 툴바 버튼 눌렀을 시 핸들러
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        private void smallButtonHandler(object sender, MouseEventArgs e)
        {
            if (sender == ribbonButtonSave)
            {
                SaveAsBoth();
            }
            else if (sender == ribbonButtonUndo)
            {
                GetActiveDocument().undo();
            }
        }

        /**
         * @brief Editor/Animation 탭 버튼 핸들러
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        public void RibbonGeneralHandler(object sender, MouseEventArgs e)
        {
            FIGURE f = FIGURE.ARROW;
            if (sender == ribbonButtonArrow)
                f = FIGURE.ARROW;
            else if (sender == ribbonButtonLine)
                f = FIGURE.LINE;
            else if (sender == ribbonButtonFreeLine)
                f = FIGURE.FREELINE;
            else if (sender == ribbonButtonCircle)
                f = FIGURE.OVAL;
            else if (sender == ribbonButtonRect)
                f = FIGURE.RECT;
            //else if (sender == toolStrip2Text)
            //    f = FIGURE.TEXT;
            else if (sender == ribbonButtonErase)
                f = FIGURE.ERASE;
            else if (sender == ribbonButtonBox)
                f = FIGURE.BOX;
            else if (sender == ribbonButtonFill)
                f = FIGURE.FILL;
            else if (sender == ribbonButtonCopy)
            {
                GetActiveDocument().copy();
            }
            else if (sender == ribbonButtonCut)
            {
                GetActiveDocument().cut();
            }
            else if (sender == ribbonButtonPaste)
            {
                GetActiveDocument().paste();
            }
            else if (sender == ribbonButtonMerge)
            {
                GetActiveDocument().mergeFigure();
                GetActiveDocument().UpdateAllViews();
            }
            else if (sender == ribbonButtonUnmerge)
            {
                GetActiveDocument().unmergeFigure();
                GetActiveDocument().UpdateAllViews();
            }
            //새로 추가된 4 버튼 핸들러입니다. 수정하시면 됩니다.
            else if(sender == ribbonButtonZoomIn)
            {
                MessageBox.Show("줌인");
            }
            else if (sender == ribbonButtonZoomOut)
            {
                MessageBox.Show("줌아웃");
            }
            else if (sender == ribbonButtonOriginal)
            {
                MessageBox.Show("원래대로");
            }
            else if (sender == ribbonButtonHand)
            {
                MessageBox.Show("손");
            }
            GetActiveDocument().deselectAll();
            GetActiveDocument().setCurrentTool(f);

        }

        /**
         * @brief AniFile 탭 버튼 핸들러
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        public void RibbonGeneralHandler2(object sender, MouseEventArgs e)
        {     
            if (sender == ShowList && objflag == 0)
            {
                Obj.Show();
                objflag = 1;
            }
            else if (sender == HideList && objflag == 1)
            {
                Obj.Hide();
                objflag = 0;
            }
            else if (sender == ObjectLoad)
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.InitialDirectory = lastSavedPath;
                OFD.Title = "Object Load";      // Dialog 창의 이름
                OFD.FileName = "ObjectName";      // Dialog 창 내의 이름 예시
                OFD.Multiselect = true;     // 다중선택 가능
                OFD.Filter = "Pictures (*.jpg, *.png, *.bmp) | *.jpg; *.png; *.bmp;"; // 파일 필터

                DialogResult result = OFD.ShowDialog();
                FileInfo[] files = new FileInfo[OFD.FileNames.Length];

                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < OFD.FileNames.Length; i++)
                    {
                        files[i] = new FileInfo(OFD.FileNames[i]);
                    }
                    Obj.Object_Load(files);
                    
                    ani.refreshPictureBox();
                }
            }
            else if (sender == FolderSetting)
            {
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                FBD.Description = "Select folder with *.m.cpp files";
                DialogResult result = FBD.ShowDialog();
                Obj.Set_model_path(FBD.SelectedPath);
            }
            if (Obj.bothSet())
            {
                rbtAnimation.Text = "Animation";
                changeEnabled(rbtAnimation, true);
            }
        }

        /**
         * @brief 파일을 여는 데 사용하는 메서드
         * @author 김민규
         * @date 2017-07-20
         */
        private void Open()
        {
            if (SaveChangesAndReturnCanceled())
                return;
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "CPaint Files (*.sof)|*.sof";
            openDlg.FileName = "";
            openDlg.DefaultExt = ".sof";
            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;
            openDlg.Multiselect = true;
            DialogResult res = openDlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                GetActiveDocument().Clear();
                for (int i = 0; i < openDlg.FileNames.Length; i++)
                { 
                    if ((openDlg.FileNames[i]).ToLower().EndsWith(".sof"))
                    {
                        GetActiveDocument().OpenDocument(openDlg.FileNames[i]);
                    }
                    else
                    {
                        MessageBox.Show("not supported extension");
                    }
                }
            }
            GetActiveDocument().UpdateAllViews();
            GetActiveView().WindowState = FormWindowState.Maximized;
        }

        /**
         * @brief 파일을 두가지 형식 모두 저장하는 메서드
         * @author 김민규
         * @return 저장된 sof파일 경로를 반환한다.
         * @date 2017-07-20
         */
        private List<String> SaveAsBoth() 
        {
            List<String> result = new List<String>();
            GetActiveDocument().deselectAll();

            GetActiveDocument().mergeAllFigure();

            GetActiveDocument().setTempGroupnames();
            List<string> groupnames = GetActiveDocument().getGroupnames();
            foreach (string groupname in groupnames)
            {
                if (groupname.StartsWith("<"))
                {
                    GetActiveDocument().setIsMultSel(true);
                    GetActiveDocument().deselectAll();
                    GetActiveDocument().selectGroup(groupname);
                    GetActiveDocument().UpdateAllViews();
                    List<String> savedFiles = new List<String>();
                    SaveFileDialog saveDlg = new SaveFileDialog();
                    saveDlg.Filter = "SOF (*.sof)|*.sof";
                    saveDlg.DefaultExt = ".sof";
                    saveDlg.FileName = "untitled.sof";
                    saveDlg.Title = "Saving " + groupname + " (see selected figures to check range)";
                    DialogResult res = saveDlg.ShowDialog();
                    GetActiveDocument().setIsMultSel(false);
                    GetActiveDocument().deselectAll();
                    GetActiveDocument().UpdateAllViews();
                    if (res == DialogResult.OK)
                    {
                        result.Add(GetActiveDocument().SaveDocument(saveDlg.FileName, groupname));
                        string pngPath = Path.ChangeExtension(saveDlg.FileName, ".png");
                        GetActiveDocument().SavePNG(pngPath, saveDlg.FileName);
                    }
                }
                else
                {
                    result.Add(GetActiveDocument().SaveDocument(groupname, groupname));
                    string pngPath = Path.ChangeExtension(groupname, ".png");
                    GetActiveDocument().SavePNG(pngPath, groupname);
                }
            }
            if (result.Count > 0)
                lastSavedPath = Path.GetDirectoryName(result[result.Count - 1]);
            GetActiveDocument().resetTempGroupnames();

            return result;
        }

        /**
         * @brief 색상을 선택할 수 있는 창을 띄우는 이벤트
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        private void colorPicBox_Click(object sender, MouseEventArgs e)
        {
            //**************************************************
            //이름 : 박성식 [2016-07-17]
            //내용 : 컬러선택 다이얼로그를 열어서 선택한 색 다큐먼트의 색으로 설정
            //이유 : 컬러링을 위한 색상 선택 방법이 필요
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                GetActiveDocument().setCurrentColor(dialog.Color);
                ribbonColorChooser.Color = dialog.Color;
            }
            //***************************************************
        }

        /**
         * @brief 프로그램을 종료한다.
         * @author 박성식
         * @date 2016-07-20
         */
        private void Exit()
        {
            Close();
        }

        /**
         * @brief 탭 선택시 Animation 리본탭 보이기/숨기기(editor,animator 통합으로 주석화)
         * @author 김효상(kimhys2@naver.com)
         * @date 2017-5-22
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
       /* private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabAnim)
            {
                rbtAnimation.Visible = true;
                ribbon1.ActiveTab = rbtAnimation; //PSS 애니메이터 선택시 탭 변경
                rbtEditor.Visible = false;

                //FILE메뉴 아이템 숨기기 불가능 enable->disable
                ribbonOrbMenuItemNew.Enabled = false;
                ribbonOrbMenuItemLoad.Enabled = false;
                ribbonOrbMenuItemSave.Enabled = false;
                ribbonDescriptionMenuItemBoth.Enabled = false;
                ribbonDescriptionMenuItemPNG.Enabled = false;
                ribbonDescriptionMenuItemSOF.Enabled = false;
            }
            else {
                rbtAnimation.Visible = false;
                rbtEditor.Visible = true;
                ribbon1.ActiveTab = rbtEditor;

                ribbonOrbMenuItemNew.Enabled = true;
                ribbonOrbMenuItemLoad.Enabled = true;
                ribbonOrbMenuItemSave.Enabled = true;
                ribbonDescriptionMenuItemBoth.Enabled = true;
                ribbonDescriptionMenuItemPNG.Enabled = true;
                ribbonDescriptionMenuItemSOF.Enabled = true;
            }
            this.ribbon1.Refresh();
        }*/

        /**
         * @brief 애니메이션 시작 버튼 이벤트
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        private void Start_Click(object sender, EventArgs e)
        {
            rbtEditor.Text = "To do anything else, reset animation";
            rbtobj.Text = "";
            rbtobj.Visible = false;
            changeEnabled(rbtEditor, false);
            changeEnabled(rbtobj, false);
            ani.button1_Click(sender,e);
        }

        /**
         * @brief 배경화면 버튼 클릭 이벤트
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        private void ribbonButtonBG_Click(object sender, EventArgs e)
        {
            ani.Background_Click(sender, e);
        }

        /**
         * @brief pictureBox에 그리는 이벤트
         * @author 박성식
         * @date 2016-07-20
         * @param sender 이벤트 발생시킨 객체
         * @param e 마우스 이벤트 관련 argument들
         */
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ani.pictureBox1_Paint(sender, e);
        }

        private void tabEdit_Click(object sender, EventArgs e)
        {

        }

        private void ribbon1_Click(object sender, EventArgs e)
        {

        }
        /**
        * @brief Editor와 Animator를 전환할 때 사용되는 리스너
        * @Detail 탭 번호를 확인해 필요한 창을 띄운다.
        * @author 김민규
        * @date 2017-07-31
        */
        private void ribbon1_TabIndexChanged(object sender, EventArgs e)
        {
            if (ribbon1.TabIndex == 0)
            {
                view.Show();
                ani.Hide();
            }
            else
            {
                view.Hide();
                ani.Show();
            }
            this.ribbon1.Refresh();
        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ribbon1_TabStopChanged(object sender, EventArgs e)
        {
        }
        /**
        * @brief Editor와 Animator를 전환할 때 사용되는 리스너
        * @Detail 전환할 때 숨겨져있는 창의 크기를 조정하고 띄우고 숨겨야할 창을 숨기며 저장된 이미지를 미리 띄워주며 각종 예외처리까지 담당한다.
        * @author 김민규
        * @date 2017-07-31
        */
        private void rbtEditor_ActiveChanged(object sender, EventArgs e)
        {
            if (rbtEditor.Active)
            {
                if (!ani.Visible) { 
                    view.Show();

                    view.Width = ani.Width;
                    view.Height = ani.Height;
                    view.Left = ani.Left;
                    view.Top = ani.Top;
                }
                foreach (List<String> currentList in ObjectList.imgList)
                {
                    foreach (String path in currentList)
                    {
                        String pathToFind = path;
                        if (pathToFind.EndsWith(".png"))
                        {
                            pathToFind =
                                pathToFind.Replace(
                                    pathToFind.Substring(
                                        pathToFind.Length - 4), ".sof");
                        }
                        if (File.Exists(pathToFind))
                            GetActiveDocument().OpenDocument(pathToFind);
                    }
                }
            }
            else
            {
                ObjectList.imgList.Clear();
                ObjectList.imgNumList.Clear();
                ObjectList.imgBitmapList.Clear();
                ObjectList.modelList.Clear();
                Obj.ClearUI();
                ani.refreshPictureBox();

                List<String> pathToLoad = GetActiveDocument().getGroupnames();
                if (GetActiveDocument().needSave)
                {
                    DialogResult result = MessageBox.Show("Do you want to save changes?\nTo preserve progress, press Yes.",
                            "DEVS Animation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            SaveAsBoth();
                            pathToLoad = GetActiveDocument().getGroupnames();
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                view.Hide();
                GetActiveDocument().Clear();
                
                ani.Width = view.Width;
                ani.Height = view.Height;
                ani.Left = view.Left;
                ani.Top = view.Top;

                FileInfo[] fileInfoToLoad = new FileInfo[pathToLoad.Count];
                for (int currentIndex = 0; currentIndex < pathToLoad.Count; ++currentIndex)
                {
                    fileInfoToLoad[currentIndex] = new FileInfo(pathToLoad[currentIndex]);
                }
                Obj.Object_Load(fileInfoToLoad);
               
                ani.refreshPictureBox();
            }
        }

        /**
       * @brief 애니메이터와 에디터를 암시적으로 전환해주는 장치
       * @Detail 애니메이터를 관리한다. rbtAnimation을 검사해 창을 띄울지 말지 결정한다.
       * @author 김민규
       * @date 2017-07-31
       */

        private void rbtAnimation_ActiveChanged(object sender, EventArgs e)
        {
            if (rbtAnimation.Active)
            {
                if (!view.Visible)
                    ani.Show();
            }
            else if (!rbtobj.Active)
            {
                if (!ani.startedSimulation)
                    ani.Hide();
            }
        }

        private void ribbon1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        /**
        * @brief 리셋 버튼 관리 리스너
        * @author 김민규
        * @date 2017-07-31
        */

        private void rbtReset_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (sender == ribbonButtonReset)
            { 
                result = MessageBox.Show("Animation will be stopped immediately\nand any progress will be lost. Continue?",
                    "DEVS Animation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
            }
            else
            {
                result = DialogResult.Yes;
            }
            switch (result)
            {
                case DialogResult.Yes:
                    rbtEditor.Text = "Editor";
                    rbtobj.Text = "Load model/object";
                    rbtAnimation.Text = "To start animation, load model/object";
                    rbtobj.Visible = true;
                    changeEnabled(rbtEditor, true);
                    changeEnabled(rbtobj, true);
                    changeEnabled(rbtAnimation, false);
                    ani.Reset_Click(sender, e);
                    return;
                case DialogResult.No:
                    return;
                default:
                    return;
            }
        }

        /**
        * @brief 애니메이터와 에디터를 암시적으로 전환해주는 장치
        * @Detail 애니메이터를 관리한다. rbtobj를 검사해 창을 띄울지 말지 결정한다.
        * @author 김민규
        * @date 2017-07-31
        */

        private void rbtobj_ActiveChanged(object sender, EventArgs e)
        {
            if (rbtobj.Active)
            {
                if (!view.Visible)
                    ani.Show();
            }
            else if (!rbtAnimation.Active)
            {
                if (!ani.startedSimulation)
                    ani.Hide();
            }
        }

        /**
        * @brief 애니메이션 중단 버튼 관련 리스너
        * @author 김민규
        * @date 2017-07-31
        */

        private void changeEnabled(RibbonTab ribbonPanel, bool newEnabled)
        {
            IEnumerator<Component> currentComponent = ribbonPanel.GetAllChildComponents().GetEnumerator();
            IEnumerator<Component> currentInnerComponent;
            while (currentComponent.MoveNext())
            {
                if (currentComponent.Current is RibbonPanel)
                {
                    currentInnerComponent = (currentComponent.Current as RibbonPanel).GetAllChildComponents().GetEnumerator();
                    while (currentInnerComponent.MoveNext())
                        if (currentInnerComponent.Current is RibbonButton)
                            (currentInnerComponent.Current as RibbonButton).Enabled = newEnabled;
                }
            }
        }

        /**
        * @brief 애니메이션 실행 중 프로그램을 닫으려는 이벤트를 관리하는 리스너
        * @author 김민규
        * @date 2017-07-31
        */

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = SaveChangesAndReturnCanceled();
            if (!e.Cancel && ani.startedSimulation)
            {
                DialogResult result = MessageBox.Show("Animation will be stopped immediately\nand any progress will be lost. Continue?",
                    "DEVS Animation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        ani.Reset_Click(sender, e);
                        return;
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                    default:
                        return;
                }
            }
        }
        /**
        * @brief 변경사항을 저장하지 않고 이동할 때 뜨는 대화창 관련 리스너
        * @author 김민규
        * @date 2017-07-31
        */
        private bool SaveChangesAndReturnCanceled()
        {
            if (GetActiveDocument().needSave)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?",
                "DEVS Animation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveAsBoth();
                        GetActiveDocument().Clear();
                        return false;
                    case DialogResult.No:
                        GetActiveDocument().Clear();
                        return false;
                    case DialogResult.Cancel:
                        return true;
                    default:
                        GetActiveDocument().Clear();
                        return false;
                }
            }
            else
            {
                ani.Reset_Click(null, null);
                GetActiveDocument().Clear();
                GetActiveDocument().UpdateAllViews();
            }
            return false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /**
        * @brief: Background 옵션을 선택하는 팝업창 메소드
        * @author 장한결(8524hg@gmail.com)
        * @date 2019-05-12
        */
        private void bgPopUp(Document document)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Title = "배경 이미지";
            ofd.Filter = "JPG|*.jpg|JPEG|*.jpeg|GIF|*.gif|PNG|*.png";
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                BackgroundPopUp bg = new BackgroundPopUp(document);
                bg.Show();
                document.setBackground(ofd.FileName);
            }
        }

        private void mainpanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            
            Panel pn = (Panel)sender;
            
            if (lines > 0)
            {
                ratio *= 1.1F;
                if (ratio > 100.0) ratio = 100.0;
            }
            else if (lines < 0)
            {
                ratio *= 0.9F;
                //if (ratio < 1) ratio = 1;
            }

            int X = e.X; 
            int Y = e.Y;
            

        }

    }
}
