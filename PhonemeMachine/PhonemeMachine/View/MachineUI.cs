using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhonemeMachine.View
{
    public partial class MachineUI : Form
    {

        private Dictionary<int, string> keyboardDic;
        private string schemeName;
        private string rootPath;
        private IndexUI _indexUI; //保存对主页实例 IndexUI 的引用
        public MachineUI(string schemeName, Dictionary<int, string> keyboardDic, IndexUI nowIndexUI)
        {
            //构建窗口
            InitializeComponent();

            //重写窗口样式
            AdjustFormPattern();

            //接收方案名、字典实例、项目根目录；记录被隐藏的主页实例
            this.schemeName= schemeName;
            this.keyboardDic = keyboardDic;
            this.rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            this._indexUI = nowIndexUI;

            //Debug:打印键值字段内容
            Console.WriteLine("MachineUI - 构造时获取键值字典 ");
            foreach (var k in this.keyboardDic)
            {
                Console.WriteLine($"Key = {k.Key}, Value = {k.Value}");
            }

            //配置键盘实例
            ConfigureKeyboardButtons();
        }

        /// <summary>
        /// 功能：载入动作
        /// </summary>
        private void MachineUI_Load(object sender, EventArgs e)
        {
            //重写UI样式
            AdjustUIPattern();
        }

        /// <summary>
        /// 功能：根据字典配置键盘
        /// </summary>
        private void ConfigureKeyboardButtons()
        {
            foreach (var key in this.keyboardDic)
            {
                int keyCode = key.Key;
                string audioFileName = key.Value;

                //根据 key.Key 和 'Button_code' 逐个寻找Button实例，并配置音频文件名
                string buttonPrefix = $"Button_{keyCode}_";
                Button button = Keyboard_Box.Controls
                    .OfType<Button>() 
                    .FirstOrDefault(b => b.Name.StartsWith(buttonPrefix));

                if (button != null)
                {
                    //绑定鼠标、键盘点击事件
                    button.Click += (sender, e) => HandleButtonPress(button, audioFileName);

                    //更换图片
                    button.BackgroundImage = Image.FromFile(Path.Combine(rootPath, "Assets", "KeyboadKey.png"));

                    //设置 Tag 属性存储音频文件名
                    button.Tag = audioFileName;
                }
                else
                {
                    //Debug:打印未找到按钮实例的键值
                    Console.WriteLine($"MachineUI.ConfigureKeyboardButtons() - 本方案未配置按钮: {buttonPrefix}");
                }
            }

            //更换未配置按钮的图片
            List<Button> buttonItems = Keyboard_Box.Controls.OfType<Button>().ToList();
            foreach (var button in buttonItems) 
            {
                if (button != null && button.Tag == null) 
                { 
                    button.BackgroundImage= Image.FromFile(Path.Combine(rootPath, "Assets", "KeyboadKey_uneffective.png"));
                }
            }
            //Debug:打印键Q的Text，验证配置成功
            Console.WriteLine($"MachineUI.ConfigureKeyboardButtons() - {Keyboard_Box.Controls.Find("Button_81_Q", true).FirstOrDefault().Text} - {Keyboard_Box.Controls.Find("Button_81_Q", true).FirstOrDefault().Tag}");
        }

        /// <summary>
        /// 功能：按钮键盘按压行为
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int keyCode = (int)keyData & 0xFFFF;

            if (this.keyboardDic.ContainsKey(keyCode))
            {
                string audioFileName = this.keyboardDic[keyCode];
                string buttonPrefix = $"Button_{keyCode}_";

                Button button = Keyboard_Box.Controls
                    .OfType<Button>()
                    .FirstOrDefault(b => b.Name.StartsWith(buttonPrefix));

                if (button != null)
                {
                    HandleButtonPress(button, audioFileName);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 功能：按钮鼠标点击行为
        /// </summary>
        private void HandleButtonPress(Button button, string audioFileName)
        {
            // 按下时改贴图
            button.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey_pressed.png"));

            // Debug: 打印按键信息
            Console.WriteLine($"MachineUI.HandleButtonPress() - 按下按键: {button.Text}-{button.Tag}");

            // 异步播放音频
            PlayAudio(audioFileName);

            // 使用 Timer 恢复原贴图，200ms 延迟恢复
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 200 };
            timer.Tick += (s, e) =>
            {
                button.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey.png"));
                timer.Stop();
            };
            timer.Start();
        }
        //private void HandleButtonPress(Button button, string audioFileName)
        //{
        //    //按下时改贴图
        //    button.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey_pressed.png"));

        //    //Debug:打印按键信息
        //    Console.WriteLine($"MachineUI.HandleButtonPress() - 按下按键: {button.Text}-{button.Tag}");

        //    //异步播放音频
        //    PlayAudio(audioFileName);

        //    //使用 Timer 恢复原贴图，200ms 延迟恢复
        //    Timer timer = new Timer { Interval = 200 }; 
        //    timer.Tick += (s, e) =>
        //    {
        //        button.BackColor = SystemColors.Control; 
        //        button.BackgroundImage= Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey.png"));
        //        timer.Stop();
        //    };
        //    timer.Start();
        //}

        /// <summary>
        /// 功能：异步播放音频
        /// </summary>
        private void PlayAudio(string audioFileName)
        {
            Task.Run(() =>
            {
                try
                {
                    string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AudioFile", this.schemeName, audioFileName);
                    if (File.Exists(audioPath))
                    {
                        // 使用 WaveOutEvent 播放音频
                        using (var audioFile = new AudioFileReader(audioPath))
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(audioFile);
                            outputDevice.Play();

                            // 等待音频播放完成
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(100); // 每 100ms 检查一次播放状态
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"音频文件未找到: {audioPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"播放音频时出错: {ex.Message}");
                }
            });
        }
        //private void PlayAudio(string audioFileName)
        //{
        //    try
        //    {
        //        string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AudioFile", this.schemeName, audioFileName);
        //        if (File.Exists(audioPath))
        //        {
        //            SoundPlayer player = new SoundPlayer(audioPath);
        //            player.Play(); //异步播放
        //        }
        //        else
        //        {
        //            Console.WriteLine($"音频文件未找到: {audioPath}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"播放音频时出错: {ex.Message}");
        //    }
        //}


        /// <summary>
        /// 功能：点击返回主页按钮
        /// </summary>
        private void Button_Back_Click(object sender, EventArgs e)
        {
            
            if (_indexUI != null)
            {
                //恢复显示 IndexUI
                _indexUI.Show(); 
            }
            else 
            {
                //或直接new一个 IndexUI 窗口
                IndexUI index = new IndexUI();
                index.Show();
            }

            this.Close();
        }
        /// <summary>
        /// 样式：动态调整窗口样式
        /// </summary>
        private void AdjustFormPattern() 
        {
            //固定尺寸H*W=900*600
            this.Size = new Size(900, 600);

            //居中显示
            this.StartPosition = FormStartPosition.CenterScreen;

            //背景颜色
            this.BackColor = Color.LightYellow;
        }


        /// <summary>
        /// 样式：动态调整UI样式
        /// </summary>
        private void AdjustUIPattern()
        {
            //【1】清空控件的 Anchor 属性
            //Button_Back.Anchor = AnchorStyles.None;


            //【2】返回主页按钮
            Button_Back.Font = new Font("微软雅黑", (float)(this.ClientSize.Height * 0.03)); // 字体大小为窗体高度的 4%
            //按钮宽度为窗体宽度的 20%，高度为窗体高度的 10%
            //Button_Back.Size = new Size(
            //    (int)(this.ClientSize.Width * 0.25),
            //    (int)(this.ClientSize.Height * 0.15)
            //);
            ////水平居中，位置为窗体高度的 70%
            //Button_Back.Location = new Point(
            //    (this.ClientSize.Width - Button_Back.Width) / 2,
            //    (int)(this.ClientSize.Height * 0.7)
            //);
        }


    }
}
