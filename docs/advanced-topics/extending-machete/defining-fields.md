# Defining Fields

What is an entity without fields? Well, to answer ever so bluntly, not very useful. Just like there is more than meets the eye to defining entities, the same can be said with defining fields. Defining a field within an entity looks like this...

```csharp
[Value|ValueList]<TValue> FieldNameHere { get; }
```

To define a field, you'll need to answer the following questions:

* Is the field value complex or primitive \(e.g. string, int, bool, etc.\)
* Is the field value singular or plural

The answers to these questions will serve to guide you in determining how to define your field, in particular, whether to use [_Value&lt;TValue&gt;_](#value) or [_ValueList&lt;TValue&gt;_](#valuelist) and what type is _TValue_.

#### Value

A _Value_ can be defined as a property on an entity that is mapped by the schema. When a field is defined as being of type _Value_, it means that the field has a single associated value \(i.e. is not a list\). If you need to mentally map this to your preferred healthcare specification then most specifications would refer to this as not _repeating_.

```csharp
Value<TValue> FieldNameHere { get; }
```

For a list of types currently supported by Machete please take a look at the table in the [supported types](#supported-types) section below.

#### ValueList

A _ValueList_ is can be defined as a property on an entity that is mapped by the schema just like Value. However, the difference is that if a field is defined as being a ValueList it means that the field has multiple associated values that are said to be "repeating".

```csharp
ValueList<TValue> FieldNameHere { get; }
```

For a list of types currently supported by Machete please take a look at the table in the [supported types](#supported-types) section below.

#### Supported Types

So, in the above code snippets, _TValue_, can either be a language primitive \(e.g. string, int, bool, etc.\) or another entity \(e.g. CWE, HD, etc.\). Below is a list of currently supported values...

| Type | Machete Support | Notes |
| :--- | :--- | :--- |
| string | Yes |  |
| char | Yes |  |
| short | Yes |  |
| ushort | Yes |  |
| int | Yes |  |
| uint | Yes |  |
| long | Yes |  |
| ulong | Yes |  |
| decimal | Yes |  |
| float | Yes |  |
| double | Yes |  |
| bool | Yes |  |
| DateTime | Yes | Consider using DateTimeOffset. See the section [Dates and Times](#dates-and-times) |
| DateTimeOffset | Yes |  |
| byte | Yes |  |
| object | No | Consider using another type |
| dynamic | No | Consider using another type |
| Entity | Yes |  |

#### Dates and Times

Dealing with dates and times can be tricky at times--no pun intended. A poorly designed schema can derail the prime directive of Machete's parsing engine, that is, _do no harm_. All data is treated as a string until you instruct Machete to return you a strongly typed value. Unfortunately, when dealing with dates and times there is a way to accidentally destroy data. Imagine that you have defined a field to be of type, _DateTime._ Let's also assume that the data for that field is accompanied with time zone information that serves to put the date and time part into context. When Machete attempts to return a DateTime object it will do so, leaving out the time zone. That's the bad news. The good news is that Machete always keeps a copy of the source data as a string so it is possible to still that data. The other part of this is that we strongly recommend using DateTimeOffset in these situations because it is not destructive.

#### Parting Thoughts

I know what you're thinking, why not define entity fields like the rest of the world would do it, you know, like this...

```csharp
TValue FieldNameHere { get; }
```

...or like this...

```csharp
List<TValue> FieldNameHere { get; }
```

The reason lies in the nuances that are embedded in most all healthcare specifications. If you are familiar with the HL7 2.x specification\(s\) there are some nuances around what constitutes a field value. For example, did you know that a double quoted empty field \(e.g. \|""\|\) should be interpreted as a null? Did you also know that an empty field \(e.g. \|\|\) is different from a field having nothing but whitespace between delimiters \(e.g. \|     \|\)? This only becomes a problem when you attempt to strongly type these values. What if you had to make a distinction between this data and "real" data? Well, you'd probably just create extension methods to perform these tasks.

