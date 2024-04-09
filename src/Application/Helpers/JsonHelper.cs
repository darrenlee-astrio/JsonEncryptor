using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers;

public static class JsonHelper
{
    public static bool IsJson(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        return true;
    }
}
