﻿namespace Machete.X12Schema.V5010.Maps
{
    using X12;
    using X12.Configuration;


    public class L2010AA_837PMap :
        X12LayoutMap<L2010AA_837P, X12Entity>
    {
        public L2010AA_837PMap()
        {
            Id = "2010AA";
            Name = "Billing Provider Name";
            
            Segment(x => x.BillingProvider, 0, x => x.IsRequired());
            Segment(x => x.Address, 1, x => x.IsRequired());
            Segment(x => x.GeographicInformation, 2, x => x.IsRequired());
            Segment(x => x.TaxIdNumber, 3,
                x => x.IsRequired().Condition = parser => parser.Where(p => p.ReferenceIdentificationQualifier.IsEqualTo("EI") ||
                                                                            p.ReferenceIdentificationQualifier.IsEqualTo("SY")));
            Segment(x => x.LicenseInformation, 4,
                x => x.Condition = parser => parser.Where(p => p.ReferenceIdentificationQualifier.IsEqualTo("O8") ||
                                                               p.ReferenceIdentificationQualifier.IsEqualTo("1G")));
            Segment(x => x.ContactInformation, 5);
        }
    }
}