﻿namespace Machete.X12Schema.V5010.Maps
{
    using X12;
    using X12.Configuration;


    public class L2330F_837IMap :
        X12LayoutMap<L2330F_837I, X12Entity>
    {
        public L2330F_837IMap()
        {
            Id = "2330F";
            Name = "Other Payer Service Facility Location";
            
            Segment(x => x.ServiceFacilityLocation, 0);
            Segment(x => x.SecondaryIdentification, 1, x => x.IsRequired());
        }
    }
}