﻿namespace Machete.X12.Tests.TestSchema
{
    using System;


    public interface GSSegment :
        X12Segment
    {
        Value<string> FunctionalIdentifierCode { get; }
        
        Value<string> ApplicationSendersCode { get; }
        
        Value<string> ApplicationReceiversCode { get; }
        
        Value<DateTime> Date { get; }
        
        Value<int> ControlNumber { get; }
        
        Value<string> ResponsibleAgencyCode { get; }
        
        Value<string> VersionIdentifierCode { get; }
    }
}