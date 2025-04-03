using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonemeMachine.Model.Enum
{
    public enum IndexMessage
    {
        //读取下拉框选项失败
        GetSelectItemFail=-95,

        //生成键值配置路径失败
        GetTxtPathFailed = -96,

        //获取键值字典失败
        GetKeyboardDicFailed = -97,

        //构建音素机UI失败
        CreateMachineUIFailed = -98,

        //未知错误
        UnkonwnFail =-99,
    }
}
