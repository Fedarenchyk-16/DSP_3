using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Lab_3
{
    public readonly struct SpectrumValue
    {
        public double Amplitude { get; }
        public double Phase { get; }
        
        public SpectrumValue(double amplitude, double phase)
        {
            Amplitude = amplitude;
            Phase = phase;
        }
        
        public static implicit operator SpectrumValue(Complex complex) =>
            new (complex.Real, complex.Imaginary);

        public void Deconstruct(out double amplitude, out double phase)
        {
            amplitude = Amplitude;
            phase = Phase;
        }
    }
    
    public static class Signal
    {
        public static IEnumerable<double> Values(int n)
        {
            for (int i = 0; i < n; i++)
                yield return 10 * Math.Cos((2 * Math.PI * i / n) - Math.PI/2);
        }

        public static IEnumerable<double> PolyharmonicSignal(int harmonicCount, int n, double[] amplitudes,
            double[] phases)
        {
            for (var i = 0; i < n; i++)
            {
                double signalValue = 0;
                var amplitudeIndex = 0;
                var phaseIndex = 0;
                for (int j = 0; j < harmonicCount; j++)
                {
                    double amplitude = amplitudes[amplitudeIndex++];
                    double phase = phases[phaseIndex++];
                    signalValue += amplitude * Math.Cos(2 * Math.PI * j * i / n - phase);
                    if (amplitudeIndex == amplitudes.Length)
                        amplitudeIndex = 0;
                    if (phaseIndex == phases.Length)
                        phaseIndex = 0;
                }
                yield return signalValue;
            }
        }

        public static SpectrumValue[] FourierTransformation(double[] signalValues)
        {
            SpectrumValue[] spectrum = new SpectrumValue[signalValues.Length];
            for (int j = 0; j < signalValues.Length; j++)
            {
                double cosineAmplitude = CosineAmplitude(signalValues, j, signalValues.Length);
                double sineAmplitude = SineAmplitude(signalValues, j, signalValues.Length);
                double amplitude = Math.Sqrt(cosineAmplitude * cosineAmplitude + sineAmplitude * sineAmplitude);
                double phase = Math.Atan2(sineAmplitude, cosineAmplitude);
                spectrum[j] = new SpectrumValue(amplitude, phase);
            }
            return spectrum;
        }
        
        public static double[] RestoreSignal(SpectrumValue[] spectrum, double startValue = 0, bool considerPhase = true)
        {
            var signalValues = new double[spectrum.Length];
            int n = spectrum.Length / 2 - 1;
            for (int i = 0; i < spectrum.Length; i++)
            {
                double signalValue = startValue;
                for (var j = 0; j < n; j++)
                {
                    signalValue += spectrum[j].Amplitude *
                                   Math.Cos(2 * Math.PI * i * j / spectrum.Length - (considerPhase ? spectrum[j].Phase : 0));
                }
                signalValues[i] = signalValue;
            }
            return signalValues;
        }

        public static SpectrumValue[] Filter(SpectrumValue[] spectrum, Func<double, bool> filter)
        {
            SpectrumValue[] filteredSpectrum = new SpectrumValue[spectrum.Length];
            int halfLength = spectrum.Length / 2;
            for (int i = 0; i < spectrum.Length; i++)
            {
                int index = i > halfLength ? spectrum.Length - i : i;
                if (!filter(index))
                    filteredSpectrum[i] = spectrum[i];
                else
                    filteredSpectrum[i] = new SpectrumValue(0, 0);
            }
            return filteredSpectrum;
        }

        private static double CosineAmplitude(double[] signalValues, int j, int n) =>
            2.0 / n * signalValues.Select((value, i) => value * Math.Cos(2 * Math.PI * j * i / n)).Sum();
        
        private static double SineAmplitude(double[] signalValues, int j, int n) =>
            2.0 / n * signalValues.Select((value, i) => value * Math.Sin(2 * Math.PI * j * i / n)).Sum();
    }
}
