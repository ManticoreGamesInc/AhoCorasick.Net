using System;
using System.Collections.Generic;
using AhoCorasick.Net.Benchmarks.Sandbox;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AhoCorasick.Net.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net80, baseline: true, iterationCount: 10, launchCount:3, warmupCount:3, invocationCount: 3, id:nameof(SmallTreeBenchmark))]
    public class SmallTreeBenchmark
    {
        private const int LengthOfKeyword = 15;
        private const int NumberOfKeywords = 1000;
        private readonly string _keyword;
        private readonly AhoCorasickTree _sut;
        private readonly AhoCorasickTreeHashBased _sut2;
        private readonly List<string> _list;

        public SmallTreeBenchmark()
        {
            var randomString = new RandomString();
            var keywords = new string[NumberOfKeywords];
            for (int i = 0; i < NumberOfKeywords; i++)
            {
                keywords[i] = randomString.GetRandomString(LengthOfKeyword);
            }
            _sut = new AhoCorasickTree(keywords);
            _sut2 = new AhoCorasickTreeHashBased(keywords);
            _list = new List<string>(keywords);

            var pick = new Random().Next(0, keywords.Length);
            _keyword = keywords[pick];
        }

        [Benchmark(Baseline = true)]
        public bool ContainsSmallWordList()
        {
            return _list.Contains(_keyword);
        }

        [Benchmark]
        public bool ContainsSmallWord()
        {
            return _sut.Contains(_keyword);
        }

        [Benchmark]
        public bool ContainsSmallWordImproved()
        {
            return _sut2.Contains(_keyword);
        }
    }
}
