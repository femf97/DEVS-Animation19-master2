namespace CPaint
{
    using System;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Collections;

    /**
    * @brief 사각형 그리기와 관련된 클래스
    */
    [Serializable]
	public class RectFigure : Figure
    {
       	///////////////////////////////////////////////////////////////////////////////////
		// non color
		///////////////////////////////////////////////////////////////////////////////////
		public RectFigure() : base() {
		}
		public RectFigure(Rect rect) : base(rect) {
		}
		public RectFigure(Pos p1, Pos p2) : base(p1, p2) {
		}
		public RectFigure(int x1, int y1, int x2, int y2) : base(x1, y1, x2, y2) {
		}
	
		///////////////////////////////////////////////////////////////////////////////////
		// color
		///////////////////////////////////////////////////////////////////////////////////
		public RectFigure(Color color) : base(color) {
		}
		public RectFigure(Rect rect,Color color) : base(rect, color) {
		}
		public RectFigure(Pos p1, Pos p2,Color color) : base(p1, p2, color) {
		}
		public RectFigure(int x1, int y1, int x2, int y2,Color color) : base(x1, y1, x2, y2, color) {
		}

        /**
        * @brief interface method implements
        */
        public override void paint(Graphics g) {
			Rect rect = getVectorElementAt(0).RelativeClone(magnificationRatio, screenPos);
            rect.normalizeRect();
			
			g.DrawRectangle( 
				new Pen( getCurrentColor(), 1),
				rect.getLeft(),
				rect.getTop(),
				rect.getWidth(),
				rect.getHeight());
            //16-07-17 박성식
            //채우기 여부를 확인하고 해당색으로 채운다.
            if (isFilled()) {
                g.FillRectangle(
                    new SolidBrush(getCurrentColor()), 
                    rect.getLeft(),
                    rect.getTop(),
                    rect.getWidth(),
                    rect.getHeight());
            }
            // 현재 선택되었는지를 확인해서 선택되었다면 조그만 사각형들을 그린다.
            if (isSelected()) {
				graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getTop());
				graphicHelper.drawRectTracker(g, rect.getLeft()+rect.getWidth()/2, rect.getTop());
				graphicHelper.drawRectTracker(g, rect.getRight(), rect.getTop());
				graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getTop()+rect.getHeight()/2);
				graphicHelper.drawRectTracker(g, rect.getRight(), rect.getTop()+rect.getHeight()/2);
				graphicHelper.drawRectTracker(g, rect.getLeft(), rect.getBottom());
				graphicHelper.drawRectTracker(g, rect.getLeft()+rect.getWidth()/2, rect.getBottom());
				graphicHelper.drawRectTracker(g, rect.getRight(), rect.getBottom());
			}

		}
        /**
     * @brief 객체가 만들어질 수 있는가를 확인한다.
     * @details 예를 들어 사각형이면 면적이 있어야 하고, PolyLine은 점이 3개 이상이어야 하고, line일 경우 시작좌표와 끝좌표가 달라야 한다.
     */
        public override Boolean isEmpty() {
			Rect rect = getVectorElementAt(0);
			return rect.isRectEmpty();
		}
        /**
  * @brief 객체가 x,y좌표와 충돌하는가를 검사한다. 객체를 선택하려고 할 때 호출된다.
  */
        public override Boolean checkHitAndSetSelectType(Pos posInRelativeSystem)
        {
            Rect r = (Rect)getVectorElementAt(0);
		
			// left top
			Rect r2 = new Rect(r.getLeft(), r.getTop(), r.getLeft(), r.getTop());
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.LEFT_TOP);
				return true;
			}	
		
			// left middle
			r2 = new Rect(r.getLeft(), r.getTop() + r.getHeight()/2, 
					r.getLeft(), r.getTop() + r.getHeight()/2);
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.LEFT_MIDDLE);
				return true;
			}
		
			// left bottom
			r2 = new Rect(r.getLeft(), r.getBottom(), 
					r.getLeft(), r.getBottom());
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.LEFT_BOTTOM);
				return true;
			}
		
			// middle top
			r2 = new Rect(r.getLeft()+r.getWidth()/2, r.getTop(), 
					r.getLeft()+r.getWidth()/2, r.getTop());
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.MIDDLE_TOP);
				return true;
			}
		
			// middle bottom
			r2 = new Rect(r.getLeft()+r.getWidth()/2, r.getBottom(), 
					r.getLeft()+r.getWidth()/2, r.getBottom());
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.MIDDLE_BOTTOM);
				return true;
			}
			// right top
			r2 = new Rect(r.getRight(), r.getTop(), r.getRight(), r.getTop());
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.RIGHT_TOP);
				return true;
			}
		
			// right middle
			r2 = new Rect(r.getRight(), r.getTop() + r.getHeight()/2, 
					r.getRight(), r.getTop() + r.getHeight()/2);
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.RIGHT_MIDDLE);
				return true;
			}
		
		
			// right bottom
			r2 = new Rect(r.getRight(), r.getBottom(), r.getRight(), r.getBottom());
			r2.inflateRect(3,3);
            if (r2.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y))
            {
                setSelectType(SELECTTYPE.RIGHT_BOTTOM);
				return true;
			}

			setSelectType(SELECTTYPE.MOVE);
			return r.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y);
		}
        /**
         * @brief 객체를 x,y로 이동시킨다. 객체가 선택될 상태로 드래그 할 때 호출된다.
         */
        public override void moveAndResize(int x, int y) {
			Rect r = (Rect)getVectorElementAt(0);
			if(r==null) {
				MessageBox.Show("Error at LineFigure class, isHit method");
			}
			if(getSelectType()==SELECTTYPE.LEFT_TOP) {
				r.setRect(r.getLeft()+x, r.getTop()+y, r.getRight(), r.getBottom());
			} else if(getSelectType()==SELECTTYPE.LEFT_MIDDLE) {
				r.setRect(r.getLeft()+x, r.getTop(), r.getRight(), r.getBottom());
			} else if(getSelectType()==SELECTTYPE.LEFT_BOTTOM) {
				r.setRect(r.getLeft()+x, r.getTop(), r.getRight(), r.getBottom()+y);
			} else if(getSelectType()==SELECTTYPE.MIDDLE_TOP) {
				r.setRect(r.getLeft(), r.getTop()+y, r.getRight(), r.getBottom());
			} else if(getSelectType()==SELECTTYPE.MIDDLE_BOTTOM) {
				r.setRect(r.getLeft(), r.getTop(), r.getRight(), r.getBottom()+y);
			} else if(getSelectType()==SELECTTYPE.RIGHT_TOP) {
				r.setRect(r.getLeft(), r.getTop()+y, r.getRight()+x, r.getBottom());
			} else if(getSelectType()==SELECTTYPE.RIGHT_MIDDLE) {
				r.setRect(r.getLeft(), r.getTop(), r.getRight()+x, r.getBottom());
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
