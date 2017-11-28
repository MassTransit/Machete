# Schema

In Machete, everything starts with the _schema_. A schema consists of [_entities_](#entities), [_fields_](#fields), and [_layouts_](#layouts), respectively. More to the point, the _schema_ defines how the data will ultimately interact with Machete's various engines \(e.g. parser, translator, etc.\).

#### Entities

Entities are schema primitives in that they consists of _fields_, which can themselves be entities. If you are familiar with the HL7 2.x specification, this  should not be a foreign concept--think segments, components, and sub-components. Enough talk, let's look at an example from Machete's HL7 2.x schema.

```csharp
public interface MSH :
    HL7V26Segment
{
    Value<string> EncodingCharacters { get; }
    Value<HD> SendingApplication { get; }
    Value<HD> SendingFacility { get; }
    Value<HD> ReceivingApplication { get; }
    Value<HD> ReceivingFacility { get; }
    Value<DateTimeOffset> DateTimeOfMessage { get; }
    Value<string> Security { get; }
    Value<MSG> MessageType { get; }
    Value<string> MessageControlId { get; }
    Value<PT> ProcessingId { get; }
    Value<VID> VersionId { get; }
    Value<decimal> SequenceNumber { get; }
    Value<string> ContinuationPointer { get; }
    Value<string> AcceptAcknowledgmentType { get; }
    Value<string> ApplicationAcknowledgmentType { get; }
    Value<string> CountryCode { get; }
    ValueList<string> CharacterSet { get; }
    Value<CWE> PrincipalLanguageOfMessage { get; }
    Value<string> AlternateCharacterSetHandlingScheme { get; }
    ValueList<EI> MessageProfileIdentifier { get; }
    Value<XON> SendingResponsibleOrganization { get; }
    Value<XON> ReceivingResponsibleOrganization { get; }
    Value<HD> SendingNetworkAddress { get; }
    Value<HD> ReceivingNetworkAddress { get; }
}
```

The above code snippet describes an MSH segment per the HL7 2.6 specification. Looking closer you'll notice that some of the properties \(aka f_ields_\) are not defined with CLR type primitives \(e.g. string, decimal, etc.\). Instead, you'll see that they are, in fact,  defined as entities \(e.g. CWE, XON, etc.\) themselves. There are no physical limitations as to how many levels deep can a schema be defined. In other words, entities can have entities, which can have entities, etc.

#### Fields

_Fields_ can best be described as data that resides between a set of predefined delimiters. The usage of the word, data, has a pretty vague definition in this context because it is not meant to describe not just valued data, but also, whitespace or even null data. In various healthcare specifications the classification of what is considered data have different meanings. For example, in HL7 nullified data can be represented as \|""\|. What this means in Machete terms is that a field is anything defined as a Value&lt;T&gt; property within an entity definition. Fields can contain schema primitives as well as entities. Take the below HL7 example...

```
MSH|^~\&|MACHETELAB|^DOSC|MACHETE|18779|20130405125146269||ORM^O01|1999077678|P|2.3|||AL|AL
```

You'll notice that some fields separate data using a caret, ^, delimiter. In HL7, this is indication that the field is complex and that the parser should return an object versus a programming language primitive. In the case of Machete, a map informs the parser that the field is an entity for which it knows how to handle by way of its corresponding entity map.

[Fields](/advanced-topics/extending-machete/defining-fields.md) are defined in Machete within entities like this...

```csharp
Value<TValue> Field { get; }
ValueList<TValue> FieldList { get; }
```



#### Layouts

If you've ever built transactional healthcare systems, you are probably familiar with the concept of a _message_. A typical message encompasses one or more segments/sections \(i.e. think entities\). If you think about what it really means to be a message, it's really just a way of describing how data is oriented and grouped within a payload sent from one system to another. In Machete, we refer to this concept as a document structure. That said, a layout can best be defined as a structured grouping of one or more entities and/or layouts.

```

```

the parallel to layouts would HL7 messages and/or groups. We know that entities

