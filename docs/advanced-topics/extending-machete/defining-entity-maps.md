# Defining Entity Maps

Think of an entity map as a set of instructions to the parser, informing it when it sees a particular identifier to parse the subsequent data accordingly.

Let's jump into some code...

```csharp
public class MSHMap :
    HL7V26SegmentMap<MSH>
{
    public MSHMap()
    {
        Id = "MSH";

        Name = "Message Header";

        Value(x => x.EncodingCharacters, 1, x => {x.Required = true;});
        Entity(x => x.SendingApplication, 2);
        Entity(x => x.SendingFacility, 3);
        Entity(x => x.ReceivingApplication, 4);
        Entity(x => x.ReceivingFacility, 5);
        Value(x => x.DateTimeOfMessage, 6, x => {x.Converter = HL7.HL7ValueConverters.VariableLongDateTime;x.Required = true;});
        Value(x => x.Security, 7);
        Entity(x => x.MessageType, 8, x => {x.Required = true;});
        Value(x => x.MessageControlId, 9, x => {x.Required = true;});
        Entity(x => x.ProcessingId, 10, x => {x.Required = true;});
        Entity(x => x.VersionId, 11, x => {x.Required = true;});
        Value(x => x.SequenceNumber, 12);
        Value(x => x.ContinuationPointer, 13);
        Value(x => x.AcceptAcknowledgmentType, 14);
        Value(x => x.ApplicationAcknowledgmentType, 15);
        Value(x => x.CountryCode, 16);
        Value(x => x.CharacterSet, 17);
        Entity(x => x.PrincipalLanguageOfMessage, 18);
        Value(x => x.AlternateCharacterSetHandlingScheme, 19);
        Entity(x => x.MessageProfileIdentifier, 20);
        Entity(x => x.SendingResponsibleOrganization, 21);
        Entity(x => x.ReceivingResponsibleOrganization, 22);
        Entity(x => x.SendingNetworkAddress, 23);
        Entity(x => x.ReceivingNetworkAddress, 24);
    }
}
```

Although the code looks pretty simple and straightforward, there is a lot to unpack here, so let's jump right in. There are three things about entity maps to pay attention to:

1. An entity map must be defined as a concrete class that derives directly or indirectly from EntityMap&lt;TEntity, TSchema&gt;
2. Mapping logic must be defined within the class constructor
3. An entity identifier must be defined to inform the parser how to parse a particular string once it finds a match

The derive EntityMap class exposes a few methods that are of great importance, that is, _Value_ and _Entity_. These methods are responsible for defining how the parser should handle certain strings that it encounters while parsing. The Value&lt;T&gt; method should be called on fields that have been defined as being language primitives \(e.g. Value&lt;string&gt;, Value&lt;decimal&gt;, etc.\). However, the Entity&lt;T&gt; method should be called on fields that have been defined as being more complex in nature, that is, fields that consist of fields, aka entities \(e.g. Value&lt;HD&gt;, Value&lt;CWE&gt;,  etc.\).

Let's breakdown some code shall we. No matter if the field happens to be complex or primitive there are three important bits of information to define so that the parser can later do its work most efficiently:

To best understand this, imagine if you were a parser's perspective; you been instructed to parse and return the MSH segment from the below HL7 message.

```
MSH|^~\&|MACHETELAB|^DOSC|MACHETE|18779|20130405125146269||ORM^O01|1999077678|P|2.3|||AL|AL
PID|1|000000000026|60043^^^MACHETE^MRN||MACHETE^JOE||19890909|F|||123 SEASAME STREET^^Oakland^CA^94600||5101234567|5101234567||||||||||||||||N
PD1|M|F|N||||F|
PV1|1|O|||||92383^Machete^Janice||||||||||||12345|||||||||||||||||||||||||201304051104
PV2||||||||20150615|20150616|1||||||||||||||||||||||||||N
IN1|1|||MACHETE INC|1234 Fruitvale ave^^Oakland^CA^94601^USA||5101234567^^^^^510^1234567|074394|||||||A1|MACHETE^JOE||19890909|123 SEASAME STREET^^Oakland^CA^94600||||||||||||N|||||666889999|0||||||F||||T||60043^^^MACHETE^MRN
GT1|1|60043^^^MACHETE^MRN|MACHETE^JOE||123 SEASAME STREET^^Oakland^CA^94600|5416666666|5418888888|19890909|F|P
AL1|1|FA|^pollen allergy|SV|jalubu daggu||
ORC|NW|PRO2350||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11636^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
ORC|NW|PRO2351||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11637^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
ORC|NW|PRO2352||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11638^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
```

So after some inspection of the above HL7, you've now found a match. The next thing you'll need to know is how to map the data you've found back into object form and pass to your eventual caller. So, here is the million dollar question, how would you be able to perform this task without knowing how the data correlates to the object? Hmm, you'd probably need a way to define how the data maps back to the object, otherwise, it would be pointless to parse the message in the first place. Now you see why entity maps are so important. Now, let's breakdown some code shall we. The below code snippet is a simple example of how mapping in Machete is done.

```csharp
Value(x => x.EncodingCharacters, 1, x => {x.Required = true;});
```

No matter if the field happens to be complex or primitive, there are three important bits of information that must be captured in order to define how the parser execute most efficiently:

1. Target field that the data should be mapped to
2. Relative position of the delimited data field to the entity's definition
3. Instructions on how to handle parsing the field \(e.g. is data required to be present, how to convert the raw string data upon mapping\)



