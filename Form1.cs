using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace STF_Trainer
{
    enum ResourceTypes
    {
        Turn,
        Credits,
        Fuel
    }
    public partial class Form1 : Form
    {
        private bool GameProgressAvailable = false;
        private string GameProcessName = "StarTradersFrontiers";
        private IntPtr GameProcessHandle;
        private IntPtr GameBaseAddress;
        public Form1()
        {
            InitializeComponent();
        }

        //从指定内存中读取字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesRead);
        //从指定内存中写入字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        private static int ReadMemoryValue(IntPtr hProcess, IntPtr address)
        {
            try
            {
                byte[] buffer = new byte[4];
                IntPtr bytesRead = IntPtr.Zero;
                ReadProcessMemory(hProcess, address, buffer, buffer.Length, out bytesRead);
                if (bytesRead == IntPtr.Zero) return -1;
                return BitConverter.ToInt32(buffer, 0);
            }
            catch { return -1; }
        }
        private static int WriteMemoryValue(IntPtr hProcess, IntPtr address, int value)
        {
            try
            {
                WriteProcessMemory(hProcess, address, new int[] { value }, 4, IntPtr.Zero);
                return 0;
            }
            catch { return -1; }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcesses();
            GameProgressAvailable = false;
            foreach (Process p in ps)
            {
                if (p.ProcessName == GameProcessName)
                {
                    GameProgressAvailable = true;
                    gameLoop.Enabled = true;
                    label1.Text = "Game status: Running";
                    label1.ForeColor = Color.Green;
                    GameProcessHandle = p.Handle;
                    GameBaseAddress = p.MainModule.BaseAddress;
                }
            }
            if (!GameProgressAvailable) 
            {
                gameLoop.Enabled = false;
                GameBaseAddress = IntPtr.Zero;
                label1.Text = "Game status: Offline";
                label1.ForeColor = Color.Red;
            }
        }

        int[] prevValue = new int[] { 0, 0, 0 };
        Boolean[] valueLock = new Boolean[] { false, false, false };
        private void gameLoop_Tick(object sender, EventArgs e)
         {
            // 若存在锁定，则写回对应的资源
            foreach (Control c in Controls)
            {
                if (c is CheckBox && c.Tag != null)
                {
                    if (((CheckBox)c).Checked)
                    {
                        int index = Convert.ToInt32(c.Tag);
                        valueLock[index] = true;
                        resourceModify((ResourceTypes)index, prevValue[index]);
                    }
                    else
                    {
                        int index = Convert.ToInt32(c.Tag);
                        valueLock[index] = false;
                    }
                }
            }
            // 更新文本框资源的值
            if (GameBaseAddress != IntPtr.Zero) {
                int valueTurn = resourceGet(ResourceTypes.Turn);
                if (!valueLock[0] && prevValue[0] != valueTurn)
                {
                    prevValue[0] = valueTurn;
                    textBoxTurn.Text = (valueTurn - 35).ToString();
                }
                int valueCredits = resourceGet(ResourceTypes.Credits);
                if (!valueLock[1] && prevValue[1] != valueCredits)
                {
                    prevValue[1] = valueCredits;
                    textBoxCredits.Text = valueCredits.ToString();
                }
                int valueFuel = resourceGet(ResourceTypes.Fuel);
                if (!valueLock[2] && prevValue[2] != valueFuel)
                {
                    prevValue[2] = valueFuel;
                    textBoxFuel.Text = valueFuel.ToString();
                }
            }
        }

        private int resourceGet(ResourceTypes res)
        {
            IntPtr resPtr = assembleResourceAddress(res);
            int value = ReadMemoryValue(GameProcessHandle, resPtr);
            return value;
        }

        private void resourceModify(ResourceTypes res, int value)
        {
            IntPtr resPtr = assembleResourceAddress(res);
            WriteMemoryValue(GameProcessHandle, resPtr, value);

        }

        private IntPtr assembleResourceAddress(ResourceTypes res)
        {
            // 每种资源在游戏基址上的偏移量序列
            int[,] offsetMap = { { 0x28, 0x14, 0x14, 0x08, 0x04, 0x14, 0xa4, 0x4c, 0xff, 0xff, 0xff },
                                 { 0x28, 0x14, 0x14, 0x08, 0x04, 0x14, 0xa4, 0x24, 0xff, 0xff, 0xff },
                                 { 0x28, 0x14, 0x14, 0x08, 0x08, 0x14, 0x124, 0x20, 0x6c, 0x50, 0xff} };
            
            int tempPtr = ReadMemoryValue(GameProcessHandle, GameBaseAddress + 0x00852D1C);
            int i = 0;
            while (offsetMap[(int)res, i + 1] != 0xff) {
                tempPtr = ReadMemoryValue(GameProcessHandle, (IntPtr)tempPtr + offsetMap[(int)res, i]);
                i++;
            }
            tempPtr += offsetMap[(int)res, i];
            return (IntPtr)tempPtr;
        }

        private void buttonTurn_Click(object sender, EventArgs e)
        {
            int newValue = int.Parse(textBoxTurn.Text);
            resourceModify(ResourceTypes.Turn, newValue + 35);
        }

        private void buttonCredits_Click(object sender, EventArgs e)
        {
            int newValue = int.Parse(textBoxCredits.Text);
            resourceModify(ResourceTypes.Credits, newValue);
        }
        private void buttonFuel_Click(object sender, EventArgs e)
        {
            int newValue = int.Parse(textBoxFuel.Text);
            resourceModify(ResourceTypes.Fuel, newValue);

        }
        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StarTradersFrontiers";
            if (Directory.Exists(path)) {
                Process.Start(path);
            }
        }
    }
}
