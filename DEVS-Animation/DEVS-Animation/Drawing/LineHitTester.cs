namespace CPaint
{
    using System;
    using System.Drawing;

   /**
    * @brief 객체를 클릭하려고 할 때 마우스가 클릭한 위치와 객체가 충돌하는 지 여부를 계산하기 위해서 사용한다.
    */
    public class LineHitTester
    {
        /**
         * @brief 선을 가로로 2칸 세로로 2칸 확장한 다음 그 안에 들어 가는지 비교한다.
         * @param r 충돌하는지 확인하고자 하는 객체
         * @param px 마우스의 클릭 위치 x
         * @param py 마우스의 클릭 위치 y
         */
        public static Boolean isLineHit(Rect r, int px, int py)
        {
            return isLineHit(r.getLeft(), r.getTop(), r.getRight(), r.getBottom(), px, py);
        }
        public static Boolean isLineHit(
            int x1, int y1, int x2, int y2, int px, int py)
        {
              
            Rect r = new Rect(x1, y1, x2, y2);

            // left가 right 보다 클 경우 바꾸고, 
            // top이 bottom 보다 클 경우 바꾸는 함수
            r.normalizeRect();

            // 크기를 확장한다. left-3, top -3, right+3, bottom+3
            r.inflateRect(3, 3);

            // 먼저 사각형 안에 점이 존재하는지 검사한다.
            if (!r.isHitPoint(px, py))
                return false;


            // 직선의 방정식을 구한다.
            double a, b, x, y;

            if (x1 == x2)
                return (Math.Abs(px - x1) < 4);

            if (y1 == y2)
                return (Math.Abs(py - y1) < 4);

            a = (double)(y1 - y2) / (double)(x1 - x2);
            b = (double)y1 - a * (double)x1;
            x = (py - b) / a;
            y = a * px + b;

            // 어느정도 비슷한지 검사한다.
            return (Math.Min(Math.Abs(x - px), Math.Abs(y - py)) < 5);
        }

    }
}
