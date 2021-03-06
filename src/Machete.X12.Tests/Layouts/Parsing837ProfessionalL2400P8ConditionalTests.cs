namespace Machete.X12.Tests.Layouts
{
    using NUnit.Framework;
    using Testing;
    using X12Schema.V5010;


    [TestFixture]
    public class Parsing837ProfessionalL2400P8ConditionalTests :
        X12MacheteTestHarness<V5010, X12Entity>
    {
        [Test(Description = @"Condition : RenderingProvider => 82,
            PurchasedServiceProvider => QB,
            ServiceFacilityLocation => 77,
            SupervisingProvider => DQ,
            OrderingProvider => DK,
            ReferringProvider => DN,
            AmbulancePickUpLocation => PW,
            AmbulanceDropOffLocation => 45")]
        public void Test()
        {
            string message =
                @"ISA*00*          *00*          *ZZ*EMEDNYBAT      *ZZ*00DJ           *180625*0712*^*00501*176073292*0*P*:
GS*HP*EMEDNYBAT*00DJ*20180625*07121200*176073292*X*005010X221A1
ST*837*0002*005010X224A3
BHT*0019*00*0123*20061123*1023*CH
NM1*41*2*PREMIER BILLING SERVICE*****46*567890
PER*IC*JERRY*TE*7176149999
NM1*40*2*KEY INSURANCE COMPANY*****46*999996666
HL*1**20*1
NM1*85*2*DENTAL ASSOCIATES*****XX*4567890123
N3*234 SEAWAY ST
N4*MIAMI*FL*33111
REF*SY*587654321
REF*1G*587654321
N3*234 SEAWAY ST
N4*MIAMI*FL*33111
N3*234 SEAWAY ST
N4*MIAMI*FL*33111
REF*2U*587654321
REF*EIB*587654321
HL*2*1*22*1
SBR*P********CI
NM1*IL*1*SMITH*JANE****MI*JS00111223333
REF*SY*587654321
REF*Y4*587654321
NM1*PR*2*KEY INSURANCE COMPANY*****PI*999996666
REF*2U*587654321
REF*G2*587654321
HL*3*2*23*0
PAT*19
NM1*QC*1*SMITH*TED
N3*236 N MAIN ST
N4*MIAMI*FL*33413
DMG*D8*19920501*M
REF*Y4*587654321
REF*1W*587654321
CLM*26403774*200***11:B:1*Y**Y*I
DTP*431*D8*20061109
REF*G1*111222333444
HI✽ABK:8901✽BF:87200✽BF:5559
HI✽BP:8901✽BF:87200✽BF:5559
HI✽BG:8901✽BF:87200✽BF:5559
NM1*77*2*KILDARE ASSOCIATES*****XX*1581234567
N3*2345 OCEAN BLVD
N4*MI
NM1*77*2*KILDARE ASSOCIATES*****XX*1581234567
N3*2345 OCEAN BLVD
N4*MI
SBR*S*01*******CI
AMT*D*411
AMT*A8*273
AMT*EAF*75
OI***Y*P**Y
NM1*IL*1*SMITH*JACK****MI*T55TY666
N3*236 N MAIN ST
N4*MIAMI*FL*33111
REF*SY*R555588
NM1*PR*2*KEY INSURANCE COMPANY*****PI*999996666
REF*2U*98765
NM1*DN*1*KILDARE*BEN****XX*6789012345
REF*0B*R555588
NM1*82*1*KILDARE*BEN****XX*6789012345
REF*0B*R555588
NM1*77*1*KILDARE*BEN****XX*6789012345
REF*0B*R555588
NM1*DQ*1*KILDARE*BEN****XX*6789012345
REF*0B*R555588
NM1*85*1*KILDARE*BEN****XX*6789012345
REF*G2*R555588
LX*1
SV1*HC:99211:25*12.25*UN*1*11**1:2:3**Y
PWK*OZ*FX***AC*DMN0012
PWK*OZ*AB***AC*DMN0012
CRC*07*Y*01
CRC*70*Y*65
CRC*09*N*ZV
DTP*472*RD8*20050314-20050325
QTY*PT*2
QTY*FL*3
AMT*T*45
AMT*F4*56.78
NTE*ADD*PATIENTGOALTOBEOFFOXYGENBYENDOFMONTH
NTE*TPO*STATE REGULATION 123 WAS APPLIED DURING THE PRICING OF THIS CLAIM
NM1*82*1*KILDARE*BEN****XX*6789012345
NM1*QB*1*KILDARE*BEN****XX*6789012345
NM1*77*1*KILDARE*BEN****XX*6789012345
N3*236 N MAIN ST
N4*MIAMI*FL*33111
NM1*DQ*1*KILDARE*BEN****XX*6789012345
NM1*DK*1*KILDARE*BEN****XX*6789012345
NM1*DN*1*KILDARE*BEN****XX*6789012345
NM1*PW*1*KILDARE*BEN****XX*6789012345
N3*236 N MAIN ST
N4*MIAMI*FL*33111
NM1*45*1*KILDARE*BEN****XX*6789012345
N3*236 N MAIN ST
N4*MIAMI*FL*33111
SE*29*0002
IEA*1*176073292";

            var entityResult = Parser.Parse(message);

            Assert.IsTrue(Schema.TryGetLayout(out ILayoutParserFactory<HC837P, X12Entity> layout));

            var query = entityResult.CreateQuery(layout);

            var queryResult = entityResult.Query(query);

            Assume.That(queryResult != null);
            Assume.That(queryResult.HasResult);

            var transactions = queryResult.Select(x => x.Transactions)[0];

            Assume.That(transactions != null);
            Assume.That(transactions.HasValue);

            var renderingProvider = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.RenderingProvider)
                .Select(x => x.Provider)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(renderingProvider != null);
            Assert.IsTrue(renderingProvider.HasValue);
            Assert.IsTrue(renderingProvider.IsPresent);
            Assert.AreEqual("82", renderingProvider.ValueOrDefault());

            var purchasedServiceProvider = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.PurchasedServiceProvider)
                .Select(x => x.Provider)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(purchasedServiceProvider != null);
            Assert.IsTrue(purchasedServiceProvider.HasValue);
            Assert.IsTrue(purchasedServiceProvider.IsPresent);
            Assert.AreEqual("QB", purchasedServiceProvider.ValueOrDefault());

            var serviceFacilityLocation = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.ServiceFacilityLocation)
                .Select(x => x.LocationName)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(serviceFacilityLocation != null);
            Assert.IsTrue(serviceFacilityLocation.HasValue);
            Assert.IsTrue(serviceFacilityLocation.IsPresent);
            Assert.AreEqual("77", serviceFacilityLocation.ValueOrDefault());

            var supervisingProvider = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.SupervisingProvider)
                .Select(x => x.Provider)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(supervisingProvider != null);
            Assert.IsTrue(supervisingProvider.HasValue);
            Assert.IsTrue(supervisingProvider.IsPresent);
            Assert.AreEqual("DQ", supervisingProvider.ValueOrDefault());

            var orderingProvider = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.OrderingProvider)
                .Select(x => x.Provider)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(orderingProvider != null);
            Assert.IsTrue(orderingProvider.HasValue);
            Assert.IsTrue(orderingProvider.IsPresent);
            Assert.AreEqual("DK", orderingProvider.ValueOrDefault());

            var referringProvider = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.ReferringProvider)[0]
                .Select(x => x.Provider)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(referringProvider != null);
            Assert.IsTrue(referringProvider.HasValue);
            Assert.IsTrue(referringProvider.IsPresent);
            Assert.AreEqual("DN", referringProvider.ValueOrDefault());

            var ambulancePickUpLocation = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.AmbulancePickUpLocation)
                .Select(x => x.PickUpLocation)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(ambulancePickUpLocation != null);
            Assert.IsTrue(ambulancePickUpLocation.HasValue);
            Assert.IsTrue(ambulancePickUpLocation.IsPresent);
            Assert.AreEqual("PW", ambulancePickUpLocation.ValueOrDefault());

            var ambulanceDropOffLocation = transactions
                .Select(x => x.PatientDetail)[0]
                .Select(x => x.ClaimInformation)[0]
                .Select(x => x.ServiceLineNumber)[0]
                .Select(x => x.AmbulanceDropOffLocation)
                .Select(x => x.DropOffLocation)
                .Select(x => x.EntityIdentifierCode);

            Assume.That(ambulanceDropOffLocation != null);
            Assert.IsTrue(ambulanceDropOffLocation.HasValue);
            Assert.IsTrue(ambulanceDropOffLocation.IsPresent);
            Assert.AreEqual("45", ambulanceDropOffLocation.ValueOrDefault());
        }
    }
}