using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CPaint
{
    /**
     * @brief Figure들의 집합이다.
     * @details Group하였을 때 Figure들의 집합을 나타내는 클래스다. 직렬화가 가능하다.
     * @author 불명(김민규 담당)
     * @date 불명(2017-1-17 확인)
     */
    [Serializable]
    public class MergedFigure : Figure
    {
        /**
         * @param figList 머지 피규어에 속하는 피규어들의 리스트
         */
        private FigureList figList = new FigureList();


        /**
         * @brief 기본 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public MergedFigure() : base()
        {
        }

        /**
         * @brief 도형 리스트를 통한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param figList 도형 리스트
         */
        public MergedFigure(List<Figure> figList)
        {
            //merged figure가 이미 존재한다면 해당 merged figure에 포함되는 figure들을 먼저 복사하고 아니면 그냥 저장
            for (int i = 0; i < figList.Count; i++)
            {
                if (figList[i] is MergedFigure)
                {
                    this.figList.AddRange(((MergedFigure)figList[i]).figList);
                }
                else this.figList.Add(figList[i]);
            }
            //합쳐진 figure의 크기를 찾기 위해 포함되는 figure들을 비교해나감
            Rect rect = (Rect)((Figure)figList[0]).getVectorElementAt(0);
            int x1 = rect.getLeft(), x2 = rect.getRight(), y1 = rect.getTop(), y2 = rect.getBottom();
            for (int i = 1; i < figList.Count; i++)
            {
                rect = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                int xx1 = rect.getLeft(), xx2 = rect.getRight(), yy1 = rect.getTop(), yy2 = rect.getBottom();
                if (x1 > xx1) x1 = xx1;
                if (x2 < xx2) x2 = xx2;
                if (y1 > yy1) y1 = yy1;
                if (y2 < yy2) y2 = yy2;

            }
            //figure 좌표 super class에 저장
            base.add(new Rect(x1, y1, x2, y2));
        }
        /**
         * @brief Rect 영역을 통한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param rect 영역이 되는 Rect
         */
        public MergedFigure(Rect rect) : base(rect)
        {
        }
        /**
         * @brief 좌상단, 우하단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param p1 좌상단 좌표
         * @param p2 우하단 좌표
         */
        public MergedFigure(Pos p1, Pos p2) : base(p1, p2)
        {
        }
        /**
         * @brief 4개 좌표(좌측 x, 상단 y, 우측 x, 하단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x1 좌측 x좌표
         * @param y1 상단 y좌표
         * @param x2 우측 x좌표
         * @param y2 하단 y좌표
         */
        public MergedFigure(int x1, int y1, int x2, int y2) : base(x1, y1, x2, y2)
        {
        }

        /**
         * @brief 색상 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param color 색상
         */
        public MergedFigure(Color color) : base(color)
        {
        }
        /**
         * @brief 색상 및 Rect 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param rect Rect 영역
         * @param color 색상
         */
        public MergedFigure(Rect rect, Color color) : base(rect, color)
        {
        }
        /**
         * @brief 색상 및 좌상단, 우하단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param p1 좌상단 좌표
         * @param p2 우하단 좌표
         * @param color 색상
         */
        public MergedFigure(Pos p1, Pos p2, Color color) : base(p1, p2, color)
        {
        }
        /**
         * @brief 색상 및 4개 좌표(좌측 x, 상단 y, 우측 x, 하단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x1 좌측 x좌표
         * @param y1 상단 y좌표
         * @param x2 우측 x좌표
         * @param y2 하단 y좌표
         * @param color 색상
         */
        public MergedFigure(int x1, int y1, int x2, int y2, Color color) : base(x1, y1, x2, y2, color)
        {
        }
        ///////////////////////////////////////////////////////////////////////////////////
        // interface method implements
        ///////////////////////////////////////////////////////////////////////////////////
        /**
         * @brief 객체를 그리는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param g 그릴 때 사용되는 Graphics객체
         */
        public override void paint(Graphics g)
        {
            Rect rect;
            //리스트에 담긴 각각의 객체가 무엇인지 확인하고 맞는 draw함수를 호출하여 처리
            if (figList.Count > 0)
            {
                for (int i = 0; i < figList.Count; i++)
                {
                    figList[i].deselect();
                    figList[i].paint(g);
                }

            }
            rect = getVectorElementAt(0).RelativeClone(magnificationRatio, screenPos);
            rect.normalizeRect();
            // 현재 선택되었는지를 확인해서 선택되었다면 조그만 사각형들을 그린다.
            if (isSelected())
            {
                graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getTop());
                graphicHelper.drawRectTracker(g, rect.getLeft() + rect.getWidth() / 2, rect.getTop());
                graphicHelper.drawRectTracker(g, rect.getRight(), rect.getTop());
                graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getTop() + rect.getHeight() / 2);
                graphicHelper.drawRectTracker(g, rect.getRight(), rect.getTop() + rect.getHeight() / 2);
                graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getBottom());
                graphicHelper.drawRectTracker(g, rect.getLeft() + rect.getWidth() / 2, rect.getBottom());
                graphicHelper.drawRectTracker(g, rect.getRight(), rect.getBottom());
            }


        }

        ///////////////////////////////////////////////////////////////////////////////////
        // abstract method implements
        ///////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////
        // 객체가 만들어 질수 있는가를 확인한다. 예를 들어 사각형이면 면적이 있어야 하고
        // PolyLine은 점이 3개 이상이어야 하고, line일경우 시작좌표와 끝좌표가 달라야한다
        ///////////////////////////////////////////////////////////////////////////////////
        /**
         * @brief 객체가 만들어 질 수 있는가를 확인한다.
         * @details 예를 들어 사각형이면 면적이 있어야 하고, PolyLine은 점이 3개 이상이어야 하고, line일경우 시작좌표와 끝좌표가 달라야한다
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return Boolean 객체가 만들어 질 수 있는지 여부
         * @warning 0번만 확인하며, 0번이 MyPoint일 경우 에러를 발생할 것이다.
         */
        public override Boolean isEmpty()
        {
            Rect rect = getVectorElementAt(0);
            return rect.isRectEmpty();
        }

        /**
         * @brief 객체가 x, y좌표와 충돌하는가를 검사한다. 객체를 선택할려고 할때 호출된다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param posInRelativeSystem 확인할 좌표
         * @return Boolean 충돌 여부
         */
        public override Boolean checkHitAndSetSelectType(Pos posInRelativeSystem)
        {
            Rect r = (Rect)getVectorElementAt(0);

            // left top
            Rect r2 = new Rect(r.getLeft(), r.getTop(), r.getLeft(), r.getTop());
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.LEFT_TOP);
                return true;
            }

            // left middle
            r2 = new Rect(r.getLeft(), r.getTop() + r.getHeight() / 2,
                    r.getLeft(), r.getTop() + r.getHeight() / 2);
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.LEFT_MIDDLE);
                return true;
            }

            // left bottom
            r2 = new Rect(r.getLeft(), r.getBottom(),
                    r.getLeft(), r.getBottom());
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.LEFT_BOTTOM);
                return true;
            }

            // middle top
            r2 = new Rect(r.getLeft() + r.getWidth() / 2, r.getTop(),
                    r.getLeft() + r.getWidth() / 2, r.getTop());
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.MIDDLE_TOP);
                return true;
            }

            // middle bottom
            r2 = new Rect(r.getLeft() + r.getWidth() / 2, r.getBottom(),
                    r.getLeft() + r.getWidth() / 2, r.getBottom());
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.MIDDLE_BOTTOM);
                return true;
            }
            // right top
            r2 = new Rect(r.getRight(), r.getTop(), r.getRight(), r.getTop());
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.RIGHT_TOP);
                return true;
            }

            // right middle
            r2 = new Rect(r.getRight(), r.getTop() + r.getHeight() / 2,
                    r.getRight(), r.getTop() + r.getHeight() / 2);
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.RIGHT_MIDDLE);
                return true;
            }


            // right bottom
            r2 = new Rect(r.getRight(), r.getBottom(), r.getRight(), r.getBottom());
            r2.inflateRect(3, 3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.RIGHT_BOTTOM);
                return true;
            }
            setSelectType(SELECTTYPE.MOVE);

            return r.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y);
        }

        /**
         * @brief 객체를 x,y로 이동시킨다.  객체가 선택된 상태로 드래그 할때 호출된다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x x좌표
         * @param y y좌표
         */
        public override void moveAndResize(int x, int y)
        {
            Rect r = (Rect)getVectorElementAt(0);
            if (r == null)
            {
                MessageBox.Show("Error at LineFigure class, isHit method");
            }

            int right, bottom;
            int width, height;

            // 07-24 방준혁
            // merge된 상태에서 확대 축소를 할 경우 Rect의 구성 요소가 int라 한계가 있어
            // merge된 Figure들 각각에 mergedFigure 내에서의 비율을 저장시켜
            // 확대 축소를 구현함. 
            #region Moving_8_direction
            //포함하고 있는 도형들도 같이 무브시켜줘야 올바른 출력이 가능
            if (getSelectType() == SELECTTYPE.LEFT_TOP)
            {
                r.setRect(r.getLeft() + x, r.getTop() + y, r.getRight(), r.getBottom());
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = ((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.LEFT_MIDDLE)
            {
                r.setRect(r.getLeft() + x, r.getTop(), r.getRight(), r.getBottom());
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.LEFT_BOTTOM)
            {
                r.setRect(r.getLeft() + x, r.getTop(), r.getRight(), r.getBottom() + y);
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.MIDDLE_TOP)
            {
                r.setRect(r.getLeft(), r.getTop() + y, r.getRight(), r.getBottom());
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.MIDDLE_BOTTOM)
            {
                r.setRect(r.getLeft(), r.getTop(), r.getRight(), r.getBottom() + y);
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.RIGHT_TOP)
            {
                r.setRect(r.getLeft(), r.getTop() + y, r.getRight() + x, r.getBottom());
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.RIGHT_MIDDLE)
            {
                r.setRect(r.getLeft(), r.getTop(), r.getRight() + x, r.getBottom());
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            else if (getSelectType() == SELECTTYPE.RIGHT_BOTTOM)
            {
                r.setRect(r.getLeft(), r.getTop(), r.getRight() + x, r.getBottom() + y);
                right = r.getRight();
                bottom = r.getBottom();
                width = r.getWidth();
                height = r.getHeight();

                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(right - (int)(width * r.getRatio(0)), bottom - (int)(height * r.getRatio(2)),
                        right - (int)(width * r.getRatio(1)), bottom - (int)(height * r.getRatio(3)));
                }
            }
            #endregion
            else if (getSelectType() == SELECTTYPE.MOVE)
            {
                r.setRect(r.getLeft() + x, r.getTop() + y, r.getRight() + x, r.getBottom() + y);
                for (int i = 0; i < figList.Count; i++)
                {
                    r = (Rect)((Figure)figList[i]).getVectorElementAt(0);
                    r.setRect(r.getLeft() + x, r.getTop() + y, r.getRight() + x, r.getBottom() + y);
                }
            }

        }

        /**
         * @brief figList의 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return FigureList figList
         */
        public FigureList getFigList()
        {
            return figList;
        }
        /**
         * @brief 도형이 Selection의 내부에 있는지 확인한다.
         * @author 박성식
         * @date 2016-07-11
         * @param s 영역을 가지는 Selection 객체
         * @return Boolean 내부에 포함되는지 여부
         */
        public override Boolean isInArea(Selection s)
        {
            Rect r = (Rect)getVectorElementAt(0);
            return r.isInArea(s);
        }

        /**
       * @brief 색 채우기
       * @author 김민규
       * @date 2017-1-29
       * @param colorToFill 채울 색
       */
        public override void fill(Color colorToFill)
        {
            foreach (Figure f in figList)
            {
                f.fill(colorToFill);
            }
        }

        /**
         * @brief 색 비우기
         * @author 김민규
         * @date 2017-1-29
         */
        public override void empty()
        {
            foreach (Figure f in figList)
            {
                f.empty();
            }
        }

        /**
         * @brief 재귀적으로 그룹명 설정
         * @author 김민규
         * @param newGroupName 설정할 그룹명
         * @date 2017-8-1
         */
        public void recursiveGroupnameSet(string newGroupName)
        {
            foreach (Figure f in figList)
            {
                if (f is MergedFigure)
                    (f as MergedFigure).recursiveGroupnameSet(newGroupName);
                else
                    f.groupName = newGroupName;
            }
            groupName = newGroupName;
        }
    }
}
