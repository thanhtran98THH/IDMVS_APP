using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MvCodeReaderSDKNet;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Reflection;


namespace MSC_BasicDemo
{
    public partial class Form1 : Form
    {
        MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST m_pstDeviceList = new MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST();
        private MvCodeReader m_MyCamera = new MvCodeReader();
        bool m_bGrabbing = false;
        Thread m_hRecvChannel0Thread = null;
        Thread m_hRecvChannel1Thread = null;
        MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 m_stChannel0FrameInfo = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();
        MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 m_stChannel1FrameInfo = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();

        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        byte[] m_BufForChannel0Driver = new byte[1024 * 1024 * 20];
        byte[] m_BufForChannel1Driver = new byte[1024 * 1024 * 20];

        // 显示
        Bitmap bmp = null;
        Graphics graBox1 = null;
        Graphics graBox2 = null;
        Pen penChannel0 = new Pen(Color.Blue, 3);                   // 画笔颜色
        Pen penChannel1 = new Pen(Color.Blue, 3);                   // 画笔颜色
        Pen penChannelRoi = new Pen(Color.Green, 3);                   // 画笔颜色
        Pen penChannelLine = new Pen(Color.Red, 5);                   // 画笔颜色
        Pen penOCR = new Pen(Color.Yellow, 3);
        Pen penWay = new Pen(Color.Red, 3);
        System.Drawing.Point[] stPointChannel0List = new System.Drawing.Point[4];                 // 条码位置的4个点坐标
        System.Drawing.Point[] stPointChannel0ListRoi = new System.Drawing.Point[4];                 // 条码位置的4个点坐标
        System.Drawing.Point[] stPointChannel1List = new System.Drawing.Point[4];                 // 条码位置的4个点坐标
        GraphicsPath WayShapePath = new GraphicsPath();           // 图形路径，内部变量 
        GraphicsPath OcrShapePath_0 = new GraphicsPath();           // 图形路径，内部变量 
        Matrix stRotateM_0 = new Matrix();
        Matrix stRotateWay = new Matrix();
        GraphicsPath OcrShapePath_1 = new GraphicsPath();           // 图形路径，内部变量 
        Matrix stRotateM_1 = new Matrix();
        public List<PointF> PointsROI = new List<PointF>();
        int rowNum, colNum, offset, startX, StartY;
        Mat matImg = new Mat();
        public int maxThreshold = 150;
        public int minThreshold = 100;
        public int binary = 100;
        public List<code_data> IDMV_Data = new List<code_data>();
        List<OpenCvSharp.Point[]> contoursToKeep = new List<OpenCvSharp.Point[]>();
        List<Point2f[]> pCodeConer = new List<Point2f[]>();

        public class code_data
        {
            public string Decoded_string { get; set; }
            public Point2f[] Code_coner { get; set; }
            public bool IsSorted { get; set; }
            public int iCenterP { get; set; }

            public code_data(string decoded_string, Point2f[] code_coner, bool isSorted, int iCenter)
            {
                Decoded_string = decoded_string;
                Code_coner = code_coner;
                IsSorted = isSorted;
                iCenterP = iCenter;
            }

            public string getDecode()
            {
                return Decoded_string;
            }

            public Point2f[] getPoint()
            {
                return Code_coner;
            }

            public bool getSorted()
            {
                return IsSorted;
            }

            public int getCenter()
            {
                return iCenterP;
            }
        }

        struct stResRemoveBlob
        {
            public List<OpenCvSharp.Point[]> lRemove;
        }

        struct parameter_remove_clean
        {
            public int iMethod;
            public int iWidthMin, iWidthMax, iHeightMin, iHeightMax, iAreaMin, iAreaMax, iPremeterMin, iPremeterMax;
            public double dbMinRatio;
        }

        static bool compareContourAreas(List<OpenCvSharp.Point> contour1, List<OpenCvSharp.Point> contour2)
        {
            double i = Cv2.ContourArea(contour1, false);
            double j = Cv2.ContourArea(contour2, false);
            return i > j;
        }

        static double cal_distance_2_point(Point2f p1, Point2f p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        static stResRemoveBlob RemoveBlobs(parameter_remove_clean para, Mat mInGray, bool bDebug)
        {
            stResRemoveBlob stRes = new stResRemoveBlob();

            try
            {
                if (mInGray.Channels() == 3) return stRes;
                Mat mInTemp = mInGray.Clone();
                Mat mDraw = new Mat(mInTemp.Rows, mInTemp.Cols, MatType.CV_8UC3, Scalar.Black);
                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(mInTemp, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);

                contours = contours.OrderByDescending(contour => Cv2.ContourArea(contour, false)).ToArray();

                if (contours.Any())
                {
                    int iContourCnt = contours.Count();
                    OpenCvSharp.Rect blobRect;
                    for (int i = 0; i < iContourCnt; i++)
                    {
                        Point2f[] vtx;
                        blobRect = Cv2.BoundingRect(contours[i]);
                        RotatedRect box = Cv2.MinAreaRect(contours[i]);
                        vtx = box.Points();
                        int iWidth = (int)cal_distance_2_point(vtx[0], vtx[1]);
                        int iHeight = (int)cal_distance_2_point(vtx[1], vtx[2]);

                        if (iWidth > iHeight)
                        {
                            int iTemp = iHeight;
                            iHeight = iWidth;
                            iWidth = iTemp;
                        }

                        double ratio = (iWidth * 1.0) / iHeight;

                        if (para.iWidthMin > 0 && iWidth < para.iWidthMin)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }
                        if (para.iHeightMin > 0 && iHeight < para.iHeightMin)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }

                        if (para.iWidthMax > 0 && iWidth > para.iWidthMax)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }

                        if (para.iHeightMax > 0 && iHeight > para.iHeightMax)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }

                        int iContourArea = (int)Cv2.ContourArea(contours[i], false);

                        if (para.iAreaMin > 0 && iContourArea < para.iAreaMin)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }
                        if (para.iAreaMax > 0 && iContourArea > para.iAreaMax)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }

                        int iPerimeter = (int)Cv2.ArcLength(contours[i], false);

                        if (para.iPremeterMin > 0 && iPerimeter < para.iPremeterMin)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }
                        if (para.iPremeterMax > 0 && iPerimeter > para.iPremeterMax)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }

                        if (para.dbMinRatio > 0 && ratio < para.dbMinRatio)
                        {
                            stRes.lRemove.Add(contours[i]);
                            continue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Handle the exception if needed
                Console.WriteLine("Morphology Fail: " + e.Message);
            }

            return stRes;
        }

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            pictureBox1.Show();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCV.SizeMode = PictureBoxSizeMode.StretchImage;
            graBox1 = pictureBox1.CreateGraphics();

            //pictureBox2.Show();
            //graBox2 = pictureBox2.CreateGraphics();

        }

        // ch:显示错误信息 | en:Show error message
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MvCodeReader.MV_CODEREADER_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MvCodeReader.MV_CODEREADER_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MvCodeReader.MV_CODEREADER_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MvCodeReader.MV_CODEREADER_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MvCodeReader.MV_CODEREADER_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MvCodeReader.MV_CODEREADER_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MvCodeReader.MV_CODEREADER_E_NODATA: errorMsg += " No data "; break;
                case MvCodeReader.MV_CODEREADER_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MvCodeReader.MV_CODEREADER_E_VERSION: errorMsg += " Version mismatches "; break;
                case MvCodeReader.MV_CODEREADER_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MvCodeReader.MV_CODEREADER_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MvCodeReader.MV_CODEREADER_E_GC_GENERIC: errorMsg += " General error "; break;
                case MvCodeReader.MV_CODEREADER_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MvCodeReader.MV_CODEREADER_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MvCodeReader.MV_CODEREADER_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MvCodeReader.MV_CODEREADER_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }

        private void bnEnum_Click(object sender, EventArgs e)
        {
            DeviceListAcq();
        }

        private void DeviceListAcq()
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            cbDeviceList.Items.Clear();
            m_pstDeviceList.nDeviceNum = 0;
            int nRet = MvCodeReader.MV_CODEREADER_EnumDevices_NET(ref m_pstDeviceList, MvCodeReader.MV_CODEREADER_GIGE_DEVICE);
            if (0 != nRet)
            {
                ShowErrorMsg("Enumerate devices fail!", nRet);
                return;
            }

            if (0 == m_pstDeviceList.nDeviceNum)
            {
                ShowErrorMsg("None Device!", 0);
                return;
            }

            byte[] chUserDefinedName = null;

            // ch:在窗体列表中显示设备名 | en:Display stDevInfo name in the form list
            for (int i = 0; i < m_pstDeviceList.nDeviceNum; i++)
            {
                MvCodeReader.MV_CODEREADER_DEVICE_INFO stDevInfo = (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_pstDeviceList.pDeviceInfo[i], typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));
                if (stDevInfo.nTLayerType == MvCodeReader.MV_CODEREADER_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(stDevInfo.SpecialInfo.stGigEInfo, 0);
                    MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO stGigeInfo = (MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO));
                    if (stGigeInfo.chUserDefinedName != "")
                    {
                        chUserDefinedName = Encoding.GetEncoding("GB2312").GetBytes(stGigeInfo.chUserDefinedName);
                        string strUserDefinedName = Encoding.UTF8.GetString(chUserDefinedName);
                        cbDeviceList.Items.Add("GEV: " + strUserDefinedName + " (" + stGigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("GEV: " + stGigeInfo.chManufacturerName + " " + stGigeInfo.chModelName + " (" + stGigeInfo.chSerialNumber + ")");
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (m_pstDeviceList.nDeviceNum != 0)
            {
                cbDeviceList.SelectedIndex = 0;
            }
        }

        private void SetCtrlWhenOpen()
        {
            bnOpen.Enabled = false;

            bnClose.Enabled = true;
            bnStartGrab.Enabled = true;
            bnStopGrab.Enabled = false;
            bnContinuesMode.Enabled = true;
            bnContinuesMode.Checked = true;
            bnTriggerMode.Enabled = true;
            cbSoftTrigger.Enabled = false;
            bnTriggerExec.Enabled = false;

            tbExposure.Enabled = true;
            tbGain.Enabled = true;
            tbFrameRate.Enabled = true;
            bnGetParam.Enabled = true;
            bnSetParam.Enabled = true;
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            if (m_pstDeviceList.nDeviceNum == 0 || cbDeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("No stDevInfo, please select", 0);
                return;
            }

            // ch:获取选择的设备信息 | en:Get selected stDevInfo information
            MvCodeReader.MV_CODEREADER_DEVICE_INFO stDevInfo =
                (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_pstDeviceList.pDeviceInfo[cbDeviceList.SelectedIndex],
                                                              typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));

            // ch:打开设备 | en:Open stDevInfo
            if (null == m_MyCamera)
            {
                m_MyCamera = new MvCodeReader();
                if (null == m_MyCamera)
                {
                    return;
                }
            }

            int nRet = m_MyCamera.MV_CODEREADER_CreateHandle_NET(ref stDevInfo);
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                return;
            }

            nRet = m_MyCamera.MV_CODEREADER_OpenDevice_NET();
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                m_MyCamera.MV_CODEREADER_DestroyHandle_NET();
                ShowErrorMsg("Device open fail!", nRet);
                return;
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerMode", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_MODE.MV_CODEREADER_TRIGGER_MODE_OFF);

            bnGetParam_Click(null, null);// ch:获取参数 | en:Get parameters

            // ch:控件操作 | en:Control operation
            SetCtrlWhenOpen();
        }

        private void SetCtrlWhenClose()
        {
            bnOpen.Enabled = true;

            bnClose.Enabled = false;
            bnStartGrab.Enabled = false;
            bnStopGrab.Enabled = false;
            bnContinuesMode.Enabled = false;
            bnTriggerMode.Enabled = false;
            cbSoftTrigger.Enabled = false;
            bnTriggerExec.Enabled = false;

            tbExposure.Enabled = false;
            tbGain.Enabled = false;
            tbFrameRate.Enabled = false;
            bnGetParam.Enabled = false;
            bnSetParam.Enabled = false;
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            // ch:取流标志位清零 | en:Reset flow flag bit
            if (m_bGrabbing == true)
            {
                m_bGrabbing = false;
                m_hRecvChannel0Thread.Join();
                m_hRecvChannel1Thread.Join();

                // ch:停止采集 | en:Stop Grabbing
                int nRet = m_MyCamera.MV_CODEREADER_StopGrabbing_NET();
                if (nRet != MvCodeReader.MV_CODEREADER_OK)
                {
                    ShowErrorMsg("Stop Grabbing Fail!", nRet);
                }
            }

            // ch:关闭设备 | en:Close Device
            m_MyCamera.MV_CODEREADER_CloseDevice_NET();
            m_MyCamera.MV_CODEREADER_DestroyHandle_NET();

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenClose();
        }

        private void bnContinuesMode_CheckedChanged(object sender, EventArgs e)
        {
            if (bnContinuesMode.Checked)
            {
                int nRet = m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerMode", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_MODE.MV_CODEREADER_TRIGGER_MODE_OFF);
                if (MvCodeReader.MV_CODEREADER_OK != nRet)
                {
                    ShowErrorMsg("Set TriggerMode Off Fail!", nRet);
                    return;
                }

                cbSoftTrigger.Enabled = false;
                bnTriggerExec.Enabled = false;
                cbSoftTrigger.Checked = false;
                bnStartGrab.Enabled = true;
                bnContinuesMode.Enabled = false;
                bnTriggerMode.Enabled = true;
            }
        }

        private void bnTriggerMode_CheckedChanged(object sender, EventArgs e)
        {
            // ch:打开触发模式 | en:Open Trigger Mode
            if (bnTriggerMode.Checked)
            {
                int nRet = m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerMode", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_MODE.MV_CODEREADER_TRIGGER_MODE_ON);
                if (MvCodeReader.MV_CODEREADER_OK != nRet)
                {
                    ShowErrorMsg("Set TriggerMode On Fail!", nRet);
                    return;
                }

                // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
                //           1 - Line1;
                //           2 - Line2;
                //           3 - Line3;
                //           4 - Counter;
                //           7 - Software;
                if (cbSoftTrigger.Checked)
                {
                    nRet = m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerSource", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_SOURCE.MV_CODEREADER_TRIGGER_SOURCE_SOFTWARE);
                    if (MvCodeReader.MV_CODEREADER_OK != nRet)
                    {
                        ShowErrorMsg("Set TriggerMode Source SoftWare Fail!", nRet);
                        return;
                    }

                    if (m_bGrabbing)
                    {
                        bnTriggerExec.Enabled = true;
                    }
                }
                else
                {
                    nRet = m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerSource", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_SOURCE.MV_CODEREADER_TRIGGER_SOURCE_LINE0);
                    if (MvCodeReader.MV_CODEREADER_OK != nRet)
                    {
                        ShowErrorMsg("Set TriggerMode Source Line0 Fail!", nRet);
                        return;

                    }
                }
                cbSoftTrigger.Enabled = true;
                bnTriggerMode.Enabled = false;
                bnContinuesMode.Enabled = true;
            }
        }

        private void SetCtrlWhenStartGrab()
        {
            bnStartGrab.Enabled = false;
            bnStopGrab.Enabled = true;
            bnContinuesMode.Enabled = false;
            bnTriggerMode.Enabled = false;
            cbSoftTrigger.Enabled = false;
            if (bnTriggerMode.Checked && cbSoftTrigger.Checked)
            {
                bnTriggerExec.Enabled = true;
                cbSoftTrigger.Enabled = false;
            }
        }
        public static bool IsTextUTF8(byte[] inputStream)
        {
            int encodingBytesCount = 0;
            bool allTextsAreASCIIChars = true;

            for (int i = 0; i < inputStream.Length; i++)
            {
                byte current = inputStream[i];

                if ((current & 0x80) == 0x80)
                {
                    allTextsAreASCIIChars = false;
                }
                // First byte
                if (encodingBytesCount == 0)
                {
                    if ((current & 0x80) == 0)
                    {
                        // ASCII chars, from 0x00-0x7F
                        continue;
                    }

                    if ((current & 0xC0) == 0xC0)
                    {
                        encodingBytesCount = 1;
                        current <<= 2;

                        // More than two bytes used to encoding a unicode char.
                        // Calculate the real length.
                        while ((current & 0x80) == 0x80)
                        {
                            current <<= 1;
                            encodingBytesCount++;
                        }
                    }
                    else
                    {
                        // Invalid bits structure for UTF8 encoding rule.
                        return false;
                    }
                }
                else
                {
                    // Following bytes, must start with 10.
                    if ((current & 0xC0) == 0x80)
                    {
                        encodingBytesCount--;
                    }
                    else
                    {
                        // Invalid bits structure for UTF8 encoding rule.
                        return false;
                    }
                }
            }

            if (encodingBytesCount != 0)
            {
                // Invalid bits structure for UTF8 encoding rule.
                // Wrong following bytes count.
                return false;
            }

            // Although UTF8 supports encoding for ASCII chars, we regard as a input stream, whose contents are all ASCII as default encoding.
            return !allTextsAreASCIIChars;
        }

        List<string> decodeList = new List<string>();

        public void RecvChannel0Thread()
        {
            int nRet = MvCodeReader.MV_CODEREADER_OK;


            IntPtr pData = IntPtr.Zero;
            MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 stFrameChannel0Info = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();
            IntPtr pstChannel0InfoEx2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)));
            Marshal.StructureToPtr(stFrameChannel0Info, pstChannel0InfoEx2, false);

            while (m_bGrabbing)
            {
                nRet = m_MyCamera.MV_CODEREADER_MSC_GetOneFrameTimeout_NET(ref pData, pstChannel0InfoEx2, 0, 1000);
                if (nRet == MvCodeReader.MV_CODEREADER_OK)
                {
                    stFrameChannel0Info = (MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)Marshal.PtrToStructure(pstChannel0InfoEx2, typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2));
                    m_stChannel0FrameInfo = stFrameChannel0Info;
                }

                if (nRet == MvCodeReader.MV_CODEREADER_OK)
                {
                    if (0 >= stFrameChannel0Info.nFrameLen)
                    {
                        continue;
                    }

                    // 通道0绘制图像
                    Marshal.Copy(pData, m_BufForChannel0Driver, 0, (int)stFrameChannel0Info.nFrameLen);
                    if (stFrameChannel0Info.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Mono8)
                    {
                        IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_BufForChannel0Driver, 0);
                        bmp = new Bitmap(stFrameChannel0Info.nWidth, stFrameChannel0Info.nHeight, stFrameChannel0Info.nWidth, PixelFormat.Format8bppIndexed, pImage);
                        ColorPalette cp = bmp.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        bmp.Palette = cp;

                        pictureBox1.Image = (Image)bmp;
                    }
                    else if (stFrameChannel0Info.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Jpeg)
                    {
                        GC.Collect();
                        MemoryStream ms = new MemoryStream();
                        ms.Write(m_BufForChannel0Driver, 0, (int)stFrameChannel0Info.nFrameLen);
                        Image img = Image.FromStream(ms);
                        pictureBox1.Image = img;
                    }

                    MvCodeReader.MV_CODEREADER_STRINGVALUE stBcrResult2 = (MvCodeReader.MV_CODEREADER_STRINGVALUE)Marshal.PtrToStructure(stFrameChannel0Info.pstCodeListEx, typeof(MvCodeReader.MV_CODEREADER_STRINGVALUE));
                    //if(stBcrResult2.chCurValue.Length != 0)
                    //{
                    //    Console.WriteLine("============================data = " + stBcrResult2.chCurValue);
                    //}

                    MvCodeReader.MV_CODEREADER_RESULT_BCR_EX stBcrResult = (MvCodeReader.MV_CODEREADER_RESULT_BCR_EX)Marshal.PtrToStructure(stFrameChannel0Info.pstCodeListEx, typeof(MvCodeReader.MV_CODEREADER_RESULT_BCR_EX));

                    if (stFrameChannel0Info.bIsGetCode)
                    {
                        if (decodeList.Count != 0)
                        {
                            decodeList.Clear();
                            Console.WriteLine("Clear list code");
                            listBox1.Items.Clear();
                        }
                        pCodeConer.Clear();
                        pictureBox1.Refresh();
                        IDMV_Data.Clear();

                        string data = "";
                        for (int i = 0; i < stBcrResult.nCodeNum; ++i)
                        {
                            // test decode data
                            bool bIsValidUTF8 = IsTextUTF8(stBcrResult.stBcrInfoEx[i].chCode);
                            if (bIsValidUTF8)
                            {
                                string strCode = Encoding.UTF8.GetString(stBcrResult.stBcrInfoEx[i].chCode);
                                Console.WriteLine("------------------------------------------------Get CodeNum: " + "CodeNum[" + i.ToString() + "], CodeString[" + strCode.Trim().TrimEnd('\0') + "]");

                            }
                            else
                            {
                                string strCode = Encoding.GetEncoding("GB2312").GetString(stBcrResult.stBcrInfoEx[i].chCode);
                                Console.WriteLine("------------------------------------------------Get CodeNum: " + "CodeNum[" + i.ToString() + "], CodeString[" + strCode.Trim().TrimEnd('\0') + "]");
                                data = strCode.Trim().TrimEnd('\0');
                                //decodeList.Add(data); 
                                //listBox1.Items.Add(data);
                            }

                            stPointChannel0List = new System.Drawing.Point[4];
                            Point2f[] tempPoint = new Point2f[4];
                            for (int j = 0; j < 4; ++j)
                            {
                                stPointChannel0List[j].X = (int)(stBcrResult.stBcrInfoEx[i].pt[j].x * (float)(pictureBox1.Size.Width) / stFrameChannel0Info.nWidth);
                                stPointChannel0List[j].Y = (int)(stBcrResult.stBcrInfoEx[i].pt[j].y * (float)(pictureBox1.Size.Height) / stFrameChannel0Info.nHeight);
                                //PointsROI.Add(ConvertPointToPointF(stPointChannel0List[j]));
                                tempPoint[j] = new Point2f(stBcrResult.stBcrInfoEx[i].pt[j].x, stBcrResult.stBcrInfoEx[i].pt[j].y);
                                //pCodeConer[j] = new Point2f((float)stBcrResult.stBcrInfoEx[i].pt[j].x * (pictureBox1.Size.Width / (float)stFrameChannel0Info.nWidth),
                                //                (float)stBcrResult.stBcrInfoEx[i].pt[j].y * (pictureBox1.Size.Height / (float)stFrameChannel0Info.nHeight));
                            }
                            pCodeConer.Add(tempPoint);
                            code_data code = new code_data(data, tempPoint, false, 0);
                            IDMV_Data.Add(code);
                            graBox1.DrawPolygon(penChannel0, stPointChannel0List);

                            // test draw roi
                            //stPointChannel0ListRoi = new System.Drawing.Point[4];

                            //stPointChannel0ListRoi[0].X = stPointChannel0List[0].X - 10;
                            //stPointChannel0ListRoi[0].Y = stPointChannel0List[0].Y + 10;

                            //stPointChannel0ListRoi[1].X = stPointChannel0List[1].X - 10;
                            //stPointChannel0ListRoi[1].Y = stPointChannel0List[1].Y + 10;

                            //stPointChannel0ListRoi[2].X = stPointChannel0List[2].X + 10;
                            //stPointChannel0ListRoi[2].Y = stPointChannel0List[2].Y + 10;

                            //stPointChannel0ListRoi[3].X = stPointChannel0List[3].X + 10;
                            //stPointChannel0ListRoi[3].Y = stPointChannel0List[3].Y - 10;

                            //for (int j = 0; j < 4; ++j)
                            //{
                            //    PointsROI.Add(ConvertPointToPointF(stPointChannel0List[j]));

                            //}

                            //test enlarge
                            //List<PointF> enlarged_points = GetEnlargedPolygon(PointsROI, 10f);

                            //graBox1.DrawPolygon(penChannelRoi, enlarged_points.ToArray());

                            //PointsROI.Clear();
                            //dataGridViewShowData.BeginInvoke(new Action(() =>
                            //{
                            //    DataGridViewRow row = new DataGridViewRow();
                            //    row.CreateCells(dataGridViewShowData);
                            //    row.Cells[0].Value = i + 1;
                            //    row.Cells[1].Value = IDMV_Data[i].getDecode();
                            //    dataGridViewShowData.Rows.Add(row);

                            //}));
                        }

                        dataGridViewShowData.Rows.Clear();
                        int iNo = 1;
                        foreach (var codeData in IDMV_Data)
                        {
                            dataGridViewShowData.BeginInvoke(new Action(() =>
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dataGridViewShowData);
                                row.Cells[0].Value = iNo;
                                row.Cells[1].Value = codeData.Decoded_string;
                                dataGridViewShowData.Rows.Add(row);
                                iNo++;
                            }));
                        }
                        picBoxToMat();
                        fillerImg();
                        contourFind();
                        drawRoi();

                    }


                    MvCodeReader.MV_CODEREADER_WAYBILL_LIST stWayList = (MvCodeReader.MV_CODEREADER_WAYBILL_LIST)Marshal.PtrToStructure(stFrameChannel0Info.pstWaybillList, typeof(MvCodeReader.MV_CODEREADER_WAYBILL_LIST));

                    for (int i = 0; i < stWayList.nWaybillNum; ++i)
                    {
                        float fWayX = (float)(stWayList.stWaybillInfo[i].fCenterX * (float)(pictureBox1.Size.Width) / stFrameChannel0Info.nWidth);
                        float fWayY = (float)(stWayList.stWaybillInfo[i].fCenterY * (float)(pictureBox1.Size.Height) / stFrameChannel0Info.nHeight);
                        float fWayW = (float)(stWayList.stWaybillInfo[i].fWidth * (float)(pictureBox1.Size.Width) / stFrameChannel0Info.nWidth);
                        float fWayH = (float)(stWayList.stWaybillInfo[i].fHeight * (float)(pictureBox1.Size.Height) / stFrameChannel0Info.nHeight);

                        WayShapePath.Reset();
                        WayShapePath.AddRectangle(new RectangleF(fWayX - fWayW / 2, fWayY - fWayH / 2, fWayW, fWayH));

                        stRotateWay.Reset();
                        PointF stCenPoint = new PointF(fWayX, fWayY);
                        stRotateWay.RotateAt(stWayList.stWaybillInfo[i].fAngle, stCenPoint);
                        WayShapePath.Transform(stRotateWay);
                        graBox1.DrawPath(penWay, WayShapePath);
                    }

                    MvCodeReader.MV_CODEREADER_OCR_INFO_LIST stOcrInfo = (MvCodeReader.MV_CODEREADER_OCR_INFO_LIST)Marshal.PtrToStructure(stFrameChannel0Info.UnparsedOcrList.pstOcrList, typeof(MvCodeReader.MV_CODEREADER_OCR_INFO_LIST));

                    for (int i = 0; i < stOcrInfo.nOCRAllNum; ++i)
                    {
                        float fOcrInfoX = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowCenterX * (float)(pictureBox1.Size.Width) / stFrameChannel0Info.nWidth);
                        float fOcrInfoY = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowCenterY * (float)(pictureBox1.Size.Height) / stFrameChannel0Info.nHeight);
                        float fOcrInfoW = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowWidth * (float)(pictureBox1.Size.Width) / stFrameChannel0Info.nWidth);
                        float fOcrInfoH = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowHeight * (float)(pictureBox1.Size.Height) / stFrameChannel0Info.nHeight);

                        OcrShapePath_0.Reset();
                        OcrShapePath_0.AddRectangle(new RectangleF(fOcrInfoX - fOcrInfoW / 2, fOcrInfoY - fOcrInfoH / 2, fOcrInfoW, fOcrInfoH));

                        stRotateM_0.Reset();
                        PointF stCenPoint = new PointF(fOcrInfoX, fOcrInfoY);
                        stRotateM_0.RotateAt(stOcrInfo.stOcrRowInfo[i].fOcrRowAngle, stCenPoint);
                        OcrShapePath_0.Transform(stRotateM_0);
                        graBox1.DrawPath(penOCR, OcrShapePath_0);

                    }
                }
                else
                {
                    if (MvCodeReader.MV_CODEREADER_E_PARAMETER == nRet)
                    {
                        break;
                    }

                    if (bnTriggerMode.Checked)
                    {
                        Thread.Sleep(5);
                    }
                    continue;
                }
            }
        }

        public void RecvChannel1Thread()
        {
            int nRet = MvCodeReader.MV_CODEREADER_OK;

            IntPtr pData = IntPtr.Zero;
            MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 stFrameInfo = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();
            IntPtr pstFrameInfoEx2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)));
            Marshal.StructureToPtr(stFrameInfo, pstFrameInfoEx2, false);

            while (m_bGrabbing)
            {
                nRet = m_MyCamera.MV_CODEREADER_MSC_GetOneFrameTimeout_NET(ref pData, pstFrameInfoEx2, 1, 1000);
                if (nRet == MvCodeReader.MV_CODEREADER_OK)
                {
                    stFrameInfo = (MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)Marshal.PtrToStructure(pstFrameInfoEx2, typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2));
                    m_stChannel0FrameInfo = stFrameInfo;
                }

                if (nRet == MvCodeReader.MV_CODEREADER_OK)
                {
                    if (0 >= stFrameInfo.nFrameLen)
                    {
                        continue;
                    }

                    // 通道1绘制图像
                    Marshal.Copy(pData, m_BufForChannel1Driver, 0, (int)stFrameInfo.nFrameLen);
                    if (stFrameInfo.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Mono8)
                    {
                        IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_BufForChannel1Driver, 0);
                        bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth, PixelFormat.Format8bppIndexed, pImage);
                        ColorPalette cp = bmp.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        bmp.Palette = cp;

                        pictureBox2.Image = (Image)bmp;
                    }
                    else if (stFrameInfo.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Jpeg)
                    {
                        GC.Collect();
                        MemoryStream ms = new MemoryStream();
                        ms.Write(m_BufForChannel1Driver, 0, (int)stFrameInfo.nFrameLen);

                        pictureBox2.Image = Image.FromStream(ms);
                    }

                    MvCodeReader.MV_CODEREADER_RESULT_BCR_EX stBcrList = (MvCodeReader.MV_CODEREADER_RESULT_BCR_EX)Marshal.PtrToStructure(stFrameInfo.pstCodeListEx, typeof(MvCodeReader.MV_CODEREADER_RESULT_BCR_EX));

                    if (stFrameInfo.bIsGetCode)
                    {
                        pictureBox2.Refresh();
                        for (int i = 0; i < stBcrList.nCodeNum; ++i)
                        {
                            stPointChannel1List = new System.Drawing.Point[4];
                            for (int j = 0; j < 4; ++j)
                            {
                                stPointChannel1List[j].X = (int)(stBcrList.stBcrInfoEx[i].pt[j].x * (float)(pictureBox2.Size.Width) / stFrameInfo.nWidth);
                                stPointChannel1List[j].Y = (int)(stBcrList.stBcrInfoEx[i].pt[j].y * (float)(pictureBox2.Size.Height) / stFrameInfo.nHeight);
                            }

                            graBox2.DrawPolygon(penChannel1, stPointChannel1List);
                        }
                    }

                    MvCodeReader.MV_CODEREADER_OCR_INFO_LIST stOcrInfo = (MvCodeReader.MV_CODEREADER_OCR_INFO_LIST)Marshal.PtrToStructure(stFrameInfo.UnparsedOcrList.pstOcrList, typeof(MvCodeReader.MV_CODEREADER_OCR_INFO_LIST));

                    for (int i = 0; i < stOcrInfo.nOCRAllNum; ++i)
                    {
                        float fOcrInfoX = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowCenterX * (float)(pictureBox2.Size.Width) / stFrameInfo.nWidth);
                        float fOcrInfoY = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowCenterY * (float)(pictureBox2.Size.Height) / stFrameInfo.nHeight);
                        float fOcrInfoW = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowWidth * (float)(pictureBox2.Size.Width) / stFrameInfo.nWidth);
                        float fOcrInfoH = (float)(stOcrInfo.stOcrRowInfo[i].nOcrRowHeight * (float)(pictureBox2.Size.Height) / stFrameInfo.nHeight);

                        OcrShapePath_1.Reset();
                        OcrShapePath_1.AddRectangle(new RectangleF(fOcrInfoX - fOcrInfoW / 2, fOcrInfoY - fOcrInfoH / 2, fOcrInfoW, fOcrInfoH));

                        stRotateM_1.Reset();
                        PointF stCenPoint = new PointF(fOcrInfoX, fOcrInfoY);
                        stRotateM_1.RotateAt(stOcrInfo.stOcrRowInfo[i].fOcrRowAngle, stCenPoint);
                        OcrShapePath_1.Transform(stRotateM_1);
                        graBox2.DrawPath(penOCR, OcrShapePath_1);
                    }
                }
                else
                {
                    if (MvCodeReader.MV_CODEREADER_E_PARAMETER == nRet)
                    {
                        break;
                    }
                    if (bnTriggerMode.Checked)
                    {
                        Thread.Sleep(5);
                    }
                    continue;
                }
            }
        }

        private void bnStartGrab_Click(object sender, EventArgs e)
        {
            // ch:标志位置位true | en:Set position bit true
            m_bGrabbing = true;

            m_hRecvChannel0Thread = new Thread(RecvChannel0Thread);
            m_hRecvChannel0Thread.Start();

            //m_hRecvChannel1Thread = new Thread(RecvChannel1Thread);
            //m_hRecvChannel1Thread.Start();

            m_stChannel0FrameInfo.nFrameLen = 0;//取流之前先清除帧长度
            m_stChannel0FrameInfo.enPixelType = MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Undefined;

            m_stChannel1FrameInfo.nFrameLen = 0;//取流之前先清除帧长度
            m_stChannel1FrameInfo.enPixelType = MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Undefined;

            // ch:开始采集 | en:Start Grabbing
            int nRet = m_MyCamera.MV_CODEREADER_StartGrabbing_NET();
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                m_bGrabbing = false;
                m_hRecvChannel0Thread.Join();
                //m_hRecvChannel1Thread.Join();
                ShowErrorMsg("Start Grabbing Fail!", nRet);
                return;
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStartGrab();

        }

        private void cbSoftTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSoftTrigger.Checked)
            {
                // ch:触发源设为软触发 | en:Set trigger source as Software
                m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerSource", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_SOURCE.MV_CODEREADER_TRIGGER_SOURCE_SOFTWARE);
                if (m_bGrabbing)
                {
                    bnTriggerExec.Enabled = true;
                }
            }
            else
            {
                m_MyCamera.MV_CODEREADER_SetEnumValue_NET("TriggerSource", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_SOURCE.MV_CODEREADER_TRIGGER_SOURCE_LINE0);
                bnTriggerExec.Enabled = false;
            }
        }

        private void bnTriggerExec_Click(object sender, EventArgs e)
        {
            // ch:触发命令 | en:Trigger command
            int nRet = m_MyCamera.MV_CODEREADER_SetCommandValue_NET("TriggerSoftware");
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                ShowErrorMsg("Trigger Software Fail!", nRet);
            }
        }

        private void SetCtrlWhenStopGrab()
        {
            bnStartGrab.Enabled = true;
            bnStopGrab.Enabled = false;

            bnTriggerExec.Enabled = false;
            if (bnTriggerMode.Checked)
            {
                cbSoftTrigger.Enabled = true;
                bnTriggerMode.Enabled = false;
                bnContinuesMode.Enabled = true;
            }
            else
            {
                bnContinuesMode.Enabled = false;
                bnTriggerMode.Enabled = true;
            }
        }

        private void bnStopGrab_Click(object sender, EventArgs e)
        {
            // ch:标志位设为false | en:Set flag bit false
            m_bGrabbing = false;

            if (null != m_hRecvChannel0Thread)
            {
                m_hRecvChannel0Thread.Join();
            }

            if (null != m_hRecvChannel1Thread)
            {
                m_hRecvChannel1Thread.Join();
            }

            // ch:停止采集 | en:Stop Grabbing
            int nRet = m_MyCamera.MV_CODEREADER_StopGrabbing_NET();
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                ShowErrorMsg("Stop Grabbing Fail!", nRet);
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStopGrab();
        }

        private void bnGetParam_Click(object sender, EventArgs e)
        {
            MvCodeReader.MV_CODEREADER_FLOATVALUE stParam = new MvCodeReader.MV_CODEREADER_FLOATVALUE();
            int nRet = m_MyCamera.MV_CODEREADER_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
            {
                tbExposure.Text = stParam.fCurValue.ToString("F1");
            }
            else
            {
                ShowErrorMsg("Get ExposureTime Fail!", nRet);
            }

            nRet = m_MyCamera.MV_CODEREADER_GetFloatValue_NET("Gain", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
            {
                tbGain.Text = stParam.fCurValue.ToString("F1");
            }
            else
            {
                ShowErrorMsg("Get Gain Fail!", nRet);
            }

            nRet = m_MyCamera.MV_CODEREADER_GetFloatValue_NET("AcquisitionFrameRate", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
            {
                tbFrameRate.Text = stParam.fCurValue.ToString("F1");
            }
            else
            {
                ShowErrorMsg("Get FrameRate Fail!", nRet);
            }
        }

        private void bnSetParam_Click(object sender, EventArgs e)
        {
            try
            {
                float.Parse(tbExposure.Text);
                float.Parse(tbGain.Text);
                float.Parse(tbFrameRate.Text);
            }
            catch
            {
                ShowErrorMsg("Please enter correct type!", 0);
                return;
            }

            bool bIsSetted = true;
            m_MyCamera.MV_CODEREADER_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = m_MyCamera.MV_CODEREADER_SetFloatValue_NET("ExposureTime", float.Parse(tbExposure.Text));
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                bIsSetted = false;
                ShowErrorMsg("Set Exposure Time Fail!", nRet);
            }

            m_MyCamera.MV_CODEREADER_SetEnumValue_NET("GainAuto", 0);
            nRet = m_MyCamera.MV_CODEREADER_SetFloatValue_NET("Gain", float.Parse(tbGain.Text));
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                bIsSetted = false;
                ShowErrorMsg("Set Gain Fail!", nRet);
            }

            nRet = m_MyCamera.MV_CODEREADER_SetFloatValue_NET("AcquisitionFrameRate", float.Parse(tbFrameRate.Text));
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                bIsSetted = false;
                ShowErrorMsg("Set Frame Rate Fail!", nRet);
            }

            if (bIsSetted)
            {
                MessageBox.Show("Set Param Secceed");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_bGrabbing = false;
            if (null != m_hRecvChannel0Thread)
            {
                m_hRecvChannel0Thread.Join();
            }

            if (null != m_hRecvChannel1Thread)
            {
                m_hRecvChannel1Thread.Join();
            }
        }

        private List<PointF> GetEnlargedPolygon(List<PointF> old_points, float offset)
        {
            List<PointF> enlarged_points = new List<PointF>();
            int num_points = old_points.Count;
            for (int j = 0; j < num_points; j++)
            {
                // Find the new location for point j.
                // Find the points before and after j.
                int i = (j - 1);
                if (i < 0) i += num_points;
                int k = (j + 1) % num_points;

                // Move the points by the offset.
                Vector v1 = new Vector(
                    old_points[j].X - old_points[i].X,
                    old_points[j].Y - old_points[i].Y);
                v1.Normalize();
                v1 *= offset;
                Vector n1 = new Vector(-v1.Y, v1.X);

                PointF pij1 = new PointF(
                    (float)(old_points[i].X + n1.X),
                    (float)(old_points[i].Y + n1.Y));
                PointF pij2 = new PointF(
                    (float)(old_points[j].X + n1.X),
                    (float)(old_points[j].Y + n1.Y));

                Vector v2 = new Vector(
                    old_points[k].X - old_points[j].X,
                    old_points[k].Y - old_points[j].Y);
                v2.Normalize();
                v2 *= offset;
                Vector n2 = new Vector(-v2.Y, v2.X);

                PointF pjk1 = new PointF(
                    (float)(old_points[j].X + n2.X),
                    (float)(old_points[j].Y + n2.Y));
                PointF pjk2 = new PointF(
                    (float)(old_points[k].X + n2.X),
                    (float)(old_points[k].Y + n2.Y));

                // See where the shifted lines ij and jk intersect.
                bool lines_intersect, segments_intersect;
                PointF poi, close1, close2;
                FindIntersection(pij1, pij2, pjk1, pjk2,
                    out lines_intersect, out segments_intersect,
                    out poi, out close1, out close2);
                Debug.Assert(lines_intersect,
                    "Edges " + i + "-->" + j + " and " +
                    j + "-->" + k + " are parallel");

                enlarged_points.Add(poi);
            }

            return enlarged_points;
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointF intersection,
            out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        private void numericUpDownStartX_ValueChanged(object sender, EventArgs e)
        {
            startX = (int)numericUpDownStartX.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            picBoxToMat();
            //Bitmap bitmap = null;
            //if (pictureBox1.Image != null)
            //{
            //    pictureBoxCV.Image = pictureBox1.Image;
            //    bitmap = (Bitmap)pictureBox1.Image;

            //}
            //else
            //{
            //    bitmap = (Bitmap)pictureBoxCV.Image;
            //}
            //matImg = new Mat(5440, 3648, MatType.CV_8UC1);
            //matImg = BitmapConverter.ToMat(bitmap);
            //Cv2.CvtColor(matImg, matImg, ColorConversionCodes.BGRA2GRAY);
            //Cv2.ImShow("Mat img", matImg);
        }

        public void picBoxToMat()
        {
            Bitmap bitmap = null;
            if (pictureBox1.Image != null)
            {
                pictureBoxCV.Image = pictureBox1.Image;
                bitmap = (Bitmap)pictureBox1.Image;

            }
            else
            {
                bitmap = (Bitmap)pictureBoxCV.Image;
            }
            matImg = new Mat(5440, 3648, MatType.CV_8UC1);
            matImg = BitmapConverter.ToMat(bitmap);
            Cv2.CvtColor(matImg, matImg, ColorConversionCodes.BGRA2GRAY);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cv2.Threshold(matImg, matImg, thresh: binary, maxval: 256, type: ThresholdTypes.Binary);
            //Cv2.ImShow("binary", matImg);
            pictureBoxCV.Image = matImg.ToBitmap();
        }

        private void findContour_Click(object sender, EventArgs e)
        {
            //Mat grayImg = new Mat();
            //Cv2.CvtColor(matImg, grayImg,ColorConversionCodes.BGRA2GRAY);

            //code
            //OpenCvSharp.Point[][] contours;
            //HierarchyIndex[] hierarchy;
            //Cv2.FindContours(matImg, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            //Mat result = new Mat(matImg.Size(), MatType.CV_8UC1, Scalar.Black);

            //contoursToKeep.Clear();

            //foreach (var Contour in contours)
            //{
            //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(Contour);
            //    if (boundingRect.Width >= minThreshold && boundingRect.Width <= maxThreshold && boundingRect.Height >= minThreshold && boundingRect.Height <= maxThreshold)
            //    {
            //        contoursToKeep.Add(Contour);
            //    }
            //}

            //for (int i = 0; i < contoursToKeep.Count; i++)
            //{
            //    Cv2.DrawContours(result, contoursToKeep, i, Scalar.White, thickness: 6);
            //}
            //code


            //Point2f point = new Point2f();
            //InputArray inputArray = contoursToKeep.ToArray();
            ////Cv2.DrawContours
            //InputArray inputArray = InputArray.Create(contoursToKeep[0]);
            //isInside(point, inputArray);
            //foreach (var contour in contoursToKeep)
            //{
            //    RotatedRect rotatedRect = Cv2.MinAreaRect(contour);
            //    Point2f[] vertices = Cv2.BoxPoints(rotatedRect);

            //    OpenCvSharp.Point[] boxVertices = new OpenCvSharp.Point[4];
            //    for (int i = 0; i < 4; i++)
            //    {
            //        boxVertices[i] = new OpenCvSharp.Point((int)vertices[i].X, (int)vertices[i].Y);
            //    }

            //    result.Polylines(new[] {boxVertices}, true, Scalar.White, 5);
            //}
            //for (int i = 0; i < contours.Length; i++)
            //{
            //    Cv2.DrawContours(result, contours, i, Scalar.White, thickness: 5);
            //}
            //Cv2.ImShow("Blobs Detection Result", matImg);


            //pictureBoxCV.Image = result.ToBitmap();
            contourFind();

        }

        public void contourFind()
        {
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(matImg, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            Mat result = new Mat(matImg.Size(), MatType.CV_8UC1, Scalar.Black);

            contoursToKeep.Clear();

            foreach (var Contour in contours)
            {
                OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(Contour);
                if (boundingRect.Width >= minThreshold && boundingRect.Width <= maxThreshold && boundingRect.Height >= minThreshold && boundingRect.Height <= maxThreshold)
                {
                    contoursToKeep.Add(Contour);
                }
            }

            for (int i = 0; i < contoursToKeep.Count; i++)
            {
                Cv2.DrawContours(result, contoursToKeep, i, Scalar.White, thickness: 6);
            }

            pictureBoxCV.Image = result.ToBitmap();
        }

        public void isCenter()
        {

        }

        private void numericUpDownH_ValueChanged(object sender, EventArgs e)
        {
            colNum = (int)numericUpDownH.Value;
        }

        private void buttonMono_Click(object sender, EventArgs e)
        {
            matImg = Mophology(9, 1, MorphTypes.Dilate, matImg);
            //Cv2.ImShow("mono",matImg);
            pictureBoxCV.Image = matImg.ToBitmap();
        }

        private void numericUpDownThresholdMin_ValueChanged(object sender, EventArgs e)
        {
            minThreshold = (int)numericUpDownThresholdMin.Value;
        }

        private void numericUpDownBinary_ValueChanged(object sender, EventArgs e)
        {
            binary = (int)numericUpDownBinary.Value;
        }

        private void buttonNewContour_Click(object sender, EventArgs e)
        {
            parameter_remove_clean parameter_Remove_Clean = new parameter_remove_clean();
            parameter_Remove_Clean.iHeightMax = 5;
            parameter_Remove_Clean.iWidthMax = 5;
            RemoveBlobs(parameter_Remove_Clean, matImg, false);
        }

        private void numericUpDownThreshold_ValueChanged(object sender, EventArgs e)
        {
            maxThreshold = (int)numericUpDownThresholdMax.Value;
        }

        private void buttonRoi_Click(object sender, EventArgs e)
        {
            //foreach (var contour in contoursToKeep)
            //{
            //    RotatedRect rotatedRect = Cv2.MinAreaRect(contour);
            //    Point2f[] vertices = Cv2.BoxPoints(rotatedRect);

            //    OpenCvSharp.Point[] boxVertices = new OpenCvSharp.Point[4];
            //    for (int i = 0; i < 4; i++)
            //    {
            //        boxVertices[i] = new OpenCvSharp.Point((int)vertices[i].X, (int)vertices[i].Y);
            //    }

            //    result.Polylines(new[] { boxVertices }, true, Scalar.White, 5);
            //}
            //for (int i = 0; i < contours.Length; i++)
            //{
            //    Cv2.DrawContours(result, contours, i, Scalar.White, thickness: 5);
            //}
            //Cv2.ImShow("Blobs Detection Result", matImg);

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            matImg = Cv2.ImRead("C:\\Users\\Doctor\\Desktop\\idmv\\Image_20240130174739094.jpg", ImreadModes.Color);
            pictureBoxCV.Image = matImg.ToBitmap();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            offset = (int)numericUpDown1.Value;
        }

        private void numericUpDownR_ValueChanged(object sender, EventArgs e)
        {
            rowNum = (int)numericUpDownR.Value;
        }

        private void buttonDrawRoi_Click(object sender, EventArgs e)
        {
            //Mat tempImg = new Mat(5440, 3648, MatType.CV_8UC1);
            //if (pictureBox1.Image != null)
            //{
            //    //Mat tempImg = new Mat(5440, 3648, MatType.CV_8UC1);
            //    Bitmap bitmapTemp = (Bitmap)pictureBox1.Image;
            //    tempImg = BitmapConverter.ToMat(bitmapTemp);
            //}
            //else
            //{
            //    //Mat tempImg = new Mat(5440, 3648, MatType.CV_8UC1);
            //    Bitmap bitmapTemp = (Bitmap)pictureBoxCV.Image;
            //    tempImg = BitmapConverter.ToMat(bitmapTemp);
            //}

            //foreach (var contour in contoursToKeep)
            //{
            //    RotatedRect rotatedRect = Cv2.MinAreaRect(contour);
            //    Point2f[] vertices = Cv2.BoxPoints(rotatedRect);
            //    OpenCvSharp.Point[] boxVertices = new OpenCvSharp.Point[4];
            //    for (int i = 0; i < 4; i++)
            //    {
            //        boxVertices[i] = new OpenCvSharp.Point((int)vertices[i].X, (int)vertices[i].Y);
            //    }

            //    tempImg.Polylines(new[] { boxVertices }, true, Scalar.Red, 5);

            //    //for (int i = 0; i < contours.Length; i++)
            //    //{
            //    //    Cv2.DrawContours(result, contours, i, Scalar.White, thickness: 5);
            //    //}
            //}
            //pictureBox1.Image = tempImg.ToBitmap();

            drawRoi();
        }

        public void drawRoi()
        {
            Mat tempImg = new Mat(5440, 3648, MatType.CV_8UC1);
            if (pictureBox1.Image != null)
            {
                //Bitmap bitmapTemp = new Bitmap(5440, 3648);
                //pictureBox1.DrawToBitmap(bitmapTemp, pictureBox1.ClientRectangle);
                //Mat tempImg = new Mat(5440, 3648, MatType.CV_8UC1);
                Bitmap bitmapTemp = (Bitmap)pictureBox1.Image;
                tempImg = BitmapConverter.ToMat(bitmapTemp);
            }
            else
            {
                //Mat tempImg = new Mat(5440, 3648, MatType.CV_8UC1);
                Bitmap bitmapTemp = (Bitmap)pictureBoxCV.Image;
                tempImg = BitmapConverter.ToMat(bitmapTemp);
            }

            foreach (var contour in contoursToKeep)
            {
                RotatedRect rotatedRect = Cv2.MinAreaRect(contour);
                Point2f[] vertices = Cv2.BoxPoints(rotatedRect);
                OpenCvSharp.Point[] boxVertices = new OpenCvSharp.Point[4];
                for (int i = 0; i < 4; i++)
                {
                    boxVertices[i] = new OpenCvSharp.Point((int)vertices[i].X, (int)vertices[i].Y);
                }

                tempImg.Polylines(new[] { boxVertices }, true, Scalar.Red, 5);

                //for (int i = 0; i < contours.Length; i++)
                //{
                //    Cv2.DrawContours(result, contours, i, Scalar.White, thickness: 5);
                //}
            }
            DrawCodeConder(tempImg);
            pictureBox1.Image = tempImg.ToBitmap();
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            //Cv2.AdaptiveThreshold(matImg, matImg, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 777, -15);
            //matImg = Mophology(7, 5, MorphTypes.Open, matImg);
            //matImg = Mophology(33, 2, MorphTypes.Dilate, matImg);
            //pictureBoxCV.Image = matImg.ToBitmap() ;
            fillerImg();
        }

        public void fillerImg()
        {
            Cv2.AdaptiveThreshold(matImg, matImg, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 777, -15);
            matImg = Mophology(7, 5, MorphTypes.Open, matImg);
            matImg = Mophology(33, 2, MorphTypes.Dilate, matImg);
            pictureBoxCV.Image = matImg.ToBitmap();
        }

        private void buttonDrawConer_Click(object sender, EventArgs e)
        {

            //foreach(var point in pCodeConer)
            //{
            //    OpenCvSharp.Point[] boxVertices = new OpenCvSharp.Point[4];
            //    for (int i = 0; i < 4; i++)
            //    {
            //        boxVertices[i] = new OpenCvSharp.Point((int)point[i].X, (int)point[i].Y);
            //    }
            //    matImg.Polylines(new[] { boxVertices }, true, Scalar.Blue, 5);
            //}
            //pictureBox1.Image = matImg.ToBitmap() ;
        }

        public void DrawCodeConder(Mat img)
        {
            foreach (var point in pCodeConer)
            {
                OpenCvSharp.Point[] boxVertices = new OpenCvSharp.Point[4];
                for (int i = 0; i < 4; i++)
                {
                    boxVertices[i] = new OpenCvSharp.Point((int)point[i].X, (int)point[i].Y);
                }
                img.Polylines(new[] { boxVertices }, true, Scalar.Blue, 5);
            }
        }

        static PointF ConvertPointToPointF(System.Drawing.Point point)
        {
            // Create a new PointF using the coordinates of the Point
            return new PointF(point.X, point.Y);
        }

        public static Mat Mophology(int iSize, int iLoop, MorphTypes iType, Mat mIn)
        {
            Mat mMophology = new Mat();
            try
            {
                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(iSize, iSize), new OpenCvSharp.Point(iSize / 2, iSize / 2));
                if (iType == MorphTypes.Erode)
                {
                    Cv2.Erode(mIn, mMophology, kernel, iterations: iLoop);
                }
                else if (iType == MorphTypes.Dilate)
                {
                    Cv2.Dilate(mIn, mMophology, kernel, iterations: iLoop);
                }
                else
                {
                    Cv2.MorphologyEx(mIn, mMophology, iType, kernel, anchor: new OpenCvSharp.Point(-1, -1), iterations: iLoop);
                }
                return mMophology;
            }
            catch (Exception e)
            {
                // Handle the exception if needed
                Console.WriteLine("Morphology Fail: " + e.Message);
                return mMophology;
            }
        }

        public static Point2f FindCenterPoint(Point2f[] points)
        {
            if (points.Length != 4)
            {
                throw new ArgumentException("Point count error");
            }
            //System.Drawing.Rectangle rec = new Rectangle();
            float sumX = 0;
            float sumY = 0;
            foreach (var point in points)
            {
                sumX += point.X;
                sumY += point.Y;
            }
            float centerX = sumX / 4;
            float centerY = sumY / 4;
            return new Point2f(centerX, centerY);
        }

        public bool isInside(Point2f point, InputArray contours)
        {
            double distance = Cv2.PointPolygonTest(contours, point, false);
            if (distance >= 0)
            {
                return true;
            }
            else
                return false;
        }

    }
}