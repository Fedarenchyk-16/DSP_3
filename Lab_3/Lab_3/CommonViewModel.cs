using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Series;

namespace Lab_3
{
    public class CommonViewModel
    {
        public PlotController Controller { get; }
        
        public CommonViewModel()
        {
            Controller = new PlotController();
            Controller.UnbindMouseWheel();
        }
    }
}
