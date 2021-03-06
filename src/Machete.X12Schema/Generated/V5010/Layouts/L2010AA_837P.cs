﻿namespace Machete.X12Schema.V5010
{
    using X12;


    public interface L2010AA_837P :
        X12Layout
    {
        Segment<NM1> BillingProvider { get; }
        
        Segment<N3> Address { get; }
        
        Segment<N4> GeographicInformation { get; }
        
        Segment<REF> TaxIdNumber { get; }
        
        SegmentList<REF> LicenseInformation { get; }
        
        SegmentList<PER> ContactInformation { get; }
    }
}