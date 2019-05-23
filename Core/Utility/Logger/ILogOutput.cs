using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sanita.Utility.Logger
{
    public interface ILogOutput
    {
        void Write(LogLevel.LogLevelInfo logLevel, String tag, String message);
    }
}
