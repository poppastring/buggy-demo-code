using System.Threading.Tasks;
using System;

namespace BuggyDemoWeb.Services
{
    public class AudioSpatialSignalProcess
    {
        public Task<AudioRatioControl> RetrieveRatioControl()
        {
            return Task.FromResult(new AudioRatioControl(0.0, 0.0));
        }

        public Task<QuadraticRoots> RetrieveQuadraticRoots(double a, double b, double c)
        {
            double discriminant = (Math.Pow(b, 2)) + (-4 * a * c);

            double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return Task.FromResult(new QuadraticRoots(root1, root2));
        }
    }

    public record AudioRatioControl(double dBAboveThreshold, double CompressorOutput);

    public record QuadraticRoots(double FirstRoot, double SecondRoot);
}
