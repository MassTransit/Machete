﻿namespace Machete.X12Schema.V5010.Layouts.Maps
{
    using X12;
    using X12.Configuration;


    public class L2420B_837DMap :
        X12LayoutMap<L2420B_837D, X12Entity>
    {
        public L2420B_837DMap()
        {
            Id = "2420B";
            Name = "Assistant Surgeon Name";
            
            Segment(x => x.AssistantSurgeon, 0);
            Segment(x => x.SpecialtyInformation, 1);
            Segment(x => x.SecondaryIdentification, 2);
        }
    }
}