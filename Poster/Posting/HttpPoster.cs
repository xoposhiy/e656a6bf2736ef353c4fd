﻿using System.Linq;
using SomeSecretProject;
using SomeSecretProject.IO;

namespace Emulator.Posting
{
    public class HttpPoster
    {
        private readonly IProblemSolver problemSolver;

        public HttpPoster(IProblemSolver problemSolver)
        {
            this.problemSolver = problemSolver;
        }

        public void PostAll(DavarAccount account, string tag = null)
        {
            var outputs = Enumerable.Range(0, 24)
                .Select(ProblemServer.GetProblem)
                .SelectMany(problem => problem.sourceSeeds
                    .Select(seed =>
                    {
                        return new Output
                        {
                            problemId = problem.id,
                            seed = seed,
                            solution = problemSolver.Solve(problem, seed, DavarMagicSpells.Items),
                            tag = tag
                        };
                    }));

            foreach (var output in outputs)
            {
                HttpHelper.SendOutput(account, output);
            }
        }
    }
}