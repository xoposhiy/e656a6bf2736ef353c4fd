﻿using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace SomeSecretProject.IO
{
    [TestFixture]
    public class IOTest
    {
        [TestCase]
        public void TestLoadProblem()
        {
            var problem = HttpHelper.GetProblem(0);
            Console.WriteLine(problem.width +" x "+problem.height);
        }

        [TestCase]
        public void TestSendSolution()
        {
            var solution = new Output()
            {
                problemId = 0,
                seed = 0,
                tag = "re",
                solution = "Ei!Ei!"
            };
            bool result = HttpHelper.SendOutput2(solution);
            Assert.IsTrue(result);
        }

        [TestCase]
        public void LoadAllProblems()
        {
            for (int i=0; i<24; ++i)
                ProblemServer.GetProblem(i);
        }
    }
}