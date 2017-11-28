# Querying Document Structures

As was described in [Parsing Document Structures](/using-machete/parsing-document-structures.md), parsing is not responsible for querying document structures. This may come as a surprise since parsing has become anonymous with scanning document structures looking for specific matches and typing data to an object all in one swoop. The problem is that this approach is prone to error and can be extremely slow. However, we've taken a different approach with Machete. In Machete, the query engine sits atop the parser and works in conjunction with the entity mappers to deliver the exact same result as most parsers but with a much lower memory footprint, greater accuracy and speed.

Querying is an action that place against entities, which means the parser subsequently understands which mappers to call. Remember, Machete does parsing lazily. That said, there are two ways to query over entities:

* [LINQ Support](/advanced-topics/linq-support.md) \(e.g. LINQ to HL7, LINQ to X12, etc.\)
* Layouts

While each have slightly different use cases, they both allow you to accomplish the same result.

...or you could accomplish the very same task using Layouts like so...

```csharp
Result<Cursor<HL7Entity>, ORM_O01_ORDER> result = parse.Query(x => x.Layout<ORM_O01_ORDER>());
```

&lt;explain, code here&gt;

#### 



