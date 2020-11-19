using System;
using System.Collections.Generic;
using System.Text;

namespace FacebookAPI.delegatefolder
{
    public delegate void ProcessLoading(Object obj, int process = 0);
    public delegate void ErrorLoading(Object obj, int process = 0);
    public delegate void SuccessLoading(Object obj, int process = 0);
}
