# Why Machete?

Ahh, the million dollar question. Why was Machete created in the first place. I mean, its not like the world needs yet another parser claiming to do what others can't. Surely there are parsers and APIs out in the wild that can parse, format, and validate healthcare documents, right? To best answer that question let's take stock of the current situation in the healthcare industry. First off, healthcare data exchange standards \(e.g. HL7, X12, etc.\) date back over 40 years. The world has been utilizing computers for quite some time to transmit and process healthcare data. Over this timespan, there has been a massive explosion of technological advancement in both hardware and software. Unfortunately, during the same timespan, healthcare simply has not benefitted in much the same way as has other industries, e.g. communication, social media, and entertainment. This is an unusual trend considering that industry usually trails right behind advances in computing. There are several reasons for this, some technological, some cultural. But whatever the reason, the fact remains, healthcare has not kept up with the times.



#### Technological

In the more than 40 years of healthcare computing, standards organizations have attempted to solve this problem through both outreach and adapting specifications to more modern data exchange formats like XML and JSON. The reality is that modern data exchange formats are more accessible to the masses of would be pool of healthcare programmers, not to mention that all modern programming languages have built in support for these formats as well. On surface, this makes perfect sense.

#### Cultural

The healthcare IT industry has all but been asleep during the Open Source Software \(OSS\) revolution. If you are a healthcare industry veteran, you are probably noding your head in aggreeance right now. Ask yourself, how many healthcare parsers you've created for different organizations over the years? How many parsers has your current organization amassed through the years via mergers and acquisitions? Parsing healthcare data hasn't changed over the years, why are there so many different parsers at your organization that perform the very same function? I know what your thinking, why not pool resources and build something generic that can be adapted to fit all use case? Simple, why waste time pooling resources and managing said resources and use cases when I can build something relatively quick and cheap that fits my specific use cases. This attitude is pervasive throughout the healthcare IT industry.



Imagine a software package that runs on all major operating systems \(e.g. Windows, Linux, macOS, Unix\). Imagine an API that abstracts away the inherit complexity of dealing with healthcare data. Imagine a framework that allows you to define your own schema and run natively with the same core functionality that the out-of-the-box schemas have. Imagine having access to an extensible API core that provide services such as parsing, validation, translation, and formatting. Imagine having an API layer that is so pleasurable to use your developers enjoy coming to work everyday to solve worlds problems.

The unfortunate reality is that most healthcare IT organizations don't operate like software shops.



