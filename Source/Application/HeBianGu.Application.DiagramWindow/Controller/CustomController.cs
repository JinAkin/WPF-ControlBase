﻿using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.DiagramWindow
{
    [Controller("Custom")]
    internal class CustomController : Controller<CustomViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }
    }
}