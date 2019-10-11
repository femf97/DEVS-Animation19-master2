﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace CPaint
{
	/**
     * @brief view에 그려지는 객체들의 정보를 저장하는 클래스
     */
	public class Document
	{
        /**
         * @param isMultSel 현재 선택모드가 단일인지 멀티인지 저장한다.
         */
        private bool isMultSel;
        /**
         * @param currentTool 현재 선택된 도구를 저장한다.
         */
		private FIGURE currentTool;
        /**
         * @param currentColor 현재 그려질 색을 저장한다.
         */
        private Color currentColor;
        /**
         * @param fileName 현재 view의 이름을 저장하고 파일 저장 시 저장하는 이름이 되는 필드이다.
         */
		private string fileName="noname";
        /**
         * @param originalFigures 그려진 모든 객체들의 원본을 저장하고 있는 리스트이다.
         */
        private FigureList drawnFigures = new FigureList();
        /**
         * @param drawingFigures 현재 선택되거나 그려지고 있는 객체들의, 확대비율 및 화면 이동으로 변환된 객체를 저장하고 있는 리스트이다.(멀티일때만 여러개 아닌경우 한개)
         */
        private FigureList drawingFigures = new FigureList();
        /**
         * @param clipboardFigure 객체를 복사할 때 저장되는 클립보드 리스트이다.
         * @author 박성식
         * @date 16-07-13
         */
        private FigureList clipboardFigure = new FigureList();
        /**
         * @param viewList 현재 켜져있는 모든 view의 객체를 저장하고 있는 리스트이다.
         */
        public List<View> viewList = new List<View>();
        /**
         * @brief 실행 취소를 하기 위해서 사용한 행동들을 저장한다.
         */
        Stack<FigureList> undoSnapshot = new Stack<FigureList>();

        /**
         * @brief 확대 비율 (100 ~ 1000) (내부 변수이므로 수정 금지)
         */
        private int innerMagnificationRatio = 100;


        /**
         * @brief   : 배경 이미지를 설정하기 위해 Main과 View에서 접근 할 수 있는 배경옵션, 파일경로 선언
         * @author  : 장한결(8524hg@gmail.com)
         * @date    : 2019-05-11
         */
        public int bgOption;
        private string str;


        /**
         * @brief 확대 비율 (100 ~ 1000)
         */
        public int magnificationRatio {
            get {
                return innerMagnificationRatio;
            }
            set {
                innerMagnificationRatio = value;
                drawingFigures = drawingFigures.RelativeClone(magnificationRatio, screenPos);
                drawnFigures = drawnFigures.RelativeClone(magnificationRatio, screenPos);
            }
        }

        /**
         * @brief 화면의 현재 위치 (내부 변수이므로 수정 금지)
         */
        private Pos innerScreenPos = new Pos(0, 0);

        /**
         * @brief 저장 필요 여부 (내부 변수이므로 수정 금지)
         */
        private bool innerNeedSave = false;

        /**
         * @brief 저장 필요 여부
         */
        public bool needSave
        {
            get
            {
                return innerNeedSave;
            }
        }

        /**
         * @brief 화면의 현재 위치
         */
        public Pos screenPos {
            get {
                return innerScreenPos;
            }
            set {
                innerScreenPos = value;
                drawingFigures = drawingFigures.RelativeClone(magnificationRatio, screenPos);
                drawnFigures = drawnFigures.RelativeClone(magnificationRatio, screenPos);
            }
        }




        /**
         * @brief Document 클래스의 생성자이다.
         * @details View를 하나 생성하고 View의 parent로 입력받은 폼을 설정한다. 생성한 View를 viewList에 저장하고 툴, 색상, 다중선택 여부를 초기화한다.
         * @param parent View의 부모폼으로 설정될 폼
         */
        public Document(Form parent)
		{
			View child = new View(parent, this);
			viewList.Add(child);
			child.Show();
            isMultSel = false;
			currentTool = FIGURE.ARROW;
			currentColor = Color.Black;


            /**
            * @brief   : Default 배경 설정. 흰색에 옵션 1으로 설정
            * @author  : 장한결(8524hg@gmail.com)
            * @date    : 2019-05-11
            */
            bgOption = 2;
            this.bgReset();
        }

        public void bgReset()
        {
            str = @"../../Images/empty.jpg";
        }
        
        public View View
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        /**
         * @brief getFile 필드를 얻기 위해서 사용한다.
         */
        public string getFileName()
		{
			return fileName;
		}
        /**
         * @brief 현재 사용하고 있는 도구를 구하기 위해서 사용된다.
         */
		public FIGURE getCurrentTool()
		{
			return currentTool;
		}
        /**
         * @brief 현재 사용하고 있는 색상을 구하기 위해서 사용된다.
         */
        public Color getCurrentColor()
		{
			return currentColor;
		}
        /**
         * @brief 현재 사용하고 있는 도형을 구하기 위해서 사용된다.
         */
        public Figure getCurrentFigure()
		{
            if(drawingFigures.Count>0)
                return (Figure)drawingFigures[0];
            return null;
		}
        /**
         * @brief 현재 objList의 크기를 구하기 위해서 사용된다.
         */
        public int getObjectSize()
		{
			return drawnFigures.Count;
		}
        /**
         * @brief objList의 특정 위치 원소를 구하는 메서드
         * @details 현재 ObjList에서 n번째 요소의 도형 종류를 구하기 위해서 사용된다.
         * @param n 구하고자 하는 원소의 위치
         * @author 불명(이혜리 확인)
         * @date 불명
         */
        public Figure getObjectElementAt(int n)
		{
			if(n+1>getObjectSize())
			{
				MessageBox.Show("Out of bound array at Document:getObjectElementAt()");
				return null;
			}
			return	(Figure)drawnFigures[n];

		}
        /**
         * @brief fileName의 setter
         * @details 현재 Document의 파일이름을 설정하기 위해서 사용된다.
         * @param fn set하고자 하는 fileName
         */
		public void setFileName(string fn)
		{
			fileName = fn;
		}
        /**
         * @brief currentTool의 setter
         * @details 현재 사용하고 있는 색상을 설정하기 위해서 사용된다.
         * @param f set하고자 하는 currentTool
         */
		public void setCurrentTool(FIGURE f)
		{
			if(f == FIGURE.TEXT || f == FIGURE.CURVE)
			{
				MessageBox.Show("Sorry!!..");
				return;
			}

			currentTool = f;
		}
		/**
         * @brief currentColor의 setter
         * @details 현재 사용하고 있는 색상을 설정하기 위해서 사용된다.
         * @param c set하고자 하는 currentColor
         */
		public void setCurrentColor(Color c)
		{
			currentColor = c;
		}

        /**
         * @brief 여러개의 객체들을 모두 합치기 위해서 사용한다.
         * @details figList는 현재 선택된 도형들을 담고있다. 선택된 객체가 저장된 figList에서 객체들을 선택해서 select 필드를 false로 변경하고 figList에서 삭제한다. 그리고 mergedFigure 객체를 새로 생성해서 선택되었던 객체들을 모두 포함하는 Figure를 생성한다.
         * @author 불분명(이혜리 확인)
         * @date 불분명
         */
        public Figure mergeFigure()
        {
            if (drawingFigures.Count > 0)
            {
                foreach (Figure f in drawingFigures)
                {
                    //FreeLine은 현재 merge가 불가능하다.
                    if (f is FreeLineFigure)
                    {
                        MessageBox.Show("FreeLine is not able to Merge", "Editor");
                        deselectAll();
                        UpdateAllViews();
                        return null;
                    }
                    else if (f is MergedFigure)
                    {
                        MessageBox.Show("Recursive merging is not available", "Editor");
                        deselectAll();
                        UpdateAllViews();
                        return null;
                    }
                }
                MergedFigure merged = new MergedFigure(drawingFigures);
                merged.groupName = drawingFigures[0].groupName;
                setRatio(merged);
                for (int i = 0; i < drawingFigures.Count; i++)
                {
                    drawingFigures[i].deselect();
                    removeOriginalFigure(drawingFigures[i].RelativeClone(magnificationRatio, screenPos));
                }
                addFigure(merged);
                drawingFigures.Clear();
                return merged;
            }
            return null;
        }
        /**
        * @brief mergedFigure를 unmerge된 상태의 Figure들로 분리한다.
        */
        public void unmergeFigure()
        {
            if (drawingFigures.Count > 0)
            {
                foreach (Figure f in drawingFigures)
                {
                    if (f is MergedFigure)
                    {
                        MergedFigure mf = f as MergedFigure;
                        string motherGName = f.groupName;
                        mf.deselect();
                        FigureList unmerge = mf.getFigList();
                        foreach(Figure temp in unmerge)
                        {
                            temp.groupName = motherGName;
                        }
                        drawnFigures.Remove(mf);
                        drawnFigures.AddRange(unmerge);
                    }
                }
            }
        }

        public void mergeAllFigure()
        {
            drawingFigures.Clear();
            foreach (Figure f in drawnFigures)
            {
                if (!(f is MergedFigure))
                {
                    drawingFigures.Add(f);
                }
            }
            mergeFigure();
        }

        /**
         * @brief 병합된 상태에서 크기를 조정할 때 비율을 제대로 조정하기 위해서 사용
         * @details 병합된 상태에서 각 Figure의 위치의 비율값을 각 Figure에 저장한다. 병합된 상태에서 확대 축소를 올바르게 작동시키기 위해 사용한다. 각 Figure들에 대해서 현재 MergeFigure에 대한 각 객체의 상대적인 위치를 저장해서 확대 축소 시 그 값을 사용한다.
         * @param merged 비율을 조절하기 위한 mergedFigure
         * @author 방준혁
         * @date 07-24
         */
        public void setRatio(MergedFigure merged)
        {
            Rect r = (Rect)merged.getVectorElementAt(0);
            int mLeft = r.getLeft();
            int mRight = r.getRight();
            int mTop = r.getTop();
            int mBottom = r.getBottom();
            int mWidth = r.getWidth();
            int mHeight = r.getHeight();

            for (int i = 0; i < merged.getFigList().Count; i++)
            {
                r = (Rect)((Figure)merged.getFigList()[i]).getVectorElementAt(0);

                r.setRatio(0, (float)(mRight - r.getLeft()) / mWidth);
                r.setRatio(1, (float)(mRight - r.getRight()) / mWidth);
                r.setRatio(2, (float)(mBottom - r.getTop()) / mHeight);
                r.setRatio(3, (float)(mBottom - r.getBottom()) / mHeight);
            }
        }
        /**
         * @author 박성식
         * @date 07-06
         * @brief 다중선택 여부를 설정하기 위해서 사용한다.
         * @details multselect 툴을 선택할 때와 ctrl+마우스 일때만 view의 마우스핸들러에서 트루로 한다.
         * @param a set하고자 하는 isMultSel의 상태
         */
        public void setIsMultSel(bool a)
        {
            isMultSel = a;
        }

        /**
         * @brief 현재 Document의 다중 선택 여부를 구하기 위해서 사용된다.
         */
        public bool getIsMultSel()
        {
            return isMultSel;
        }
         /**
         * @brief 멀티선택일때 사용되는 리스트 제어함수
         * @details 멀티선택일때만 리스트에 저장하고 아닌경우 기존처럼 figure와 리스트[0] 에 저장
         * @param f 추가할 Figure
         */
        public void refreshDrawingFigure(Figure f)
        {
            if (isMultSel == false)
            {
                drawingFigures.Clear();
                deselectAll();
            }
            //f가 null이 아닌경우만 넣는다.
            if (f != null)
            {
                drawingFigures.Remove(f);
                drawingFigures.Add(f);
            }
        }

        /**
         * @brief figList의 n번째 원소를 구하는 메서드
         * @details getCurrentFigure의 오버로드 함수
         * @param n 구하고자 하는 원소의 위치
         */
        public Figure getCurrentFigure(int n)
        {
            if (n + 1 > getDrawingFiguresCount())
            {
                throw new System.IndexOutOfRangeException();
            }
            return (Figure)drawingFigures[n];
        }

        /**
       * @brief 모든 객체를 제거한다
       * @author 김민규
       * @date 2017-7-13
       */
        public void Clear()
        {
            deselectAll();
            drawnFigures.Clear();
            innerNeedSave = false;
        }

        /**
        * @brief 선택된 객체가 맨 앞으로 가지 않고 Z값을 준수하도록 더 앞 객체의 리스트를 불러온다.
        * @author 김민규
        * @date 2017-1-27
        * @param currentFigure 비교할 객체
        * @return FigureList 더 높은 Z값을 가지는 오브젝트의 리스트
        */
        public FigureList getHigherZFigures(Figure currentFigure)
        {
            FigureList higherZFigures = new FigureList();
            int currentFigureIndex = drawnFigures.IndexOf(currentFigure);
            for (int currentIndex = currentFigureIndex + 1; currentIndex < drawnFigures.Count; currentIndex++)
            {
                higherZFigures.Add(drawnFigures[currentIndex]);
            }
            return higherZFigures;
        }

         /**
         * @brief figList의 원소 개수를 구하는 메서드
         */
        public int getDrawingFiguresCount()
        {
            return drawingFigures.Count;
        }
        /**
         * @brief figList에서 figure를 삭제한다.
         * @details 현재 FigList에서 f도형을 제거하기 위해서 사용된다. 다중선택중에 하나의 도형을 제외할 때 사용한다.
         * @param f 삭제하고자 하는 figure
         */
        public void removeFromDrawingFigures(Figure f)
        {
            if (drawingFigures.Count > 0)
                drawingFigures.Remove(f);
        }
        /**
         * @brief objList에 Figure를 추가한다.
         * @details Document의 objList에 새로운 도형을 추가하기 위해서 사용된다.
         * @param figure 추가하고자 하는 Figure
         */
        public void addFigure(Figure figure)
		{
			drawnFigures.Add(figure);
		}

        /**
         * @brief objList에서 Figure를 삭제한다.
         * @param f 삭제하고자 하는 Figure
         */
		public void removeOriginalFigure(Figure f)
		{
			int size = getObjectSize();
			for(int i=0; i<size; i++)
			{
				Figure figure = getObjectElementAt(i);
				if(figure.Equals(f))
				{
					drawnFigures.RemoveAt(i);
					break;
				}
			}

		}

        /**
         * @brief (x,y)위치에 있는 Figure를 찾는 메서드
         * @details x,y 좌표에 있는 도형이 무엇인지 구하기 위해서 사용된다. 각 객체에 isHit 메서드를 실행하여 선택되는 figure를 리턴한다. 없으면 null을 리턴한다.
         * @param x 찾고자 하는 위치의 x좌표
         * @param y 찾고자 하는 위치의 y좌표
         */
		public Figure findFigureAt(int x, int y) 
		{
			int size = getObjectSize();
			for(int i = size - 1; i >= 0; i--)
			{
				Figure figure = getObjectElementAt(i);
                // each figure hit test
                Pos posInRelativeSystem = new Pos(x, y).RelativeClone(magnificationRatio, screenPos);
				if(figure.checkHitAndSetSelectType(posInRelativeSystem))
				{
                    if (drawingFigures.Count != 0 && drawingFigures[0].groupName != figure.groupName) continue; // 그룹 이름이 같지 않으면 선택을 막는다
					return figure;
				}
			}
			return null;
		}
         /**
         * @brief 모든 객체들을 선택하지 않은 상태로 만든다.
         * @details 선택되었던 도형 리스트도 지운다.
         */        
        public void deselectAll() 
		{
			int size = getObjectSize();
			for(int i=0; i<size; i++)
			{
				Figure figure = getObjectElementAt(i);
				figure.deselect();
			}

            drawingFigures.Clear();
		}
         /**
         * @brief 모든 뷰를 갱신시킨다.
         */
        public void UpdateAllViews()
		{
			View view;
			for(int i = 0; i < viewList.Count ; i++)
			{
				view = (View)viewList[i];
				view.Invalidate(true);
				view.Update();
			}
		}

        /**
         * @brief 실행 취소 기능을 구현하기 위해서 objList를 stack에 푸시한다.
         */
		public void makeSnapshot()
		{
            innerNeedSave = true;
            undoSnapshot.Push((FigureList)drawnFigures.Clone());
		}

        /**
        * @brief stack에 들어있던 objList를 Document객체의 objList에 넣는다.
        */
		public void restoreToLastSnapshot()
		{
			drawnFigures = undoSnapshot.Pop();
			UpdateAllViews();
		}

        /**
         * @brief 실행 취소 기능을 사용하기 위해서 사용된다. 스택에 있는 objList를 pop해서 구현된다.
         */
		public void undo()
		{
			if(undoSnapshot.Count == 0)
			{
				return;
			}
			refreshDrawingFigure(null);
			restoreToLastSnapshot();
        }
        /**
         * @brief 여러개의 도형을 복사, 붙여넣기, 잘라내기 할 수 있도록 하였다.
         * @details figList로 부터 도형정보를 얻어 clippedfigure에 넣음. select box사용중일 때도 복사, 붙여넣기 가능함.
         * @author 박성식
         * @date 16-17-13
         */
        public void copy()
		{
			// 현재 선택모드이고.
			if(currentTool==FIGURE.ARROW || currentTool == FIGURE.BOX)
			{
				// 그려진 객체가 있다면
				if(drawingFigures.Count>0)
				{
                    clipboardFigure.Clear();
                    // 클립보드객체에 복사한다.
                    int cnt = drawingFigures.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        clipboardFigure.Add((Figure)drawingFigures[i].Clone());
                        ((Figure)clipboardFigure[clipboardFigure.Count - 1]).moveAndResize(5, 5);
                    }
				}

			}
		}

        /**
        * @brief 잘라내기 기능을 사용하기 위해서 사용된다.
        * @details 선택되어 있는 객체들을 현재 Document에서 지우고 클립보드리스트에 넣는다.
        */
        public void cut()
		{
			// 현재 선택모드이고,
			if(currentTool==FIGURE.ARROW || currentTool==FIGURE.BOX)
			{
				if(drawingFigures.Count>0)
				{
                    clipboardFigure.Clear();
                    int cnt = drawingFigures.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        clipboardFigure.Add((Figure)drawingFigures[i].Clone());
                        removeOriginalFigure((Figure)drawingFigures[i]);
                    }
                    drawingFigures.Clear();
                    UpdateAllViews();
				}
			}

		}

        /**
        * @brief 붙여넣기 기능을 사용하기 위해서 사용된다.
        * @details 클립보드리스트에 있는 객체들을 현재 Document에 붙여 넣는다.
        */
        public void paste()
		{
			// 현재 선택모드이고.
			if(currentTool==FIGURE.ARROW || currentTool==FIGURE.BOX)
			{
				// 클립보드에 객체가 있다면.
				if(clipboardFigure.Count>0)
				{
					// 스택에 넣는다.
					makeSnapshot();

					// 현재 선택된 객체는 선택을 해제한다. 즉 새로 붙여넣기를 한
					// 객체가 선택되어 있도록 하기 위해서 호출하였다.
					if(drawingFigures.Count>0)
					{
                        deselectAll();
					}
                    // Document에 클립보드에 있는 객체를 주가한다.
                    int cnt = clipboardFigure.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        addFigure(((Figure)((Figure)clipboardFigure[i]).Clone()));
                    }

					UpdateAllViews();
				}
			}
		}

        /**
         * @brief 입력으로 받은 파일이름으로 이미지 형식이 아닌 바이트스트림으로 저장되는 파일로 저장한다.
         * @details 도형이 잘리지 않도록 하기 위해서 View의 크기에 맞게 잘라서 저장된다. 저장하는 파일 경로가 각 객체의 groupName에 저장된다.
         * @author 김효상
         * @date 2017-07-23
         * @param fileName 저장하고자 하는 파일이름
         * @return 저장된 sof파일 경로를 반환한다.
         */
        public String SaveDocument(string fileName)
		{
            this.fileName = fileName;
            Stream streamWrite = File.Create(fileName);
            BinaryFormatter binaryWrite = new BinaryFormatter();
            foreach(Figure figure in drawnFigures)
            {
                figure.groupName = Path.GetFullPath(fileName);
            }
            binaryWrite.Serialize(streamWrite, drawnFigures);
            streamWrite.Close();
            innerNeedSave = false;

            return fileName;
        }

        /**
         * @brief 입력으로 받은 파일이름으로 입력받은 그룹에 해당하는 figure만 저장하기 위해 오버로딩된 메서드이다.
         * @details 도형이 잘리지 않도록 하기 위해서 View의 크기에 맞게 잘라서 저장된다. 저장하는 파일 경로가 각 객체의 groupName에 저장된다.
         * @author 김효상
         * @date 2017-07-31
         * @param fileName 저장하고자 하는 파일이름
         * @param groupName 저장하고자 하는 그룹의 이름
         */
        public String SaveDocument(string fileName,string groupName)
        {
            View ptemp = ((View)viewList[0]);
            int W = ptemp.Width;
            int H = ptemp.Height;

            this.fileName = fileName;
            Stream streamWrite = File.Create(fileName);
            BinaryFormatter binaryWrite = new BinaryFormatter();
            FigureList groupList = new FigureList();
            foreach (Figure figure in drawnFigures)
            {
                if(figure.groupName.Equals(groupName))
                {
                    figure.groupName = Path.GetFullPath(fileName);
                    groupList.Add(figure);
                }
             
            }
            binaryWrite.Serialize(streamWrite, groupList);
            streamWrite.Close();
            innerNeedSave = false;

            return fileName;
        }

        /**
        * @brief 현재 그린 객체를 PNG 형식 이미지 파일로 저장하기 위해서 사용한다.
        * @details view에 있는 pictureBox를 들고 와서 pictureBox를 Bitmap형식으로 저장한 다음에 객체만 저장하기 위해서 객체의 크기로 자른다. 자른 후에 PNG형식으로 저장한다.
        * @return 저장된 png파일 경로를 반환한다.
        * @param fileName 저장할 파일 이름
        */
        public String SavePNG(string fileName)
        {
            PictureBox ptemp = ((View)viewList[1]).getPictureBox();

            int W = ptemp.Width;
            int H = ptemp.Height;

            Bitmap temp = new Bitmap(W, H);
            ptemp.DrawToBitmap(temp, new Rectangle(0, 0, W, H));
            temp.MakeTransparent(Color.White);
            temp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            innerNeedSave = false;

            return fileName;
        }

        /**
        * @brief 현재 그려져 있는 객체 중에서 해당 그룹에 해당하는 객체만 저장하기 위해 오버로딩된 메서드이다.
        * @details view에 있는 pictureBox를 들고 와서 pictureBox를 Bitmap형식으로 저장한 다음에 객체만 저장하기 위해서 객체의 크기로 자른다. 자른 후에 PNG형식으로 저장한다.
        * @author 김효상
        * @date 2017-07-31
        * @param fileName 저장할 파일 이름
        * @param groupName 저장할 그룹 이름
        */
        public String SavePNG(string fileName, string groupName)
        {
            FigureList copyList = (FigureList)drawnFigures.Clone();

            drawnFigures.Clear();
            foreach (Figure figure in copyList)
            {
                if (figure.groupName.Equals(groupName))
                {
                    drawnFigures.Add(figure);
                }
            }
            UpdateAllViews();

            PictureBox ptemp = ((View)viewList[1]).getPictureBox();

            int W = ptemp.Width;
            int H = ptemp.Height;

            Bitmap temp = new Bitmap(W, H);
            ptemp.DrawToBitmap(temp, new Rectangle(0, 0, W, H));
            temp.MakeTransparent(Color.White);
            temp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            innerNeedSave = false;

            drawnFigures = copyList;
            UpdateAllViews();
            return fileName;
        }

        /**
         * @brief 색을 비교해서 도형의 위치를 찾는 함수
         * @details 이미지를 저장할 때 객체의 크기에 맞게 잘라서 저장하기 위해서 사용한다. 이미지에서 색을 비교해서 white가 아니면 객체의 일부로 인식하고 이를 바탕으로 이미지의 크기를 줄인 값을 point[]로 리턴한다.
         * @param bitmap 이미지를 자를 이미지
         * @author 박성식
         * @date 06-29
         */
        public Point[] findPos(Bitmap bitmap)
        {
            Color xy;
            Point[] pos = new Point[2];
            pos[0] = new Point(bitmap.Width - 1, bitmap.Height - 1);
            pos[1] = new Point(0, 0);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    xy = bitmap.GetPixel(x, y);
                    if (xy != Color.FromArgb(255,Color.White))
                    {
                        if (x < pos[0].X) pos[0].X = x;
                        if (y < pos[0].Y) pos[0].Y = y;
                        if (x > pos[1].X) pos[1].X = x;
                        if (y > pos[1].Y) pos[1].Y = y;
                    }
                }
            }
            return pos;
        }

        /**
         * @brief 이미지를 자르는 함수
         * @details findPos 메서드를 통해 이미지의 자른 위치를 Point[]로 구하고 이 값을 바탕으로 새로운 Bitmap 객체를 생성하기 위해서 사용한다.
         * @param bitmap 자르고자 하는 이미지
         * @param cropX 자르고자 하는 위치의 x좌표
         * @param cropY 자르고자 하는 위치의 y좌표
         * @param cropWidth 자르고자 하는 너비
         * @param cropHeight 자르고자 하는 높이
         */
        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }
        /**
         * @brief sof 형식의 document파일을 open할 때 사용한다.
         * @details document파일을 불러와서 저장되어 있는 view와 objList를 설정한다.
         * @param fileName 열고자 하는 파일이름
         * @author 박성식
         * @date 16-07-13
         */
        public void OpenDocument(string fileName)
		{
            this.fileName = fileName;
            Stream streamRead = File.OpenRead(fileName);
            BinaryFormatter binaryRead = new BinaryFormatter();
            FigureList readFigures;
            readFigures = (FigureList)binaryRead.Deserialize(streamRead);
            foreach(Figure temp in readFigures)
            {
                temp.groupName = Path.GetFullPath(fileName);
            }
            drawnFigures.AddRange(readFigures);
            streamRead.Close();
        }
        /**
         * @brief png형식의 이미지 파일을 open할 때 사용한다.
         * @details 이미지를 불러와서 Bitmap 객체를 생성하고 ImageFigure를 새로 생성하여 객체리스트에 추가한다.
         * @author 박성식
         * @date 16-07-17
         * @param fileName 열고자 하는 파일이름
         */
        public void OpenPNG(string fileName)
        {

            Bitmap img = graphicHelper.getUnlockedBitmapHandle(fileName);
            ((View)viewList[0]).Width = img.Width + 100;
            ((View)viewList[0]).Height = img.Height + 100;

            //저장된 이미지를 가지고 imagefigure 생성
            //imageFigure가 자기 이미지를 담고 있음
            ImageFigure temp = new ImageFigure(img);
            drawnFigures.Add(temp);
        }
        /**
         * @author 방준혁
         * @date 16-07-09
         * @brief Selection모드에서 객체들을 선택하려고 할 때 Selection 객체를 생성하는 데 선택한 객체들을 figList에 넣기 위해서 사용한다.
         * @details Selection이라는 사각형 안에 Figure들이 있을 경우 모두 선택한다. 이 때 사각형 안에 있다는 것은 Figure의 모든 부분이 사각형 내부에 있는 것을 의미한다.
         */
        public void selectFigures(Selection s)
        {
            drawingFigures.Clear();
            foreach (Figure temp in drawnFigures)
            {
                if (temp.isInArea(s))
                {
                    if(drawingFigures.Count!=0)
                    {
                        if ((temp.groupName != drawingFigures[0].groupName) && 
                            (temp.groupName != "")) continue; // 먼저 선택된 객체의 groupName과 일치하지 않으면 선택되지 않는다
                    }
                    drawingFigures.Add(temp);
                    temp.select();
                }
            }
        }

        /**
         * @brief 선택된 Figure를 맨 앞으로 가져온다.
         * @author 김민규
         * @date 2017-1-29
         */
        public void bringSelectedToVeryFront()
        {
            foreach (Figure currentFigure in drawingFigures)
            {
                drawnFigures.bringVeryFront(currentFigure);
            }
        }
        /**
         * @brief 선택된 Figure를 앞으로 가져온다.
         * @author 김민규
         * @date 2017-1-29
         */
        public void bringSelectedToFront()
        {
            foreach (Figure currentFigure in drawingFigures)
            {
                drawnFigures.bringFront(currentFigure);
            }
        }

        /**
         * @brief 선택된 Figure를 맨 뒤로 민다.
         * @author 김민규
         * @date 2017-1-29
         */
        public void pushSelectedToVeryBack()
        {
            foreach (Figure currentFigure in drawingFigures)
            {
                drawnFigures.pushVeryBack(currentFigure);
            }
        }

        /**
         * @brief 선택된 Figure를 뒤로 민다.
         * @author 김민규
         * @date 2017-1-29
         */
        public void pushSelectedToBack()
        {
            foreach (Figure currentFigure in drawingFigures)
            {
                drawnFigures.pushBack(currentFigure);
            }
        }

        /**
        * @brief 현재 그려져있는 figure 들의 groupName을 가져온다.
        * @author 김효상
        * @return 문자열의 리스트를 돌려준다.
        * @date 2017-7-31
        */
        public List<string> getGroupnames()
        {
            List<string> result = new List<string>();
            foreach (Figure currentFigure in drawnFigures)
            {
                if (!currentFigure.groupName.Equals(""))
                {
                    if (!result.Contains(currentFigure.groupName))
                    {
                        result.Add(currentFigure.groupName);
                    }
                }
            }
            return result;
        }
        /**
        * @brief 입력 받은 groupName에 해당하는 모든 figure를 선택한다.
        * @author 김효상
        * @date 2017-7-31
        * @param groupName 선택하고자 하는 그룹이름
        */
        public void selectGroup(string groupName)
        {
            foreach (Figure currentFigure in drawnFigures)
            {
                if (currentFigure.groupName.Equals(groupName))
                {
                    currentFigure.select();
                }
            }
        }

        /**
         * @brief 그룹명이 부여되지 않은 그룹에 임시 그룹명 부여
         * @author 김민규
         * @date 2017-8-1
         */
        public void setTempGroupnames()
        {
            int currentUnnamedIndex = 1;
            foreach (Figure currentFigure in drawnFigures)
            {
                if (currentFigure is MergedFigure)
                {
                    MergedFigure currentMergedFigure = currentFigure as MergedFigure;
                    if (currentMergedFigure.groupName == "")
                    {
                        currentMergedFigure.recursiveGroupnameSet(
                            "<Unnamed" + currentUnnamedIndex.ToString() + ">");
                        ++currentUnnamedIndex;
                    }
                }
                else {
                        
                }
            }
        }

        /**
         * @brief 저장되지 않은 임시 그룹에서 임시 그룹명 제거
         * @author 김민규
         * @date 2017-8-1
         */
        public void resetTempGroupnames()
        {
            foreach (Figure currentFigure in drawnFigures)
            {
                if (currentFigure is MergedFigure)
                {
                    MergedFigure currentMergedFigure = currentFigure as MergedFigure;
                    if (currentMergedFigure.groupName.StartsWith("<"))
                    {
                        currentMergedFigure.recursiveGroupnameSet("");
                    }
                }
            }
        }


        /**
        * @brief   : Background 설정을 위해 생성된 변수를 외부에서 접근하기 위한 메소드
        * @author  : 장한결(8524hg@gmail.com)
        * @date    : 2019-05-12
        */
        public void setBackground(string path)
        {
            this.str = path;
        }
        public Bitmap getBackground()
        {
            return new Bitmap(@str);
        }
        public void setBgOption(int optionNum)
        {
            this.bgOption = optionNum;
        }

    }
}
