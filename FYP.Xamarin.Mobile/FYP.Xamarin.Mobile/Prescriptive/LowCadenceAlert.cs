﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Prescriptive
{
    public class LowCadenceAlert
    {
        private string alertMessage = "- Reduce training volume \n- Reduce resistance \n- Reduce gear ratio";

        public string AlertMessage { get => alertMessage; set => alertMessage = value; }
    }
}
