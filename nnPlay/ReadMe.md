﻿

@genekogan
https://genekogan.com/
https://ml4a.github.io/
@ml4a


https://ml-cheatsheet.readthedocs.io/en/latest/activation_functions.html

rectofied linear unit (relu) (leaky relu)

BitConverter.DoubleToInt64Bits(Double) Method
https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter.doubletoint64bits?view=netframework-4.8

BitConverter.Int64BitsToDouble(Int64) Method
https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter.int64bitstodouble?view=netframework-4.8

How to Convert a Byte Array to Double in C#
https://www.c-sharpcorner.com/UploadFile/mahesh/convert-byte-array-to-double-in-C-Sharp/

https://www.youtube.com/user/culurciello/videos

https://youtu.be/QJoa0JYaX1I


201909291021
CNNs are complicated, as I have learned today how the convolutions are connected to the neurons. In previous 
demonstrations it was showen that the output of one convolution went into the next convolution. I couldn't 
understand how it was possible that a convolution showing only one trait could be feed into another layer to pic
out traits that don't exit in the input data. It turns out that the videos are not covering the actual way the data
is handeled. The convolution layer is composed of many convolutions that all convolve the original data. This 
produces many data sets. One 1 mp image can produce many 1 mp data sets. The 2d data becomes 3d data. Now the 
resulting data can be assoiated to one another by the x,y location of each data set in the resulting 3d map of 
the data. When we construct sequential layers you do not want to have every neuron to be fully connected to the 
previous layer because the resulting exponential number of connections would result in years of useless processing. 
What we want to do is to target each neuron to only be connected to neurons within an offset of the x,y location. 
Another way of saying this is that the features we are looking for are within a region. Now the question becomes 
how do we detect patterns that occupy a larger region. To detect larger patterns, it is pretty clean that there are 
convolutions on top of the convolutions. In order for the process to recognize patterns that only appear in the 
origianl output layer it seems necessary that these outputs should be ether passed through to the next layer or 
that the first of the fully connected layers need to also be connected to the first convolutioinal layers output even 
if there are more convolutional layers. 
