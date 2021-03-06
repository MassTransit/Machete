﻿namespace Machete.X12Schema.V5010
{
    using X12;


    public interface L2310E_837D :
        X12Layout
    {
        Segment<NM1> SupervisingProvider { get; }
        
        SegmentList<REF> SecondaryIdentification { get; }
    }
}