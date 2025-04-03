using PhonemeMachine.Model.Enum;
using PhonemeMachine.Tool.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhonemeMachine.Tool.implement
{
    /// <summary>
    ///  异步获取配置信息
    /// </summary>
    public class GetConfiger : IGetConfiger
    {
        /// <summary>
        /// 获取音频文件夹路径
        /// </summary>
        public string GetAudioPath(string schemeName)
        {
            //生成路径
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AudioFile", schemeName);

            //Debug:打印键值配置文件路径内容
            Console.WriteLine($"GetConfiger.GetKeyboardConfigPath() - 音频文件夹路径：{path}");

            //检查音频文件夹是否存在
            if (!File.Exists(path))
            {
                return GetConfigerMessage.AudioFilePathNotFound.ToString();
                //throw new FileNotFoundException(GetConfigerMessage.AudioFilePathNotFound.ToString());
                //TODO:优化返回内容，尽量不要用字符串去一个方法的返回结果…
            }

            return path;
        }

        /// <summary>
        /// 获取键值配置文件路径
        /// </summary>
        public string GetKeyboardConfigPath(string schemeName)
        {
            //生成路径
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KeyboardConfigerFile", $"{ schemeName}.txt");
            
            //Debug:打印键值配置文件路径内容
            Console.WriteLine($"GetConfiger.GetKeyboardConfigPath() - 键值配置文件路径{path}");

            //检查键值配置文件是否存在
            if (!File.Exists(path))
            {
                return GetConfigerMessage.KeyboardConfigFileNotFound.ToString();
                //throw new FileNotFoundException(GetConfigerMessage.KeyboardConfigFileNotFound..ToString());
            }

            return path;
        }

        /// <summary>
        /// 获取键值配置
        /// </summary>
        public Dictionary<int, string> GetKeyboardDic(string keyboardConfigPath)
        {
            Dictionary<int,string> keyboardDic = new Dictionary<int,string>();


            //逐行读取配置文件
            foreach (var line in File.ReadAllLines(keyboardConfigPath))
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    //将键盘码转换为整数，写入字典
                    if (int.TryParse(parts[0].Trim(), out int keyCode)) 
                    {
                        keyboardDic.Add(keyCode, parts[1].Trim());
                    }
                    else
                    {
                        throw new FormatException(GetConfigerMessage.KeyboardCodeInvalid.ToString());
                    }
                }
            }
            return keyboardDic;
        }
    }
}
