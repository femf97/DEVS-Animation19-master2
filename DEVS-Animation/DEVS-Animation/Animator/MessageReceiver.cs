using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPaint
{
    /**
    * @brief 전달받은 메세지를 담는 클래스
    * @details IPC로 받은 스트링 값들을 쉽게 사용하기 위해 쓰이는 클래스
    * @author 방준혁 sonsidal@gmail.com
    * @date 2016-10-31
    */
    class msg
    {
        /**
        * @param msgOrig 값의 계산을 쉽게 해주기 위해 생성한 프로퍼티
        */
        public string[] msgOrig
        {
            get
            {
                return new string[] 
                { modelN.ToString(), seqNo.ToString(), X.ToString(), Y.ToString(), R.ToString(), T.ToString() };
            }
            set
            {
                modelN = int.Parse(value[0]);
                seqNo = int.Parse(value[1]);
                X = float.Parse(value[2]);
                Y = float.Parse(value[3]);
                if (value[4].Length == 8)
                {
                    R = 1;
                }
                else if (value[4].Length == 5)
                {
                    R = 2;
                }
                else
                {
                    R = 0;
                }
                T = int.Parse(value[5]);
            }
        }
        /**
        * @param modeIN 모델번호
        */
        public int modelN; //모델번호
        /**
        * @param seqNo 시퀀스넘버
        */
        public int seqNo; //시퀀스넘버
        /**
        * @param X 도착x
        */
        public float X; //도착x
        /**
        * @param Y 도착y
        */
        public float Y; //도착y
        /**
        * @param R 각도
        */
        public float R; //각도
        /**
        * @param T 지속시간
        */
        public int T; //지속시간
    }

    /**
    * @brief IPC를 통해 메세지를 전달 받아 큐에 저장하는 클래스
    * @details IPC로 메세지를 수신하기 위해 사용한다. 스레드의 일종인 BackgroundWorker가 사용된다.
    * @author 방준혁 sonsidal@gmail.com
    * @date 2016-10-31
    */
    class MessageReceiver
    {
        /**
        * @param worker 백그라운드 워커 스레드
        */
        BackgroundWorker worker; //백그라운드 워커 스레드
        /**
        * @param messageQ 메세지 큐
        */
        static Queue<string> messageQ = new Queue<string>(); //메시지 큐
        /**
        * @param key 락 키
        */
        static object key = new object(); //락 키

        /**
        * @brief MessageReceiver 클래스 생성자
        * @details MessageReceiver의 생성자이다. 사용하는 worker와 관련된 기본 설정들을 수행한다.
        * @author 방준혁 sonsidal@gmail.com
        * @date 2016-10-31
        */
        public MessageReceiver()
        {
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(getMsg);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerSupportsCancellation = true;
        }

        /**
        * @brief 워커 실행 메소드
        * @details worker를 시작한다.
        * @author 방준혁 sonsidal@gmail.com
        * @date 2016-10-31
        */
        public void RunWorkerAsync()
        {
            worker.RunWorkerAsync();
        }

        /**
        * @brief 워커 중지 메소드
        * @details worker를 중지한다.
        * @author 김민규
        * @date 2017-7-13
        */
        public void StopWorkerAsync()
        {
            messageQ.Clear();
            worker.CancelAsync();
        }

        /**
        * @brief 메세지 가져오는 워커의 메소드
        * @details IPC 연결이 수립되어 있다면 전송하는 메세지를 받아 큐에 저장한다. 연결되어 있지 않다면 연결을 기다린다.
        * @author 방준혁 sonsidal@gmail.com
        * @date 2016-10-31
        * @code
        * //파이프 생성
            NamedPipeServerStream pipe = new NamedPipeServerStream("DEVS_Animation", PipeDirection.InOut);
            //파이프 연결 안되 있으면 대기
            if (!pipe.IsConnected)
            {
                pipe.WaitForConnection();
            }

            //파이프 연결되 있으면 계속 유지하면서 데이터 긁어와서 큐에 저장
            while (pipe.IsConnected)
            {
                //바이트 읽어와서 뒤에 쓸데없는 null지우고 쓴다.
                byte[] sr = new byte[1024];
                pipe.Read(sr, 0, 1024);
                string str = System.Text.Encoding.Default.GetString(sr);
                str = str.Split('\0')[0];
                System.Diagnostics.Debug.WriteLine(messageQ.Count);
                lock (key)
                {
                    messageQ.Enqueue(str);
                }
            }
        * @endcode
        */
        public void getMsg(object sender, DoWorkEventArgs e)
        {
            //파이프 생성
            NamedPipeServerStream pipe = new NamedPipeServerStream("DEVS_Animation", PipeDirection.InOut, 10);
            lock (key)
            {
                messageQ.Enqueue("Waiting for simulator...");
            }

            //파이프 연결 안되 있으면 대기
            if (!pipe.IsConnected)
            {
                pipe.WaitForConnection();
            }

            //파이프 연결되 있으면 계속 유지하면서 데이터 긁어와서 큐에 저장
            while (pipe.IsConnected && !worker.CancellationPending)
            {
                //바이트 읽어와서 뒤에 쓸데없는 null지우고 쓴다.
                byte[] sr = new byte[1024];
                pipe.Read(sr, 0, 1024);
                string str = System.Text.Encoding.Default.GetString(sr);
                str = str.Split('\0')[0];
                System.Diagnostics.Debug.WriteLine(messageQ.Count);
                lock (key)
                {
                    messageQ.Enqueue(str);
                }
            }

            lock (key)
            {
                messageQ.Enqueue("Disconnected");
            }
            pipe.Close();
        }


        /// <summary>
        /// 워커의 작업 완료 이벤트
        /// 추후 필요하면 사용하자
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        /**
        * @brief 메세지 확인용 메소드
        * @details 큐에 메세지가 있는지 확인하고, 있다면 이를 쪼개서 반환한다.
        * @author 방준혁 sonsidal@gmail.com
        * @date 2016-10-31
        * @param msg 이후 출력을 위해 메세지 자체를 전달한다.
        */
        public string[] checkMsg(out string msg)
        {
            msg = "";
            string[] res = null;
            lock (key)
            {
                //큐데이터가 있는경우
                if (messageQ.Count > 0)
                {
                    msg = messageQ.Dequeue();
                    res = msg.Split(',');
                }
            }
            return res;
        }

        /**
        * @brief 남은 메세지 수 확인용
        * @details 큐에 메세지가 있는지 확인하고 그 수를 반환한다.
        * @author 김민규
        * @date 2016-7-13
        */
        public int checkMsgCount()
        {
            return messageQ.Count;
        }

    }
}
