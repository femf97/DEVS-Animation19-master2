namespace CPaint
{
    using System;

    /**
    * @brief 프로젝트에서 사용하는 좌표값을 저장하기 위한 클래스
    * @author 불명(김효상 담당)
    * @date 불명(2017-01-22 확인)
    */
    [Serializable]
	public class Pos : ICloneable
    {
        /**
        * @param x x 좌표를 저장한다
        */
		public int x;
        /**
        * @param y y 좌표를 저장한다
        */
        public int y;

        /**
        * @brief Pos 클래스의 생성자
        * @author 불명(김효상 담당)
        * @date 불명(2017-01-22 확인)
        * @param x 생성할 인스턴스의 x 좌표
        * @param y 생성할 인스턴스의 y 좌표
        */
        public Pos(int x, int y)
        {
            this.x = x;
			this.y = y;
        }

        /**
        * @brief Pos 인스턴스를 복사한다.
        * @details CloneObject 클래스의 Clone 메서드를 재정의한 메서드로 Pos 인스턴스를 복사하기 위해 사용된다.
        * @author 불명(김효상 담당)
        * @date 불명(2017-01-22 확인)
        */
        public virtual object Clone() 
		{
			return new Pos(this.x, this.y);
        }

        /**
         * @brief 상대좌표계 복제
         * @author 김민규
         * @date 2017-2-7
         * @return Pos 상대좌표계에서 지정된 절대좌표와 똑같은 값을 가지는 새로운 Pos를 생성해 반환한다.
         * @param magnificationRatio 확대 비율
         * @param screenPos 화면 위치
         */
        public virtual Pos RelativeClone(int magnificationRatio, Pos screenPos)
        {
            Pos newPos = new Pos(
                (int)((this.x - screenPos.x) * ((double)magnificationRatio / 100)),
                (int)((this.y - screenPos.y) * ((double)magnificationRatio / 100))
                );
            return newPos;
        }

        /**
         * @brief 상대좌표->절대좌표계 변환 복제
         * @author 김민규
         * @date 2017-2-7
         * @return Pos 절대좌표계에서 지정된 상대좌표와 똑같은 값을 가지는 새로운 Pos를 생성해 반환한다.
         * @param magnificationRatio 확대 비율
         * @param screenPos 화면 위치
         */
        public virtual Pos InverseRelativeClone(int magnificationRatio, Pos screenPos)
        {
            Pos newPos = new Pos(
                (int)((this.x / ((double)magnificationRatio / 100)) + screenPos.x),
                (int)((this.y / ((double)magnificationRatio / 100)) + screenPos.y)
                );
            return newPos;
        }

        /**
        * @brief Pos 객체끼리 같은 지 여부를 확인하기 위한 메서드
        * @code
        * if (!(obj is Pos)) return false;
		*	Pos pt =(Pos)obj;
		*	if(this.x==pt.x && this.y==pt.y)
		*	{
		*		return true;
		*	}
		*	return false;
        * @endcode
        * @author 불명(김효상 담당)
        * @date 불명(2017-01-22 확인)
        * @param obj 같은 지 여부를 확인할 객체
        */
        public override bool Equals(object obj)
		{
            if (!(obj is Pos)) return false;
			Pos pt =(Pos)obj;
			if(this.x==pt.x && this.y==pt.y)
			{
				return true;
			}
			return false;
		}
	}
	
}
