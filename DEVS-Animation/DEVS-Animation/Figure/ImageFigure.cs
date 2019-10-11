using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace CPaint
{
    /**
    * @brief Figure의 자식 클래스
    * @details 이미지를 불러서 조작할 때 사용하기 위한 Figure의 자식 클래스 이다.
    * @author 박성식
    * @date 2016-06-29
    */
    [Serializable]
    public class ImageFigure : Figure
    {
        /**
        * @param image 사용자가 불러온 이미지를 저장한다. 이동, 확대, 축소와 같은 작업 시 사용하는 원본 이미지이다.
        */ 
        private Bitmap image;
        ///////////////////////////////////////////////////////////////////////////////////
        // non color
        ///////////////////////////////////////////////////////////////////////////////////
        /**
        * @brief ImageFigure의 생성자
        * @details 이미지의 Bitmap클래스를 받아 자신의 Rect를 생성한다.
        * @author 박성식
        * @date 2016-06-29
        * @param image 사용할 이미지
        */
        public ImageFigure(Bitmap image) : base()
        {
            this.image = image;
            add(new Rect(0, 0, image.Width, image.Height));   
        }
        public ImageFigure(Rect rect) : base(rect)
        {
        }
        public ImageFigure(Pos p1, Pos p2) : base(p1, p2)
        {
        }
        public ImageFigure(int x1, int y1, int x2, int y2) : base(x1, y1, x2, y2)
        {
        }
        ///////////////////////////////////////////////////////////////////////////////////
        // color
        ///////////////////////////////////////////////////////////////////////////////////
        public ImageFigure(Color color) : base(color)
        {
        }
        public ImageFigure(Rect rect, Color color) : base(rect, color)
        {
        }
        public ImageFigure(Pos p1, Pos p2, Color color) : base(p1, p2, color)
        {
        }
        public ImageFigure(int x1, int y1, int x2, int y2, Color color) : base(x1, y1, x2, y2, color)
        {
        }
        
        ///////////////////////////////////////////////////////////////////////////////////
        // interface method implements
        ///////////////////////////////////////////////////////////////////////////////////

        /**
        * @brief Figure를 그릴때 호출되는 method
        * @details 현재 Rect크기에 맞게 이미지를 그리고, 선택된 상태라면 외곽에 작은 점들도 그린다.
        * @author 박성식
        * @date 2016-06-29
        * @param g 그려줄 그래픽 객체
        */
        public override void paint(Graphics g)
        {
            Rect rect = getVectorElementAt(0).RelativeClone(magnificationRatio, screenPos);
            rect.normalizeRect();
            //16-07-13 박성식
            //사각형 사이즈에 맞게 이미지를 그린다.
            g.DrawImage(image, rect.getLeft(),rect.getTop(),rect.getWidth(),rect.getHeight());
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

        /**
        * @brief 객체가 만들어 질 수 있는지 여부를 확인하기 위해 사용
        * @details 사각형은 면적이 있어야하고, PolyLine은 점이 3개 이상, Line은 시작 좌표와 끝이 좌표가 달라야 한다.
        * @author 박성식
        * @date 2016-06-29
        */
        public override Boolean isEmpty()
        {
            Rect rect = getVectorElementAt(0);
            return rect.isRectEmpty();
        }

        /**
        * @brief 객체가 입력받은 x,y 좌표와 충돌하는 지 확인하기 위해서 사용
        * @details 주로 객체를 선택하려고 할 때 호출된다.
        * @author 박성식
        * @date 2016-06-29
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
        * @brief 객체를 x,y 좌표로 이동시키기 위해 사용
        * @details 주로 객체가 선택된 상태로 드래그할 때 호출된다.
        * @author 박성식
        * @date 2016-06-29
        * @param x 이동할 x좌표
        * @param y 이동할 y좌표
        */
        public override void moveAndResize(int x, int y)
        {
            Rect r = (Rect)getVectorElementAt(0);
            if (r == null)
            {
                MessageBox.Show("Error at LineFigure class, isHit method");
            }

            if (getSelectType() == SELECTTYPE.LEFT_TOP)
            {
                r.setRect(r.getLeft() + x, r.getTop() + y, r.getRight(), r.getBottom());
            }
            else if (getSelectType() == SELECTTYPE.LEFT_MIDDLE)
            {
                r.setRect(r.getLeft() + x, r.getTop(), r.getRight(), r.getBottom());

            }
            else if (getSelectType() == SELECTTYPE.LEFT_BOTTOM)
            {
                r.setRect(r.getLeft() + x, r.getTop(), r.getRight(), r.getBottom() + y);
            }
            else if (getSelectType() == SELECTTYPE.MIDDLE_TOP)
            {
                r.setRect(r.getLeft(), r.getTop() + y, r.getRight(), r.getBottom());
            }
            else if (getSelectType() == SELECTTYPE.MIDDLE_BOTTOM)
            {
                r.setRect(r.getLeft(), r.getTop(), r.getRight(), r.getBottom() + y);
            }
            else if (getSelectType() == SELECTTYPE.RIGHT_TOP)
            {
                r.setRect(r.getLeft(), r.getTop() + y, r.getRight() + x, r.getBottom());
            }
            else if (getSelectType() == SELECTTYPE.RIGHT_MIDDLE)
            {
                r.setRect(r.getLeft(), r.getTop(), r.getRight() + x, r.getBottom());
            }
            else if (getSelectType() == SELECTTYPE.RIGHT_BOTTOM)
            {
                r.setRect(r.getLeft(), r.getTop(), r.getRight() + x, r.getBottom() + y);
            }
            else if (getSelectType() == SELECTTYPE.MOVE)
            {
                r.setRect(r.getLeft() + x, r.getTop() + y, r.getRight() + x, r.getBottom() + y);
            }
        }

        /**
        * @brief image의 getter
        * @author 박성식
        * @date 2016-06-29
        */
        public Bitmap getImage()
        {
            return image;
        }

        /**
        * @brief 도형이 Selection 내부에 포함되는지를 확인하기 위해 사용하는 method
        * @details 객체의 모든 좌표가 Selection 객체의 내부에 있으면 포함되는 것으로 판단한다.
        * @author 박성식
        * @date 2016-07-11
        * @param s 검사할 Selection 객체
        */
        public override Boolean isInArea(Selection s)
        {
            Rect r = (Rect)getVectorElementAt(0);
            return r.isInArea(s);
        }
    }
}
