using ProbCSharp;
using System;
using System.Linq;
using static ProbCSharp.ProbBase;

namespace BC
{
    class Program
    {
        static Func<bool, Dist<bool>> EuVetosExtensionRequest = isBorisInPower =>
            from franceVetos in isBorisInPower ? Bernoulli(0.8) : Bernoulli(0.20)
            from germanyVetos in Bernoulli(0.01)
            from italyVetos in Bernoulli(0.1)
            from otherVetos in Bernoulli(0.01)
            select franceVetos || germanyVetos || italyVetos || otherVetos;

        static void Main(string[] args)
        {
            var willWeExtend =
                from johnsonInPower in Bernoulli(0.7)
                from askForExtension in johnsonInPower ? Bernoulli(0.2) : Bernoulli(0.95)
                from euExercisesVeto in EuVetosExtensionRequest(johnsonInPower)
                select askForExtension ? !euExercisesVeto : false;

            var samples = willWeExtend.SampleN(10000);

            var extending = samples.Count(x => x) / (double)samples.Count();

            Console.WriteLine($"Probably of extending beyond deadline: {extending.ToString("##0%")}");
            Console.ReadLine();
        }
    }
}
