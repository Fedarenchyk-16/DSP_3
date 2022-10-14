using System.Linq;
using OxyPlot;
using static Lab_3.PlotModelCreator;

namespace Lab_3
{
    public class FFTViewModel
    {
        public PlotModel OriginalSignalModel { get; }
        public PlotModel RestoredSignalModel { get; }
        
        public FFTViewModel()
        {
            double[] signalValues = Signal.PolyharmonicSignal(30, Source.N, Source.Amplitudes, Source.Phases).ToArray();
            SpectrumValue[] spectrum = FFT.FFTSpectrum(signalValues);
            double[] restoredSignal = Signal.RestoreSignal(spectrum);
            OriginalSignalModel = CreatePlotModel(signalValues, "Исходный сигнал");
            RestoredSignalModel = CreatePlotModel(restoredSignal, "Восстановленный сигнал");
        }
    }
}
