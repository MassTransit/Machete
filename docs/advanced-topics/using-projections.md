# Using Projections

If you are coming to C\# from another high level modern programming language you are probably already familiar with Projections and how they are used. However, for those who are not we'll start off with a definition and work our way into some Machete code. Fair enough? Alright, here we go. A projection is a programming language facility that allows for the selection of a subset of data objects from a list. For example, say you had a list of the below object...

```csharp
public interface Patient
{
    Guid Identifier { get; }
    string FirstName { get; }
    string LastName { get; }
    DateTimeOffset BirthDate { get; }
    string Address { get; }
    string City { get; }
    string State { get; }
    string ZipCode { get; }
}
```

...and you wanted to return only a list of objects with only a subset of the above data. Your code might look something like this...

```csharp
var patients = patientList.Select(patient =>
        new {
                Identifier = patient.Identifier,
                Name = $"{patient.LastName}, {patient.FirstName}"
            });
```

In the above code, the projection operator is the _Select_ method and the actual projection is the function parameter, _patient_. Since I am interested in only a subset of the data from each object, I define a projection that is executed on each object in the list without condition and return me a new list with only that data. This is an extremely power way to filter data and is an integral part of LINQ. In Machete, we recognize the need for this throughout the Parsing API.

This is in large part due to the nature of the beast, that is, there are nuances to dealing with healthcare data that do not exist in, say, relational data or perhaps other industries. In the healthcare space, data is not always neatly presented on a single line, which means that orientation and context matters as much as the content of the data. That said, they all follow the same guidelines when it comes to expected behavior.

Machete provides two projection operators, _Select_ and _SelectMany_, respectively.

#### Select

_Select_ is a projection operator whose purpose is to go grab data and return an object. Notice no mention of list here. That is because a projection does not necessarily refer to a list of objects. In Machete, the _Select_ method is used in various scenarios:

To grab entire rows of data \(e.g. segments\) from a message...

```csharp
var schema = Machete.Schema.Factory.CreateHL7<HL7Entity>(cfg => cfg.AddFromNamespaceContaining<MSH>());
var parser = Machete.Parser.Factory.CreateHL7(schema);

ParseResult<HL7Entity> parse = parser.Parse(message);

var result = parse.Query(query =>
    from msh in query.Select<MSHSegment>()
    from pid in query.Select<PIDSegment>()
    select pid);
```

To extract a field from a row which happens to be a list...

```csharp
ValueList<CX> patientIdList = result.Select(x => x.PatientIdentifierList);
```

To extract a subcomponent of a complex field having multiple components...

```csharp
string patientId = patientIdList[0]
    .Select(x => x.IdNumber)
    .ValueOrDefault();
```

The one common theme of using Select is that you are guaranteed that the returned object is real and not null. Remember, do no harm.



#### SelectMany

&lt;explain, code sample here&gt;

_SelectMany_ is a projection operator whose purpose is to flatten queries that return lists of lists.

```csharp

```

&lt;explain, code sample here&gt;

