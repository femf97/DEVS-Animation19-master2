using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPaint
{
    /**
    * @brief 줌인 줌아웃을 위한 Navigation Class
    * @author 김효상
    * @date 2017-02-06
    */
    public partial class Navigation : Form
    {
        /**
        * @brief Navigation Class 생성자
        * @author 김효상
        * @date 2017-02-06
        */
        public Navigation(Form parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
        }
        /**
        * @brief Textbox와 scrollbar 연동
        * @details Textbox의 값이 변경 되었을 때, scrollbar의 값도 조정한다. 또한 범위 이외의 값이 들어오면 최소 범위나 최대 범위로 변경한다.
        * @author 김효상
        * @date 2017-02-06
        */
        private void checkText(object sender, EventArgs e)
        {

            int textValue;
            int.TryParse(percentText.Text, out textValue);
            if (textValue < 100)
            {
                percentText.Text = "100";
                percentBar.Value = 100;
            }
            else if (textValue > 1000)
            {
                percentText.Text = "1000";
                percentBar.Value = 1000;
            }
            else
            {
                percentBar.Value = textValue;
            }

        }

        /**
        * @brief Textbox와 scrollbar 연동
        * @details scrollbar의 값이 변경 되었을 때, textbox의 값도 조정한다.
        * @author 김효상
        * @date 2017-02-06
        */

        private void checkScroll(object sender, EventArgs e)
        {
            percentText.Text = Convert.ToString(percentBar.Value);
        }

        /**
        * @brief focus를 초기화 한다.
        * @details Text의 입력이 완료 되었을때를 판별하기 위해 focus를 날리는 방식을 사용
        * @author 김효상
        * @date 2017-02-06
        */

        private void resetFocus(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        /**
        * @brief 숫자만 입력 받게 한다.
        * @details 숫자 이외에는 textbox에 값이 못 들어오게 한다.
        * @author 김효상
        * @date 2017-02-12
        */
        private void checkType(object sender, KeyPressEventArgs e)
        {
            int KeyCode = (int)e.KeyChar;
            if((KeyCode < 48||KeyCode>57)&&KeyCode!=8&&KeyCode!=46)
            {
                e.Handled = true;
            }
        }

        /**
        * @brief focus를 초기화 한다.
        * @details Enter를 입력 받으면 역시 focus를 날려서 checkText를 불러오는 역할을 한다.
        * @author 김효상
        * @date 2017-02-06
        * @todo Key값을 구분하기 위해서 KeyEventArgs가 필요해 Focus 날리는 이벤트핸들러를 두개를 만들었는데 하나로 합칠만한 방법이 있을것 같다.
        */
        private void setValue(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = null;
            }
        }
    }
}

