using System;
using System.Windows.Media;
using OxyPlot;
using OxyPlot.Series;

namespace Lab_3
{
    public static class PlotModelCreator
    {
        public static PlotModel CreatePlotModel(double[] values, string title)
        {
            LineSeries series = new()
            {
                Color = OxyColor.FromRgb(255, 0, 0)
            };
            for (int i = 0; i < values.Length; i++)
            {
                DataPoint point = new(i, values[i]);
                series.Points.Add(point);
            }
            PlotModel plotModel = new()
            {
                Series = { series },
                Title = title
            };
            return plotModel;
        }
        
        public static PlotModel CreatePlotModel(SpectrumValue[] spectrum, Func<SpectrumValue, double> valueSelector, string title)
        {
            LinearBarSeries series = new() {
                FillColor = OxyColor.FromRgb(255, 0, 0)
            };
            for (int i = 0; i < spectrum.Length; i++)
            {
                DataPoint point = new(i, valueSelector(spectrum[i]));
                series.Points.Add(point);
            }
            PlotModel plotModel = new()
            {
                Series = { series },
                Title = title
            };
            return plotModel;
        }

        public static double AmplitudeSelector(SpectrumValue spectrumValue) => spectrumValue.Amplitude;
        public static double PhaseSelector(SpectrumValue spectrumValue) => spectrumValue.Phase;
    }
}
