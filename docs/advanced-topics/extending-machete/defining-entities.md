# Defining Entities

Now that you understand what a Machete [entity](/core-concepts/schema.md) is, this section will now show you how to extend the default schemas by defining your own. Defining an entity in Machete is pretty straightforward. Take a look at the below pseudo code...

```csharp
public interface SampleMacheteEntity :
    Entity
{
    [Value|ValueList]<TValue> FieldNameHere { get; }
}
```

Let's dissect the above pseudo code a bit. There are five attributes to be aware of that will guide you as you define your own entities:

1. An entity is [immutable](/core-concepts/immutability.md), therefore, it must be defined as a contract \(i.e. interface\) without implementation details 
2. An entity must inherit directly or indirectly from the Machete _Entity_ interface
3. Fields \(i.e. programming language properties\) within an entity must be read only and are defined by using either [_Value_](/extending-machete/defining-fields.md) or [_ValueList_](/extending-machete/defining-fields.md)
4. There are no limits to how many fields one can define within an entity
5. There is no particular order in which fields \(i.e. programming language properties\) are defined within an entity definition interface

Now, let's build something...

```csharp
public interface ME :
    Entity
{
    Value<string> Field { get; }
    ValueList<int> FieldList { get; }
}
```

Pretty simple, huh?

So, let's it break down a little further then. Listed above in the second attribute, an entity must inherit either directly or indirectly from the Machete Entity interface. We make it a point to explicitly call this out due its significance in helping define useful metadata and

The great thing about entities is the fact that I can use them from within other entities like so...

```csharp
public interface MyOtherEntity :
    Entity
{
    Value<ME> Field { get; }
    ValueList<ME> FieldList { get; }
}
```

Pretty cool, eh?

All the magic is in the [entity maps](/extending-machete/defining-entity-maps.md) in the schema.

#### Entity

All entities must directly or indirectly inherit the Entity interface.

