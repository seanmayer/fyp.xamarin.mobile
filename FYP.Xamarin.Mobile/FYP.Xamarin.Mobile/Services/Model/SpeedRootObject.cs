﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class SpeedRootObject
    {
        public SpeedRootObject(string speedstream)
        {
            this.speedstream = speedstream;
        }
        public string speedstream { get; set; }
    }
}
