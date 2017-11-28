# Defining Layouts

Defining a layout in Machete is pretty straightforward. Similar to entities, _layouts_ are immutable objects defined as interfaces with properties having read only access.

```csharp
public interface ORM_O01 :
    HL7V26Layout
{
    Segment<MSH> MSH { get; }
    SegmentList<NTE> NTE { get; }
    Layout<ORM_O01_PATIENT> Patient { get; }
    LayoutList<ORM_O01_ORDER> Order { get; }
}
```

&lt;explain, code here&gt;

```csharp
public class ORM_O01Map :
    HL7V26LayoutMap<ORM_O01>
{
    public ORM_O01Map()
    {
        Segment(x => x.MSH, 0, x => x.Required = true);
        Segment(x => x.NTE, 1);
        Layout(x => x.Patient, 2);
        Layout(x => x.Order, 3, x => x.Required = true);
    }
}
```

&lt;explain, code here&gt;

