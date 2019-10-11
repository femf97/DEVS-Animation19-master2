using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CPaint
{
    /**
     * @brief 각 모델들의 움직임을 표현하는 클래스
     * @details 모델들의 에니메이션을 실제 진행하는 클래스. 도착좌표, 시간등을 인자로 받아 C#의 타이머를 이용해 애니메이션을 진행한다. 현재는 원운동과 선운동이 가능하다.
     * @author 박성식 (9205011@naver.com)
     * @date 16-10-31
     */
    class ModelAnimator
    {
        /**
         * @param timerInterval 타이머의 호출 빈도를 나타낸다. 자주 호출할 수록 더 부드러운 애니메이션을 얻을 수 있으나 더 부정확해질 수 있다.
         */
        static int timerInterval = 10;
        /**
         * @param t C# 스레드의 일종이다. 주기적으로 실행되어 설정된 이벤트를 수행한다. ModelAnimator에서는 모델의 좌표를 업데이트 해주기 위해 사용하고 있다.
         */
        System.Timers.Timer t;
        /**
         * @param background 그려질 픽쳐박스. Invalidate()메소드 호출을 위해 사용된다. 
         */
        System.Windows.Forms.PictureBox background;
        /**
         * @param img 모델과 연결된 오브젝트의 이미지 파일을 저장하기 위한 프로퍼티이다.
         */
        public Bitmap img { get; set; }
        /**
         * @param startX 모델의 x좌표를 나타내기 위해 쓰이는 프로퍼티이다.
         */
        public float startX { get; set; }
        /**
         * @param stratY 모델의 y좌표를 나타내기 위해 쓰이는 프로퍼티.
         */
        public float startY { get; set; }
        /**
         * @param isDoingAnim modelAnimator가 현재 애니메이션을 진행중인지 나타내는 프로퍼티이다. 메시지를 전달하기 전 이를 통해 애니메이션이 진행중인지 확인한다.
         */
        public bool isDoingAnim { get; set; }
        /**
         * @param seqNo 현재 진행중인 애니메이션의 시퀀스 넘버를 나타내는 프로퍼티이다. 메시지 전달 전 이를 확인하여 대기 여부를 판단한다. 시작시 seqNo은 -1이다.
         */
        public int seqNo { get; set; }
        /**
         * @param time 반복횟수를 의미하는 변수이다. Timer의 interval과, 건네받은 duration을 통해 반복횟수를 계산한다.
         */
        int time;
        /**
         * @param perMovex,perMovey 1회 반복당 얼마나 움직이는지 담고 있는 변수이다. Timer의 이벤트가 호출될 때 이 변수들 값만큼 startX, Y를 업데이트한다.
         */
        float perMovex, perMovey;
        /**
         * @param xPi,yPi 1회 반복당 회전하는지 담고 있는 변수이다. Timer이벤트 호출 시 이 변수들 값만큼 inx,y를 증가시킨다.
         */
        double xPi, yPi;
        /**
         * @param inx,iny 시작시 x,y각도를 나타낸다. 원운동에 사용된다.
         */
        double inx,iny;
        /**
         * @param cx,cy 원의 중심을 나타내는 값이다.
         */
        float cx, cy;
        /**
         * @param radius 원의 반지름을 나타낸다.
         */
        double radius;
        /**
         * @param modelNo 모델 넘버를 나타낸다.
         */
        int modelNo;
        /**
         * @brief ModelAnimator의 생성자이다. 타이머 생성, seqNo, startX,Y의 초기화를 진행
         * @param Bitmap 매핑된 이미지
         * @param PictureBox 배경이 될 PictureBox
         */
        public ModelAnimator(int modelNumber, Bitmap img, System.Windows.Forms.PictureBox pic)
        {
            this.img = img;
            background = pic;
            modelNo = modelNumber;
            t = new System.Timers.Timer(timerInterval);
            seqNo = -1; //최초시작을 구분짓기 위해 시퀀스 넘버 초기화
            startX = 0;
            startY = 0;

            // 커브 이동의 경우 첫번째 점을 저장하는 딕셔너리
            CurveBuffer = new Dictionary<int, List<float>>();
        }
        /** @brief  : 커브 이동을 위해 첫번째 점을 저장하는 딕셔너리
         *  @details: 커브 이동을 위해서는 3개의 점이 필요(start 점, 첫번째 점, 두번째 점).
         *            두번째 점을 받을 때까지 첫번째로 받은 점 데이터를 저장하기 위한 버퍼이다.
         *            modeINo을 키 값으로 데이터를 저장하고 커브 이동을 실행한 다음에는 해당 값을 제거한다.
         *  @author : 장한결 (8524hg@gmail.com)
         *  @date   : 2019 07 07
         */
        Dictionary<int, List<float>> CurveBuffer;

        /**  
         * @brief 애니메이션을 시작하는 메소드이다.
         * @details  인자들을 받아 반복횟수, 회당 이동거리(각도)를 계산하고 원운동인 경우 중심좌표, 시작각도등도 계산한다. 타이머를 시작한다. 
         * @param seqNo 시퀀스 넘버
         * @param ang 움직일 각도, degree(선운동=0)
         * @param x 도착x
         * @param y 도착y
         * @param duration 지속시간
         */
        public void startAnim(int seqNo, float ang, float x, float y, int duration)
        {
            this.seqNo = seqNo;
            //기존 이벤트를 먼저 제거해야함
            t.Elapsed -= OnTimedEventCurve;
            t.Elapsed -= OnTimedEventCircle;
            t.Elapsed -= OnTimedEventLine;
            //애니메이션 중임을 나타냄
            isDoingAnim = true;
            //각0이면 선운동. 시작시 반복횟수,회당움직임 계산하고 타이머 시작
            /** @brief  : 각(ang)의 용도 변경. 곡선 이동 구현.
             *  @details: 각(ang)을 직선 운동인지, 곡선 운동인지를 표시하는 파라미터로 사용.
             *            이를 위해 기존 if문을 switch문으로 변경.
             *  @author : 장한결 (8524hg@gmail.com)
             *  @date   : 2019 07 07
             */
             switch(ang)
             {
                case 1f:    // 직선 이동의 경우
                    if (duration == 0) //지속시간0이면 바로 움직임
                        time = 1;
                    else
                        time = duration / timerInterval;
                    perMovex = (float)(x - startX) / time;
                    perMovey = (float)(y - startY) / time;
                    t.Elapsed += OnTimedEventLine;
                    t.Enabled = true;
                    break;

                case 2f:    // 커브 이동의 경우
                    if (CurveBuffer.ContainsKey(modelNo) == false)
                    {
                        // 커브 이동에는 점이 2개가 필요하다(시작점을 제외하고).
                        // 점을 1번째로 받았을 때에는 Dictionary에 저장만하고 커브 이동은 실행되지 않는다.
                        CurveBuffer.Add(modelNo, new List<float>() { x, y });
                        t.Stop();
                        isDoingAnim = false;
                    }
                    else
                    {
                        // 점을 2번째로 받았을 때에는 커브 이동이 실행된다.
                        // 3개의 점 사이에는 2개의 코스가 있다. 점1 -> 점2, 점2 -> 점3.
                        // 코스를 쪼개야하는데 int형을 나누는 연산이라 오차가 발생할 수 있다. 이를 방지하는 과정이다.
                        if ((time=duration/timerInterval) < 2)
                            curveInterval = 1;
                        else
                            curveInterval = time / 2;
                        time = 2 * curveInterval; 

                        // 3개의 점을 가지고 곡선 그리기 위해 필요한 5개 점으로 생성.
                        Gen_CrvPtr(new List<float>() { startX, startY }, CurveBuffer[modelNo], new List<float>() { x, y });
                        // 출력하는 횟수를 저장하는 변수이다. 이를 초기화 한다.
                        curveCnt = 0;

                        // 커브 이동을 실행한다.
                        t.Elapsed += OnTimedEventCurve;
                        t.Enabled = true;

                        // 커브의 첫번째 점을 저장한 딕셔너리 항목을 제거한다.
                        CurveBuffer.Remove(modelNo);
                    }
                    break;

                default:    // 그 외의 경우
                    // 일단 일반적인 움직임(직진 운동)으로 설정
                    if (duration == 0) //지속시간0이면 바로 움직임
                        time = 1;
                    else
                        time = duration / timerInterval;
                    perMovex = (float)(x - startX) / time;
                    perMovey = (float)(y - startY) / time;
                    t.Elapsed += OnTimedEventLine;
                    t.Enabled = true;
                    break;
             }
            
            /** @brief  : 기존에 원 운동을 구현하는 코드를 주석처리 함.
             *  @details: 커브 이동을 위해 startAnim 매소드를 수정해야 함.
             *            기존에 원 운동을 위해 아래 코드가 사용되었지만 더 이상
             *            원 운동이 쓰이지 않는다고 보고 전체 코드를 주석처리 함.
             *  @author : 장한결 (8524hg@gmail.com)
             *  @data   : 2019 07 07
             *
            //그외 원운동. 마찬가지로 반복횟수,회당각도변화량 계산하고 타이머 시작
            else {
                if (duration == 0) //지속시간0이면 바로 움직임
                    time = 1;
                else
                    time = duration * 1000 / timerInterval;
        
                //원운동은 회전밖에 못한다.
                //x=rsinT,y=rcosT 생각하고 계산
                xPi = (ang / 180 * System.Math.PI) / time;
                yPi = (ang / 180 * System.Math.PI) / time;
                cx = x;
                cy = y;
                radius = System.Math.Sqrt((cx - startX) * (cx - startX) + (cy - startY) * (cy - startY));
                
                inx = System.Math.Asin((startX - cx) / radius);
                iny = System.Math.Acos((startY - cy) / radius);

                t.Elapsed += OnTimedEventCircle;
                t.Enabled = true;
            }*/
        }

        /** @brief  : 커브 이동을 실행하는 함수
         *  @author : 장한결 (8524hg@gmail.com)
         *  @date   : 2019 07 07
         */
        int curveCnt = 0;
        int curveInterval;
        private void OnTimedEventCurve(Object source, System.Timers.ElapsedEventArgs e)
        {
            //남은횟수 계산
            if ((--time) == 0)
            {
                t.Stop();
                isDoingAnim = false;
            }

            // 시작 점과 첫번째 점 사이를 이동할 때
            
            if(curveCnt<curveInterval)
            {
                //curvePoint = CatmullRomSpline(CrvPtr[0], CrvPtr[1], CrvPtr[2], CrvPtr[3], curveInterval, curveCnt + 1);
            }
            // 첫번째 점과 두번째 점 사이를 이동할 때
            else
            {
                //curvePoint = CatmullRomSpline(CrvPtr[1], CrvPtr[2], CrvPtr[3], CrvPtr[4], curveInterval, curveCnt - curveInterval + 1);
            }

            curvePoint = Bezier2(CrvPtr[1], CrvPtr[2], CrvPtr[3], 2*curveInterval, ++curveCnt);
            //curvePoint = Bezier3(CrvPtr[1], CrvPtr[2], CrvPtr[3], curveInterval, ++curveCnt);

            startX = curvePoint[0];
            startY = curvePoint[1];

            //픽처박스 초기화
            background.Invalidate();
        }

        /**
         * @brief 선운동을 진행하는 Timer이벤트이다.
         * @details 앞서 계산한 perMove만큼 좌표를 업데이트 해주고 picturebox의 invalidate 메소드를 호출한다.
         */
        private void OnTimedEventLine(Object source, System.Timers.ElapsedEventArgs e)
        {
            //남은횟수 계산
            if ((--time) == 0)
            {
                t.Stop();
                isDoingAnim = false;
            }

            //좌표 업데이트
            startX += perMovex;
            startY += perMovey;

            //픽처박스 초기화
            background.Invalidate();
        }

        /**
         * @brief 원운동을 진행하는 Timer이벤트이다.
         * @details 앞서 계산한 x, y, Pi만큼 각도를 업데이트 하여 새로운 좌표를 계산한다. picturebox의 invalidate메소드를 호출한다.
         */
        private void OnTimedEventCircle(Object source, System.Timers.ElapsedEventArgs e)
        {
            //남은횟수 계산
            if ((--time) == 0)
            {
                t.Stop();
                isDoingAnim = false;
            }

            //새로운 각 계산
            inx += xPi;
            iny += yPi;

            //각에 따라 좌표 재계산
            startX = (float)(System.Math.Sin(inx)*radius + cx);
            startY = (float)(System.Math.Cos(iny)*radius + cy);

            //픽처박스 초기화
            background.Invalidate();
        }



        /** @brief  : 3개의 점을 통해 커브 이동을 구현하는 함수
         *  @details: 3개의 점을 받아서 추가 점을 생성하는 Gen_CrvPtr 함수
         *            4개 점을 통해 CatMull Spline을 구현하는 함수(최소 4개의 점이 필요하다)
         *            부수적인 함수
         *  @author : 장한결
         *  @data   : 2019 07 07
         */
        //*********************** < Catmull-Rom Spline 생성 함수 > ************************//
        // Curve를 그리기 위한 점들을 저장하는 리스트. 프로그램 상에서는 최대 5개까지 저장한다
        private List<List<float>> CrvPtr = new List<List<float>>();
        // 현재 위치를 출력하기 위해 선언한 임시 변수
        private List<float> curvePoint;
        

        /** brief   : 3개의 점을 받아서 2개의 점을 추가로 생성하는 함수.
         *  author  : 장한결
         *  data    : 2019 07 07
         */
        private void Gen_CrvPtr(List<float> P0, List<float> P1, List<float> P2)
        {
            CrvPtr.Clear();

            //Point headPt = new Point();
            float headPt_X = 2 * P0[0] - P1[0];
            float headPt_Y = 2 * P0[1] - P1[1];

            //Point tailPt = new Point();
            float tailPt_X = 2 * P2[0] - P1[0];
            float tailPt_Y = 2 * P2[1] - P1[1];
            
            CrvPtr.Add(new List<float>(){ headPt_X,headPt_Y});
            CrvPtr.Add(P0);
            CrvPtr.Add(P1);
            CrvPtr.Add(P2);
            CrvPtr.Add(new List<float>() { tailPt_X, tailPt_Y });
        }


        /** brief   : Catmull-Rom Spline을 생성하는 함수
         *  author  : 장한결
         *  data    : 2019 07 07
         */
        private List<float> CatmullRomSpline(List<float> p0, List<float> p1, List<float> p2, List<float> p3, int nPoints, int cnt)
        {
            double t0 = 0.0f;
            double t1 = Tj(t0, p0, p1);
            double t2 = Tj(t1, p1, p2);
            double t3 = Tj(t2, p2, p3);

            double T_interval = (t2 - t1) / (double)nPoints;

            double t = t1 + T_interval * cnt;

            double a1_X = (t1 - t) / (t1 - t0) * p0[0] + (t - t0) / (t1 - t0) * p1[0];
            double a1_Y = (t1 - t) / (t1 - t0) * p0[1] + (t - t0) / (t1 - t0) * p1[1];

            double a2_X = (t2 - t) / (t2 - t1) * p1[0] + (t - t1) / (t2 - t1) * p2[0];
            double a2_Y = (t2 - t) / (t2 - t1) * p1[1] + (t - t1) / (t2 - t1) * p2[1];

            double a3_X = (t3 - t) / (t3 - t2) * p2[0] + (t - t2) / (t3 - t2) * p3[0];
            double a3_Y = (t3 - t) / (t3 - t2) * p2[1] + (t - t2) / (t3 - t2) * p3[1];


            double b1_X = (t2 - t) / (t2 - t0) * a1_X + (t - t0) / (t2 - t0) * a2_X;
            double b1_Y = (t2 - t) / (t2 - t0) * a1_Y + (t - t0) / (t2 - t0) * a2_Y;

            double b2_X = (t3 - t) / (t3 - t1) * a2_X + (t - t1) / (t3 - t1) * a3_X;
            double b2_Y = (t3 - t) / (t3 - t1) * a2_Y + (t - t1) / (t3 - t1) * a3_Y;

            float c_X = (float)((t2 - t) / (t2 - t1) * b1_X + (t - t1) / (t2 - t1) * b2_X);
            float c_Y = (float)((t2 - t) / (t2 - t1) * b1_Y + (t - t1) / (t2 - t1) * b2_Y);

            return new List<float>() { c_X, c_Y };
        }

        /** brief   : CatMull Spline을 실행하기 위해 필요한 부수적인 함수
         *  author  : 장한결
         *  data    : 2019 07 07
         */
        private double Tj(double ti, List<float> pi, List<float> pj)
        {
            double alpha = 0.5f;
            double xi = pi[0];
            double yi = pi[1];
            double xj = pj[0];
            double yj = pj[1];

            double x = (xj - xi);
            double y = (yj - yi);

            return Math.Pow(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)), alpha) + ti;
        }
        //*********************************************************************************//

        private List<float> Bezier2(List<float> p1, List<float> p2, List<float> p3, int nPoints, int cnt)
        {
            double t = (double)cnt / (double)nPoints; ;
            double x = Math.Pow(1 - t, 2) * p1[0] + 2 * t * (1 - t) * p2[0] + Math.Pow(t, 2) * p3[0];
            double y = Math.Pow(1 - t, 2) * p1[1] + 2 * t * (1 - t) * p2[1] + Math.Pow(t, 2) * p3[1];
            return new List<float>() { (float) x , (float) y };
        }
        private List<float> Bezier3(List<float> p1, List<float> p2, List<float> p3, int nPoints, int cnt)
        {
            double t = (double)cnt / (double)nPoints; ;
            double x = Math.Pow(1 - t, 3) * p1[0] + 3 * (t * Math.Pow(1 - t, 2) + Math.Pow(t,2) * (1-t) )* p2[0] + Math.Pow(t, 3) * p3[0];
            double y = Math.Pow(1 - t, 3) * p1[1] + 3 * (t * Math.Pow(1 - t, 2) + Math.Pow(t, 2) * (1 - t)) * p2[1] + Math.Pow(t, 3) * p3[1];
            return new List<float>() { (float)x, (float)y };
        }
    }
}