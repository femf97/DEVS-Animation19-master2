namespace CPaint
{
    using System;
	using System.Drawing;
    using System.IO;

    /**
    * @brief 도형을 클릭했을 때 크기조절하는 핸들을 그리는 클래스
    * @author 불명(김효상 담당)
    * @date 불명(2017-01-22 확인)
    */
    public class graphicHelper
    {
        /**
        * @brief 4x4 의 검정색 사각형을 그린다.
        * @author 불명(김효상 담당)
        * @date 불명(2017-01-22 확인)
        * @param g 사각형을 그릴 Graphics 객체
        * @param x 사각형이 들어갈 x좌표
        * @param y 사각형이 들어갈 y좌표
        */
        public static void drawRectTracker(Graphics g, int x, int y)
        {
            Rect rt = new Rect(x, y, x, y);
            // 크기를 늘린다. 즉 left-2, top-2, right+2, bottom+2
            rt.inflateRect(2, 2);

            g.FillRectangle(
                new SolidBrush(Color.Black),
                rt.getLeft(), rt.getTop(), rt.getWidth(), rt.getHeight());
        }


        /**
        * @brief 파일이 잠기지 않는 Bitmap 핸들을 만든다
        * @author 김민규
        * @date 2017-7-31
        * @return 복사된 비트맵
        * @param pathToOpen 파일 경로
        */
        public static Bitmap getUnlockedBitmapHandle(String pathToOpen)
        {
            if (Path.GetExtension(pathToOpen).ToLower() == ".sof")
                pathToOpen = Path.ChangeExtension(pathToOpen, ".png");
            byte[] bytes = System.IO.File.ReadAllBytes(pathToOpen);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            return new Bitmap(Image.FromStream(ms));
        }
    }
}
