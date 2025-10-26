### SunFlower.VB

This is an ~idiomatic~ plugin for SunFlower kernel for a
Visual Basic embedded runtime lookup.
In other words this is a Visual Basic runtime walker, which
scans all `PE32` linked image, sets up the long memory pointers
for a file-positions and reinterprets all structures as Markdown tables.

> [!TIP]
> The Visual Basic runtime meant the Classic-era embedded components
> into the project. The VB.NET uses .NET platform and Common Object Runtime
> specification before the `Cor 2.0 header` section starts.


### Frameworks

Target toolchain is an experimental SunFlower build
 - VB.NET 16;
 - .NET 8.0+;
 - SunFlower 4.0.0.0 (alpha tested 3.1+ components)
 - SunFlower 4.0.0 contract scheme

In the early commits you can see `3.1.0` contract attribute
because SunFlower stays at the alpha-stage. New adapters and support
schemes are incoming soon in 4.0 build.

### Example of result

File with full report for VB Semi Decompiler pinned [here](vbartefacts.md)

