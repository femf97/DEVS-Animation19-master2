using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPaint
{
    /**
     * @brief 선택을 표현하는 클래스다.
     * @author 불명(김민규 담당)
     * @date 불명(2017-1-17 확인)
     */
    public class Selection : Figure
    {
        /**
         * @brief 4개 좌표(좌측 x, 상단 y, 우측 x, 하단 y)를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x1 좌측 x좌표
         * @param y1 상단 y좌표
         * @param x2 우측 x좌표
         * @param y2 하단 y좌표
         */
        public Selection(int x1, int y1, int x2, int y2) : base(x1, y1, x2, y2) {
        }

        /**
         * @brief 객체를 그리는 메서드
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param g 그릴 때 사용되는 Graphics객체
         */
        public override void paint(Graphics g)
        {
            Rect rect = new Rect((Rect)getVectorElementAt(0));
            rect.normalizeRect();

            Pen newpen = new Pen(Color.Gray, 1);
            newpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.DrawRectangle(
                newpen,
                rect.getLeft(),
                rect.getTop(),
                rect.getWidth(),
                rect.getHeight());
        }

        /**
         * @brief 객체가 만들어 질 수 있는가를 확인한다.
         * @details Selection은 기본적으로 비어있지 않은 것으로 처리한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public override bool isEmpty()
        {
            return false;
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
            
        }

        /**
         * @brief 객체가 x, y좌표와 충돌하는가를 검사한다. 선택 그 자체이므로 false를 리턴한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param posInRelativeSystem 확인할 좌표
         * @return Boolean 충돌 여부
         */
        public override Boolean checkHitAndSetSelectType(Pos posInRelativeSystem)
        {
            return false;
        }

        /**
         * @brief 도형이 Selection의 내부에 있는지 확인한다. 선택 그 자체이므로 false를 리턴한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param s 영역을 가지는 Selection 객체
         * @return Boolean 내부에 포함되는지 여부
         */
        public override Boolean isInArea(Selection s)
        {
            return false;
        }
    }
}
