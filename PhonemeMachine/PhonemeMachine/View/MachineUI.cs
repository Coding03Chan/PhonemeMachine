using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhonemeMachine.View
{
    public partial class MachineUI : Form
    {

        private Dictionary<int, string> keyboardDic;
        private string schemeName;
        public MachineUI(string schemeName, Dictionary<int, string> keyboardDic)
        {
            //构建窗口
            InitializeComponent();

            //重写窗口样式
            AdjustFormPattern();

            //接收方案名、字典实例
            this.schemeName= schemeName;
            this.keyboardDic = keyboardDic;

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
            //AdjustUIPattern();
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
                    button.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey.png"));

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
                    button.BackgroundImage= Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey_uneffective.png"));
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
            //按下时改贴图
            button.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey_pressed.png"));
            
            //Debug:打印按键信息
            Console.WriteLine($"MachineUI.HandleButtonPress() - 按下按键: {button.Text}-{button.Tag}");

            //异步播放音频
            PlayAudio(audioFileName);

            //使用 Timer 恢复原贴图，200ms 延迟恢复
            Timer timer = new Timer { Interval = 200 }; 
            timer.Tick += (s, e) =>
            {
                button.BackColor = SystemColors.Control; 
                button.BackgroundImage= Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KeyboadKey.png"));
                timer.Stop();
            };
            timer.Start();
        }

        /// <summary>
        /// 功能：异步播放音频
        /// </summary>
        private void PlayAudio(string audioFileName)
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AudioFile", this.schemeName, audioFileName);
                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.Play(); //异步播放
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
    }
}
