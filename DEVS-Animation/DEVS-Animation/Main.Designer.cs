﻿namespace CPaint
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonOrbMenuItemNew = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonOrbMenuItemLoad = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonOrbMenuItemBackgound = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonOrbMenuItemSave = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonOrbMenuItemExit = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonButtonSave = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonUndo = new System.Windows.Forms.RibbonButton();
            this.rbtEditor = new System.Windows.Forms.RibbonTab();
            this.ribbonPanelSelect = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonArrow = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonBox = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.ribbonButtonErase = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator2 = new System.Windows.Forms.RibbonSeparator();
            this.ribbonButtonMerge = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonUnmerge = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelFigure = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonRect = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonCircle = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonLine = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonFreeLine = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelColor = new System.Windows.Forms.RibbonPanel();
            this.ribbonColorChooser = new System.Windows.Forms.RibbonColorChooser();
            this.ribbonButtonFill = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelTools = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonCopy = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonCut = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonPaste = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonZoomIn = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonZoomOut = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonOriginal = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonHand = new System.Windows.Forms.RibbonButton();
            this.rbtobj = new System.Windows.Forms.RibbonTab();
            this.ListShow = new System.Windows.Forms.RibbonPanel();
            this.ShowList = new System.Windows.Forms.RibbonButton();
            this.HideList = new System.Windows.Forms.RibbonButton();
            this.Object = new System.Windows.Forms.RibbonPanel();
            this.FolderSetting = new System.Windows.Forms.RibbonButton();
            this.ObjectLoad = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonLoadReset = new System.Windows.Forms.RibbonButton();
            this.rbtAnimation = new System.Windows.Forms.RibbonTab();
            this.Animation_control = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonStart = new System.Windows.Forms.RibbonButton();
            this.Stop = new System.Windows.Forms.RibbonButton();
            this.RWD = new System.Windows.Forms.RibbonButton();
            this.FFW = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelBg = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonBG = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelRS = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonReset = new System.Windows.Forms.RibbonButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainpanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemNew);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemLoad);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemBackgound);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemSave);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemExit);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 292);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbImage = null;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon1.OrbText = "FILE";
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.Items.Add(this.ribbonButtonSave);
            this.ribbon1.QuickAcessToolbar.Items.Add(this.ribbonButtonUndo);
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon1.Size = new System.Drawing.Size(1337, 159);
            this.ribbon1.TabIndex = 7;
            this.ribbon1.Tabs.Add(this.rbtEditor);
            this.ribbon1.Tabs.Add(this.rbtobj);
            this.ribbon1.Tabs.Add(this.rbtAnimation);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            this.ribbon1.TabIndexChanged += new System.EventHandler(this.ribbon1_TabIndexChanged);
            this.ribbon1.TabStopChanged += new System.EventHandler(this.ribbon1_TabStopChanged);
            this.ribbon1.Click += new System.EventHandler(this.ribbon1_Click);
            this.ribbon1.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.ribbon1_ChangeUICues);
            // 
            // ribbonOrbMenuItemNew
            // 
            this.ribbonOrbMenuItemNew.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemNew.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemNew.Image")));
            this.ribbonOrbMenuItemNew.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemNew.SmallImage")));
            this.ribbonOrbMenuItemNew.Text = "New";
            this.ribbonOrbMenuItemNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OrbMenuHandler);
            // 
            // ribbonOrbMenuItemLoad
            // 
            this.ribbonOrbMenuItemLoad.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemLoad.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemLoad.Image")));
            this.ribbonOrbMenuItemLoad.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemLoad.SmallImage")));
            this.ribbonOrbMenuItemLoad.Text = "Load";
            this.ribbonOrbMenuItemLoad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OrbMenuHandler);
            // 
            // ribbonOrbMenuItemBackgound
            // 
            this.ribbonOrbMenuItemBackgound.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemBackgound.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemBackgound.Image")));
            this.ribbonOrbMenuItemBackgound.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemBackgound.SmallImage")));
            this.ribbonOrbMenuItemBackgound.Text = "Background";
            this.ribbonOrbMenuItemBackgound.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OrbMenuHandler);
            // 
            // ribbonOrbMenuItemSave
            // 
            this.ribbonOrbMenuItemSave.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemSave.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemSave.Image")));
            this.ribbonOrbMenuItemSave.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemSave.SmallImage")));
            this.ribbonOrbMenuItemSave.Text = "Save";
            this.ribbonOrbMenuItemSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OrbMenuHandler);
            // 
            // ribbonOrbMenuItemExit
            // 
            this.ribbonOrbMenuItemExit.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemExit.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemExit.Image")));
            this.ribbonOrbMenuItemExit.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItemExit.SmallImage")));
            this.ribbonOrbMenuItemExit.Text = "Exit";
            this.ribbonOrbMenuItemExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OrbMenuHandler);
            // 
            // ribbonButtonSave
            // 
            this.ribbonButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSave.Image")));
            this.ribbonButtonSave.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.ribbonButtonSave.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSave.SmallImage")));
            this.ribbonButtonSave.Text = "ribbonButtonsave";
            this.ribbonButtonSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.smallButtonHandler);
            // 
            // ribbonButtonUndo
            // 
            this.ribbonButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonUndo.Image")));
            this.ribbonButtonUndo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.ribbonButtonUndo.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonUndo.SmallImage")));
            this.ribbonButtonUndo.Text = "ribbonButtonundo";
            this.ribbonButtonUndo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.smallButtonHandler);
            // 
            // rbtEditor
            // 
            this.rbtEditor.Panels.Add(this.ribbonPanelSelect);
            this.rbtEditor.Panels.Add(this.ribbonPanelFigure);
            this.rbtEditor.Panels.Add(this.ribbonPanelColor);
            this.rbtEditor.Panels.Add(this.ribbonPanelTools);
            this.rbtEditor.Panels.Add(this.ribbonPanel1);
            this.rbtEditor.Text = "Editor";
            this.rbtEditor.ActiveChanged += new System.EventHandler(this.rbtEditor_ActiveChanged);
            // 
            // ribbonPanelSelect
            // 
            this.ribbonPanelSelect.ButtonMoreVisible = false;
            this.ribbonPanelSelect.Items.Add(this.ribbonButtonArrow);
            this.ribbonPanelSelect.Items.Add(this.ribbonButtonBox);
            this.ribbonPanelSelect.Items.Add(this.ribbonSeparator1);
            this.ribbonPanelSelect.Items.Add(this.ribbonButtonErase);
            this.ribbonPanelSelect.Items.Add(this.ribbonSeparator2);
            this.ribbonPanelSelect.Items.Add(this.ribbonButtonMerge);
            this.ribbonPanelSelect.Items.Add(this.ribbonButtonUnmerge);
            this.ribbonPanelSelect.Text = "Select";
            // 
            // ribbonButtonArrow
            // 
            this.ribbonButtonArrow.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonArrow.Image")));
            this.ribbonButtonArrow.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonArrow.SmallImage")));
            this.ribbonButtonArrow.Text = "Arrow";
            this.ribbonButtonArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonBox
            // 
            this.ribbonButtonBox.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonBox.Image")));
            this.ribbonButtonBox.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonBox.SmallImage")));
            this.ribbonButtonBox.Text = "Box";
            this.ribbonButtonBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonErase
            // 
            this.ribbonButtonErase.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonErase.Image")));
            this.ribbonButtonErase.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonErase.SmallImage")));
            this.ribbonButtonErase.Text = "Erase";
            this.ribbonButtonErase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonMerge
            // 
            this.ribbonButtonMerge.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonMerge.Image")));
            this.ribbonButtonMerge.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonMerge.SmallImage")));
            this.ribbonButtonMerge.Text = "Merge";
            this.ribbonButtonMerge.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonUnmerge
            // 
            this.ribbonButtonUnmerge.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonUnmerge.Image")));
            this.ribbonButtonUnmerge.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonUnmerge.SmallImage")));
            this.ribbonButtonUnmerge.Text = "unMerge";
            this.ribbonButtonUnmerge.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonPanelFigure
            // 
            this.ribbonPanelFigure.ButtonMoreVisible = false;
            this.ribbonPanelFigure.Items.Add(this.ribbonButtonRect);
            this.ribbonPanelFigure.Items.Add(this.ribbonButtonCircle);
            this.ribbonPanelFigure.Items.Add(this.ribbonButtonLine);
            this.ribbonPanelFigure.Items.Add(this.ribbonButtonFreeLine);
            this.ribbonPanelFigure.Text = "Figure";
            // 
            // ribbonButtonRect
            // 
            this.ribbonButtonRect.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonRect.Image")));
            this.ribbonButtonRect.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonRect.SmallImage")));
            this.ribbonButtonRect.Text = "Rect";
            this.ribbonButtonRect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonCircle
            // 
            this.ribbonButtonCircle.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonCircle.Image")));
            this.ribbonButtonCircle.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonCircle.SmallImage")));
            this.ribbonButtonCircle.Text = "Circle";
            this.ribbonButtonCircle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonLine
            // 
            this.ribbonButtonLine.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonLine.Image")));
            this.ribbonButtonLine.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonLine.SmallImage")));
            this.ribbonButtonLine.Text = "Line";
            this.ribbonButtonLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonFreeLine
            // 
            this.ribbonButtonFreeLine.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFreeLine.Image")));
            this.ribbonButtonFreeLine.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFreeLine.SmallImage")));
            this.ribbonButtonFreeLine.Text = "FreeLine";
            this.ribbonButtonFreeLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonPanelColor
            // 
            this.ribbonPanelColor.ButtonMoreVisible = false;
            this.ribbonPanelColor.Items.Add(this.ribbonColorChooser);
            this.ribbonPanelColor.Items.Add(this.ribbonButtonFill);
            this.ribbonPanelColor.Text = "Color";
            // 
            // ribbonColorChooser
            // 
            this.ribbonColorChooser.Color = System.Drawing.Color.Black;
            this.ribbonColorChooser.Image = ((System.Drawing.Image)(resources.GetObject("ribbonColorChooser.Image")));
            this.ribbonColorChooser.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonColorChooser.SmallImage")));
            this.ribbonColorChooser.Text = "Color";
            this.ribbonColorChooser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colorPicBox_Click);
            // 
            // ribbonButtonFill
            // 
            this.ribbonButtonFill.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFill.Image")));
            this.ribbonButtonFill.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFill.SmallImage")));
            this.ribbonButtonFill.Text = "Fill";
            this.ribbonButtonFill.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonPanelTools
            // 
            this.ribbonPanelTools.ButtonMoreVisible = false;
            this.ribbonPanelTools.Items.Add(this.ribbonButtonCopy);
            this.ribbonPanelTools.Items.Add(this.ribbonButtonCut);
            this.ribbonPanelTools.Items.Add(this.ribbonButtonPaste);
            this.ribbonPanelTools.Text = "Tools";
            // 
            // ribbonButtonCopy
            // 
            this.ribbonButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonCopy.Image")));
            this.ribbonButtonCopy.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonCopy.SmallImage")));
            this.ribbonButtonCopy.Text = "Copy";
            this.ribbonButtonCopy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonCut
            // 
            this.ribbonButtonCut.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonCut.Image")));
            this.ribbonButtonCut.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonCut.SmallImage")));
            this.ribbonButtonCut.Text = "Cut";
            this.ribbonButtonCut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonPaste
            // 
            this.ribbonButtonPaste.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonPaste.Image")));
            this.ribbonButtonPaste.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonPaste.SmallImage")));
            this.ribbonButtonPaste.Text = "Paste";
            this.ribbonButtonPaste.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbonButtonZoomIn);
            this.ribbonPanel1.Items.Add(this.ribbonButtonZoomOut);
            this.ribbonPanel1.Items.Add(this.ribbonButtonOriginal);
            this.ribbonPanel1.Items.Add(this.ribbonButtonHand);
            this.ribbonPanel1.Text = "Zooming";
            this.ribbonPanel1.Visible = false;
            // 
            // ribbonButtonZoomIn
            // 
            this.ribbonButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonZoomIn.Image")));
            this.ribbonButtonZoomIn.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonZoomIn.SmallImage")));
            this.ribbonButtonZoomIn.Text = "ZoomIn";
            this.ribbonButtonZoomIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonZoomOut
            // 
            this.ribbonButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonZoomOut.Image")));
            this.ribbonButtonZoomOut.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonZoomOut.SmallImage")));
            this.ribbonButtonZoomOut.Text = "ZoomOut";
            this.ribbonButtonZoomOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonOriginal
            // 
            this.ribbonButtonOriginal.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonOriginal.Image")));
            this.ribbonButtonOriginal.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonOriginal.SmallImage")));
            this.ribbonButtonOriginal.Text = "Original";
            this.ribbonButtonOriginal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // ribbonButtonHand
            // 
            this.ribbonButtonHand.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonHand.Image")));
            this.ribbonButtonHand.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonHand.SmallImage")));
            this.ribbonButtonHand.Text = "Hand";
            this.ribbonButtonHand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler);
            // 
            // rbtobj
            // 
            this.rbtobj.Panels.Add(this.ListShow);
            this.rbtobj.Panels.Add(this.Object);
            this.rbtobj.Panels.Add(this.ribbonPanel2);
            this.rbtobj.Text = "Load model/object";
            this.rbtobj.ActiveChanged += new System.EventHandler(this.rbtobj_ActiveChanged);
            // 
            // ListShow
            // 
            this.ListShow.ButtonMoreVisible = false;
            this.ListShow.Items.Add(this.ShowList);
            this.ListShow.Items.Add(this.HideList);
            this.ListShow.Text = "Animation Status";
            // 
            // ShowList
            // 
            this.ShowList.Image = ((System.Drawing.Image)(resources.GetObject("ShowList.Image")));
            this.ShowList.SmallImage = ((System.Drawing.Image)(resources.GetObject("ShowList.SmallImage")));
            this.ShowList.Text = "Show";
            this.ShowList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler2);
            // 
            // HideList
            // 
            this.HideList.Image = ((System.Drawing.Image)(resources.GetObject("HideList.Image")));
            this.HideList.SmallImage = ((System.Drawing.Image)(resources.GetObject("HideList.SmallImage")));
            this.HideList.Text = "Hide";
            this.HideList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler2);
            // 
            // Object
            // 
            this.Object.ButtonMoreVisible = false;
            this.Object.Items.Add(this.FolderSetting);
            this.Object.Items.Add(this.ObjectLoad);
            this.Object.Text = "Load to start animation";
            // 
            // FolderSetting
            // 
            this.FolderSetting.Image = ((System.Drawing.Image)(resources.GetObject("FolderSetting.Image")));
            this.FolderSetting.SmallImage = ((System.Drawing.Image)(resources.GetObject("FolderSetting.SmallImage")));
            this.FolderSetting.Text = "Model Folder";
            this.FolderSetting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler2);
            // 
            // ObjectLoad
            // 
            this.ObjectLoad.Image = ((System.Drawing.Image)(resources.GetObject("ObjectLoad.Image")));
            this.ObjectLoad.SmallImage = ((System.Drawing.Image)(resources.GetObject("ObjectLoad.SmallImage")));
            this.ObjectLoad.Text = "Object (Picture)";
            this.ObjectLoad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RibbonGeneralHandler2);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ButtonMoreEnabled = false;
            this.ribbonPanel2.ButtonMoreVisible = false;
            this.ribbonPanel2.Items.Add(this.ribbonButtonLoadReset);
            this.ribbonPanel2.Text = "Reset";
            // 
            // ribbonButtonLoadReset
            // 
            this.ribbonButtonLoadReset.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonLoadReset.Image")));
            this.ribbonButtonLoadReset.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonLoadReset.SmallImage")));
            this.ribbonButtonLoadReset.Text = "Reset";
            this.ribbonButtonLoadReset.Click += new System.EventHandler(this.rbtReset_Click);
            // 
            // rbtAnimation
            // 
            this.rbtAnimation.Panels.Add(this.Animation_control);
            this.rbtAnimation.Panels.Add(this.ribbonPanelBg);
            this.rbtAnimation.Panels.Add(this.ribbonPanelRS);
            this.rbtAnimation.Text = "To start animation, load model/object";
            this.rbtAnimation.ToolTip = "";
            this.rbtAnimation.ActiveChanged += new System.EventHandler(this.rbtAnimation_ActiveChanged);
            // 
            // Animation_control
            // 
            this.Animation_control.ButtonMoreVisible = false;
            this.Animation_control.Items.Add(this.ribbonButtonStart);
            this.Animation_control.Items.Add(this.Stop);
            this.Animation_control.Items.Add(this.RWD);
            this.Animation_control.Items.Add(this.FFW);
            this.Animation_control.Text = "Animaton Control";
            // 
            // ribbonButtonStart
            // 
            this.ribbonButtonStart.Enabled = false;
            this.ribbonButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStart.Image")));
            this.ribbonButtonStart.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStart.SmallImage")));
            this.ribbonButtonStart.Text = "Start";
            this.ribbonButtonStart.ToolTipTitle = "Start";
            this.ribbonButtonStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.SmallImage = ((System.Drawing.Image)(resources.GetObject("Stop.SmallImage")));
            this.Stop.Text = "Stop";
            // 
            // RWD
            // 
            this.RWD.Image = ((System.Drawing.Image)(resources.GetObject("RWD.Image")));
            this.RWD.SmallImage = ((System.Drawing.Image)(resources.GetObject("RWD.SmallImage")));
            this.RWD.Text = "RWD";
            // 
            // FFW
            // 
            this.FFW.Image = ((System.Drawing.Image)(resources.GetObject("FFW.Image")));
            this.FFW.SmallImage = ((System.Drawing.Image)(resources.GetObject("FFW.SmallImage")));
            this.FFW.Text = "FFW";
            // 
            // ribbonPanelBg
            // 
            this.ribbonPanelBg.ButtonMoreVisible = false;
            this.ribbonPanelBg.Items.Add(this.ribbonButtonBG);
            this.ribbonPanelBg.Text = "Background";
            // 
            // ribbonButtonBG
            // 
            this.ribbonButtonBG.Enabled = false;
            this.ribbonButtonBG.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonBG.Image")));
            this.ribbonButtonBG.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonBG.SmallImage")));
            this.ribbonButtonBG.Text = "Background";
            this.ribbonButtonBG.Click += new System.EventHandler(this.ribbonButtonBG_Click);
            // 
            // ribbonPanelRS
            // 
            this.ribbonPanelRS.ButtonMoreVisible = false;
            this.ribbonPanelRS.Items.Add(this.ribbonButtonReset);
            this.ribbonPanelRS.Text = "Reset";
            // 
            // ribbonButtonReset
            // 
            this.ribbonButtonReset.Enabled = false;
            this.ribbonButtonReset.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonReset.Image")));
            this.ribbonButtonReset.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonReset.SmallImage")));
            this.ribbonButtonReset.Text = "Reset";
            this.ribbonButtonReset.Click += new System.EventHandler(this.rbtReset_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 393F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mainpanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 159);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1337, 646);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(944, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 646);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // mainpanel
            // 
            this.mainpanel.BackColor = System.Drawing.SystemColors.Window;
            this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpanel.Location = new System.Drawing.Point(0, 0);
            this.mainpanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(944, 646);
            this.mainpanel.TabIndex = 12;
            this.mainpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainpanel_Paint);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1337, 805);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ribbon1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "DEVS-Animation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonButton ribbonButtonSave;
        private System.Windows.Forms.RibbonButton ribbonButtonUndo;
        private System.Windows.Forms.RibbonTab rbtEditor;
        private System.Windows.Forms.RibbonPanel ribbonPanelSelect;
        private System.Windows.Forms.RibbonPanel ribbonPanelFigure;
        private System.Windows.Forms.RibbonButton ribbonButtonArrow;
        private System.Windows.Forms.RibbonButton ribbonButtonBox;
        private System.Windows.Forms.RibbonButton ribbonButtonRect;
        private System.Windows.Forms.RibbonButton ribbonButtonCircle;
        private System.Windows.Forms.RibbonButton ribbonButtonLine;
        private System.Windows.Forms.RibbonButton ribbonButtonFreeLine;
        private System.Windows.Forms.RibbonPanel ribbonPanelColor;
        private System.Windows.Forms.RibbonColorChooser ribbonColorChooser;
        private System.Windows.Forms.RibbonPanel ribbonPanelTools;
        private System.Windows.Forms.RibbonButton ribbonButtonCopy;
        private System.Windows.Forms.RibbonButton ribbonButtonCut;
        private System.Windows.Forms.RibbonButton ribbonButtonPaste;
        private System.Windows.Forms.RibbonButton ribbonButtonFill;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemNew;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemLoad;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemSave;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonButton ribbonButtonErase;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator2;
        private System.Windows.Forms.RibbonButton ribbonButtonMerge;
        private System.Windows.Forms.RibbonTab rbtobj;
        private System.Windows.Forms.RibbonPanel Object;
        private System.Windows.Forms.RibbonButton ShowList;
        private System.Windows.Forms.RibbonButton HideList;
        private System.Windows.Forms.RibbonButton ObjectLoad;
        private System.Windows.Forms.RibbonButton FolderSetting;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemExit;
        private System.Windows.Forms.RibbonTab rbtAnimation;
        private System.Windows.Forms.RibbonPanel Animation_control;
        private System.Windows.Forms.RibbonButton ribbonButtonStart;
        private System.Windows.Forms.RibbonButton Stop;
        private System.Windows.Forms.RibbonButton RWD;
        private System.Windows.Forms.RibbonButton FFW;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RibbonButton ribbonButtonUnmerge;
        private System.Windows.Forms.RibbonPanel ribbonPanelBg;
        private System.Windows.Forms.RibbonButton ribbonButtonBG;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbonButtonZoomIn;
        private System.Windows.Forms.RibbonButton ribbonButtonZoomOut;
        private System.Windows.Forms.RibbonButton ribbonButtonOriginal;
        private System.Windows.Forms.RibbonButton ribbonButtonHand;
        private System.Windows.Forms.Panel mainpanel;
        private System.Windows.Forms.RibbonPanel ribbonPanelRS;
        private System.Windows.Forms.RibbonButton ribbonButtonReset;
        private System.Windows.Forms.RibbonPanel ListShow;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton ribbonButtonLoadReset;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemBackgound;
    }
}

