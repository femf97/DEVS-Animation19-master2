using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPaint
{
    /**
    * @brief Rect의 리스트
    * @author 김민규
    * @date 2017-1-27
    */
    [Serializable]
    public class RectList : List<Rect>, ICloneable
    {
        /**
        * @brief RectList 복제
        * @author 김민규
        * @date 2017-1-27
        * @return object 똑같은 값을 가지는 새로운 RectList 생성해 반환한다.
        */
        public virtual object Clone()
        {
            RectList list = new RectList();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add((Rect)this[i].Clone());
            }
            return list;
        }

        /**
        * @brief RectList 상대좌표계 복제
        * @author 김민규
        * @date 2017-2-7
        * @param magnificationRatio 확대 비율
        * @param screenPos 화면의 현재 위치
        * @return RectList 똑같은 값을 가지는 새로운 RectList 생성해 반환한다.
        */
        public virtual RectList RelativeClone(int magnificationRatio, Pos screenPos)
        {
            RectList list = new RectList();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add((Rect)this[i].RelativeClone(magnificationRatio, screenPos));
            }
            return list;
        }

        /**
        * @brief Equals의 재정의 메서드
        * @details 리스트 내부의 인스턴스들을 전부 비교해서 확인한다.
        * @author 김민규
        * @date 2017-1-27
        * @param rawrhs 같은 지 비교할 객체
        */
        public override bool Equals(object rawrhs)
        {
            if (rawrhs == null || GetType() != rawrhs.GetType())
                return false;

            RectList rhs = (RectList)rawrhs;
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(rhs[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
