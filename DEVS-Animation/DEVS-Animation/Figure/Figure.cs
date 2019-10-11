using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CPaint
{
    /**
     * @brief ~Figure 접미사가 붙는 객체의 부모 클래스다.
     * @details 도형 객체 중 ~Figure 접미사가 붙는 클래스들의 부모 클래스다. 즉 Rect는 이에 해당하지 않음을 주의하라. 직렬화가 가능하다.
     * @author 불명(김민규 담당)
     * @date 불명(2017-1-17 확인)
     */
	[Serializable]
    public abstract class Figure : ICloneable, IDrawable
    {
        /**
         * @param coordinates 사각형 틀 (점은 0길이 Rect로 변환)
         */
        protected RectList coordinates;
        /**
         * @param currentColor 자신의 색
         */
        protected Color filledColor;
        /**
         * @param currentColor 색채우기 여부
         */
        protected Boolean bFilled;
        [NonSerialized]
        /**
         * @param bSelected 선택되어있는가?(직렬화되진 않는다.)
         */
        private Boolean bSelected;
        /**
         * @param 현재 객체의 그룹의 이름을 저장한다.
         */
        public string groupName { get; set; } = "";
        /**
         * @brief 확대 비율 (100 ~ 1000)
         */
        public int magnificationRatio { get; set; } = 100;

        /**
         * @brief 화면의 현재 위치
         */
        public Pos screenPos { get; set; } = new Pos(0, 0);
        [NonSerialized]
        /**
         * @param selectType 선택 유형
         */
        private SELECTTYPE selectType;

        /**
         * @brief 기본 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public Figure()
        {
            coordinates = new RectList();
            filledColor = Color.Black;
            bSelected = false;
        }

        /**
         * @brief Rect 영역을 통한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param rect 영역이 되는 Rect
         */
        public Figure(Rect rect) : this()
        {
            add(rect);
        }

        /**
         * @brief 좌상단, 우하단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param p1 좌상단 좌표
         * @param p2 우하단 좌표
         */
        public Figure(Pos p1, Pos p2) : this()
        {
            add(new Rect(p1, p2));
        }

        /**
         * @brief 4개 좌표(좌측 x, 상단 y, 우측 x, 하단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param left 좌측 x좌표
         * @param top 상단 y좌표
         * @param right 우측 x좌표
         * @param bottom 하단 y좌표
         */
        public Figure(int left, int top, int right, int bottom) : this()
        {
            add(new Rect(left, top, right, bottom));
        }

        /**
         * @brief 좌상단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param point 좌상단 좌표
         */
        public Figure(Pos point) : this()
        {
            add(point);
        }

        /**
         * @brief 2개 좌표(좌측 x, 상단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 좌측 x좌표
         * @param y 상단 y좌표
         */
        public Figure(int x, int y) : this()
        {
            add(new Pos(x, y));
        }

        /**
         * @brief 색상 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param c 색상
         */
        public Figure(Color c)
        {
            coordinates = new RectList();
            this.filledColor = c;
            bSelected = false;
        }

        /**
         * @brief 색상 및 Rect 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param rect Rect 영역
         * @param c 색상
         */
        public Figure(Rect rect, Color c) : this(c)
        {
            add(rect);
        }

        /**
         * @brief 색상 및 좌상단, 우하단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param p1 좌상단 좌표
         * @param p2 우하단 좌표
         * @param c 색상
         */
        public Figure(Pos p1, Pos p2, Color c) : this(c)
        {
            add(new Rect(p1.x, p1.y, p2.x, p2.y));
        }
        /**
         * @brief 색상 및 4개 좌표(좌측 x, 상단 y, 우측 x, 하단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param left 좌측 x좌표
         * @param top 상단 y좌표
         * @param right 우측 x좌표
         * @param bottom 하단 y좌표
         * @param c 색상
         */
        public Figure(int left, int top, int right, int bottom, Color c) : this(c)
        {
            add(new Rect(left, top, right, bottom));
        }

        /**
         * @brief 색상 및 좌상단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param point 좌상단 좌표
         * @param c 색상
         */
        public Figure(Pos point, Color c) : this(c)
        {
            add(point);
        }

        /**
         * @brief 색상 및 2개 좌표(좌측 x, 상단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 좌측 x좌표
         * @param y 상단 y좌표
         * @param c 색상
         */
        public Figure(int x, int y, Color c) : this(c)
        {
            add(new Pos(x, y));
        }

        /**
         * @brief coordinates의 원소 개수를 구하는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int coordinates의 원소 수
         */
        public int getVectorSize()
        {
            return coordinates.Count;
        }

        /**
         * @brief currentColor의 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return Color currentColor
         */
        public Color getCurrentColor()
        {
            return filledColor;
        }

        /**
         * @brief coordinates의 n번째 원소를 구하는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param n 리스트 내에서 선택할 요소 위치
         * @return Rect coordinates의 n번째 원소
         */
        public Rect getVectorElementAt(int n)
        {
            if ((n + 1 > getVectorSize()) || (n < 0))
            {
                throw new System.ArgumentOutOfRangeException();
            }
            return coordinates[n];
        }

        /**
         * @brief coordinates의 n번째 원소를 지정하는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param n 리스트 내에서 선택할 요소 위치
         * @param newElement 치환해 넣을 새로운 요소
         */
        public void setVectorElementAt(int n, Rect newElement)
        {
            if ((n + 1 > getVectorSize()) || (n < 0))
            {
                throw new System.ArgumentOutOfRangeException();
            }
            coordinates[n] = newElement;
        }

        /**
         * @brief selectType의 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return SELECTTYPE selectType 값
         */
        public SELECTTYPE getSelectType()
        {
            return selectType;
        }

        /**
         * @brief bSelected의 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return Boolean bSelected 값
         */
        public Boolean isSelected()
        {
            return bSelected;
        }

        /**
         * @brief bFilled의 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return Boolean bFilled 값
         */
        public Boolean isFilled()
        {
            return bFilled;
        }

        /**
         * @brief coordinates를 복제하는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return RectList coordinates와 동일한 리스트의 복제
         */
        public RectList getCoordinateClone()
        {
            RectList temp = (RectList)coordinates.Clone();
            return temp;
        }

        /**
         * @brief bSelected의 setter
         * @author 김민규
         * @date 2017-1-29
         */
        public void select()
        {
            bSelected = true;
        }

        /**
         * @brief bSelected의 setter
         * @author 김민규
         * @date 2017-1-29
         */
        public void deselect()
        {
            bSelected = false;
        }

        /**
         * @brief selectType의 setter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param type set하고자 하는 선택 타입
         */
        public void setSelectType(SELECTTYPE type)
        {
            selectType = type;
        }

        /**
         * @brief 색 채우기
         * @author 김민규
         * @date 2017-1-29
         * @param colorToFill 채울 색
         */
        public virtual void fill(Color colorToFill)
        {
            bFilled = true;
            filledColor = colorToFill;
        }
        /**
         * @brief 색 비우기
         * @author 김민규
         * @date 2017-1-29
         */
        public virtual void empty()
        {
            bFilled = false;
        }

        /**
         * @brief coordinates에 Rect를 추가한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param rect 추가하고자 하는 Rect
         */
        public void add(Rect rect)
        {
            coordinates.Add(rect);
        }

        /**
         * @brief coordinates에 좌표를 추가한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param point 추가하고자 하는 Pos
         */
        public void add(Pos point)
        {
            coordinates.Add(new Rect(point));
        }
        /**
         * @brief coordinates에 x,y좌표를 가지는 MyPoint를 추가한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 추가하고자 하는 x좌표
         * @param y 추가하고자 하는 y좌표
         */
        public void add(int x, int y)
        {
            coordinates.Add(new Rect(new Pos(x, y)));
        }

        /**
         * @brief 도형이 Selection의 내부에 있는지 확인한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param s 영역을 가지는 Selection 객체
         * @return Boolean 내부에 포함되는지 여부
         */
        public abstract Boolean isInArea(Selection s);

        /**
         * @brief 객체가 만들어 질 수 있는가를 확인한다.
         * @details 예를 들어 사각형이면 면적이 있어야 하고, PolyLine은 점이 3개 이상이어야 하고, line일경우 시작좌표와 끝좌표가 달라야한다
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return Boolean 객체가 만들어 질 수 있는지 여부
         */
        public abstract Boolean isEmpty();

        /**
         * @brief 객체가 x, y좌표와 충돌하는가를 검사한다. 객체를 선택할려고 할때 호출된다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param posInRelativeSystem 확인할 좌표
         * @return Boolean 충돌 여부
         */
        public abstract Boolean checkHitAndSetSelectType(Pos posInRelativeSystem);

        /**
         * @brief 객체를 x,y로 이동시킨다.  객체가 선택된 상태로 드래그 할때 호출된다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x x좌표
         * @param y y좌표
         */
        public abstract void moveAndResize(int x, int y);

        /**
         * @brief 객체를 그리는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param g 그릴 때 사용되는 Graphics객체
         */
        public abstract void paint(Graphics g);

        /**
         * @brief ==연산자에 대한 연산자 오버로딩
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return bool 두 Figure를 비교해 동일 여부를 반환한다.
         */
        public override bool Equals(object obj)
        {
            Figure f = (Figure)obj;
            if (this.coordinates.Equals(f.coordinates) &&
                this.filledColor == f.filledColor)
            {
                return true;
            }
            return false;
        }

        /**
         * @brief 복제
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return object 똑같은 값을 가지는 새로운 Figure를 생성해 반환한다. 사실상 팩토리 역할을 한다.
         */
        public virtual object Clone()
        {

            Figure figure = null;
            if (this.GetType() == typeof(LineFigure))
            {
                figure = new LineFigure();
            }
            else if (this.GetType() == typeof(PolyLineFigure))
            {
                figure = new PolyLineFigure();
            }
            else if (this.GetType() == typeof(FreeLineFigure))
            {
                figure = new FreeLineFigure();
            }
            else if (this.GetType() == typeof(RectFigure))
            {
                figure = new RectFigure();
            }
            else if (this.GetType() == typeof(OvalFigure))
            {
                figure = new OvalFigure();
            }
            else if (this.GetType() == typeof(ImageFigure))
            {
                figure = new ImageFigure(((ImageFigure)this).getImage());
            }
            else if (this.GetType() == typeof(MergedFigure))
            {
                FigureList figureListToClone = ((MergedFigure)this).getFigList();
                figure = new MergedFigure((FigureList)figureListToClone.Clone());
            }
            else
            {
                throw new System.NotImplementedException();
            }

            figure.coordinates = (RectList)this.coordinates.Clone();
            figure.filledColor = this.filledColor;
            figure.bFilled = this.bFilled;
            figure.groupName = this.groupName;
            return figure;
        }

        /**
         * @brief 복제 뒤 상대좌표 반영
         * @author 김민규
         * @date 2017-02-07
         * @param magnificationRatio 확대 비율
         * @param screenPos 화면의 현재 위치
         */
        public virtual Figure RelativeClone(int magnificationRatio, Pos screenPos)
        {

            Figure figure = null;
            if (this.GetType() == typeof(LineFigure))
            {
                figure = new LineFigure();
            }
            else if (this.GetType() == typeof(PolyLineFigure))
            {
                figure = new PolyLineFigure();
            }
            else if (this.GetType() == typeof(FreeLineFigure))
            {
                figure = new FreeLineFigure();
            }
            else if (this.GetType() == typeof(RectFigure))
            {
                figure = new RectFigure();
            }
            else if (this.GetType() == typeof(OvalFigure))
            {
                figure = new OvalFigure();
            }
            else if (this.GetType() == typeof(ImageFigure))
            {
                figure = new ImageFigure(((ImageFigure)this).getImage());
            }
            else if (this.GetType() == typeof(MergedFigure))
            {
                FigureList figureListToClone = ((MergedFigure)this).getFigList();
                figure = new MergedFigure((FigureList)figureListToClone.RelativeClone(magnificationRatio, screenPos));
            }
            else
            {
                throw new System.NotImplementedException();
            }

            figure.coordinates = (RectList)this.coordinates.RelativeClone(magnificationRatio, screenPos);
            figure.filledColor = this.filledColor;
            return figure;
        }
    }
}
