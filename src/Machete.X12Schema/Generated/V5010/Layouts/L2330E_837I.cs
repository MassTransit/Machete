﻿namespace Machete.X12Schema.V5010
{
    using X12;


    public interface L2330E_837I :
        X12Layout
    {
        Segment<NM1> OperatingPhysician { get; }
        
        SegmentList<REF> SecondaryIdentification { get; }
    }
}