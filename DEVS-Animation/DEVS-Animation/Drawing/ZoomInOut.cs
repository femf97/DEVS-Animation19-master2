using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CPaint
{

    /**
    * @brief 화면을 확대, 축소, 이동하는 클래스
    * @author 장한결
    * @date 2019-04-30
    */
    class ZoomInOut
    {
        //****** < VARIABLES > ************************************************************

        //** ZOOM IN/OUT을 하기 위해 곱해지는 변형용 Matrix
        public Matrix mat;

        //** 배율. zoom = 1 은 배율 없는 상태(100% 상태)를 의미
        public float zoom;

        //** ZOOM 정도 조절. 한번 휠 돌릴 때 얼마나 확대/축소 할 것인가(절대량)
        private float zoomFactor;

        //** zoomFactor를 조절하는 상수값. 클 수록 빠르게 확대&축소
        private float zoom_const = 0.07f;

        //** 확대 했을 때 원점의 위치
        PointF oriPos;

        //** 마우스 클릭 플래그
        public bool mouse_R_down;

        //** 마우스 클릭시 마우스 위치
        private float click_posX;
        private float click_posY;

        //** 임시 포인트 변수
        private PointF temp;


        //****** < METHODS > ************************************************************

        // 변수 초기화 메소드
        public ZoomInOut()
        {
            this.mat = new Matrix();
            this.oriPos = new PointF(0, 0);
            this.zoom = 1f;
            this.zoomFactor = this.zoom * this.zoom_const;
            this.temp = oriPos;
            this.mouse_R_down = false;
        }

        // 변경 된 zooming 정도와 이미지 이동 위치를 초기화하는 메소드
        // 호출 후에는 Invalidate()를 호출해줘야한다.
        public void Reset()
        {
            this.zoom = 1f;
            this.oriPos.X = 0;
            this.oriPos.Y = 0;
            this.zoomFactor = this.zoom * this.zoom_const;
            this.Matrix_control(oriPos.X, oriPos.Y);
        }

        // 마우스 드래그를 통한 화면 이동을 종료하는 메소드
        public void Releasing_screen()
        {
            this.mouse_R_down = false;
            this.oriPos = this.temp;
        }

        // 마우스 드래그를 통해 화면을 이동시키기 위해, 현재 마우스의 위치를 고정시키는 메소드
        public void Anchoring_screen(float mouse_posX, float mouse_posY)
        {
            this.click_posX = mouse_posX;
            this.click_posY = mouse_posY;
            this.temp = this.oriPos;
            this.mouse_R_down = true;
        }

        // 마우스 이동을 화면 이동에 반영하는 메소드
        // 이 메소드를 실행하기 전에 Anchoring_mouse를, 실행 후엔 Releasing_mouse를 호출해야한다.
        public void Moving_screen(float mouse_posX, float mouse_posY)
        {
            this.temp.X = this.oriPos.X + mouse_posX - this.click_posX;
            this.temp.Y = this.oriPos.Y + mouse_posY - this.click_posY;
            this.Matrix_control(temp.X, temp.Y);
        }

        // zoom in method
        public void Zoom_IN(float mouse_posX, float mouse_posY)
        {
            float real_posX = (mouse_posX - this.oriPos.X) / this.zoom;
            float real_posY = (mouse_posY - this.oriPos.Y) / this.zoom;
            float x_factor = real_posX * this.zoomFactor;
            float y_factor = real_posY * this.zoomFactor;

            this.zoom += this.zoomFactor;
            this.oriPos.X -= x_factor;
            this.oriPos.Y -= y_factor;

            this.Matrix_control(this.oriPos.X, this.oriPos.Y);

            this.zoomFactor = this.zoom * this.zoom_const;
        }
        // zoom out method
        public void Zoom_OUT(float mouse_posX, float mouse_posY)
        {
            float real_posX = (mouse_posX - this.oriPos.X) / this.zoom;
            float real_posY = (mouse_posY - this.oriPos.Y) / this.zoom;
            float x_factor = real_posX * this.zoomFactor;
            float y_factor = real_posY * this.zoomFactor;

            this.zoom -= this.zoomFactor;
            this.oriPos.X += x_factor;
            this.oriPos.Y += y_factor;

            this.Matrix_control(this.oriPos.X, this.oriPos.Y);

            this.zoomFactor = this.zoom * this.zoom_const;
        }

        // 줌 아웃을 반영하여 Matrix를 조정하는 메소드
        // 여기서 조정된 Matrix는 transform을 통해 차후에 화면 출력에 반영된다.
        // 해당 메소드를 호출하고 Invalidata()함수를 호출해야 화면이 수정된다.
        private void Matrix_control(float Ori_X, float Ori_Y)
        {
            this.mat.Reset();
            this.mat.Translate(Ori_X, Ori_Y);
            this.mat.Scale(this.zoom, this.zoom);
            //pictureBox1.Invalidate();
        }


        // Zoom In / Out을 실행하면 보이는 좌표와 실제 좌표의 차이가 발생
        // 해당 차이를 무시하고 실제 위치를 찾아내서 return
        // MouseDownHandler와 MouseMoveHandler에 적용
        public int Real_X(int mouse_posX)
        {
            int real_posX = (int)((mouse_posX - this.oriPos.X) / this.zoom);
            return real_posX;
        }
        public int Real_Y(int mouse_posY)
        {
            int real_posY = (int)((mouse_posY - this.oriPos.Y) / this.zoom);
            return real_posY;
        }
    }
}
