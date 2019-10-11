namespace CPaint
{  
    using System;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Collections;

        /**
         * @brief Line객체 생성과 관련된 클래스
         */
    [Serializable]
	public class LineFigure : Figure
    {

        /**
         * @brief noncolor요소를 다룬다.
         */
        public LineFigure() : base() {
		}

        /**
         * @brief noncolor요소를 다룬다.
         */
        public LineFigure(Rect rect) :base(rect) {
		}

        /**
         * @brief noncolor요소를 다룬다.
         */
        public LineFigure(Pos x1, Pos x2) : base(x1, x2) {
		}

        /**
         * @brief noncolor요소를 다룬다.
         */
        public LineFigure(int x1, int y1, int x2, int y2) : base(x1, y1, x2, y2) {
		}


        /**
         * @brief color요소를 다룬다.
         */
        public LineFigure(Color color) : base(color) {
        }
        /**
         * @brief color요소를 다룬다.
         */
        public LineFigure(Rect rect,Color color) : base(rect,color) {
		}

        /**
         * @brief color요소를 다룬다.
         */
        public LineFigure(Pos x1, Pos x2, Color color) : base(x1, x2, color)
        {
        }

        /**
         * @brief color요소를 다룬다.
         */
        public LineFigure(int x1, int y1, int x2, int y2,Color color) : base(x1, y1, x2, y2, color) {
		}

        /**
             * @brief interface method implements
             */
        public override void paint(Graphics g) {
			Rect rect = (Rect)getVectorElementAt(0);
			g.DrawLine(
				new Pen(getCurrentColor(), 1),
				rect.getLeft(),
				rect.getTop(),
				rect.getRight(),
				rect.getBottom());
			// 현재 선택되었는지를 확인해서 선택되었다면 조그만 사각형들을 그린다.
			if(isSelected()) {
				graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getTop());
				graphicHelper.drawRectTracker(g, rect.getRight(), rect.getBottom());
			}
		}

        /**
         * @brief 객체가 만들어질 수 있는가를 확인한다.
         * @details 예를 들어 사각형이면 면적이 있어야 하고 PolyLine은 점이 3개 이상이어야 하고 ,line일 경우 시작좌표와 끝좌표가 달라야 한다.
         */
        public override Boolean isEmpty() {
			Rect rect = (Rect)getVectorElementAt(0);
			if(rect.getWidth()==0 &&
				rect.getHeight()==0) {
				return true;
			}
			return false;
		}
        /**
         * @brief 객체가 x,y 좌표와 충돌하는가를 검사한다. 객체를 선택하려고 할 때 호출된다.
         */
        public override Boolean checkHitAndSetSelectType(Pos posInRelativeSystem) {
			Rect rect = (Rect)getVectorElementAt(0);
		
			Rect rect2 = new Rect(
				rect.getLeft(), 
				rect.getTop(), 
				rect.getLeft(), 
				rect.getTop());
			rect2.inflateRect(3,3);
			if(rect2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y)) {
				setSelectType(SELECTTYPE.LEFT_TOP);
				return true;
			}
		
			rect2 = new Rect(
				rect.getRight(), 
				rect.getBottom(), 
				rect.getRight(), 
				rect.getBottom());
			rect2.inflateRect(3,3);
		
			if(rect2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y)) {
				setSelectType(SELECTTYPE.RIGHT_BOTTOM);
				return true;
			}
		
			setSelectType(SELECTTYPE.MOVE);
			return LineHitTester.isLineHit(rect, posInRelativeSystem.x, posInRelativeSystem.y);
		}
        /**
        * @brief 객체를 x,y로 이동시킨다. 객체가 선택될 상태로 드래글 할 때 호출된다.
        */
        public override void moveAndResize(int x, int y) {
			Rect r = (Rect)getVectorElementAt(0);
			if(r==null) {
				MessageBox.Show("Error at LineFigure class, isHit method");	
			}
			if(getSelectType()==SELECTTYPE.LEFT_TOP) {
				r.setRect(r.getLeft()+x, r.getTop()+y, r.getRight(), r.getBottom());
			} else if(getSelectType()==SELECTTYPE.RIGHT_BOTTOM) {
				r.setRect(r.getLeft(), r.getTop(), r.getRight()+x, r.getBottom()+y);
			} else if(getSelectType()==SELECTTYPE.MOVE) {
				r.setRect(r.getLeft()+x, r.getTop()+y, r.getRight()+x, r.getBottom()+y);
			}
		}
        /**
            * @brief 오버라이드한 inInArea함수
            * @author 박성식
            * @date 16-07-11
            */
        public override Boolean isInArea(Selection s)
        {
            Rect r = (Rect)getVectorElementAt(0);
            return r.isInArea(s);
        }
    }
}
