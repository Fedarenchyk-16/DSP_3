using System.Linq;
using OxyPlot;
using static Lab_3.PlotModelCreator;

namespace Lab_3
{
    public class PolyHarmonicViewModel
    {
        public PlotModel OriginalSignalModel { get; }
        public PlotModel AmplitudeSpectrumModel { get; }
        public PlotModel PhaseSpectrumModel { get; }
        public PlotModel RestoredSignalModel { get; }
        public PlotModel RestoredSignalWithoutPhaseModel { get; }

        public PolyHarmonicViewModel()
        {
            double[] signalValues = Signal.PolyharmonicSignal(30, Source.N, Source.Amplitudes, Source.Phases).ToArray();
            SpectrumValue[] spectrum = Signal.FourierTransformation(signalValues);
            double[] restoredSignalWithPhase = Signal.RestoreSignal(spectrum, spectrum[0].Amplitude / 2, considerPhase: true);
            double[] restoredSignalWithoutPhase = Signal.RestoreSignal(spectrum, spectrum[0].Amplitude / 2, considerPhase: false);
            OriginalSignalModel = CreatePlotModel(signalValues, "Исходный сигнал");
            AmplitudeSpectrumModel = CreatePlotModel(spectrum, AmplitudeSelector, "Амплитудный спект");
            PhaseSpectrumModel = CreatePlotModel(spectrum, PhaseSelector, "Фазовый спектр");
            RestoredSignalModel = CreatePlotModel(restoredSignalWithPhase, "Восстановленный сигнал");
            RestoredSignalWithoutPhaseModel = CreatePlotModel(restoredSignalWithoutPhase, "Восстановленный сигнал без учёта фазы");
        }
    }
}
