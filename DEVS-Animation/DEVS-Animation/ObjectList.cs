using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CPaint
{
    /**
    * @brief 오브젝트와 모델을 연동해 관리하는 클래스
    */
    public partial class ObjectList : Form
    {
        private string model_path = "";    //model이 위치한 경로를 저장한다.
        //******************************************************
        //이름 : 김효상 [2017-07-22]
        //내용 : 모델에 해당하는 리스트와 각 모델에 해당하는 이미지의 번호를 기록하는 리스트 추가
        public static List<List<string>> imgList = new List<List<string>>();
        public static List<string> modelList = new List<string>(); //모델을 저장하는 리스트
        public static List<List<string>> imgNumList = new List<List<string>>();//각 모델마다 몇개의 이미지가 있는지를 저장하는 리스트
        public static List<List<Bitmap>> imgBitmapList = new List<List<Bitmap>>();//각 모델마다 배정된 비트맵을 저장하는 리스트
        //****************************************************
        string model_name;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="parent">현재 폼의 부모 폼</param>
        public ObjectList(Form parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            SetupDataGrid();

            this.listView1.Columns.Add("Object (Picture)", this.listView1.Width, HorizontalAlignment.Left);
            this.listView2.Columns.Add("Linked Model", this.listView2.Width, HorizontalAlignment.Left);
        }

        /// <summary>
        /// modelPath의 setter
        /// </summary>
        /// <param name="path">설정하고자 하는 경로</param>
        public void Set_model_path(string path)
        {
            model_path = path;
        }

        /**
         * @brief 폴더와 그림 둘 다 설정되었는지 확인
         * @author 김민규
         * @date 2017-07-13
         */
        public bool bothSet()
        {
            return (model_path != "") && (imgList.Count() > 0);
        }

        /**
         * @brief UI 초기화
         * @author 김민규
         * @date 2017-07-31
         */
        public void ClearUI()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
        }

      
        /**
        * @brief 오브젝트와 연결된 모델을 불러오는 메서드
        * @details 오브젝트 이름을 파싱해서 해당하는 모델과 연결한다.
        * @author 김효상
        * @date 2017-07-31
        */
        public void Object_Load(FileInfo[] files)
        {
            List<string> objects = new List<string>();
            string[] Namecopy = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                string path = files[i].FullName;
                string fileName = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetFileName(path);
                string[] split = fileName.Split('-');
                string modelname = model_path;
                string model = split[0];
                foreach (List<String> currentList in imgList) {
                    foreach (String currentPath in currentList) {  
                        if ((Path.GetDirectoryName(currentPath) == Path.GetDirectoryName(path)) &&
                            (Path.GetFileNameWithoutExtension(currentPath) == Path.GetFileNameWithoutExtension(path)))
                                return;
                    }
                }
                if (!modelList.Contains(split[0]))
                {
                    modelList.Add(model);
                    imgNumList.Add(new List<string>());
                    imgList.Add(new List<string>());
                    imgBitmapList.Add(new List<Bitmap>());
                }
                if(split.Length==1)
                {
                    imgNumList[modelList.IndexOf(model)].Add("1"); // model-숫자 의 형태가 아닐때는 그냥 0을 삽입한다.
                }
                else
                {
                    imgNumList[modelList.IndexOf(model)].Add(split[1]); // 숫자가 있는경우 숫자를 넣는다.
                }
                if (!Namecopy.Contains(fileName))
                {
                    imgList[modelList.IndexOf(model)].Add(path);
                    String bitmapToLoad = path;
                    if (bitmapToLoad.ToLower().EndsWith(".sof"))
                    {
                        bitmapToLoad =
                            bitmapToLoad.Replace(
                                bitmapToLoad.Substring(
                                    bitmapToLoad.Length - 4), ".png");
                    }
                    imgBitmapList[modelList.IndexOf(model)].Add(graphicHelper.getUnlockedBitmapHandle(bitmapToLoad));
                    FileInfo fi = new FileInfo(path);
                    DirectoryInfo di = fi.Directory;
                    foreach (FileInfo temp in di.GetFiles())
                    {
                        if (temp.FullName.Contains(di.FullName + "\\" + fileName + "_"))
                        {
                            imgList[modelList.IndexOf(model)].Add(temp.FullName);
                        }
                    }

                    Namecopy[i] = fileName;
                    modelname = modelname + "\\" + fileName + ".m.cpp";
                    ListViewItem fileitem = new ListViewItem(fileName);
                    fileitem.SubItems.Add(files[i].LastWriteTime.ToString());
                    fileitem.SubItems.Add(files[i].Directory.ToString());
                    fileitem.SubItems.Add(extension);
                    listView1.Items.Add(fileitem);
                    ListViewItem modelitem = new ListViewItem(fileName + ".m"); //파싱한 모델의 이름을 넣는다.
                    modelitem.SubItems.Add(modelname);
                    listView2.Items.Add(modelitem);
                    model_name = fileName;
                    model_name = model_name.Substring(0, model_name.Length - 1);
                    
                    //name = name.Substring(0, name.Length - 1);
                    //dataGridView1[1, 0].Value = name;

                }
                
            }
           
        }

        public string ModelName(int num)
        {
            string name = model_name;
            name = name + num;
            return name;
        }

     
         /**
        * @brief 변수상태창을 빈칸으로 초기화
        * @author 김민규
        * @date 2017-07-31
        */
        private void SetupDataGrid()
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 140;

            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Received Value";

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            string[] blankrow = { "", "" };

            for (int i = 0; i < 5; i++) dataGridView1.Rows.Add(blankrow);
            dataGridView1[0, 0].Value = "Model ID";
            dataGridView1[0, 1].Value = "Sequence Number";
            dataGridView1[0, 2].Value = "X";
            dataGridView1[0, 3].Value = "Y";
            dataGridView1[0, 4].Value = "Radius";
            dataGridView1[0, 5].Value = "Duration";
        }

        /**
        * @brief 모델 클릭시 해당 모델 파싱
        * @author 김민규
        * @date 2017-07-31
        */
        private void listView2_Click(object sender, EventArgs e)
        {
            return;
            string fullpath = listView2.SelectedItems[0].SubItems[1].Text; // 선택한 모델의 경로
            string name = listView2.SelectedItems[0].SubItems[0].Text; // 선택한 모델의 이름

            string[] textvalue = File.ReadAllLines(fullpath);   // 선택한 모델을 읽음

            string[] variables = new string[10];   // 추출한 변수명을 저장함
            int index = 0;      // 변수 배열의 index 및 사이즈
            string state = "";      // 변수의 상태를 저장함

            int flag = 0;
            int check = 0;

            int start = -1;  // 생성자 시작줄
            int end = 0;    // 생성자 끝줄

            name = name.Split('.')[0].ToUpper();    // 생성자 판별

            for (int i = 0; i < textvalue.Length; i++)
            {
                if (textvalue[i].IndexOf("STATE_VARS") >= 0)
                {
                    variables[index] = textvalue[i].Split(' ')[1].Split(';')[0].Trim();
                    index++;
                }
            }

            for (int i = 0; i < textvalue.Length; i++)
            {
                if (textvalue[i].IndexOf(name + "::" + name) >= 0)
                {
                    start = i;
                }
                if (start >= 0)
                {
                    if (textvalue[i].IndexOf('{') >= 0)
                    {
                        check++;
                    }
                    if (textvalue[i].IndexOf('}') >= 0)
                    {
                        check--;
                        if (check == 0)
                        {
                            end = i;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < index; i++)
            {
                for (int j = start; j < end; j++)
                {
                    // ************************************************************
                    // 이름 : 조은성(chot1198@gmail.com) [2016-11-01]
                    // 내용 : 모델파일 파싱방식 변경
                    // 이유 : DEVS-ObjectC에서 상태변수 적용방식을 변경함
                    if (flag != 2)
                    {
                        if (textvalue[j].IndexOf("sigma =") >= 0)
                        {
                            state = textvalue[j].Split('=')[1].Split(';')[0].Trim();
                            string[] row = { "sigma", state };
                            dataGridView1.Rows.Add(row);
                            flag++;
                        }
                        if (textvalue[j].IndexOf("phase =") >= 0)
                        {
                            state = textvalue[j].Split('=')[1].Split(';')[0].Trim();
                            string[] row = { "phase", state };
                            dataGridView1.Rows.Add(row);
                            flag++;
                        }
                    }
                    if (textvalue[j].IndexOf(variables[i] + " =") >= 0)
                    {
                        state = textvalue[j].Split('=')[1];
                        state = state.Split(';')[0].Trim();
                        if (state.IndexOf("\"") >= 0)
                        {
                            state = state.Split('\"')[1];
                        }
                        string[] row = { variables[i], state };
                    }
                    // ************************************************************
                }
            }
        }

        public RichTextBox getRTB()
        {
            return rtbActivity;
        }

        
        public DataGridView getGridView()
        {         
            return dataGridView1;
        }

        public void Clear()
        {
            dataGridView1[1, 0].Value = "";
            dataGridView1[1, 1].Value = "";
            dataGridView1[1, 2].Value = "";
            dataGridView1[1, 3].Value = "";
            dataGridView1[1, 4].Value = "";
            dataGridView1[1, 5].Value = "";
            rtbActivity.Text = "";
            listView1.Items.Clear();
            listView2.Items.Clear();
            model_path = "";
            imgList.Clear();
            imgNumList.Clear();
            imgBitmapList.Clear();
            modelList.Clear();
            ClearUI();
        }

        public String getModelFriendlyName(int modelN)
        {
            return listView1.Items[modelN].Text;
        }

        private void lblMsg_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
