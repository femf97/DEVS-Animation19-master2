namespace CPaint
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;

    /**
    * @brief FreeLine 그리기와 관련된 클래스
    */
    [Serializable]
    public class FreeLineFigure : Figure
    {
        public FreeLineFigure() : base()
        {
        }
        public FreeLineFigure(Pos x) : base(x)
        {
        }
        public FreeLineFigure(int x, int y) : base(x, y)
        {
        }
        public FreeLineFigure(Color color) : base(color)
        {
        }
        public FreeLineFigure(Pos x, Color color) : base(x, color)
        {
        }
        public FreeLineFigure(int x, int y, Color color) : base(x, y, color)
        {
        }

        /**
        * @brief interface method implements
        */

        public override void paint(Graphics g)
        {
            int size = getVectorSize();
            Rect old = null;
            // 모든 점들을 선으로 연결한다.
            for (int i = 0; i < size; i++)
            {
                Rect pt = getVectorElementAt(i).RelativeClone(magnificationRatio, screenPos);
                if (old != null)
                {
                    g.DrawLine(
                        new Pen(getCurrentColor(), 1),
                        old.getLeft(), old.getTop(), pt.getLeft(), pt.getTop());
                    // 현재 선택되었는지를 확인해서 선택되었다면 조그만 사각형들을 그린다.
                    if (isSelected())
                    {
                        graphicHelper.drawRectTracker(g, old.getLeft(), old.getTop());
                        graphicHelper.drawRectTracker(g, pt.getLeft(), pt.getTop());
                    }
                }
                old = pt;
            }
        }

        /**
        * @brief abstract method implements
        * @details 객체가 만들어 질수 있는가를 확인한다. 예를 들어 사각형이면 면적이 있어야 하고 PolyLine은 점이 3개 이상이어야 하고, line일경우 시작좌표와 끝좌표가 달라야한다
        */
        public override Boolean isEmpty()
        {
            if (getVectorSize() < 2)
                return true;
            else
                return false;
        }

        /**
        * @brief 객체가 x, y  좌표와 충돌하는가를 검사한다. 객체를 선택할려고 할때 호출된다.
        */
        public override Boolean checkHitAndSetSelectType(Pos posInRelativeSystem)
        {
            int size = getVectorSize();
            Rect old = null;
            setSelectType(SELECTTYPE.MOVE);
            for (int i = 0; i < size; i++)
            {
                Rect pt = getVectorElementAt(i);
                Rect rect = new Rect(pt);
                rect.inflateRect(3, 3);
                if (rect.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
                {
                    //setSelectType(i);
                    setSelectType(SELECTTYPE.MOVE);
                    return true;
                }
                if (old != null)
                {
                    if (LineHitTester.isLineHit(old.getLeft(), old.getTop(), pt.getLeft(), pt.getTop(), posInRelativeSystem.x, posInRelativeSystem.y))
                        return true;
                }
                old = pt;
            }
            Rect pt2 = getVectorElementAt(0);
            if (LineHitTester.isLineHit(old.getLeft(), old.getTop(), pt2.getLeft(), pt2.getTop(), posInRelativeSystem.x, posInRelativeSystem.y))
                return true;
            return false;
        }

        /**
        * @brief 객체를 x,y로 이동시킨다.  객체가 선택된 상태로 드래그 할때 호출된다.
        */
        public override void moveAndResize(int x, int y)
        {
            int size = getVectorSize();
            if (getSelectType() == SELECTTYPE.MOVE)
            {
                for (int i = 0; i < size; i++)
                {
                    Rect pt = getVectorElementAt(i);
                    setVectorElementAt(i, new Rect(new Pos(pt.getLeft() + x, pt.getTop() + y)));
                }
            }
            /*else {
				Pos pt = (Pos)getVectorElementAt(getSelectType());
				pt.x+=x;
				pt.y+=y;
			}
			*/
        }

        /**
        * @brief FreeLineFigure의 최대범위를 저장해서 Selection안에 있는지 확인하는 함수
        */
        public override Boolean isInArea(Selection s)
        {
            int size = getVectorSize();
            int left=0x7fffffff, top= 0x7fffffff, right=0, bottom=0;
            Rect rf = s.getVectorElementAt(0);

            for (int i = 0; i < size; i++)
            {
                Rect pt = getVectorElementAt(i);
                if (pt.getLeft() < left) left = pt.getLeft();
                if (pt.getLeft() > right) right = pt.getLeft();
                if (pt.getTop() < top) top = pt.getTop();
                if (pt.getTop() > bottom) bottom = pt.getTop();
            }

            if (rf.getLeft() < left && rf.getRight() > right)
                if (rf.getTop() < top && rf.getBottom() > bottom)
                    return true;

            return false;
        }
    }
}
