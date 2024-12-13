using CommonUtilYwh.Communication.ModbusTCP;
using DWZ_Scada.ctrls.LogCtrl;
using LogTool;
using Sunny.UI;
using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using CommunicationUtilYwh.Communication.PLC;
using VisionNet472.CommunicationYwh.Device;

namespace DWZ_Scada.Pages.StationPages.OP10
{
    public partial class PageOP10 : UIPage
    {
        public PlcState PlcState;

        private readonly Action _clearAlarmDelegate;

        private KeyencePLC PLC = new KeyencePLC();

        public int OKCount { get; set; }

        public int NGCount { get; set; }

        private static PageOP10 _instance;
        public static PageOP10 Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(PageOP10))
                    {
                        if (_instance == null)
                        {
                            _instance = new PageOP10();
                        }
                    }
                }
                return _instance;
            }
        }

        public int SelectCodeType { get; set; } = -1;


        private CancellationTokenSource cts = new CancellationTokenSource();

        private PageOP10()
        {
            InitializeComponent();
            _instance = this;
        }

        private async void Page_Load(object sender, EventArgs e)
        {
            LogMgr.Instance.Debug("打开激光打标上位机软件");
            myLogCtrl1.BindingControl = uiPanel1;
            Mylog.Instance.Init(myLogCtrl1);

            Thread t = new Thread(() => PLCMainWork(cts.Token));
            t.Start();
        }

        public void PLCMainWork(CancellationToken token)
        {
            int state = -1;
            //TODO IO卡通讯
            Thread.Sleep(2000);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (PLC.IsConnect)
                    {
                        PlcState = PlcState.Online;
                    }
                    else
                    {
                        ZCForm.Instance.UpdatePlcState(PlcState);
                    }
                }
                catch (Exception ex)
                {
                    LogMgr.Instance.Error($"Exception in PLC Work: {ex.Message} {ex.StackTrace}");
                    UIMessageBox.ShowError($"错误：{ex.StackTrace}");
                }
                Thread.Sleep(100);
            }
        }

        private void uiLabel1_Click(object sender, EventArgs e)
        {

        }

        private void PageOP10_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogMgr.Instance.Info("关闭程序");
            cts.Cancel();
            PLC?.Dispose();
            _instance = null;
            //调用 Close() 方法,先进入  FormClosing 事件 ，之后再调用Designer类的Dispose
        }

        private async void uiButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void PageOP10_Initialize(object sender, EventArgs e)
        {

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {

        }
    }
    public enum PlcState
    {
        Online = 0,
        OffLine = -1,
        Alarm = 2,
        Running = 1,
        Stop = 3,
    }

    }
