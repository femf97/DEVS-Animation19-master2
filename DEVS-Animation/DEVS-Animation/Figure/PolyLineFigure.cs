namespace CPaint
{
    using System;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Collections;

    /**
    * @brief PolyLine 생성과 관련된 클래스
    */
    [Serializable]
	public class PolyLineFigure : Figure
    {
        /**
         * @brief noncolor요소를 다룬다.
         */
		public PolyLineFigure() : base() {
        }
        /**
         * @brief noncolor요소를 다룬다.
         */
        public PolyLineFigure(Pos x) : base(x) {
        }
        /**
         * @brief noncolor요소를 다룬다.
         */
        public PolyLineFigure(int x, int y) : base(x,y) {
		}


        /**
         * @brief color요소를 다룬다.
         */
        public PolyLineFigure(Color color) : base(color) {
		} 
        /**
         * @brief color요소를 다룬다.
         */
        public PolyLineFigure(Pos x,Color color) : base(x, color) {
		}
        /**
         * @brief color요소를 다룬다.
         */
        public PolyLineFigure(int x, int y,Color color) : base(x, y, color) {
		}

        /**
        * @brief override method implements
        */
        public override void paint(Graphics g) {
			
			int size = getVectorSize();
            Rect old = null;
			for(int i=0; i<size; i++) {
				Rect pt = getVectorElementAt(i).RelativeClone(magnificationRatio, screenPos);
				
				if(old!=null) {
					
					g.DrawLine(
						new Pen(getCurrentColor(),1),
						old.getLeft(), old.getTop(), pt.getLeft(), pt.getTop());
					
					// 현재 선택되었는지를 확인해서 선택되었다면 조그만 사각형들을 그린다.
					if(isSelected()) {
						graphicHelper.drawRectTracker(g, old.getLeft(), old.getTop());
						graphicHelper.drawRectTracker(g, pt.getLeft(), pt.getTop());
					}	
				}
				old = pt;
			}

            // 시작점과 끝점을 연결한다.
            Rect pt2 = getVectorElementAt(0).RelativeClone(magnificationRatio, screenPos);
            g.DrawLine(
				new Pen(getCurrentColor(), 1),
				old.getLeft(), old.getTop(), pt2.getLeft(), pt2.getTop());
			// 현재 선택되었는지를 확인해서 선택되었다면 조그만 사각형들을 그린다.
			if(isSelected()) {
				graphicHelper.drawRectTracker(g, old.getLeft(), old.getTop());
				graphicHelper.drawRectTracker(g, pt2.getLeft(), pt2.getTop());
			}	
		}

        /**
         * @brief 객체가 만들어질 수 있는가를 확인한다.
         * @details 예를 들어 사각형이면 면적이 있어야 하고, PolyLine은 점이 3개 이상이어야 하고, line일 경우 시작좌표와 끝좌표가 달라야 한다.
         */
		public override Boolean isEmpty() {
			if(getVectorSize()<3) 
				return true;
			else
				return false;
		}

        /**
         * @brief 객체가 x,y좌표와 충돌하는가를 검사한다. 객체를 선택하려고 할 때 호출된다.
         */
        public override Boolean checkHitAndSetSelectType(Pos posInRelativeSystem)
        {
            int size = getVectorSize();
            Rect old = null;
			setSelectType(SELECTTYPE.MOVE);
			for(int i=0; i<size; i++) {
                Rect pt = getVectorElementAt(i);
				Rect rect = new Rect(pt);
				rect.inflateRect(3,3);
				if(rect.isHitPoint(posInRelativeSystem.x, posInRelativeSystem.y)) {
					//setSelectType(i);
					setSelectType(SELECTTYPE.MOVE);
					return true;
				}
				if(old!=null) {
					if(LineHitTester.isLineHit(old.getLeft(), old.getTop(), pt.getLeft(), pt.getTop(), posInRelativeSystem.x, posInRelativeSystem.y))
						return true;
				}	
				old = pt;
			}
			Rect pt2 = getVectorElementAt(0);
			if(LineHitTester.isLineHit(old.getLeft(), old.getTop(), pt2.getLeft(), pt2.getTop(), posInRelativeSystem.x, posInRelativeSystem.y))
				return true;
			return false;
		}
	    /**
         * @brief 객체를 x,y로 이동시킨다. 객체가 선택될 상태로 드래그 할 때 호출된다.
         */
		public override void moveAndResize(int x, int y) {
			int size = getVectorSize();
			if(getSelectType()==SELECTTYPE.MOVE) {
				for(int i=0; i<size; i++) {
                    Rect pt = getVectorElementAt(i);
                    setVectorElementAt(i, new Rect(new Pos(pt.getLeft() + x, pt.getTop() + y)));
				}
			} else {
				/*
				Pos pt = (Pos)getVectorElementAt(getSelectType());
				pt.x+=x;
				pt.y+=y;
				*/
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
