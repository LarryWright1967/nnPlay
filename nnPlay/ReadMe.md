

I'm not sure if it maters but at leaset one example I'm reading about is suggesting that each individual backprop
through the network should be handeled individually. This means that as each layer is computed each backprop to the 
previous node must be saved in the node. This would imply an array of backprop values that should be computed and 
transfered to the previous layer. This would also imply that the calculations become exponential with the layers 
and neuron count. It seams that if possible each node should have it's gradient compiled to avoide un-necessary 
calculations. This will only work however, if all of the same nurons are active for all calculations in this 
sequence. Implying that this can not be done for batches.

201910271400
Two items I'm not sure how to handle.
 - do I update all of the weights of each convolution based on the backpropagation or only for those instances
 where it is activated?
 - How do I handle the fact that each convolution (weights) are used over and over again with each filter 
 process. i.e. if the filter is used 100 times across the image do I backpropagate wtih with the gradient as if 
 it was 1 or 100 in parallel?

 201910261322
https://youtu.be/d14TUNcbn1k?t=1259
appearently the gredient of 1/x = -1/(x^2)

https://youtu.be/d14TUNcbn1k?t=1441
the gredient of addition is 1

https://youtu.be/d14TUNcbn1k?t=499
the gredient of ether input to a multiplication is the other value of the multiplication

https://youtu.be/d14TUNcbn1k?t=1331
an exponential function gredient is e-1 or 0.367879441171442321

201910281016 update
How to perform backpropagation on a max pooling layer.
Only the value that is the max is important do the derivative of this value is 1 for this
pixel, all others are 0. Another way to express this is to pass the gradient from the pevious
layer only to the weight on the max pixel and ignore all of the others.

How to write 3 as super set
√
∂
∑
θ



201910200936
I was thinking about how to solve a couple of issues I have with the nn and the cnn. The basic issues I have
probably relate to the fact that I don't understand the math. I don't know how the back propigation works in 
general and specifically how it relates to the cnns. I hear stuff about derivitives and activation formullas 
and error formulas and the fact that without these formulas the equations would be linear and not "learn". I 
see this to some extent, But I don't really understand it. So this is my current thinking, based on the little 
I understand. 
 - each "learn" operation will correct only ~50%? of the values using a random function
 - every pixel of a cnn will essentially be a neuron
 - each neuron will remember the previous weight change and the resulting change in error to compute the next 
 weight change.


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

