﻿namespace Machete.X12Schema.V5010
{
    using X12;


    public interface L2100A_276 :
        X12Layout
    {
        Segment<NM1> PayerName { get; }
    }
}