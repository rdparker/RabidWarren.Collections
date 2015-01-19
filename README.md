# RabidWarren.Collections

Provides a generic Multimap class for .NET.  Multimap behaves like the
standard Dictionary class except multiple values may be associated with a single key.

## Design

Multimap is designed to provide efficient lookup of a relatively small number
of items per key.  The original use case had class types as the key and
property metadata as the values.  It did not contain the value data for the
properties within instances of the classes, so the number of items per key was
of small magnitude.

So, the the keys are maintained in a `System.Collections.Generic.Dictionary`
and their values are stored in a simple `System.Collections.Generic.List`.
