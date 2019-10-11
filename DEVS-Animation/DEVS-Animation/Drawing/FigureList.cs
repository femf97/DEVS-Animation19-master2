using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPaint
{
    /**
    * @brief Figure의 리스트
    * @author 김민규
    * @date 2017-1-27
    */

    [Serializable]
    public class FigureList : List<Figure>, ICloneable
    {
        /**
        * @brief FigureList 복제
        * @author 김민규
        * @date 2017-1-27
        * @return object 똑같은 값을 가지는 새로운 FigureList를 생성해 반환한다.
        */
        public virtual object Clone()
        {
            FigureList list = new FigureList();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add((Figure)this[i].Clone());
            }
            return list;
        }

        /**
        * @brief FigureList 상대좌표계 복제
        * @author 김민규
        * @date 2017-2-7
        * @param magnificationRatio 확대 비율
        * @param screenPos 화면의 현재 위치
        * @return FigureList 똑같은 값을 가지는 새로운 FigureList를 생성해 반환한다.
        */
        public virtual FigureList RelativeClone(int magnificationRatio, Pos screenPos)
        {
            FigureList list = new FigureList();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add((Figure)this[i].RelativeClone(magnificationRatio, screenPos));
            }
            return list;
        }

        /**
        * @brief Equals의 재정의 메서드
        * @details FigureList 내부의 인스턴스들을 전부 비교해서 확인한다.
        * @author 김민규
        * @date 2017-1-27
        * @param rawrhs 같은 지 비교할 객체
        */
        public override bool Equals(object rawrhs)
        {
            if (rawrhs == null || GetType() != rawrhs.GetType())
                return false;

            FigureList rhs = (FigureList)rawrhs;
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(rhs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /**
        * @brief 두 Figure 객체의 인덱스를 바꾼다
        * @author 김민규
        * @date 2017-1-27
        * @param lhs 좌항
        * @param rhs 우항
        */
        private void swap(int lhs, int rhs)
        {
            if ((lhs < 0) || (rhs < 0) ||
                (lhs >= Count) || (rhs >= Count))
                return;

            Figure tmp = this[lhs];
            this[lhs] = this[rhs];
            this[rhs] = tmp;
        }

        /**
        * @brief 맨 앞으로 보내기
        * @author 김민규
        * @date 2017-1-29
        * @param target 보낼 대상
        */
        public void bringVeryFront(Figure target)
        {
            for(int itemindex = IndexOf(target); itemindex < Count - 1; itemindex++)
                swap(itemindex, itemindex + 1);
        }

        /**
        * @brief 맨 뒤로 보내기
        * @author 김민규
        * @date 2017-1-29
        * @param target 보낼 대상
        */
        public void pushVeryBack(Figure target)
        {
            for (int itemindex = IndexOf(target); itemindex > 0; itemindex--)
                swap(itemindex, itemindex - 1);
        }

        /**
        * @brief 앞으로 보내기
        * @author 김민규
        * @date 2017-1-29
        * @param target 보낼 대상
        */
        public void bringFront(Figure target)
        {
            int itemindex = IndexOf(target);
            swap(itemindex, itemindex + 1);
        }

        /**
        * @brief 뒤로 보내기
        * @author 김민규
        * @date 2017-1-29
        * @param target 보낼 대상
        */
        public void pushBack(Figure target)
        {
            int itemindex = IndexOf(target);
            swap(itemindex, itemindex - 1);
        }
    }
}
