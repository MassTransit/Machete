# Formatting Document Structures

Most of what you'll do in Machete is accessing and manipulating data by way of objects. However, when the need arises to save and/or transmit data, being able to format data into forms other than objects becomes crucial. This is where Machete's Formatting API comes into play.



```csharp
var schema = Machete.Schema.Factory.CreateHL7<HL7Entity>(cfg => cfg.AddFromNamespaceContaining<MSH>());
var parser = Machete.Parser.Factory.CreateHL7(schema);
var formatter = Machete.Formatter.Factory.CreateHL7(schema);

ParseResult<HL7Entity> entityResult = parser.Parse(data);
var formattedResult = await formatter.FormatToStringAsync(entityResult);
string formattedText = formattedResult.Text;
```

&lt;explain, code here&gt;

