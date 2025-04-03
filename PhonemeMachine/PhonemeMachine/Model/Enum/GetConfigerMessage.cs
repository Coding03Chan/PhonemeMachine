using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonemeMachine.Model.Enum
{
    public enum GetConfigerMessage
    {
        //键盘配置文件不存在
        KeyboardConfigFileNotFound=-1,

        //键盘码不存在
        KeyboardCodeInvalid=-2,

        //音频文件夹不存在
        AudioFilePathNotFound = -3,
    }
}
