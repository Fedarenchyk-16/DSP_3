using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Lab_3
{
    public class FFT
    {
        private static Complex w(int k, int N)
        {
            if (k % N == 0) return 1;
            double arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }
        
        public static Complex[] Transform(Complex[] x)
        {
            Complex[] X;
            int N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }
                Complex[] X_even = Transform(x_even);
                Complex[] X_odd = Transform(x_odd);
                X = new Complex[N];
                for (int i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + w(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - w(i, N) * X_odd[i];
                }
            }
            return X;
        }

        private static SpectrumValue[] SpectrumFromFFT(Complex[] fft)
        {
            SpectrumValue[] spectrumValues = new SpectrumValue[fft.Length];
            for (int i = 0; i < fft.Length; i++)
            {
                double amplitude = Complex.Abs(fft[i] * 2 / fft.Length);
                double phase = 0;
                if (Math.Abs(amplitude) > 0.0001)
                    phase = -Math.Atan2(fft[i].Imaginary, fft[i].Real);
                spectrumValues[i] = new SpectrumValue(amplitude, phase);
            }
            return spectrumValues;
        }

        public static SpectrumValue[] FFTSpectrum(double[] signalValues)
        {
            Complex[] fftResult = Transform(signalValues.Select(value => (Complex)value).ToArray());
            return SpectrumFromFFT(fftResult);
        }
    }
}
