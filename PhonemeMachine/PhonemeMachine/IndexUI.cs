using PhonemeMachine.Model.Enum;
using PhonemeMachine.Tool.implement;
using PhonemeMachine.Tool.Interface;
using PhonemeMachine.View;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhonemeMachine
{
    public partial class IndexUI : Form
    {

        private readonly IGetConfiger getConfiger;

        /// <summary>
        /// 功能：构造/初始化
        /// </summary>
        public IndexUI()
        {
            //构建窗口实例
            InitializeComponent();

            //构建配置工具类实例
            this.getConfiger = new GetConfiger(); 

            //重写窗口样式
            AdjustFormPattern();

            //加载语言下拉框选项
            foreach (var language in Enum.GetValues(typeof(LanguageSelectItem)))
            {
                ComboBox_LanguageSelect.Items.Add(language.ToString());
            }
            ComboBox_LanguageSelect.SelectedIndex = 0;
        }

        /// <summary>
        /// 功能：载入动作
        /// </summary>
        private void IndexUI_Load(object sender, EventArgs e)
        {
            //重写UI样式
            AdjustUIPattern();
        }

        /// <summary>
        /// 功能：开始按钮
        /// </summary>
        private void Button_Start_Click(object sender, EventArgs e)
        {
            //获取用户选择的语言方案
            string schemeName = ComboBox_LanguageSelect.SelectedItem.ToString();

            //获取键值文件路径
            string keyboardConfigerPath = getConfiger.GetKeyboardConfigPath(schemeName);

            //获取键值字典
            Dictionary<int, string> keyboardDic = getConfiger.GetKeyboardDic(keyboardConfigerPath);

            //构建音素机UI
            MachineUI machineUI = new MachineUI(schemeName, keyboardDic);
            machineUI.Show();
        }

        /// <summary>
        /// 样式：动态调整窗口样式
        /// </summary>
        private void AdjustFormPattern()
        {
            //获取屏幕分辨率
            Screen screen = Screen.PrimaryScreen;
            Rectangle screenBounds = screen.Bounds;
            int screenWidth = screenBounds.Width;
            int screenHeight = screenBounds.Height;

            //设置窗体大小为屏幕的80%
            this.Size = new Size((int)(screenWidth * 0.8), (int)(screenHeight * 0.8));

            //居中显示
            this.StartPosition = FormStartPosition.CenterScreen;

            //背景颜色
           this.BackColor = Color.LightYellow;
        }

        /// <summary>
        /// 样式：动态调整内部UI样式
        /// </summary>
        private void AdjustUIPattern() {

            //【1】清空控件的 Anchor 属性
            //PictureBox_IndexBanner.Anchor = AnchorStyles.None;
            ComboBox_LanguageSelect.Anchor = AnchorStyles.None;
            Button_Start.Anchor = AnchorStyles.None;

            //【2】Banner
            //图片拉伸填充满图片区
            PictureBox_IndexBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            //PictureBox_IndexBanner.Font = new Font("微软雅黑", (float)(this.ClientSize.Height * 0.05)); // 字体大小为窗体高度的 5%
            //PictureBox_IndexBanner.AutoSize = true;
            // 水平居中，位置为窗体高度的 10%
            //PictureBox_IndexBanner.Location = new Point(
            //    (this.ClientSize.Width - PictureBox_IndexBanner.Width) / 2,
            //    (int)(this.ClientSize.Height * 0.1)
            //);

            //【3】语言选择下拉框
            ComboBox_LanguageSelect.Font = new Font("微软雅黑", (float)(this.ClientSize.Height * 0.03)); // 字体大小为窗体高度的 5%
            ComboBox_LanguageSelect.AutoSize = true;
            //宽度为窗体宽度的 30%，高度为窗体高度的 5%
            ComboBox_LanguageSelect.Size = new Size(
                (int)(this.ClientSize.Width * 0.4), 
                (int)(this.ClientSize.Height * 0.05)
            );
            //水平居中，垂直位置为窗体高度的 55%
            ComboBox_LanguageSelect.Location = new Point(
                (this.ClientSize.Width - ComboBox_LanguageSelect.Width) / 2, 
                (int)(this.ClientSize.Height * 0.55)
            );

            //【4】开始按钮
            Button_Start.Font = new Font("微软雅黑", (float)(this.ClientSize.Height * 0.03)); // 字体大小为窗体高度的 4%
            //按钮宽度为窗体宽度的 20%，高度为窗体高度的 10%
            Button_Start.Size = new Size(
                (int)(this.ClientSize.Width * 0.25),
                (int)(this.ClientSize.Height * 0.15)
            );
            //水平居中，位置为窗体高度的 70%
            Button_Start.Location = new Point(
                (this.ClientSize.Width - Button_Start.Width) / 2, 
                (int)(this.ClientSize.Height * 0.7)
            );
        }
    }
}
