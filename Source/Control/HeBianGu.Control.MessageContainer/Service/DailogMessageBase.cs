﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Control.MessageContainer
{
    public abstract class DailogMessageBase : MessageBase
    {
        public Func<bool> Sumit { get; set; }
    }
}
