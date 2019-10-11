using System;
using System.Drawing;

namespace CPaint
{
    /**
     * @brief 직사각형
     * @details 직사각형을 담는 객체이다. 직렬화가 가능하다.
     * @author 불명(김민규 담당)
     * @date 불명(2017-1-17 확인)
     */
    [Serializable]
    public class Rect : ICloneable
    {
        /**
         * @param left 왼쪽 끝의 x좌표를 나타낸다.
         */
        private int left;
        /**
         * @param right 오른쪽 끝의 x좌표를 나타낸다.
         */
        private int right;
        /**
         * @param top 위 끝의 y좌표를 나타낸다.
         */
        private int top;
        /**
         * @param bottom 아래 끝의 y좌표를 나타낸다.
         */
        private int bottom;
        /**
         * @param mergedRatio[4] 병합되었을 때 확대 축소를 위한 비율값.
         */
        private float[] mergedRatio = new float[4];

        /**
         * @brief 디버그용 ToString
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return string 직사각형의 각 좌표를 출력한다.
         */
        public override string ToString()
        {
            return "left=" + left + ",top=" + top + ",right=" + right + ",bottom=" + bottom;
        }

        /**
         * @brief 복제
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return object 똑같은 값을 가지는 새로운 Rect를 생성해 반환한다.
         */
        public virtual object Clone()
        {
            return new Rect(this);
        }

        /**
         * @brief 상대좌표계 복제
         * @author 김민규
         * @date 2017-2-7
         * @return Rect 똑같은 값을 가지는 새로운 Rect를 생성해 반환한다.
         * @param magnificationRatio 확대 비율
         * @param screenPos 화면 위치
         */
        public virtual Rect RelativeClone(int magnificationRatio, Pos screenPos)
        {
            Rect newRect = new Rect(
                (int)((getLeft() - screenPos.x) * ((double)magnificationRatio / 100)),
                (int)((getTop() - screenPos.y) * ((double)magnificationRatio / 100)),
                (int)((getRight() - screenPos.x) * ((double)magnificationRatio / 100)),
                (int)((getBottom() - screenPos.y) * ((double)magnificationRatio / 100))
                );
            return newRect;
        }

        /**
         * @brief ==연산자에 대한 연산자 오버로딩
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return bool 두 Rect를 비교해 동일 여부를 반환한다.
         * @warning 본 연산자를 사용하고자 할 경우 함수를 쓰지 말고 ==연산자를 사용할 것을 권한다.
         */
        public override bool Equals(object obj)
        {
            if (!(obj is Rect)) return false;
            Rect rt = (Rect)obj;
            if (this.left == rt.left &&
                this.right == rt.right &&
                this.top == rt.top &&
                this.bottom == rt.bottom)
            {
                return true;
            }
            return false;

        }

        /**
         * @brief 기본 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public Rect()
        {
            left = top = right = bottom = 0;
        }

        /**
         * @brief 좌표만 존재하는 0길이 사각형 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public Rect(Pos point)
        {
            left = right = point.x;
            top = bottom = point.y;
        }

        /**
         * @brief 복사 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param other 복사할 Rect
         */
        public Rect(Rect other)
        {
            this.left = other.left;
            this.top = other.top;
            this.right = other.right;
            this.bottom = other.bottom;
            this.mergedRatio = other.mergedRatio;
        }

        /**
         * @brief 좌상단, 우하단 좌표를 이용한 생성자
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param start 좌상단 좌표
         * @param end 우하단 좌표
         */
        public Rect(Pos start, Pos end)
        {
            left = start.x;
            top = start.y;
            right = end.x;
            bottom = end.y;
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
        public Rect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        /**
         * @brief 좌측 x좌표 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int 좌측 x좌표
         */
        public int getLeft()
        {
            return left;
        }
        /**
         * @brief 우측 x좌표 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int 우측 x좌표
         */
        public int getRight()
        {
            return right;
        }
        /**
         * @brief 상단 y좌표 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int 상단 y좌표
         */
        public int getTop()
        {
            return top;
        }
        /**
         * @brief 하단 y좌표 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int 하단 y좌표
         */
        public int getBottom()
        {
            return bottom;
        }
        /**
         * @brief 비율 getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param i 얻어올 비율 위치(0: 좌, 1: 상, 2: 우, 3: 하)
         * @return float 현재 비율
         */
        public float getRatio(int i)
        {
            return mergedRatio[i];
        }

        /**
         * @brief 좌측 x좌표 setter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 설정할 값
         */
        public void setLeft(int x)
        {
            left = x;
        }
        /**
         * @brief 우측 x좌표 setter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 설정할 값
         */
        public void setRight(int x)
        {
            right = x;
        }
        /**
         * @brief 상단 y좌표 setter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 설정할 값
         */
        public void setTop(int x)
        {
            top = x;
        }
        /**
         * @brief 하단 y좌표 setter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 설정할 값
         */
        public void setBottom(int x)
        {
            bottom = x;
        }
        /**
         * @brief 비율 setter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param i 설정할 비율 위치(0: 좌, 1: 상, 2: 우, 3: 하)
         * @param x 설정할 비율
         */
        public void setRatio(int i, float x)
        {
            mergedRatio[i] = x;
        }

        /**
         * @brief 좌표 부정합 해결
         * @details 좌측 x 좌표가 우측 x보다 클때, 혹은 상단 y 좌표가 하단 y보다 클 때 이를 보정한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public void normalizeRect()
        {
            if (left > right)
            {
                int temp = left;
                left = right;
                right = temp;
            }
            if (top > bottom)
            {
                int temp = top;
                top = bottom;
                bottom = temp;
            }
        }

        /**
         * @brief 사각형 곱하기
         * @details 모든 좌표에 x를 곱한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 곱할 값
         * @return Rect 곱해진 상태로 생성된 새 Rect 객체.
         */
        public Rect multipleRect(double x)
        {
            return new Rect(
                (int)((double)left * x),
                (int)((double)top * x),
                (int)((double)right * x),
                (int)((double)bottom * x));
        }

        /**
         * @brief 사각형 나누기
         * @details 모든 좌표를 x로 나눈다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 곱할 값
         * @return Rect 나눠진 상태로 생성된 새 Rect 객체.
         */
        public Rect divideRect(int x)
        {
            return new Rect(
                (int)Math.Round((double)left / x),
                (int)Math.Round((double)top / x),
                (int)Math.Round((double)right / x),
                (int)Math.Round((double)bottom / x)
                );

        }

        /**
         * @brief 사각형 늘리기
         * @details 좌우로 2x만큼, 위아래로 2y만큼 늘린다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 중심점을 기준으로 좌우로 늘릴 길이의 절반
         * @param y 중심점을 기준으로 상하로 늘릴 길이의 절반
         */
        public void inflateRect(int x, int y)
        {
            left -= x;
            right += x;
            top -= y;
            bottom += y;
        }

        /**
         * @brief 사각형 좁히기
         * @details 좌우로 2x만큼, 위아래로 2y만큼 좁힌다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 중심점을 기준으로 좌우로 좁힐 길이의 절반
         * @param y 중심점을 기준으로 상하로 좁힐 길이의 절반
         */
        public void deflateRect(int x, int y)
        {
            left += x;
            right -= x;
            top += y;
            bottom -= y;
        }

        /**
         * @brief 빈 사각형 여부 검사
         * @details 해당 사각형의 좌우/상하 길이가 모두 0인지 확인한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         */
        public Boolean isRectEmpty()
        {
            if (right == left && bottom == top)
                return true;
            return false;
        }

        /**
         * @brief 충돌 검사
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param x 확인할 x좌표
         * @param y 확인할 y좌표
         */
        public Boolean isHitPoint(int x, int y)
        {
            // 복사본을 만들고,
            Rect r = new Rect(this);
            r.normalizeRect();
            if (x >= r.left && x <= r.right)
            {
                if (y >= r.top && y <= r.bottom)
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * @brief 선택 영역 확인
         * @details 선택 영역 내에 Rect가 있는지 확인한다.
         * @author 방준혁
         * @date 16-07-09
         * @param f Selection 객체
         * @return Boolean 선택 영역 내에 Rect가 있는지 여부를 반환한다.
         */
        public Boolean isInArea(Selection f)
        {
            Rect r = new Rect(this);
            r.normalizeRect();
            
            Rect rf = (Rect)f.getVectorElementAt(0);
            rf.normalizeRect();

            if (rf.left < r.left && rf.right > r.right)
                if (rf.top < r.top && rf.bottom > r.bottom)
                    return true;
            return false;
        }

        /**
         * @brief Width/Height getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return Pos Width/Height를 반환한다.
         */
        public Pos getSize()
        {
            return new Pos(right - left, bottom - top);
        }

        /**
         * @brief Width getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int Width를 반환한다.
         */
        public int getWidth()
        {
            return right - left;
        }

        /**
         * @brief Height getter
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @return int Height를 반환한다.
         */
        public int getHeight()
        {
            return bottom - top;
        }

        /**
         * @brief left, top, right, bottom 필드값을 바꾼다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param left 설정할 좌측 x좌표
         * @param top 설정할 상단 y좌표
         * @param right 설정할 우측 x좌표
         * @param bottom 설정할 하단 y좌표
         */
        public void setRect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        /**
         * @brief left, top, right, bottom 필드값을 다른 Rect에서 복사한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param other 복사할 Rect
         */
        public void setRect(Rect other)
        {
            this.left = other.left;
            this.top = other.top;
            this.right = other.right;
            this.bottom = other.bottom;
        }

        /**
         * @brief 두 Rect의 겹치는 영역을 반환한다.
         * @author 불명(김민규 담당)
         * @date 불명(2017-1-17 확인)
         * @param other 확인할 Rect
         * @return Rect 있으면 겹치는 영역의 Rect를, 없으면 null을 반환한다.
         */
        public Rect intersectRect(Rect other)
        {
            int x1, y1, x2, y2;

            // left와 top 은 큰 수를 택하고
            if (this.left > other.left)
                x1 = this.left;
            else
                x1 = other.left;
            if (this.top > other.top)
                y1 = this.top;
            else
                y1 = other.top;

            // right와 bottom은 작은 수를 택한다.
            if (this.right > other.right)
                x2 = other.right;
            else
                x2 = this.right;
            if (this.bottom > other.bottom)
                y2 = other.bottom;
            else
                y2 = this.bottom;

            // 만약 일치하는 영역이 존재한다면 그 영역의 Rect객체를 리턴하고
            // 그렇지 않다면 0길이 사각형을 리턴한다.
            if (x2 > x1 && y2 > y1)
                return new Rect(x1, y1, x2, y2);
            else
                return new Rect(0, 0, 0, 0);
        }

    }
}
