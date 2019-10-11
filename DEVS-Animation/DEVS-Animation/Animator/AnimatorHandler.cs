using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPaint
{
    /**
     * @brief 애니메이션을 총괄하는 클래스이다.
     * @details 모든 모델과 매핑되는 애니메이터 객체를 가지고 있으며 메시지를 받아 시작,업데이트를 맡아 진행한다.
     * @author 박성식(9205011@naver.com)
     * @date 2016-10-31
     */
    class AnimatorHandler
    {
        /**
         * @param modelList 모델-애니메이터 리스트
         */
        Dictionary<int, List<ModelAnimator>> modelList;
        /**
         * @param currSeq 현재 시퀀스 넘버
         */
        int currSeq;

        /**
         * @brief 기본 생성자
         * @author 박성식(9205011@naver.com)
         * @date 2016-10-31
         * @param g 픽쳐박스를 업데이트 해줄 그래픽 객체
         * @param b 픽쳐박스
         */
        public AnimatorHandler(Graphics g, PictureBox b)
        {
            int modelNumber;
            modelList = new Dictionary<int, List<ModelAnimator>>();

            for (int currentModelNoListIndex = 0;  currentModelNoListIndex < ObjectList.imgList.Count; ++currentModelNoListIndex)
            {
                //같은 모델에 연결된 오브젝트 리스트를 모델리스트에 추가한다.
                for (int currentObjectIndex = 0; currentObjectIndex < ObjectList.imgList[currentModelNoListIndex].Count; currentObjectIndex++)
                {
                    ModelAnimator createdModelAnimator;
                    modelNumber = Convert.ToInt32(ObjectList.imgNumList[currentModelNoListIndex][currentObjectIndex]);
                    createdModelAnimator = new ModelAnimator(modelNumber,
                        graphicHelper.getUnlockedBitmapHandle(ObjectList.imgList[currentModelNoListIndex][currentObjectIndex]), b);
                    try
                    {
                        modelList[modelNumber].Add(createdModelAnimator);
                    }
                    catch (KeyNotFoundException)
                    {
                        List<ModelAnimator> newModelAnimatorList = new List<ModelAnimator>();
                        modelList.Add(modelNumber, newModelAnimatorList);
                        modelList[modelNumber].Add(createdModelAnimator);
                    }
                }
            }
        }

        /**
         * @brief 애니메이션 시작 메소드, 메시지를 받아 모델에 애니메이션 전달
         * @author 박성식(9205011@naver.com)
         * @date 2016-10-31
         * @param m 전달할 메시지
         */
        public void startAnim(msg m)
        {
            while (!canDoSeq(m.seqNo)) { } //시퀀스 넘버를 먼저 확인
            currSeq = m.seqNo;
            try
            { 
                while(modelList[m.modelN][0].isDoingAnim){ } //메시지 해당하는 모델이 애니메이션 중이면 대기

                //모델에 연결된 모든 오브젝트 리스트에 대해서 애니메이션 시작
                for (int i = 0; i < modelList[m.modelN].Count(); i++)
                {
                    modelList[m.modelN][i].startAnim(m.seqNo, m.R, m.X, m.Y, m.T); //애니메이션 시작
                }
            }
            catch(KeyNotFoundException)
            {
                //모든 메시지에 해당하는 모델 번호가 없을 수도 있음. 정상.
            }
        }

        /**
         * @brief 시퀀스 넘버가 일치하는지 확인하는 메소드
         * @author 박성식(9205011@naver.com)
         * @date 2016-10-31
         * @param seqNo 시퀀스넘버
         * @return bool 일치 또는 모든 모델 움직임 없음=참, 불일치=거짓
         */
        private bool canDoSeq(int seqNo)
        {
            
            //모든 모델들에 대해 확인한다.
            Dictionary<int, List<ModelAnimator>>.Enumerator itr = modelList.GetEnumerator();
            while (itr.MoveNext())
            {
                List<ModelAnimator> ani = itr.Current.Value;
                ModelAnimator anim = ani[0];
                if (anim.seqNo != -1 && anim.seqNo != seqNo && anim.isDoingAnim)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0},{1},{2}", anim.seqNo, anim.isDoingAnim, seqNo));
                    return false;
                }
            }
            return true;
        }

        /**
         * @brief 좌표 업데이트
         * @author 박성식(9205011@naver.com)
         * @date 2016-10-31
         * @param g 업데이트 해줄 그래픽 객체
         */
        public void updatePos(Graphics g)
        {
            if (modelList != null)
            {
                //모든 모델들에 대해 업데이트
                Dictionary<int, List<ModelAnimator>>.Enumerator itr = modelList.GetEnumerator();
                while (itr.MoveNext())
                {
                    List<ModelAnimator> anim = itr.Current.Value;
                    //시퀀스 넘버가 있다 == 애니메이션을 진행하는 모델이다.
                    foreach (var ani in anim)
                    {
                        //새 좌표에 새로 그려줌
                        g.DrawImage(ani.img, ani.startX, ani.startY);
                    }
                }
            }
        }
    }
}
