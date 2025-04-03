using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonemeMachine.Tool.Interface
{
    /// <summary>
    /// 接口：异步获取配置信息接口
    /// </summary>
    public interface IGetConfiger
    {
        //获取键值配置文件夹路径
        string GetKeyboardConfigPath(string schemeName);

        //获取音频文件夹路径
        string GetAudioPath(string schemeName);

        //获取键值字典
        Dictionary<int,string> GetKeyboardDic(string keyboardConfigPath);
    }
}
