using System;
using System.Drawing;

namespace CPaint
{
   /**
    * @brief paint를 정의하는 인터페이스로 상속하기 위해서 사용한다.
    * @author 불명(이혜리 확인)
    * @date 불명
    */
    public interface IDrawable
    { 
     /** 
     * @brief 그릴 때 사용되는 메소드
     * @details 상속 받는 클래스에서 각 도형들을 그리기 위해서 사용된다.
     * @param g 그릴 때 사용되는 Graphics 객체
     */
        void paint(Graphics g);
    }
}
