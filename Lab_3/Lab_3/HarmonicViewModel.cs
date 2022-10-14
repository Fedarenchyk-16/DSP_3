using System.Linq;
using OxyPlot;
using static Lab_3.PlotModelCreator;

namespace Lab_3
{
    public class HarmonicViewModel
    {
        public PlotModel OriginalSignalModel { get; }
        public PlotModel AmplitudeSpectrumModel { get; }
        public PlotModel PhaseSpectrumModel { get; }
        public PlotModel RestoredSignalModel { get; }

        public HarmonicViewModel()
        {
            int polyHarmonic = 1;
            double[] signalValues = Signal.Values(Source.N).ToArray();
            SpectrumValue[] spectrum = Signal.FourierTransformation(signalValues);
            double[] restoredSignal = Signal.RestoreSignal(spectrum);
            OriginalSignalModel = CreatePlotModel(signalValues, "Исходный сигнал");
            RestoredSignalModel = CreatePlotModel(restoredSignal, "Восстановленный сигнал");
            AmplitudeSpectrumModel = CreatePlotModel(spectrum, AmplitudeSelector, "Амплитудный спектр");
            PhaseSpectrumModel = CreatePlotModel(spectrum, PhaseSelector, "Фазовый спектр");
        }
    }
}
