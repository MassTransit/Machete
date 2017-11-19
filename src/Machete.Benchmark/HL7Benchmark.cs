﻿namespace Machete.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Engines;
    using BenchmarkDotNet.Environments;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Jobs;
    using HL7;
    using HL7Schema.V26;


    [Config(typeof(Config))]
    [MinColumn, MaxColumn]
    public class HL7Benchmark
    {
        readonly ParseResult<HL7Entity> _parse;
        readonly ParseResult<HL7Entity> _layoutParse;

        const string Message1 = @"MSH|^~\&|MACHETELAB|^DOSC|MACHETE|18779|20130405125146269||ORM^O01|1999077678|P|2.3|||AL|AL
    NTE|1||KOPASD
    NTE|2||A3RJ
    NTE|3||7ADS
    NTE|4||G46DG
    PID|1|000000000026|60043^^^MACHETE^MRN||MACHETE^JOE||19890909|F|||123 SEASAME STREET^^Oakland^CA^94600||5101234567|5101234567||||||||||||||||N
    PD1|M|F|N||||F|
    NTE|1||IN42
    PV1|1|O|||||92383^Machete^Janice||||||||||||12345|||||||||||||||||||||||||201304051104
    PV2||||||||20150615|20150616|1||||||||||||||||||||||||||N
    IN1|1|||MACHETE INC|1234 Fruitvale ave^^Oakland^CA^94601^USA||5101234567^^^^^510^1234567|074394|||||||A1|MACHETE^JOE||19890909|123 SEASAME STREET^^Oakland^CA^94600||||||||||||N|||||666889999|0||||||F||||T||60043^^^MACHETE^MRN
    GT1|1|60043^^^MACHETE^MRN|MACHETE^JOE||123 SEASAME STREET^^Oakland^CA^94600|5416666666|5418888888|19890909|F|P
    AL1|1|FA|^pollen allergy|SV|jalubu daggu||
    ORC|NW|PRO2350||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
    OBR|1|PRO2350||11636^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
    DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
    OBX|1||URST^Urine Specimen Type^^^||URN
    NTE|1||abc
    NTE|2||dsa
    ORC|NW|PRO2351||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
    OBR|1|PRO2350||11637^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
    DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
    OBX|1||URST^Urine Specimen Type^^^||URN
    NTE|1||abc
    NTE|2||dsa
    ORC|NW|PRO2352||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
    OBR|1|PRO2350||11638^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
    DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
    OBX|1||URST^Urine Specimen Type^^^||URN
    NTE|1||abc
    NTE|2||dsa";
        
        const string Message2 = @"MSH|^~\&|MACHETELAB|^DOSC|MACHETE|18779|20130405125146269||ORM^O01|1999077678|P|2.3|||AL|AL
    NTE|1||KOPASD
    NTE|2||A3RJ
    NTE|3||7ADS
    NTE|4||G46DG
    PID|1|000000000026|60043^^^MACHETE^MRN||MACHETE^JOE||19890909|F|||123 SEASAME STREET^^Oakland^CA^94600||5101234567|5101234567||||||||||||||||N
    PD1|M|F|N||||F|
    NTE|1||IN42
    PV1|1|O|||||92383^Machete^Janice||||||||||||12345|||||||||||||||||||||||||201304051104
    PV2||||||||20150615|20150616|1||||||||||||||||||||||||||N
    IN1|1|||MACHETE INC|1234 Fruitvale ave^^Oakland^CA^94601^USA||5101234567^^^^^510^1234567|074394|||||||A1|MACHETE^JOE||19890909|123 SEASAME STREET^^Oakland^CA^94600||||||||||||N|||||666889999|0||||||F||||T||60043^^^MACHETE^MRN
    GT1|1|60043^^^MACHETE^MRN|MACHETE^JOE||123 SEASAME STREET^^Oakland^CA^94600|5416666666|5418888888|19890909|F|P
    AL1|1|FA|^pollen allergy|SV|jalubu daggu||
    ORC|NW|PRO2350||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
    OBR|1|PRO2350||11636^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
    DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
    OBX|1||URST^Urine Specimen Type^^^||URN
    NTE|1||abc
    NTE|2||dsa
    ORC|NW|PRO2351||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
    OBR|1|PRO2350||11637^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
    DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
    OBX|1||URST^Urine Specimen Type^^^||URN
    NTE|1||abc
    NTE|2||dsa
    ORC|NW|PRO2352||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
    OBR|1|PRO2350||11638^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
    DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
    OBX|1||URST^Urine Specimen Type^^^||URN
    NTE|1||abc
    NTE|2||dsa";

        public HL7Benchmark()
        {
            var schema = Schema.Factory.CreateHL7<HL7Entity>(cfg => cfg.AddFromNamespaceContaining<MSH>());
            var parser = Parser.Factory.CreateHL7(schema);
            
            _parse = parser.Parse(Message1);
            _layoutParse = parser.Parse(Message2);
        }

        [Benchmark]
        public void BenchmarkLinqToSchema()
        {
            var query = _parse.Query(q =>
            {
                var obxQuery = from obx in q.Select<OBX>()
                    from nte in q.Select<NTE>().ZeroOrMore()
                    select new
                    {
                        OBX = obx,
                        NTE = nte
                    };

                var obrQuery = from obr in q.Select<OBR>()
                    from dg1 in q.Select<DG1>().Optional()
                    from obx in obxQuery.Optional()
                    select new
                    {
                        OBR = obr,
                        DG1 = dg1,
                        OBX = obx
                    };

                var testQuery = from orc in q.Select<ORC>()
                    from obr in obrQuery.ZeroOrMore()
                    select new
                    {
                        ORC = orc,
                        OBR = obr
                    };

                return from msh in q.Select<MSH>()
                    from nte in q.Select<NTE>().ZeroOrMore()
                    from skip in q.Except<HL7Segment, ORC>().ZeroOrMore()
                    from tests in testQuery.ZeroOrMore()
                    select new
                    {
                        MSH = msh,
                        Notes = nte,
                        Tests = tests
                    };
            });

            var segment = query.Result.Tests;
            for (int i = 0; i < segment.Count; i++)
            {
                string orderGroupNumber = segment[0]
                    .ORC
                    .PlacerGroupNumber
                    .Select(x => x.UniversalId)
                    .ValueOrDefault();

                var tests = segment[i].OBR;
                for (int j = 0; j < tests.Count; j++)
                {
                    string placerOrderNumber = tests[j]
                        .OBR
                        .PlacerOrderNumber
                        .Select(x => x.EntityIdentifier)
                        .ValueOrDefault();
                }
            }
        }

//        [Benchmark]
        public void BenchmarkLayoutQuery()
        {
            Result<Cursor<HL7Entity>, ORM_O01> result = _layoutParse.Query(x => x.Layout<ORM_O01>());

            string orderGroupNumber = result.Result.Order[0]
                .Select(x => x.ORC)
                .Select(x => x.PlacerGroupNumber)
                .Select(x => x.UniversalId)
                .ValueOrDefault();
        }


        class Config :
            ManualConfig
        {
            const int Iteration = 500;

            public Config()
            {
                Add(MemoryDiagnoser.Default);
                Add(HtmlExporter.Default);
                Add(new Job
                {
                    Env = { Runtime = Runtime.Clr },
                    Run =
                    {
                        TargetCount = Iteration,
                        RunStrategy = RunStrategy.Throughput,
                        WarmupCount = 1,
                        LaunchCount = 1,
                        UnrollFactor = 1,
                        InvocationCount = Iteration
                    }
                });
                Add(new Job
                {
                    Env = { Runtime = Runtime.Mono },
                    Run =
                    {
                        TargetCount = Iteration,
                        RunStrategy = RunStrategy.Throughput,
                        WarmupCount = 1,
                        LaunchCount = 1,
                        UnrollFactor = 1,
                        InvocationCount = Iteration
                    }
                });
                Add(new Job
                {
                    Env = { Runtime = Runtime.Core },
                    Run =
                    {
                        TargetCount = Iteration,
                        RunStrategy = RunStrategy.Throughput,
                        WarmupCount = 1,
                        LaunchCount = 1,
                        UnrollFactor = 1,
                        InvocationCount = Iteration
                    }
                });
            }
        }
    }
}