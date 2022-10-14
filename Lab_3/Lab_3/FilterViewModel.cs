using System.Linq;
using OxyPlot;
using static Lab_3.PlotModelCreator;

namespace Lab_3
{
    public class FilterViewModel
    {
        public PlotModel OriginalSignalModel { get; }
        public PlotModel LowFrequencyFilteredModel { get; }
        public PlotModel LowFrequencyAmplitudeSpectrumModel { get; }
        public PlotModel LowFrequencyPhaseSpectrumModel { get; }
        public PlotModel HighFrequencyFilteredModel { get; }
        public PlotModel HighFrequencyAmplitudeSpectrumModel { get; }
        public PlotModel HighFrequencyPhaseSpectrumModel { get; }
        public PlotModel BandpassFrequencyFilteredModel { get; }
        public PlotModel BandpassFrequencyAmplitudeSpectrumModel { get; }
        public PlotModel BandpassFrequencyPhaseSpectrumModel { get; }

        public FilterViewModel()
        {
            double[] signalValues = Signal.PolyharmonicSignal(30, Source.N, Source.Amplitudes, Source.Phases).ToArray();
            SpectrumValue[] spectrum = Signal.FourierTransformation(signalValues);
            SpectrumValue[] lowFrequencyFiltered = Signal.Filter(spectrum, i => i < 15);
            SpectrumValue[] highFrequencyFiltered = Signal.Filter(spectrum, i => i > 15);
            SpectrumValue[] bandpassFrequencyFiltered = Signal.Filter(spectrum, i => i < 10 || i > 20);
            double[] restoredLowFrequencyFiltered = Signal.RestoreSignal(lowFrequencyFiltered);
            double[] restoredHighFrequencyFiltered = Signal.RestoreSignal(highFrequencyFiltered);
            double[] restoredBandpassFrequencyFiltered = Signal.RestoreSignal(bandpassFrequencyFiltered);
            
            OriginalSignalModel = CreatePlotModel(signalValues, "Исходный сигнал");
            
            LowFrequencyFilteredModel = CreatePlotModel(restoredLowFrequencyFiltered, "НЧ-фильтр");
            LowFrequencyAmplitudeSpectrumModel = CreatePlotModel(lowFrequencyFiltered, AmplitudeSelector, "Амплитудный спектр (НЧ-фильтр)");
            LowFrequencyPhaseSpectrumModel = CreatePlotModel(lowFrequencyFiltered, PhaseSelector, "Фазовый спект (НЧ-фильтр)");
            
            HighFrequencyFilteredModel = CreatePlotModel(restoredHighFrequencyFiltered, "ВЧ-фильтр");
            HighFrequencyAmplitudeSpectrumModel = CreatePlotModel(highFrequencyFiltered, AmplitudeSelector, "Амлитудный спект (ВЧ-фильтр)");
            HighFrequencyPhaseSpectrumModel = CreatePlotModel(highFrequencyFiltered, PhaseSelector, "Фазовый спект (ВЧ-фильтр)");
            
            BandpassFrequencyFilteredModel = CreatePlotModel(restoredBandpassFrequencyFiltered, "Полосовой фильтр");
            BandpassFrequencyAmplitudeSpectrumModel = CreatePlotModel(bandpassFrequencyFiltered, AmplitudeSelector, "Амплитудный спектр (Полосовой фильтр)");
            BandpassFrequencyPhaseSpectrumModel = CreatePlotModel(bandpassFrequencyFiltered, PhaseSelector, "Фазовый спект (Полосовой фильтр))");
        }
    }
}
