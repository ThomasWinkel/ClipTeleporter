﻿using System;
using System.ComponentModel;

namespace ClipTeleporter
{
    internal class Clip
    {
        public DateTime Date { get; set; }
        public string Direction { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        [Browsable(false)]
        public string DataObject { get; set; }
    }
}